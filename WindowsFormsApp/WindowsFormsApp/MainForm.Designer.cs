namespace WindowsFormsApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.goodsReceivedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPhoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.existPhoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goodsDeliverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staticsticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createAccountForAgentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.name_account = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goodsReceivedToolStripMenuItem,
            this.goodsDeliverToolStripMenuItem,
            this.staticsticsToolStripMenuItem,
            this.createAccountForAgentToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(207, 700);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // goodsReceivedToolStripMenuItem
            // 
            this.goodsReceivedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPhoneToolStripMenuItem,
            this.existPhoneToolStripMenuItem});
            this.goodsReceivedToolStripMenuItem.Name = "goodsReceivedToolStripMenuItem";
            this.goodsReceivedToolStripMenuItem.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.goodsReceivedToolStripMenuItem.Size = new System.Drawing.Size(194, 39);
            this.goodsReceivedToolStripMenuItem.Text = "Goods Received";
            // 
            // newPhoneToolStripMenuItem
            // 
            this.newPhoneToolStripMenuItem.Name = "newPhoneToolStripMenuItem";
            this.newPhoneToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.newPhoneToolStripMenuItem.Text = "New Phone";
            this.newPhoneToolStripMenuItem.Click += new System.EventHandler(this.newPhoneToolStripMenuItem_Click);
            // 
            // existPhoneToolStripMenuItem
            // 
            this.existPhoneToolStripMenuItem.Name = "existPhoneToolStripMenuItem";
            this.existPhoneToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.existPhoneToolStripMenuItem.Text = "Exist Phone";
            this.existPhoneToolStripMenuItem.Click += new System.EventHandler(this.existPhoneToolStripMenuItem_Click);
            // 
            // goodsDeliverToolStripMenuItem
            // 
            this.goodsDeliverToolStripMenuItem.Name = "goodsDeliverToolStripMenuItem";
            this.goodsDeliverToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.goodsDeliverToolStripMenuItem.Size = new System.Drawing.Size(194, 44);
            this.goodsDeliverToolStripMenuItem.Text = "Goods deliver";
            this.goodsDeliverToolStripMenuItem.Click += new System.EventHandler(this.goodsDeliverToolStripMenuItem_Click);
            // 
            // staticsticsToolStripMenuItem
            // 
            this.staticsticsToolStripMenuItem.Name = "staticsticsToolStripMenuItem";
            this.staticsticsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.staticsticsToolStripMenuItem.Size = new System.Drawing.Size(194, 44);
            this.staticsticsToolStripMenuItem.Text = "Statisticss";
            this.staticsticsToolStripMenuItem.Click += new System.EventHandler(this.staticsticsToolStripMenuItem_Click);
            // 
            // createAccountForAgentToolStripMenuItem
            // 
            this.createAccountForAgentToolStripMenuItem.Name = "createAccountForAgentToolStripMenuItem";
            this.createAccountForAgentToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10);
            this.createAccountForAgentToolStripMenuItem.Size = new System.Drawing.Size(194, 44);
            this.createAccountForAgentToolStripMenuItem.Text = "Create Account for Agent";
            this.createAccountForAgentToolStripMenuItem.Click += new System.EventHandler(this.createAccountForAgentToolStripMenuItem_Click);
            // 
            // name_account
            // 
            this.name_account.AutoSize = true;
            this.name_account.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.name_account.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_account.ForeColor = System.Drawing.Color.DarkBlue;
            this.name_account.Location = new System.Drawing.Point(25, 195);
            this.name_account.Name = "name_account";
            this.name_account.Padding = new System.Windows.Forms.Padding(5);
            this.name_account.Size = new System.Drawing.Size(93, 28);
            this.name_account.TabIndex = 2;
            this.name_account.Text = "Wellcome";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1199, 700);
            this.Controls.Add(this.name_account);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem goodsReceivedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goodsDeliverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staticsticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createAccountForAgentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPhoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem existPhoneToolStripMenuItem;
        private System.Windows.Forms.Label name_account;
    }
}

