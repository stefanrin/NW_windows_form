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
//RIN 8/19
namespace PrviProjekat
{
    public partial class frmCategories : Form
    {
        NorthwindDataSet ds;
        CategoriesTableAdapter daCategories;
        ProductsTableAdapter daProducts;
        BindingSource bsCategories;
        public frmCategories()
        {
            InitializeComponent();
            ds = new NorthwindDataSet();
            daCategories = new CategoriesTableAdapter();
            daProducts = new ProductsTableAdapter();
        }

        private void frmKategorijePocetna_Load(object sender, EventArgs e)
        {
            daCategories.Fill(ds.Categories);
            daProducts.Fill(ds.Products);
            bsCategories = new BindingSource(ds, "Categories");
            listBox1.DataSource = bsCategories;
            listBox1.DisplayMember = "CategoryName";
            listBox1.ValueMember = "CategoryID";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(listBox1.SelectedValue.ToString());
            frmProducts frm2 = new frmProducts(ds, (int)listBox1.SelectedValue);
            frm2.ShowDialog();
        }
    }
}
