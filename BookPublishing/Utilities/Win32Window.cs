using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPublishing
{
    public class Win32Window : System.Windows.Forms.IWin32Window
    {
        public Win32Window(int handle) { _handle = new IntPtr(handle); }
        protected IntPtr _handle;
        public IntPtr Handle { get { return(_handle); } }
    }
}
