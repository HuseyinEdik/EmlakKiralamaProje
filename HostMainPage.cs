using System;
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
    public partial class HostMainPage : Form
    {
        public HostMainPage()
        {
            InitializeComponent();
        }

        private void ilanlarımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HListingManageForm form = new HListingManageForm();
            form.ShowDialog();
        }

        private void rezervasyonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HRezerManageForm form = new HRezerManageForm();
            form.ShowDialog();
        }

        private void puanlarVeYorumlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HReviewsForm form = new HReviewsForm();
            form.ShowDialog();
        }

        private void ödemeBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HPayManageForm form = new HPayManageForm();
            form.ShowDialog();
        }
    }
}
