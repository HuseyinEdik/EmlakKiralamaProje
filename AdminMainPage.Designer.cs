namespace EmlakKiralamaProje
{
    partial class AdminMainPage
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
            this.AdminMenuStrip = new System.Windows.Forms.MenuStrip();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kullanıcıYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rezervasyonYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilanYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ödemeYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.istatisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AMainPageGBX = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AdminMenuStrip.SuspendLayout();
            this.AMainPageGBX.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // AdminMenuStrip
            // 
            this.AdminMenuStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.AdminMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem,
            this.kullanıcıYönetimiToolStripMenuItem,
            this.rezervasyonYönetimiToolStripMenuItem,
            this.ilanYönetimiToolStripMenuItem,
            this.ödemeYönetimiToolStripMenuItem,
            this.istatisToolStripMenuItem});
            this.AdminMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.AdminMenuStrip.Name = "AdminMenuStrip";
            this.AdminMenuStrip.Padding = new System.Windows.Forms.Padding(11, 4, 0, 4);
            this.AdminMenuStrip.Size = new System.Drawing.Size(854, 33);
            this.AdminMenuStrip.TabIndex = 1;
            this.AdminMenuStrip.Text = "menuStrip1";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(93, 25);
            this.aToolStripMenuItem.Text = "Ana Menü";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // kullanıcıYönetimiToolStripMenuItem
            // 
            this.kullanıcıYönetimiToolStripMenuItem.Name = "kullanıcıYönetimiToolStripMenuItem";
            this.kullanıcıYönetimiToolStripMenuItem.Size = new System.Drawing.Size(145, 25);
            this.kullanıcıYönetimiToolStripMenuItem.Text = "Kullanıcı Yönetimi";
            this.kullanıcıYönetimiToolStripMenuItem.Click += new System.EventHandler(this.kullanıcıYönetimiToolStripMenuItem_Click);
            // 
            // rezervasyonYönetimiToolStripMenuItem
            // 
            this.rezervasyonYönetimiToolStripMenuItem.Name = "rezervasyonYönetimiToolStripMenuItem";
            this.rezervasyonYönetimiToolStripMenuItem.Size = new System.Drawing.Size(175, 25);
            this.rezervasyonYönetimiToolStripMenuItem.Text = "Rezervasyon Yönetimi";
            this.rezervasyonYönetimiToolStripMenuItem.Click += new System.EventHandler(this.rezervasyonYönetimiToolStripMenuItem_Click);
            // 
            // ilanYönetimiToolStripMenuItem
            // 
            this.ilanYönetimiToolStripMenuItem.Name = "ilanYönetimiToolStripMenuItem";
            this.ilanYönetimiToolStripMenuItem.Size = new System.Drawing.Size(112, 25);
            this.ilanYönetimiToolStripMenuItem.Text = "İlan Yönetimi";
            this.ilanYönetimiToolStripMenuItem.Click += new System.EventHandler(this.ilanYönetimiToolStripMenuItem_Click);
            // 
            // ödemeYönetimiToolStripMenuItem
            // 
            this.ödemeYönetimiToolStripMenuItem.Name = "ödemeYönetimiToolStripMenuItem";
            this.ödemeYönetimiToolStripMenuItem.Size = new System.Drawing.Size(138, 25);
            this.ödemeYönetimiToolStripMenuItem.Text = "Ödeme Yönetimi";
            this.ödemeYönetimiToolStripMenuItem.Click += new System.EventHandler(this.ödemeYönetimiToolStripMenuItem_Click);
            // 
            // istatisToolStripMenuItem
            // 
            this.istatisToolStripMenuItem.Name = "istatisToolStripMenuItem";
            this.istatisToolStripMenuItem.Size = new System.Drawing.Size(175, 25);
            this.istatisToolStripMenuItem.Text = "İstatistik ve raporlama";
            this.istatisToolStripMenuItem.Click += new System.EventHandler(this.istatisToolStripMenuItem_Click);
            // 
            // AMainPageGBX
            // 
            this.AMainPageGBX.Controls.Add(this.dataGridView1);
            this.AMainPageGBX.Controls.Add(this.label5);
            this.AMainPageGBX.Controls.Add(this.label3);
            this.AMainPageGBX.Controls.Add(this.label2);
            this.AMainPageGBX.Controls.Add(this.label1);
            this.AMainPageGBX.Location = new System.Drawing.Point(14, 36);
            this.AMainPageGBX.Name = "AMainPageGBX";
            this.AMainPageGBX.Size = new System.Drawing.Size(828, 384);
            this.AMainPageGBX.TabIndex = 2;
            this.AMainPageGBX.TabStop = false;
            this.AMainPageGBX.Text = "Ana Sayfa";
            this.AMainPageGBX.Enter += new System.EventHandler(this.AMainPageGBX_Enter);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(288, 88);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(534, 290);
            this.dataGridView1.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(430, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(250, 24);
            this.label5.TabIndex = 12;
            this.label5.Text = "Yapılan Son Rezervasyonlar:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(246, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "Toplam Rezervasyon Sayısı:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "Toplam İlan Sayısı:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Toplam Kullanıcı Sayısı:";
            // 
            // AdminMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 432);
            this.Controls.Add(this.AMainPageGBX);
            this.Controls.Add(this.AdminMenuStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "AdminMainPage";
            this.Text = "AdminMainPage";
            this.AdminMenuStrip.ResumeLayout(false);
            this.AdminMenuStrip.PerformLayout();
            this.AMainPageGBX.ResumeLayout(false);
            this.AMainPageGBX.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip AdminMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem kullanıcıYönetimiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rezervasyonYönetimiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ilanYönetimiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ödemeYönetimiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem istatisToolStripMenuItem;
        private System.Windows.Forms.GroupBox AMainPageGBX;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
    }
}