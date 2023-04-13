using System;
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

namespace WindowsFormsApp
{
    public partial class CreateAgent : Form
    {
        private int id_accountant { get; set; }
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString);
        public CreateAgent(int id)
        {
            InitializeComponent();
            this.id_accountant  = id;
        }

        // click Create button to create account's agent and check validate.
        private void button1_Click(object sender, EventArgs e)
        {
            if(txtName.Text == "")
            {
                MessageBox.Show("Enter agent name!");
            }
            else if (txtUsername.Text == "")
            {
                MessageBox.Show("Enter username of agent!");
            }
            else if (txtPass.Text == "")
            {
                MessageBox.Show("Enter agent password!");
            }
            else if (txtPass.Text !=  txtConfirmPass.Text)
            {
                MessageBox.Show("Confirm password not match password!");
            }
            else if (txtAddress.Text == "")
            {
                MessageBox.Show("Enter agent location!");
            }
            else if (txtPhonenumber.Text == "")
            {
                MessageBox.Show("Enter agent phone number!");
            }
            else
            {
                string query = "insert into agent(agent_name,username,address, password,phone_number,create_by_accountID) " +
                "values('" + txtName.Text + "', '" + txtUsername.Text + "','" + txtAddress.Text + "','" + txtPass.Text + "'," + txtPhonenumber.Text + "," + id_accountant.ToString() + ")";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn); 
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Create success agent account");
                txtName.Text = "";
                txtUsername.Text = "";
                txtAddress.Text = "";
                txtPass.Text = "";
                txtConfirmPass.Text = "";
                txtPhonenumber.Text = "";
            }

        }
    }
}
