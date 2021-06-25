using PrviProjekat.NorthwindDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrviProjekat
{
    public partial class frmProductsIzmena : Form
    {
        NorthwindDataSet ds;
        BindingSource bsProducts;
       
        public frmProductsIzmena(NorthwindDataSet ds, BindingSource bsProducts)
        {
            InitializeComponent();
            this.ds = ds;
            this.bsProducts = bsProducts;
            textBox1.DataBindings.Add("Text", bsProducts, "ProductName");
            comboBox1.DataSource = ds.Suppliers;
            comboBox1.DisplayMember = "CompanyName";
            comboBox1.ValueMember = "SupplierID";
            comboBox1.DataBindings.Add("SelectedValue",bsProducts,"SupplierID");
            comboBox2.DataSource = ds.Categories;
            comboBox2.DisplayMember = "CategoryName";
            comboBox2.ValueMember = "CategoryID";
            comboBox2.DataBindings.Add("SelectedValue",bsProducts,"CategoryID");
            textBox2.DataBindings.Add("Text", bsProducts, "QuantityPerUnit");
            textBox3.DataBindings.Add("Text", bsProducts, "UnitPrice");
            textBox4.DataBindings.Add("Text", bsProducts, "UnitsInStock");
            checkBox1.DataBindings.Add("Checked", bsProducts, "Discontinued");
        }

        private void frmProductsIzmena_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductsTableAdapter daProducts=new ProductsTableAdapter();
            bsProducts.EndEdit();
            daProducts.Update(ds.Products);
            MessageBox.Show("Izmene sacuvane");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ds.RejectChanges();
            this.Close();
            
        }
    }
}
