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
    public partial class frmProductsNovi : Form
    {
        NorthwindDataSet ds;
        public frmProductsNovi(NorthwindDataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
            comboBox1.DataSource = ds.Suppliers;
            comboBox1.DisplayMember = "CompanyName";
            comboBox1.ValueMember = "SupplierID";
            
            comboBox2.DataSource = ds.Categories;
            comboBox2.DisplayMember = "CategoryName";
            comboBox2.ValueMember = "CategoryID";
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NorthwindDataSet.ProductsRow noviProizvod = ds.Products.NewProductsRow();
            try
            {
                noviProizvod.ProductName = textBox1.Text;
                noviProizvod.SupplierID = (int)comboBox1.SelectedValue;
                noviProizvod.CategoryID = (int)comboBox2.SelectedValue;
                noviProizvod.QuantityPerUnit = textBox2.Text;
                noviProizvod.UnitPrice = int.Parse(textBox3.Text);
                noviProizvod.UnitsInStock = short.Parse(textBox4.Text);
                noviProizvod.Discontinued = checkBox1.Checked;
                ds.Products.AddProductsRow(noviProizvod);
                ProductsTableAdapter daProducts = new ProductsTableAdapter();
                daProducts.Update(ds.Products);
                MessageBox.Show("Novi proizvod dodat");
                this.Close();
            }
            catch {
                MessageBox.Show("Proverite unos!");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
