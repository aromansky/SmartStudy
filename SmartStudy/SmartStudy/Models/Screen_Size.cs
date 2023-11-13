using System.Runtime.InteropServices;

namespace SmartStudy.Models
{
    internal class Screen_Size
    {
#if WINDOWS
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);
        public int Width = GetSystemMetrics(0);
        public int Height = GetSystemMetrics(1);
#else
        public int Width = 0;
        public int Height = 0;
#endif
    }
}
