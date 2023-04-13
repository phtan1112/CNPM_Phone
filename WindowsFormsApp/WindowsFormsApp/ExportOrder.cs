using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WindowsFormsApp
{
    
    public partial class ExportOrder : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString);
        private int idOfAccoutant { get; set; }
        private String indexOrder;
        private String indexAgent;

        //info agent
        private string name_of_agent;
        private string address_of_agent;
        private string numberphone_of_agent;
        public ExportOrder(int id)
        {
            InitializeComponent();
            this.idOfAccoutant = id;
            load_form_list_of_agent();
        }

       

        //get all data of agent and load into datagridview first
        private void load_form_list_of_agent()
        {
            conn.Open();
            string query = "select id,agent_name,address,phone_number from agent";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet dataSetds = new DataSet();
            da.Fill(dataSetds);
            dataGridViewAgent.DataSource = dataSetds.Tables[0];

            conn.Close();   
        }

        // click one of these accountant and will load all order of agent clicked
        private void dataGridViewAgent_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.RowIndex < dataGridViewAgent.RowCount - 1)
            {
                DataGridViewRow row = dataGridViewAgent.Rows[e.RowIndex];
                int id = (int)row.Cells[0].Value;
                Load_order_of_specific_agent(id.ToString());
                name_of_agent = row.Cells[1].Value.ToString();
                address_of_agent = row.Cells[2].Value.ToString();
                numberphone_of_agent = row.Cells[3].Value.ToString();
            }
        }

        //load all orders of agent clicked
        private void Load_order_of_specific_agent(string id)
        {
            indexAgent = id;
            string query = "select id,order_date,status_order,status_pay,method_pay from agent_order where agent_id=" + id;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet dataSetds = new DataSet();
            da.Fill(dataSetds);
            dataGridViewOrders.DataSource = dataSetds.Tables[0];
            conn.Close();
        }


        // click one of these order of accountant and will load all information of this order into final datagridview and fill Textbox
        private void dataGridViewOrders_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            

            if (e.RowIndex == -1 || e.RowIndex == dataGridViewOrders.Rows.Count - 1) return;

            indexOrder = dataGridViewOrders.Rows[e.RowIndex].Cells["id"].Value.ToString();

            if (e.RowIndex == -1) return;
            DateTime dt = (DateTime)dataGridViewOrders.Rows[e.RowIndex].Cells["order_date"].Value;
            txtDateOrder.Text = dt.ToShortDateString();
            txtMethod.Text = dataGridViewOrders.Rows[e.RowIndex].Cells["method_pay"].Value.ToString();
            bool check = (bool)dataGridViewOrders.Rows[e.RowIndex].Cells["status_pay"].Value;
            if (check)
            {
                txtStatusPayment.Text = "Pay Successful";
            }
            else
            {
                txtStatusPayment.Text = "Ain't Pay";
            }
            check = (bool)dataGridViewOrders.Rows[e.RowIndex].Cells["status_order"].Value;
            if (check)
            {
                txtOrderStatus.Text = "Delivering";
            }
            else
            {
                txtOrderStatus.Text = "Processing";
            }
            if (e.RowIndex < dataGridViewOrders.RowCount - 1)
            {
                dataGridViewOrderDetails(dataGridViewOrders.Rows[e.RowIndex].Cells["id"].Value.ToString());
            }
        }

        //load all phones of order clicked
        private void dataGridViewOrderDetails(string idOfOrder)
        {
            String query = "select  p.id, p.name,p.price,aod.quantity,(p.price * aod.quantity) as 'price'\r\nfrom agent_order_detail aod, agent_order ao, phones p\r\nwhere aod.order_id = ao.id and p.id = aod.id_phone and aod.order_id =" + idOfOrder;
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            conn.Open();
            DataSet ds = new DataSet();
            if (ds != null)
            {
                da.Fill(ds);
                dataGridViewOrderDetail.DataSource = ds.Tables[0];
            }
            query = "select sum(p.price * aod.quantity)\r\nfrom agent_order_detail aod, agent_order ao, phones p\r\nwhere aod.order_id = ao.id and p.id = aod.id_phone and aod.order_id =" + idOfOrder;
            SqlCommand cmd = new SqlCommand(query, conn);
            txtTotal.Text = cmd.ExecuteScalar().ToString();
            conn.Close();
        }

        // click button Export and this will show a bill form with all phone and total price
        private void btnExport_Click(object sender, EventArgs e)
        {
            //btnExport
            if (txtDateOrder.Text == "")
            {
                MessageBox.Show("Make sure that you clicked the order!!");
            }
            else if (txtOrderStatus.Text == "Delivering" && txtStatusPayment.Text == "Pay Successful")
            {
                MessageBox.Show("This order have already exported before");
            }
            else if (txtOrderStatus.Text.ToLower() == "processing" && txtStatusPayment.Text.ToLower() == "ain't pay" && (txtMethod.Text.ToLower() == "banking" || txtMethod.Text.ToLower() == "momo"))
            {
                MessageBox.Show("This order haven't paid sucessful");
            }
            else if (txtOrderStatus.Text.ToLower() == "delivering" && txtMethod.Text.ToLower() == "cash"  && txtStatusPayment.Text.ToLower() == "ain't pay")
            {
                MessageBox.Show("This order is delivering!");
            }
            else
            {
                conn.Open();

                foreach (DataGridViewRow row in dataGridViewOrderDetail.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        //update quantity of table phones
                        int quantityOfOrder = (int)row.Cells["quantity"].Value;
                        string idOfPhone = row.Cells["id"].Value.ToString();
                        

                        //get current quantity of phone with id phone
                        string query = "select quantity from phones where id =" + idOfPhone;
                        SqlCommand c = new SqlCommand(query, conn);
                        int currQuantityPhone = (int)c.ExecuteScalar();

                        currQuantityPhone = currQuantityPhone -quantityOfOrder;

                        
                        query = "update phones set quantity  =" + currQuantityPhone + " where id = " + idOfPhone;
                        c = new SqlCommand(query, conn);
                        c.ExecuteNonQuery();

                        //insert phones sold into table sold
                        query = "insert into sold(phone_id,quantity) values(" + idOfPhone + "," + quantityOfOrder + " )";
                        c = new SqlCommand(query, conn);
                        c.ExecuteNonQuery();

                    }
                }
                //change status order -> delivering
                string query1 = "update agent_order set status_order=1 where id = " + indexOrder;
                SqlCommand cmd = new SqlCommand(query1, conn);
                cmd.ExecuteNonQuery();
                txtOrderStatus.Text = "Delivering";

                conn.Close();

                //load datagridview order of agent
                Load_order_of_specific_agent(indexAgent);

                DeliverySlips DS =
                    new DeliverySlips(name_of_agent, address_of_agent, numberphone_of_agent, txtDateOrder.Text,
                   "Delivering", txtStatusPayment.Text, txtTotal.Text, txtMethod.Text, indexOrder);
                DS.Show();
            }
        }
        // click button Update to update status pay and status order of order's agent
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //btnUpdate
            if (txtDateOrder.Text == "")
            {
                MessageBox.Show("Make sure that you clicked the order!!");
            }
           
            else
            {
                
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                foreach (DataGridViewRow row in dataGridViewOrders.Rows)
                {
                    
                    if (row.Cells[0].Value != null)
                    {
                        string idOfOrder = row.Cells["id"].Value.ToString();
                        
                        bool check_pay = (bool)row.Cells["status_pay"].Value;
                        bool check_shipping = (bool)row.Cells["status_order"].Value;

                        int i_pay = check_pay == true ? 1 : 0;
                        int i_shipping = check_shipping == true ? 1 : 0;

                        string queryChangeStatusPay = "update agent_order set status_order="+ i_shipping  + ",status_pay = "+ i_pay + " where id = " + idOfOrder;
                        cmd = new SqlCommand(queryChangeStatusPay, conn);
                        cmd.ExecuteNonQuery();
                        txtOrderStatus.Text = check_shipping ==true ? "delivering":"processing";
                        txtStatusPayment.Text = check_pay == true ? "Pay Successful" : "Ain't pay";
                    }
                }
               

                conn.Close();

                //load datagridview order of agent
                Load_order_of_specific_agent(indexAgent);

                MessageBox.Show("Update successful!");
            }
        }
    }
}
