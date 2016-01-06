using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace 重庆三峡学院网络电视IPTV
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        void x_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem x = sender as ToolStripMenuItem;
            string url = "mms://202.202.160.9/" + x.Text;
            axWindowsMediaPlayer.URL = url;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead("http://vod.sanxiau.edu.cn/IPTV.htm");
            StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            Regex reg = new Regex(@"(?is)<a[^>]*?href=(['""\s]?)(?<href>[^'""\s]*)\1[^>]*?>");
            MatchCollection match = reg.Matches(strHTML);
            List<string> url = new List<string>();
            foreach (Match m in match)
                url.Add(m.Groups["href"].Value);
            foreach (string v in url)
            {
                char[] temp = v.ToArray();
                string chT = "";
                for (int i = temp.Length - 1; i >= 0; i--)
                {
                    if (temp[i] != '/')
                        chT += temp[i];
                    else
                        break;
                }
                char[] t = chT.ToArray();
                string ch = "";
                for (int i = t.Length - 1; i >= 0; i--)
                    ch += t[i];
                comboBox.Items.Add(ch);
                //ToolStripMenuItem x = new ToolStripMenuItem(ch);
                //x.Click += new EventHandler(x_Click);
                //choseChannelCToolStripMenuItem.DropDownItems.Add(x);
            }

            //this.FormBorderStyle = FormBorderStyle.FixedSingle;
            comboBox.SelectedIndex = 3;
            axWindowsMediaPlayer.URL = "mms://202.202.160.9/"+comboBox.Items[3];
            axWindowsMediaPlayer.settings.volume = 100;
            axWindowsMediaPlayer.enableContextMenu = false;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox x=sender as ComboBox;
            string url = "mms://202.202.160.9/" + x.Text;
            axWindowsMediaPlayer.URL = url;
        }
    }
}
