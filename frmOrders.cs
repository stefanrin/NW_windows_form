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
    public partial class frmOrders : Form
    {
        NorthwindDataSet ds;
        int productID;
        OrdersTableAdapter daOrder;
        Order_DetailsTableAdapter daOrderDetails;
        //BindingSource bsOrderDetails;
        BindingSource bsOrder;
        string filter = "";
        List<int> orderID_lista = new List<int>();
        public frmOrders(NorthwindDataSet ds, int productID)
        {
            InitializeComponent();
            this.ds = ds;
            this.productID = productID;
            daOrderDetails = new Order_DetailsTableAdapter();
            daOrder = new OrdersTableAdapter();
        }

        private void frmOrders_Load(object sender, EventArgs e)
        {
            daOrderDetails.Fill(ds.Order_Details);
            daOrder.Fill(ds.Orders);
            //bsOrderDetails = new BindingSource(ds, "Order Details");
            bsOrder = new BindingSource(ds, "Orders");
            //bsOrderDetails.Filter = "ProductID=" + productID;
            // Uzimam sve redove iz medjutabele, filtriram samo one koji odgovaraju selektovanom proizvodu i uzimam ideve ordera
            try
            {
                foreach (NorthwindDataSet.Order_DetailsRow red in ds.Order_Details.Rows)
                {
                    if (red.ProductID == productID)
                    {
                        orderID_lista.Add((int)red.OrderID);
                    }
                }

                filter = "orderID in (" + String.Join(",", orderID_lista) + ")";
                MessageBox.Show("Postoji "+orderID_lista.Count().ToString()+" porudzbina za ovaj proizvod");
                bsOrder.Filter = filter;
                dataGridView1.DataSource = bsOrder;


                //MessageBox.Show(productID.ToString());
            }
            catch {
            }
            
            
        }

        
    }
}
