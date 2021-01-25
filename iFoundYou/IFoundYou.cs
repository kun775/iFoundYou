using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace iFoundYou
{
    enum HotKey
    {
        HotKey_ADKillPause = 1001,
        HotKey_ADKillOpen = 1002
    };
    public partial class IFoundYou : Form
    {
        [DllImport("user32", SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        internal static extern IntPtr WindowFromPoint(Point Point);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr handle, out uint processId);
        [DllImport("user32.dll")] //申明API函数
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);
        [DllImport("user32.dll")] //申明API函数
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);
        [DllImport("kernel32.dll")]
        private static extern bool QueryFullProcessImageName(IntPtr hprocess, int dwFlags, StringBuilder lpExeName, out int size);
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hHandle);
        [Flags]
        enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x1000,
            Synchronize = 0x00100000
        }

        private string RunPath = "";
        private bool IsPause = false;
        private Process ProcessById = null;
        public IFoundYou()
        {
            InitializeComponent();
            Text = "我找到你了-获取弹窗信息(按F9暂停，按F10打开运行路径)";
        }
        private void IFoundYou_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            try
            {
                RegisterHotKey(Handle, (int)HotKey.HotKey_ADKillOpen, 0, Keys.F10);
                RegisterHotKey(Handle, (int)HotKey.HotKey_ADKillPause, 0, Keys.F9);
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册快捷键失败:\n" + ex.Message);
            }
        }
        // 打开路径
        private void OpenPath()
        {
            if (string.IsNullOrEmpty(RunPath))
            {
                return;
            }

            string FilePath = Path.GetDirectoryName(RunPath);
            string FileName = Path.GetFileName(RunPath);
            Process.Start("Explorer", "/select," + FilePath + "\\" + FileName);
        }
        private void Pause()
        {
            IsPause = !IsPause;
            if (IsPause)
            {
                Text = "我找到你了-获取弹窗信息(按F9继续，F10打开运行路径)";
            }
            else
            {
                Text = "我找到你了-获取弹窗信息(按F9暂停，按F10打开运行路径)";
            }
        }
        private void ProcessHotkey(Message m) //按下设定的键时调用该函数
        {
            int id = m.WParam.ToInt32();
            switch (id)
            {
                case (int)HotKey.HotKey_ADKillOpen: OpenPath(); break;
                case (int)HotKey.HotKey_ADKillPause: Pause();   break;
            }
        }
        protected override void WndProc(ref Message m)//监视Windows消息
        {
            const int WM_HOTKEY = 0x0312;//如果m.Msg的值为0x0312那么表示用户按下了热键
            switch (m.Msg)
            {
                //按下热键时调用ProcessHotkey()函数
                case WM_HOTKEY: ProcessHotkey(m); break;
            }
            base.WndProc(ref m); //将系统消息传递自父类的WndProc
        }

        private void ADKillForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(Handle, (int)HotKey.HotKey_ADKillPause);
            UnregisterHotKey(Handle, (int)HotKey.HotKey_ADKillOpen);
        }
        private static string GetExecutablePath(Process Process)
        {
            //If running on Vista or later use the new function
            if (Environment.OSVersion.Version.Major >= 6)
            {
                return GetExecutablePathAboveVista(Process.Id);
            }

            return Process.MainModule.FileName;
        }
        private static string GetExecutablePathAboveVista(int ProcessId)
        {
            StringBuilder buffer = new StringBuilder(256);
            IntPtr hprocess = OpenProcess(ProcessAccessFlags.QueryLimitedInformation, false, ProcessId);
            if (hprocess != IntPtr.Zero)
            {
                try
                {
                    int size = buffer.Capacity;
                    if (QueryFullProcessImageName(hprocess, 0, buffer, out size))
                    {
                        return buffer.ToString();
                    }
                }
                finally
                {
                    CloseHandle(hprocess);
                }
            }
            return Marshal.GetLastWin32Error().ToString();
        }
        private void MouseMoveEvent()
        {
            GetCursorPos(out Point point);
            int x = point.X;
            int y = point.Y;
            label_Position.Text = $"({x}, {y})";
            Point p = new Point(x, y);
            IntPtr intPtr = WindowFromPoint(p);//得到窗口句柄
            StringBuilder title = new StringBuilder(256);
            GetWindowText(intPtr, title, title.Capacity);//得到窗口的标题
            StringBuilder className = new StringBuilder(256);
            GetClassName(intPtr, className, className.Capacity);//得到窗口的句柄

            label_ID.Text = $"窗口句柄：{intPtr}";
            label_Title.Text = $"窗口标题：{title}";
            label_Type.Text = $"窗口类型：{className}";
            GetWindowThreadProcessId(intPtr, out uint processId);
            ProcessById = Process.GetProcessById((int)processId);
            try
            {
                RunPath = GetExecutablePath(ProcessById);
                label_Run.Text = $"运行路径：{RunPath}";
            }
            catch (Exception ex)
            {
                RunPath = "";
                label_Run.Text = ex.Message;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (IsPause)
                return;
            MouseMoveEvent();
        }
        // 关闭进程
        private void Button_Close_Click(object sender, EventArgs e)
        {
            if (ProcessById != null)
            {
                ProcessById.Kill();
                ProcessById = null;

                MessageBox.Show("关闭成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // 重命名
        private void Button_Rename_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(RunPath))
                return;

            DialogResult dr = MessageBox.Show("" +
                "重命名会备份弹窗文件，为了防止下载新的弹窗文件，会在原地创建同名文件\n" +
                "此功能一般用于单独弹窗进程，如果弹窗功能集成在软件里，重命名会导致软件无法使用。\n" +
                "重命名前请先关闭弹窗进程。重命名后，可用恢复功能恢复原先文件。\n" +
                "是否确认重命名？", 
                "！！注意 ！！", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.No)
                return;

            try
            {
                File.Move(RunPath, RunPath + "_bak");
                MessageBox.Show($"重命名成功！\n{RunPath}_bak", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                using (FileStream fs = File.Open(RunPath, FileMode.Create))
                {
                    fs.WriteByte(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"重命名出错！\n{ex.Message}", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        // 恢复
        private void Button_Resume_Click(object sender, EventArgs e)
        {
            if (!File.Exists(RunPath + "_bak"))
            {
                MessageBox.Show($"备份路径不存在，无法恢复！\n{RunPath}_bak", "错误", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (File.Exists(RunPath))
                    File.Delete(RunPath);

                File.Move(RunPath + "_bak", RunPath);
                MessageBox.Show($"恢复成功！\n{RunPath}", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"恢复错误！\n{ex.Message}", "提示", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Label_Run_Click(object sender, EventArgs e)
        {
            OpenPath();
        }
    }
}
