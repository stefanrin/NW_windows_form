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
    public partial class frmProducts : Form
    {
        NorthwindDataSet ds;
        BindingSource bsProducts;
        //Potrebno je ucitati i povezati Dobavljace zbog filtra
        SuppliersTableAdapter daSuppliers;
        BindingSource bsSuppliers;
        int categoryID;
        string filter1;
        string filter2;
        public frmProducts(NorthwindDataSet ds, int categoryID)
        {
            InitializeComponent();
            this.ds = ds;
            this.categoryID = categoryID;
            daSuppliers = new SuppliersTableAdapter();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            filter1 = "";
            filter2 = "";
            daSuppliers.Fill(ds.Suppliers);
            bsSuppliers = new BindingSource(ds, "Suppliers");
            bsProducts = new BindingSource(ds, "Products");
            comboBox1.DataSource = bsSuppliers;
            comboBox1.DisplayMember = "CompanyName";
            comboBox1.ValueMember = "SupplierID";
            bsProducts.Filter = "categoryid=" + categoryID+filter1+filter2;
            dataGridView1.DataSource = bsProducts;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                filter1 = " AND SupplierID=" + comboBox1.SelectedValue.ToString();
            }
            else {
                filter1 = "";
            }
            bsProducts.Filter = "categoryid=" + categoryID + filter1 + filter2;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                double cena;
                bool uspesno = double.TryParse(textBox1.Text, out cena);
                if (uspesno)
                {
                    filter2 = " AND UnitPrice<" + cena.ToString();
                }
            }
            else {
                filter2 = "";
            }
            bsProducts.Filter = "categoryid=" + categoryID + filter1 + filter2;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Datgrid selekcija promenjena");
            try
            {
                double suma = 0;
                NorthwindDataSet.ProductsRow proizvod = (NorthwindDataSet.ProductsRow)((DataRowView)bsProducts.Current).Row;
                suma = ((float)proizvod.UnitPrice * proizvod.UnitsInStock);
                label4.Text = suma.ToString();
            }
            catch {
                label4.Text = "0";
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmProductsIzmena frm2 = new frmProductsIzmena(ds,bsProducts);
            
            frm2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmProductsNovi frm = new frmProductsNovi(ds);
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (bsProducts.Current!=null) {
                try
                {
                    ((DataRowView)bsProducts.Current).Row.Delete();
                    ProductsTableAdapter daProducts = new ProductsTableAdapter();
                    bsProducts.EndEdit();
                    daProducts.Update(ds.Products);
                }
                catch {
                    ds.RejectChanges();
                    MessageBox.Show("Selektovani red nije moguce obrisati zbog restrikcija u bazi ☹");
                    
                }
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NorthwindDataSet.ProductsRow proizvod = (NorthwindDataSet.ProductsRow)((DataRowView)bsProducts.Current).Row;
            frmOrders frm = new frmOrders(ds, (int)proizvod.ProductID);
            frm.ShowDialog();
        }
    }
   
}
