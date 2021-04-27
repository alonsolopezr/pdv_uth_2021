using LibPdvUTh2021.Products;
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
    public partial class FrmProducto : Form
    {

        //Objetos que se requieren para Productos
        Product product = new Product();
        Brand brand = new Brand();
        Category category = new Category();
        Subcategory subcategory = new Subcategory();

        public FrmProducto()
        {
            InitializeComponent();
            //set comboBox brands
            comboBrand.DisplayMember = "Display";
            comboBrand.ValueMember = "Value";
            comboBrand.DataSource = brand.comboData();  
            comboCategory.DisplayMember = "Display";
            comboCategory.ValueMember = "Value";
            comboCategory.DataSource = category.comboData(); 
            comboSubcategory.DisplayMember = "Display";
            comboSubcategory.ValueMember = "Value";
            comboSubcategory.DataSource = subcategory.comboData();
            comboUnitOfMeasure.Items.AddRange( new object[] { "UNIT", "KILO", "LITTER", "BOX" });
            comboUnitOfMeasure.SelectedItem = "UNIT";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
