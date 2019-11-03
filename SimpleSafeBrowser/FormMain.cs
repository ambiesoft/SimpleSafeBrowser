using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ambiesoft;

namespace SimpleSafeBrowser
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            mainWB.StatusTextChanged += new EventHandler(mainWB_StatusTextChanged);
            mainWB.ScriptErrorsSuppressed = true;
            mainWB.DocumentTitleChanged += new EventHandler(mainWB_DocumentTitleChanged);
            mainWB.NewWindow += new CancelEventHandler(mainWB_NewWindow);
        }
        private void mainWB_NewWindow(object sender, CancelEventArgs e)
        {
            if (newWindowToolStripMenuItem.Checked)
                return;

            e.Cancel = true;
            //メッセージ（情報）を鳴らす
            System.Media.SystemSounds.Asterisk.Play();

        }

        private void mainWB_DocumentTitleChanged(object sender, EventArgs e)
        {
            string doctitle = mainWB.DocumentTitle;
            if (string.IsNullOrEmpty(doctitle))
                this.Text = Application.ProductName;
            else
                this.Text = doctitle + " : " + Application.ProductName;

        }
        private void mainWB_StatusTextChanged(object sender, EventArgs e)
        {
            mainStatusLabel.Text = mainWB.StatusText;
        }


        private void FormMain_Load(object sender, EventArgs e)
        {
            ///AmbLib.MakeTripleClickTextBox(cmbAddress.ComboBox);
            try
            {
                string url = Clipboard.GetText();
                Uri u = new Uri(url);
                mainWB.Navigate(u);
            }
            catch (Exception)
            {
                mainWB.Navigate("about:blank");
            }
            
            Ambiesoft.AmbLib.StretchToolItem(mainTool, cmbAddress);
        }

        private void openClipboardURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string url = Clipboard.GetText();
                mainWB.Navigate(url);
            }
            catch (Exception)
            {
            }
        }

        private void openBlankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainWB.Navigate("about:blank");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mainWB.Stop();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            mainWB.Refresh();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            mainWB.Navigate(cmbAddress.Text);
        }

        private void cmbAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                mainWB.Navigate(cmbAddress.Text);
                e.Handled = true;
            }
        }



        private void mainWB_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (cmbAddress.Focused)
                return;

            if(mainWB.Url != null)
                cmbAddress.Text = mainWB.Url.ToString();
        }

        private void mainWB_FileDownload(object sender, EventArgs e)
        {

        }

        private void Qclosereboot()
        {
            if (DialogResult.Yes != MessageBox.Show("Close and Reboot?", Application.ProductName,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question))
            {
                return;
            }
            Close();
            Program.Reboot();
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AmbLib.setRegMaxIE(8000);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AmbLib.setRegMaxIE(9000);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            AmbLib.setRegMaxIE(9999);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            AmbLib.setRegMaxIE(10000);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            AmbLib.setRegMaxIE(11000);
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            AmbLib.StretchToolItem(mainTool, cmbAddress);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ambiesoft.CppUtils.Info(string.Format("{0} ver{1}",
                Application.ProductName, AmbLib.getAssemblyVersion(System.Reflection.Assembly.GetExecutingAssembly(), 3)));

        }
    }
}
