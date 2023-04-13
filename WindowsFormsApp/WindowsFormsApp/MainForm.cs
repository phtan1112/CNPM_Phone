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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp
{
    public partial class MainForm : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConn"].ConnectionString);
        private string name_accountant { get; set; }
        private int id_accountant { get; set; }
        public MainForm(string name_accountant1)
        {
            InitializeComponent();
            this.name_accountant = name_accountant1;
            setNameAccountant();
            setIDOfAccountant();
        }

        // set id when accountant login success and assign id for all form in main form
        private void setIDOfAccountant() 
        {
            int getUserID;
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from accountant where username=@name", conn);
            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name_accountant;
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                getUserID = Convert.ToInt32(result);
                this.id_accountant = getUserID;
            }
            conn.Close();
        }
        private void setNameAccountant()
        {
            name_account.Text = "Wellcome "+ this.name_accountant;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // after click existphone in menutrip so a small form will be displayed
        private void existPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExistPhone exist = new ExistPhone(this.id_accountant);
            exist.MdiParent = this;
            exist.Text = "Import";
            exist.Dock = DockStyle.Fill;
            exist.Show();
        }

        // after click newphone in menutrip so a small form will be displayed
        private void newPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewPhone newFrm = new NewPhone(this.id_accountant);
            newFrm.MdiParent = this;
            newFrm.Text = "Import";
            newFrm.Dock = DockStyle.Fill;
            newFrm.Show();
        }

        // after click goodsDeliver in menutrip so a small form will be displayed
        private void goodsDeliverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportOrder export = new ExportOrder(this.id_accountant);
            export.MdiParent = this;
            export.Text = "Export";
            export.Dock = DockStyle.Fill;
            export.Show();
        }

        // after click Staticstic in menutrip so a small form will be displayed
        private void staticsticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Statistics statistic = new Statistics();
            statistic.MdiParent = this;
            statistic.Text = "Statistics";
            statistic.Dock = DockStyle.Fill;
            statistic.Show();
        }

        // after click CreateAgent in menutrip so a small form will be displayed
        private void createAccountForAgentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateAgent creaAgent = new CreateAgent(this.id_accountant);
            creaAgent.MdiParent = this;
            creaAgent.Text = "AgentAccount";
            creaAgent.Dock = DockStyle.Fill;
            creaAgent.Show();
        }
    }
    
}
