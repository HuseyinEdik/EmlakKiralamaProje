﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class AIstRaporForm : Form
    {
        public AIstRaporForm()
        {
            InitializeComponent();
        }

        private void BtnAnaMenu_Click(object sender, EventArgs e)
        {

            AdminMainPage adminPage = new AdminMainPage();      
            adminPage.Show();

            this.Close();
        }
    }
}
