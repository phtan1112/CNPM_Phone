using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp
{
    public partial class NewPhone : Form
    {
        int no = 0;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString);
        private int idOfAccoutant { get; set; }
        public NewPhone(int id)
        {
            InitializeComponent();
            this.idOfAccoutant = id;
            load_categories_intoComboBox();
            
        }

        //load all phone groups into combobox
        private void load_categories_intoComboBox()
         {
             conn.Open();   
             string query = "select * from phone_group";
             SqlDataAdapter da = new SqlDataAdapter(query,conn);

             DataSet ds = new DataSet();
             da.Fill(ds);
             cbCate.DataSource = ds.Tables[0];
             cbCate.DisplayMember= "name";
             cbCate.ValueMember = "id";
             conn.Close();

           
         }

        //after they enter all textbox required so add to below datagrid to review all phone that will prepare to import
        private void btnAddPhone_Click(object sender, EventArgs e)
        {
            if (validate_add())
            {
                int numberOfPhone = Int32.Parse(txtQuantity.Text);
                double newPrice = double.Parse(txtPrice.Text, System.Globalization.CultureInfo.InvariantCulture);
                /*MessageBox.Show(cbCate.Text+ "  "+"  "+ txtNamePhone.Text  + numberOfPhone.ToString() + "   " +
                                    newPrice.ToString());*/
                no++;
                if (txtImage.Text == "")
                {
                    dataGridView1.Rows.Add(no.ToString(), txtNamePhone.Text, newPrice.ToString(), numberOfPhone.ToString(), null, cbCate.Text);
                }
                else
                {
                    dataGridView1.Rows.Add(no.ToString(), txtNamePhone.Text, newPrice.ToString(), numberOfPhone.ToString(), txtImage.Text, cbCate.Text);
                }
                
               


            }
        }

        // add all phone of datagrid below to database
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[0].Cells[0].Value != null)
            {
                conn.Open();
                //Tao new import cho accountant
                string query = "insert into import(created_date,created_by) values ('" + DateTime.Today.ToShortDateString() + "'," + this.idOfAccoutant + ")";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                //Get import id just inserted
                query = "select max(id) from import";
                cmd = new SqlCommand(query, conn);
                int idImport = (int)cmd.ExecuteScalar();

                

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value!= null)
                    {
                       
                        string name_phone = row.Cells[1].Value.ToString();
                        string price_phone = row.Cells[2].Value.ToString();
                        string quantity_phone = row.Cells[3].Value.ToString();
                        string image = row.Cells[4].Value == null ? "" : row.Cells[4].Value.ToString();
                        string idCatee = getIdOfCategory(row.Cells[5].Value.ToString()).ToString(); //dung function
                                        
                        //add phone into table Phones
                        query = "insert into phones(name,price,quantity,image,group_id) values('" + name_phone + "'," + price_phone + "," +
                             quantity_phone + ",'" + image + "'," + idCatee + ")";
                        cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();


                        //get id of phone that have just added
                        query = "select max(id) from phones";
                        cmd = new SqlCommand(query, conn);
                        int idPhone = (int)cmd.ExecuteScalar();

                        //Add to import_detail
                        query = "insert into import_detail(import_id,phone_id,quantity) values (" + idImport.ToString() + ", " + idPhone.ToString() + ", " + quantity_phone + ")";
                        cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();

                    }
                   

                }              
                conn.Close();
                no = 0;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                MessageBox.Show("Import successfully");

            }
            else
            {
                MessageBox.Show("Please insert phone!");
            }
            
        }

        //get id of phone group from name of group
        public int getIdOfCategory(string name_category)
        {
            
            string query = "select id from phone_group where name ='" +name_category+"'";
            SqlCommand cmd = new SqlCommand(query, conn);
            int idCate = (int)cmd.ExecuteScalar();
            return idCate;
        }
       
        //validate of textbox
        public bool validate_add()
        {
            int num;
            double price;
            if (txtNamePhone.Text == "")
            {
                MessageBox.Show("Please insert name of phone!!");
            }
            else if (txtQuantity.Text == "")
            {
                MessageBox.Show("Please insert quantity of phone!!");
            }
            else if (!int.TryParse(txtQuantity.Text, out num))
            {
                MessageBox.Show("Please insert the number, not string!");
            }
            else if (txtPrice.Text == "")
            {
                MessageBox.Show("Please insert price of phone!!");
            }
            else if (!double.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Please insert the number, not string!");
            }
            else
            {
                return true;
            }
            return false;
        }

    }
}
