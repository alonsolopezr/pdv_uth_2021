using LibPdvUTh2021.App;
using LibPdvUTh2021.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        //Flag to Add/Save button change
        bool addSaveFlag = false;
        //string of product image path, for saving the FILE in the project FOLDER
        string imgPath;
        //path to save the relative path in the PRODUCTS TABLE image field
        string imgBDPath;

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

            //Add/Save button
            if (!addSaveFlag) btnAdd.Text = "Add"; else btnAdd.Text = "Save";

            fillDataGrid();
            
        }
        private void fillDataGrid() 
        {
            //fill the datagrid
            //fill the datagrid
            if (dgvData.RowCount >= 0)
            {
                dgvData.DataSource = null;
                dgvData.Rows.Clear();
                

            }
            if (dgvData.Columns.Count >= 0) 
            {
                dgvData.Columns.Clear();
                dgvData.Columns.Add("colId", "Id");
                dgvData.Columns.Add("colName", "Name");
                dgvData.Columns.Add("colDescription", "Description");
                dgvData.Columns.Add("colPrice", "Price");
                dgvData.Columns.Add("colBarcode", "Barcode");
                dgvData.Columns.Add("colBrand", "Brand");
                dgvData.Columns.Add("colSubcategory", "Subcategory");
                dgvData.Columns.Add("colImage", "Image");
                dgvData.Columns.Add("colUnit", "Unit of Measure");
            }
            List<List<object>> data = product.index(new LibBD.OrderBy("name", LibBD.Order.ASC));
            foreach (List<object> rowData in data)
            {
                object[] resArray = rowData.ToArray();
                resArray[5] = brand.nameOf(int.Parse(resArray[5].ToString()));
                resArray[6] = subcategory.nameOf(int.Parse(resArray[6].ToString()));
                //resArray[8] = comboUnitOfMeasure.Items[int.Parse(resArray[8].ToString())];
                dgvData.Rows.Add(resArray);
            }
            dgvData.Refresh();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //when clicked, load the data to the form
            txtID.Text = dgvData.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvData.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dgvData.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtBarcode.Text = dgvData.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtPrice.Text = dgvData.Rows[e.RowIndex].Cells[4].Value.ToString();
            //comboBrand.SelectedItem = dgvData.Rows[e.RowIndex].Cells[5].Value.ToString();
            foreach (ParaCombo brand in comboBrand.Items)
            {
                if (brand.Display.Equals(dgvData.Rows[e.RowIndex].Cells[5].Value.ToString()))
                    comboBrand.SelectedItem = brand;
            }
            foreach (ParaCombo subcatego in comboSubcategory.Items)
            {
                if (subcatego.Display.Equals(dgvData.Rows[e.RowIndex].Cells[6].Value.ToString()))
                    comboSubcategory.SelectedItem = subcatego;
            }

            //activateform
            this.activateForm(true);
            //activate edit and delete buttons
            btnDelete.Enabled = btnUpdate.Enabled = true;
        }
        /// <summary>
        /// Deactivate or Activate the form txtBoxes and such
        /// </summary>
        /// <param name="activate">True = activate, False = deactivate</param>
        private void activateForm(bool activate) 
        {
       
            txtID.Enabled = activate;
            txtName.Enabled = activate;
            txtDescription.Enabled = activate;
            txtPrice.Enabled = activate;
            txtBarcode.Enabled = activate;
            txtBarcode.Enabled = activate;
            txtImage.Enabled = activate;
            comboBrand.Enabled = activate;
            comboCategory.Enabled = activate;
            comboSubcategory.Enabled = activate;
            comboUnitOfMeasure.Enabled = activate;
            btnLoadImg.Enabled = activate;
            //Edit and delete buttons
            btnUpdate.Enabled = btnDelete.Enabled = (activate == true && addSaveFlag == false) ? activate : false;
            //change panel background color
            if (activate) gBoxForm.BackColor = Color.FromArgb(244, 211, 94);
            else gBoxForm.BackColor = Color.FromArgb(100, 100, 100);
        }

        private void cleanForm() 
        {
            txtID.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            txtBarcode.Clear();
            txtBarcode.Clear();
            txtImage.Clear();
            comboBrand.SelectedItem = "";
            comboCategory.SelectedItem = "";
            comboSubcategory.SelectedItem = "";
            comboUnitOfMeasure.SelectedItem = "";
            picBoxBrand.Image = null;
            picBoxImage.Image = null;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if Add button is clicked, we change the TEXT to 'Save'
            if (addSaveFlag==false)
            {
                //enable and clean form
                addSaveFlag = true;
                btnAdd.Text = "Save";
                //Clean form
                this.activateForm(true);
                this.cleanForm();
            }
            else 
            {
                //Save the data
                if (product.save(txtName.Text, txtDescription.Text, double.Parse(txtPrice.Text), txtBarcode.Text, int.Parse(comboBrand.SelectedValue.ToString()), int.Parse(comboSubcategory.SelectedValue.ToString()), (comboUnitOfMeasure.SelectedIndex+1).ToString(), imgBDPath) ) 
                {
                    MessageBox.Show("The product <" + txtName.Text + "> has been stored.", "New Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnAdd.Text = "Add";
                    addSaveFlag = false;
                    
                    //img FILE save in full system PATH
                    picBoxImage.Image.Save(imgPath);
                    fillDataGrid();

                    //cleanForm and disable
                    this.activateForm(false);
                    this.cleanForm();
                }
                else
                    MessageBox.Show("Error, Producto NO Guardado. "+product.ERROR);
            }

        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            //Add/Save button
            if (!addSaveFlag) btnAdd.Text = "Add"; else btnAdd.Text = "Save";

            this.fillDataGrid();

            //clean the form and disable form
            this.cleanForm();
            this.activateForm(false);
        }

        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            // open file dialog   
           
            // image filters  
            openFileDialogImage.Filter = "Image Files(*.png; *.jpg; *.jpeg; *.gif; *.bmp)|*.png; *.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                picBoxImage.Image = new Bitmap(openFileDialogImage.FileName);
                
                string extension = openFileDialogImage.FileName.Substring(openFileDialogImage.FileName.IndexOf(".")+1);
                // image file NEW name, including UniqueID, and the extension of original file
                txtImage.Text = string.Format(@"{0}.{1}", Guid.NewGuid(), extension);
                // obtener el dir de la app
                string bin = Path.GetDirectoryName(Application.StartupPath);

                //ir a un dir arriba
                string dir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(bin)));
                //image save path, the products goes in imgs/products/uniqueNAME.extension
                imgPath = string.Format(@"{0}\\imgs\\products\\{1}",dir, txtImage.Text);
                imgBDPath = string.Format(@"\\imgs\\products\\{0}",  txtImage.Text);
            }

        }

        private void comboBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            picBoxBrand.Image = null;            
            // obtener el dir de la app
            string bin = Path.GetDirectoryName(Application.StartupPath);

            //ir a un dir arriba
            string dir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(bin)));

            //agregar el path para guardar la imagen
            dir += brand.imageOf(int.Parse(comboBrand.SelectedValue.ToString()));

            //guardar la imagen
            picBoxBrand.Image = Image.FromFile(dir);
                 }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //delete the ID selected
            //confirm the delete
            if (MessageBox.Show($"Are you Sure you want to delete <{txtName.Text}>?", "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) 
            {
                if (product.delete(int.Parse(txtID.Text)))
                {
                    MessageBox.Show($"Product ID:{txtID.Text} has been DELETED.", "Product Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //clean form
                    this.cleanForm();
                    //deactivateform
                    this.activateForm(false);
                    //fiil dgv
                    this.fillDataGrid();
                }
                else 
                {                
                    MessageBox.Show($"Product ID:{txtID.Text} has NOT been DELETED. "+product.ERROR, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
