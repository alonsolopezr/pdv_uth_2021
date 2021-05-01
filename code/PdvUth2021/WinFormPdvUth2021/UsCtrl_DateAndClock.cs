using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormPdvUth2021
{
    public partial class UsCtrl_DateAndClock : UserControl
    {
        public UsCtrl_DateAndClock()
        {
            InitializeComponent();
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
        }

        private void timerDate_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
        }
    }
}
