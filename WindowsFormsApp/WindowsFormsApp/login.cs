using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp
{
    public partial class login : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString);
        public login()
        {
            InitializeComponent();
            this.ActiveControl = txtUsername;
            txtUsername.Focus();
        }

        // after click login, check account with input username and password has exist in database or not.
        // if yes, direct to main form
        private void button1_Click(object sender, EventArgs e) 
        {
            string username = txtUsername.Text;
            string pass = txtPass.Text;
            //MessageBox.Show(username + " " + pass);

            if (username == "")
            {
                MessageBox.Show("Plese fill your username account!");
            }
            else if (pass == "")
            {
                MessageBox.Show("Please complete your password!");
            }
            else
            {
                bool check = check_Account(username, pass);
                if (check)
                {
                    MessageBox.Show("Login Successful!");
                    this.Hide();
                    MainForm fm = new MainForm(username);
                    fm.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed!");
                }
            }
        }

        //check username and password that has exist database or not
        private bool check_Account(string name, string pass) 
        {
            
            conn.Open();
            //Add to import
            String query = " select * from accountant where username ='" + name + "' and password = " + pass;
            SqlCommand cmd = new SqlCommand(query, conn);
            //cmd.ExecuteNonQuery();

            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            conn.Close();
            int count = ds.Tables[0].Rows.Count;
            //If count is equal to 1, than show frmMain form
            if (count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
