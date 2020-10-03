namespace AvailTekVegasRenderExtensions
{
    partial class RenderStaleConfiguration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblStartDirectory = new System.Windows.Forms.Label();
            this.lblMediaDirectory = new System.Windows.Forms.Label();
            this.lblMediaSubdirectory = new System.Windows.Forms.Label();
            this.lblRenderSubdirectory = new System.Windows.Forms.Label();
            this.lblRenderTemplate = new System.Windows.Forms.Label();
            this.txtStartDirectory = new System.Windows.Forms.TextBox();
            this.txtMediaDirectory = new System.Windows.Forms.TextBox();
            this.txtMediaSubdirectory = new System.Windows.Forms.TextBox();
            this.txtRenderSubdirectory = new System.Windows.Forms.TextBox();
            this.txtRenderTemplate = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblStartDirectory
            // 
            this.lblStartDirectory.Location = new System.Drawing.Point(22, 3);
            this.lblStartDirectory.Name = "lblStartDirectory";
            this.lblStartDirectory.Size = new System.Drawing.Size(100, 23);
            this.lblStartDirectory.TabIndex = 0;
            this.lblStartDirectory.Text = "Starting Directory";
            this.lblStartDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMediaDirectory
            // 
            this.lblMediaDirectory.Location = new System.Drawing.Point(22, 30);
            this.lblMediaDirectory.Name = "lblMediaDirectory";
            this.lblMediaDirectory.Size = new System.Drawing.Size(100, 23);
            this.lblMediaDirectory.TabIndex = 1;
            this.lblMediaDirectory.Text = "Media Directory";
            this.lblMediaDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMediaSubdirectory
            // 
            this.lblMediaSubdirectory.Location = new System.Drawing.Point(22, 58);
            this.lblMediaSubdirectory.Name = "lblMediaSubdirectory";
            this.lblMediaSubdirectory.Size = new System.Drawing.Size(100, 23);
            this.lblMediaSubdirectory.TabIndex = 2;
            this.lblMediaSubdirectory.Text = "Media Subdirectory";
            this.lblMediaSubdirectory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRenderSubdirectory
            // 
            this.lblRenderSubdirectory.Location = new System.Drawing.Point(0, 91);
            this.lblRenderSubdirectory.Name = "lblRenderSubdirectory";
            this.lblRenderSubdirectory.Size = new System.Drawing.Size(122, 23);
            this.lblRenderSubdirectory.TabIndex = 3;
            this.lblRenderSubdirectory.Text = "Render Subdirectory";
            this.lblRenderSubdirectory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRenderTemplate
            // 
            this.lblRenderTemplate.Location = new System.Drawing.Point(22, 114);
            this.lblRenderTemplate.Name = "lblRenderTemplate";
            this.lblRenderTemplate.Size = new System.Drawing.Size(100, 23);
            this.lblRenderTemplate.TabIndex = 4;
            this.lblRenderTemplate.Text = "Render Template";
            this.lblRenderTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStartDirectory
            // 
            this.txtStartDirectory.Location = new System.Drawing.Point(128, 5);
            this.txtStartDirectory.Name = "txtStartDirectory";
            this.txtStartDirectory.Size = new System.Drawing.Size(275, 20);
            this.txtStartDirectory.TabIndex = 5;
            // 
            // txtMediaDirectory
            // 
            this.txtMediaDirectory.Location = new System.Drawing.Point(128, 32);
            this.txtMediaDirectory.Name = "txtMediaDirectory";
            this.txtMediaDirectory.Size = new System.Drawing.Size(275, 20);
            this.txtMediaDirectory.TabIndex = 6;
            // 
            // txtMediaSubdirectory
            // 
            this.txtMediaSubdirectory.Location = new System.Drawing.Point(128, 60);
            this.txtMediaSubdirectory.Name = "txtMediaSubdirectory";
            this.txtMediaSubdirectory.Size = new System.Drawing.Size(275, 20);
            this.txtMediaSubdirectory.TabIndex = 7;
            // 
            // txtRenderSubdirectory
            // 
            this.txtRenderSubdirectory.Location = new System.Drawing.Point(128, 87);
            this.txtRenderSubdirectory.Name = "txtRenderSubdirectory";
            this.txtRenderSubdirectory.Size = new System.Drawing.Size(275, 20);
            this.txtRenderSubdirectory.TabIndex = 8;
            // 
            // txtRenderTemplate
            // 
            this.txtRenderTemplate.Location = new System.Drawing.Point(128, 114);
            this.txtRenderTemplate.Name = "txtRenderTemplate";
            this.txtRenderTemplate.Size = new System.Drawing.Size(275, 20);
            this.txtRenderTemplate.TabIndex = 9;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(328, 150);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // RenderStaleConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 182);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtRenderTemplate);
            this.Controls.Add(this.txtRenderSubdirectory);
            this.Controls.Add(this.txtMediaSubdirectory);
            this.Controls.Add(this.txtMediaDirectory);
            this.Controls.Add(this.txtStartDirectory);
            this.Controls.Add(this.lblRenderTemplate);
            this.Controls.Add(this.lblRenderSubdirectory);
            this.Controls.Add(this.lblMediaSubdirectory);
            this.Controls.Add(this.lblMediaDirectory);
            this.Controls.Add(this.lblStartDirectory);
            this.Name = "RenderStaleConfiguration";
            this.Text = "RenderStaleConfiguration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStartDirectory;
        private System.Windows.Forms.Label lblMediaDirectory;
        private System.Windows.Forms.Label lblMediaSubdirectory;
        private System.Windows.Forms.Label lblRenderSubdirectory;
        private System.Windows.Forms.Label lblRenderTemplate;
        private System.Windows.Forms.TextBox txtStartDirectory;
        private System.Windows.Forms.TextBox txtMediaDirectory;
        private System.Windows.Forms.TextBox txtMediaSubdirectory;
        private System.Windows.Forms.TextBox txtRenderSubdirectory;
        private System.Windows.Forms.TextBox txtRenderTemplate;
        private System.Windows.Forms.Button btnOK;
    }
}