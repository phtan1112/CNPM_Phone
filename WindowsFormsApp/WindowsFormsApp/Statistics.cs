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
    public partial class Statistics : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString);
        public Statistics()
        {
            InitializeComponent();
        }
        /*
            Incoming Stock
            Outcoming Stock
            Best Selling Product
            Revenue Monthly
         */


        //load four types of staticstic and handle to call Procedure
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("", conn);

            if (comboBox1.SelectedIndex == 0)
            {
                da = new SqlDataAdapter("goods_received", conn);

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                da = new SqlDataAdapter("goods_sold", conn);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                da = new SqlDataAdapter("best_selling_goods", conn);
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                da = new SqlDataAdapter("revenue_report_monthly", conn);
            }
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
    }
}
