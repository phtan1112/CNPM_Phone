using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp
{
    public partial class ExistPhone : Form
    {
       
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString);
        private int idOfAccoutant { get; set; }
        public ExistPhone(int id)
        {
            InitializeComponent();
            
            load_goods_existing();
            load_goods_into_comboBox();
            this.idOfAccoutant = id;
        }
        
        //insert all phones has exist into comboBox for Accountant to select
        private void load_goods_into_comboBox()
        {
            string query = "select * from phones";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            
            DataSet ds = new DataSet();
            da.Fill(ds);
            comboBox2.DataSource = ds.Tables[0];
            comboBox2.DisplayMember = "name";
            comboBox2.ValueMember = "id";
            conn.Close();
        }

        //insert all phones into datagridview
        private void load_goods_existing()
        {
            string query = "select p.id, p.name, p.price,p.quantity,p.image,pg.name from phones p, phone_group pg\r\nwhere p.group_id = pg.id";
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query,conn); 
            DataSet dataSetds = new DataSet();  
            da.Fill(dataSetds);
            dataGridView1.DataSource = dataSetds.Tables[0];
            conn.Close();

        }
        
        //after click import button so the quantity of phone will plus with available number that you selected
        private void btnImport_Click(object sender, EventArgs e)
        {
            int num;
            double price;
            if (txtQuantity.Text == "")
            {
                MessageBox.Show("you need to fill quantity of this phone!");
            }
            else if (!int.TryParse(txtQuantity.Text, out num))
            {
                MessageBox.Show("Please insert the number, not string!");
            }
            else
            {
                conn.Open();

                //Add to import
                string query = "insert into import(created_date,created_by) values ('" + DateTime.Today.ToShortDateString() + "'," + this.idOfAccoutant + ")";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                //Get import id just inserted
                query = "select max(id) from import";
                cmd = new SqlCommand(query, conn);
                int idImport = (int)cmd.ExecuteScalar();

                //Get phone id
                query = "select id from phones where name = '" + comboBox2.Text + "'";
                cmd = new SqlCommand(query, conn);
                int idPhone = (int)cmd.ExecuteScalar();

                //Update Quantity
                query = "select quantity from phones where id= " + idPhone.ToString();
                cmd = new SqlCommand(query, conn);
                int Quantity = (int)cmd.ExecuteScalar();

                Quantity += Int32.Parse(txtQuantity.Text);

                query = "update phones set quantity=" + Quantity.ToString() + " where id=" + idPhone.ToString();
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                if (txtPrice.Text == "")
                {
                    //Add to import_detail
                    query = "insert into import_detail(import_id,phone_id,quantity) values (" + idImport.ToString() + ", " + idPhone.ToString() + ", " + txtQuantity.Text + ")";
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Import successfully");
                    conn.Close();
                    load_goods_existing();
                }
                else if (double.TryParse(txtPrice.Text, out price))  // la import and update new price
                {
                    
                    double newPrice = double.Parse(txtPrice.Text, System.Globalization.CultureInfo.InvariantCulture);
                   

                    //Add to import_detail
                    query = "insert into import_detail(import_id,phone_id,quantity,import_price) values (" + idImport.ToString() + ", " + idPhone.ToString() + ", " + txtQuantity.Text + ", " + newPrice.ToString() + ")";
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    //update again price
                    query = "update phones set price=" + newPrice.ToString() + " where id=" + idPhone.ToString();
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Import successfully");
                    conn.Close();
                    load_goods_existing();
                }
                else
                {
                    MessageBox.Show("Please insert the number, not string!");
                }
                
            }
        }


        //after click a row of phone in datagridview, data of phone clicked will be fill on textbox and combobox above for accountant change
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.RowIndex < dataGridView1.RowCount - 1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                comboBox2.SelectedIndex = (int)row.Cells["id"].Value - 1;

                txtQuantity.Text = row.Cells[3].Value.ToString();
            }
        }

        //after click update button so the quantity and price(if they change price) of phone that you selected will be update
        private void btnUpdate_Click(object sender, EventArgs e) //btnUpdate, update luon quantity
        {
            int num;
            double price;
            if (txtQuantity.Text == "")
            {
                MessageBox.Show("you need to fill quantity of this phone!");
            }
            else if (!int.TryParse(txtQuantity.Text, out num))
            {
                MessageBox.Show("Please insert the number, not string!");
            }
            else
            {
                conn.Open();

                //Add to import
                string query = "insert into import(created_date,created_by) values ('" + DateTime.Today.ToShortDateString() + "'," + this.idOfAccoutant + ")";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                //Get import id just inserted
                query = "select max(id) from import";
                cmd = new SqlCommand(query, conn);
                int idImport = (int)cmd.ExecuteScalar();

                //Get phone id
                query = "select id from phones where name = '" + comboBox2.Text + "'";
                cmd = new SqlCommand(query, conn);
                int idPhone = (int)cmd.ExecuteScalar();

                //Update Quantity
                /*query = "select quantity from phones where id= " + idPhone.ToString();
                cmd = new SqlCommand(query, conn);
                int Quantity = (int)cmd.ExecuteScalar();*/

                int Quantity = Int32.Parse(txtQuantity.Text);

                query = "update phones set quantity=" + Quantity.ToString() + " where id=" + idPhone.ToString();
                cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                if (txtPrice.Text == "")
                {
                    //Add to import_detail
                    query = "insert into import_detail(import_id,phone_id,quantity) values (" + idImport.ToString() + ", " + idPhone.ToString() + ", " + txtQuantity.Text + ")";
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Update successfully");
                    conn.Close();
                    load_goods_existing();
                }
                else if (double.TryParse(txtPrice.Text, out price))  // la import and update new pride
                {

                    double newPrice = double.Parse(txtPrice.Text, System.Globalization.CultureInfo.InvariantCulture);


                    //Add to import_detail
                    query = "insert into import_detail(import_id,phone_id,quantity,import_price) values (" + idImport.ToString() + ", " + idPhone.ToString() + ", " + txtQuantity.Text + ", " + newPrice.ToString() + ")";
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    //update again price
                    query = "update phones set price=" + newPrice.ToString() + " where id=" + idPhone.ToString();
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Update successfully");
                    conn.Close();
                    load_goods_existing();
                }
                else
                {
                    MessageBox.Show("Please insert the number, not string!");
                }

            }
        }

        
    }
}
