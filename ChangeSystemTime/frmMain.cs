using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChangeSystemTime
{
    public partial class frmMain : Form
    {
        //设置系统时间的API函数
        [DllImport("kernel32.dll")]
        private static extern bool SetLocalTime(ref SYSTEMTIME time);
        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public short year;
            public short month;
            public short dayOfWeek;
            public short day;
            public short hour;
            public short minute;
            public short second;
            public short milliseconds;
        }

        private string timeFormat = "yyyy-MM-dd HH:mm:ss";

        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="dt">需要设置的时间</param>
        /// <returns>返回系统时间设置状态，true为成功，false为失败</returns>
        public bool SetDate(DateTime dt)
        {
            SYSTEMTIME st;
            st.year = (short)dt.Year;
            st.month = (short)dt.Month;
            st.dayOfWeek = (short)dt.DayOfWeek;
            st.day = (short)dt.Day;
            st.hour = (short)dt.Hour;
            st.minute = (short)dt.Minute;
            st.second = (short)dt.Second;
            st.milliseconds = (short)dt.Millisecond;
            bool rt = SetLocalTime(ref st);
            return rt;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txbTime.Text = DateTime.Now.ToString(timeFormat);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string strTime = txbTime.Text;
            if (SetDate(DateTime.Parse(strTime)))
            {
                lblDone.Text = string.Format($"Succeed - {DateTime.Now.ToString(timeFormat)}");
            }
            else
            {
                lblDone.Text = string.Format($"Failed - {DateTime.Now.ToString(timeFormat)}");
            }
        }
    }
}