using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CaptureWindow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var image = CaptureControl();
            Clipboard.SetImage(image);
            image.Dispose();
        }

        private Bitmap CaptureControl()
        {
            var image = new Bitmap(Width, Height);
            var graphics = Graphics.FromImage(image);
            var hdc = graphics.GetHdc();
            NativeMethods.PrintWindow(Handle, hdc, 0);
            graphics.ReleaseHdc(hdc);
            graphics.Dispose();
            return image;
        }
    }

    internal static class NativeMethods
    {
        [DllImport("User32.dll")]
        internal static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);
    }
}
