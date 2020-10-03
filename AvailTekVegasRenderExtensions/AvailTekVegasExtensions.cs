using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptPortal.Vegas;

namespace AvailTekVegasRenderExtensions
{
    public class AvailTekVegasExtensions : ICustomCommandModule
    {
        public Vegas myVegas;

        string startDirectory;
        string mediaDirectory;
        string mediaSubDirectory;
        string renderSubDirectory;
        string renderTemplate;


        public ICollection GetCustomCommands()
        {
            CustomCommand cmd = new CustomCommand(CommandCategory.Tools, "RenderAllStale");
            cmd.DisplayName = "Render all stale";
            cmd.Invoked += RenderAllStale;
            List<CustomCommand> cmds = new List<CustomCommand>();
            cmds.Add(cmd);
            return cmds;
        }

        public void InitializeModule(Vegas vegas)
        {
            myVegas = vegas;
        }

        private void RenderAllStale(object sender, EventArgs e)
        {
            try { 
                // Set Defaults
                startDirectory = "V:\\AIIM\\Change";
                mediaDirectory = startDirectory;
                mediaSubDirectory = "Media";
                renderSubDirectory = "Render";
                renderTemplate = "1920x1080-30fps-6/1.5mbps";

                RenderStaleConfiguration rsc = new RenderStaleConfiguration();
                rsc.LoadScreen(startDirectory, mediaDirectory, mediaSubDirectory, renderSubDirectory, renderTemplate);
                rsc.ShowDialog();
                if (!rsc.Valid) return;
                rsc.GetValues(out startDirectory, out mediaDirectory, out mediaSubDirectory, out renderSubDirectory, out renderTemplate);

                // Find Render Template
                RenderTemplate rt = null;
                foreach (Renderer renderer in myVegas.Renderers)
                {
                    foreach (RenderTemplate temp in renderer.Templates)
                    {
                        if (temp.Name == renderTemplate) { rt = temp; break; }
                    }
                    if (rt != null) break;
                }

                // Setup UX
                RenderStatusManager rsm = new RenderStatusManager();
                RenderStaleStatus rss = new RenderStaleStatus();
                rss.SetRenderStatusManager(rsm);
                rsm.Dialog = rss;
                rss.Show();
                // Process
                DirectoryInfo fiStart = new DirectoryInfo(startDirectory);
                DirectoryInfo fiMedia = new DirectoryInfo(mediaDirectory);
                if (fiStart.Exists && fiMedia.Exists && rt != null)
                {
                    ProcessFolder(fiStart, fiMedia, mediaSubDirectory, renderSubDirectory, rt, rsm);
                }
                rss.Close();

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileOk += Sfd_FileOk;
                sfd.ShowDialog();
                if (saveOk)
                {
                    File.WriteAllText(sfd.FileName, rsm.GetTableAsCSV());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Exception", MessageBoxButtons.OK);
            }
        }

        private bool saveOk = false;
        private void Sfd_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            saveOk = true;
        }

        public void GetConfiguration(FileInfo fiConfig)
        {
            string configInfo = System.IO.File.ReadAllText(fiConfig.FullName);
            string[] lines = configInfo.Split(new char[] { '\n', '\r' });
            foreach (string line in lines)
            {
                if (!String.IsNullOrWhiteSpace(line) && line.IndexOf('=') != -1)
                {
                    string[] kv = line.Split(new char[] { '=' });
                    switch (kv[0].ToLower())
                    {
                        case "startdirectory":
                            if (!string.IsNullOrWhiteSpace(kv[1])) startDirectory = kv[1];
                            break;
                        case "mediadirectory":
                            if (!string.IsNullOrWhiteSpace(kv[1])) mediaDirectory = kv[1];
                            break;
                        case "mediasubdirectory":
                            if (!string.IsNullOrWhiteSpace(kv[1])) mediaSubDirectory = kv[1];
                            break;
                        case "rendersubdirectory":
                            if (!string.IsNullOrWhiteSpace(kv[1])) renderSubDirectory = kv[1];
                            break;
                        case "rendertemplate":
                            if (!string.IsNullOrWhiteSpace(kv[1])) renderTemplate = kv[1];
                            break;
                        default:
                            System.Diagnostics.Debug.WriteLine("Unrecognized key {0}", kv[0]);
                            break;
                    }
                }
            }
        }
        public int ProcessFolder(DirectoryInfo diStart, DirectoryInfo diMedia, string subMedia, string subRender,
                                 RenderTemplate renderTemplate, RenderStatusManager rsm)
        {
            int rendered = 0;
            if (rsm.Cancel) return (rendered);
            System.Diagnostics.Debug.WriteLine("Checking {0}", diStart.FullName);
            // Process for files
            foreach (FileInfo fi in diStart.GetFiles("*-1st.veg"))
            {
                if (rsm.Cancel) return (rendered);
                if (ProcessFile(fi, diMedia, subMedia, subRender, renderTemplate, rsm)) rendered++;
            }

            foreach (FileInfo fi in diStart.GetFiles("*.veg"))
            {
                if (rsm.Cancel) return (rendered);
                // Make sure we didn't process this as a 1st file and it's not set to do not render
                if (fi.FullName.ToLower().IndexOf("-1st.veg") == -1 &&
                    fi.FullName.ToLower().IndexOf("-dnr.veg") == -1)
                {
                    if (ProcessFile(fi, diMedia, subMedia, subRender, renderTemplate, rsm)) rendered++;
                }
            }
            // Process subdirectories
            foreach (DirectoryInfo subDi in diStart.GetDirectories())
            {
                rendered += ProcessFolder(subDi, diMedia, subMedia, subRender, renderTemplate, rsm);
            }

            return rendered;
        }

        public bool ProcessFile(FileInfo fiVeg, DirectoryInfo diMedia, string subMedia, string subRender, 
                                RenderTemplate renderTemplate, RenderStatusManager rsm)
        {
            object dr = rsm.StartProjectProcessing(fiVeg);
            DateTime lastMedia = UpdateMediaFiles(diMedia, fiVeg.Directory, subMedia, rsm, dr);
            FileInfo renderedFile = GetRenderedFile(fiVeg, subRender);
            if ((!renderedFile.Exists ||
                renderedFile.LastWriteTimeUtc < fiVeg.LastWriteTimeUtc ||
                renderedFile.LastWriteTimeUtc < lastMedia ||
                renderedFile.Length == 0) && renderedFile.Directory.Exists)
            {
                return (RenderFile(fiVeg, renderedFile, renderTemplate,rsm,dr));
            }
            else
            {
                rsm.UpdateField(dr, RenderStatusManager.Fields.Names.RenderStatus, "Skipped");
            }
            return false;
        }

        public bool RenderFile(FileInfo fiVeg, FileInfo fiRendered, RenderTemplate renderTemplate, RenderStatusManager rsm, object dr)
        {
            DateTime renderStart = DateTime.Now;
            DateTime renderEnd;
            TimeSpan renderTime;
            rsm.Dialog.Hide(); // Temporarily hide because during load the UX kills it.
            rsm.UpdateField(dr, RenderStatusManager.Fields.Names.RenderStart, renderStart);
            if (myVegas.Project != null)
            {
                // No close method so create a clean new project without the ability to prompt for save of existing
                // and then open after that.
                myVegas.NewProject(false, false);
            }
            myVegas.UpdateUI();
            rsm.UpdateField(dr, RenderStatusManager.Fields.Names.RenderStatus, "Loading");
            myVegas.OpenFile(fiVeg.FullName);
            myVegas.UpdateUI();
            myVegas.WaitForIdle();
            rsm.Dialog.Show();

            // Render
            RenderArgs ra = new RenderArgs();
            ra.OutputFile = fiRendered.FullName;
            ra.RenderTemplate = renderTemplate;
            Timecode projectLength = GetProjectLength();
            rsm.UpdateField(dr, RenderStatusManager.Fields.Names.ProjectLength, projectLength);
            ra.Length = projectLength;
            ra.StartNanos = 0;

            rsm.UpdateField(dr, RenderStatusManager.Fields.Names.RenderStatus, "Rendering");
            RenderStatus status = myVegas.Render(ra);
            renderEnd = DateTime.Now;
            renderTime = renderEnd - renderStart;
            rsm.UpdateField(dr, RenderStatusManager.Fields.Names.RenderEnd, renderEnd);
            rsm.UpdateField(dr, RenderStatusManager.Fields.Names.RenderTime, renderTime);
            rsm.UpdateField(dr, RenderStatusManager.Fields.Names.RenderStatus, status.ToString());
            return (status == RenderStatus.Complete);
        }

        public Timecode GetProjectLength()
        {
            Timecode projectLength = myVegas.Project.Length;
            if (projectLength.FrameCount != 0) return projectLength; // the project length indicator is correct.

            foreach(Track track in myVegas.Project.Tracks)
            {
                Timecode trackLength = track.Length;
                if (trackLength.FrameCount == 0)
                {
                    // Get from events
                    foreach(TrackEvent te in track.Events)
                    {
                        Timecode eventEndLength = te.Start + te.Length;
                        if (eventEndLength > trackLength) trackLength = eventEndLength;
                    }
                }
                if (trackLength > projectLength) projectLength = trackLength;
            }
            return projectLength;
        }

        public DateTime UpdateMediaFiles(DirectoryInfo diMedia, DirectoryInfo diVeg, string subMedia, RenderStatusManager rsm, object dr)
        {
            DirectoryInfo mediaDirectory = null;
            string mediaDirectoryFullName = Path.Combine(diVeg.FullName, subMedia);
            mediaDirectory = new DirectoryInfo(mediaDirectoryFullName);

            DateTime lastFileModified = DateTime.MinValue;
            int filesCopied = 0;
            foreach (FileInfo fiMedia in diMedia.GetFiles())
            {
                switch (fiMedia.Extension.ToLower())
                {
                    // List of file types to copy
                    case ".jpg":
                    case ".gif":
                    case ".png":
                    case ".psd":
                    case ".tif":
                        if (mediaDirectory.Exists)
                        {
                            string targetName = Path.Combine(mediaDirectory.FullName, fiMedia.Name);
                            FileInfo fiTarget = new FileInfo(targetName);
                            if (!fiTarget.Exists ||
                                fiTarget.LastWriteTimeUtc < fiMedia.LastWriteTimeUtc ||
                                fiTarget.Length == 0)
                            {
                                System.Diagnostics.Debug.WriteLine("Copying {0} to {1}", fiMedia.Name, mediaDirectory.FullName);
                                fiMedia.CopyTo(targetName, true);
                                filesCopied++;
                            }
                        }
                        break;
                    default:
                        // Do nothing
                        break;
                }

            }
            rsm.UpdateField(dr, RenderStatusManager.Fields.Names.MediaFilesCopied, filesCopied);

            // Find the last modified tile for the media directory (if it exists)
            if (mediaDirectory.Exists)
            {
                foreach (FileInfo fi in mediaDirectory.GetFiles())
                {
                    if (fi.LastWriteTimeUtc > lastFileModified) lastFileModified = fi.LastWriteTimeUtc;
                }
            }
            return lastFileModified;
        }

        public FileInfo GetRenderedFile(FileInfo veg, string subRender)
        {
            string renderFileName = Path.GetFileNameWithoutExtension(veg.FullName) + ".mp4";
            string renderFilePath = Path.Combine(veg.DirectoryName, subRender, renderFileName);
            return (new FileInfo(renderFilePath));
        }

    }
}
