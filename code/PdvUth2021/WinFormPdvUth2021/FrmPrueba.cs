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
    public partial class FrmPrueba : Form
    {
        public FrmPrueba()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FrmPrueba_Load(object sender, EventArgs e)
        {
            //vamos a redimensionar el groupbox de registros con 1/3 de la altura del form
            gBoxRegistros.Height =  this.Height / 3;
        }
    }
}
