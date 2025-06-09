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
    public partial class AdminMainPage : Form
    {
        public AdminMainPage()
        {
            InitializeComponent();
        }

        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kullanıcıYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AUserManageForm form = new AUserManageForm();
            form.ShowDialog();
        }

        private void rezervasyonYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ARezerManageForm form = new ARezerManageForm();
            form.ShowDialog();
        }

        private void ilanYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AListingManageForm form = new AListingManageForm();
            form.ShowDialog();
        }

        private void ödemeYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            APayManageForm form = new APayManageForm();
            form.ShowDialog();
        }

        private void istatisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AIstRaporForm form = new AIstRaporForm();
            form.ShowDialog();
        }
    }
}
