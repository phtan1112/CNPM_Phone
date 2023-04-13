using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class DeliverySlips : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString);

        private string nameOfAgent;
        private string address;
        private string num_phone;
        private string date_order;
        private string status_order;

        private string status_pay;
        private string total;
        private string method;
        private string idOrder;
        public DeliverySlips(string nameOfAgent1, string address1, string num_phone1, string date_order1, string status_order1,
            string status_pay1, string total1, string method1,string idOfOrder)
        {
            InitializeComponent();
            this.nameOfAgent = nameOfAgent1; 
            this.address = address1;
            this.num_phone = num_phone1;

            this.date_order = date_order1;   
            this.status_order = status_order1;   
            this.status_pay = status_pay1;   
            this.total = total1; 
            this.method= method1;
            this.idOrder = idOfOrder;

            load_infor_export();
            
        }

        //load all data passed from Export form and fill into textbox
        private void load_infor_export()
        {
            txtAgent.Text = this.nameOfAgent;
            txtPhone.Text = this.num_phone;
            txtAddress.Text = this.address;

            txtOrderDate.Text = this.date_order;
            txtOrderStatus.Text = this.status_order;
            txtStatusPayment.Text = this.status_pay;
            txtTotal.Text = this.total;
            txtMethod.Text = this.method;

            load_datagridview(idOrder);
        }

        //load all phones of order to datagridview
        private void load_datagridview(string id)
        {
            String query = "select p.name,p.price,aod.quantity,(p.price * aod.quantity) as 'price'\r\nfrom agent_order_detail aod, agent_order ao, phones p\r\nwhere aod.order_id = ao.id and p.id = aod.id_phone and aod.order_id =" + id;
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            conn.Open();
            DataSet ds = new DataSet();
            if (ds != null)
            {
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
}
