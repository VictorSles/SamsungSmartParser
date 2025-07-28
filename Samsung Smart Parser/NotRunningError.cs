using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Samsung_Smart_Parser
{
    public partial class NotRunningError : Form
    {
        public NotRunningError()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;

            Timer timer = new Timer();
            timer.Interval = 5000; // 5 segundos
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                this.Close();
            };
            timer.Start();
        }

        private void NotRunningError_Load(object sender, EventArgs e)
        {

        }
    }
}
