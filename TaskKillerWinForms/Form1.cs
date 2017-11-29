using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskKillerWinForms
{
    public partial class Form1 : Form
    {
        BackgroundWorker m_oWorker;
        public Form1()
        {
            InitializeComponent();
            m_oWorker = new BackgroundWorker();

            m_oWorker.DoWork += M_oWorker_DoWork;
            m_oWorker.WorkerSupportsCancellation = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
            Opacity = 0;

            m_oWorker.RunWorkerAsync();
            base.OnLoad(e);
        }
        private void M_oWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (m_oWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    try
                    {
                        Process.GetProcesses().Where(x => x.MainWindowTitle == "Call of Duty 4").FirstOrDefault().Kill();
                        

                    }
                    catch (Exception ex)
                    {

                    }


                }
                Thread.Sleep(5000);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_oWorker.IsBusy)
            {
                m_oWorker.CancelAsync();
            }
        }
    }
}
