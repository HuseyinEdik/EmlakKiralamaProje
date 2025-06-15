using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class HostMainPage : Form
    {
        private int currentUserID;
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";

        public HostMainPage(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
            this.Load += new EventHandler(HostMainPage_Load);
        }

        private void HostMainPage_Load(object sender, EventArgs e)
        {
            LoadStatistics();
            LoadRecentReservations();
        }

        private void LoadStatistics()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM tinyhouses WHERE ownerID = @OwnerID", conn);
                cmd1.Parameters.AddWithValue("@OwnerID", currentUserID);
                label6.Text = cmd1.ExecuteScalar().ToString();

                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM reservations r JOIN tinyhouses t ON r.houseID = t.houseID WHERE t.ownerID = @OwnerID", conn);
                cmd2.Parameters.AddWithValue("@OwnerID", currentUserID);
                label7.Text = cmd2.ExecuteScalar().ToString();

                SqlCommand cmd3 = new SqlCommand("SELECT ISNULL(SUM(p.amount), 0) FROM payments p JOIN reservations r ON p.reservationID = r.reservationID JOIN tinyhouses t ON r.houseID = t.houseID WHERE p.status = 'Ödendi' AND t.ownerID = @OwnerID", conn);
                cmd3.Parameters.AddWithValue("@OwnerID", currentUserID);
                label8.Text = cmd3.ExecuteScalar().ToString() + " ₺";
            }
        }

        private void LoadRecentReservations()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            SELECT TOP 10 r.reservationID, 
                           u.firstName + ' ' + u.lastName AS UserName, 
                           t.title, 
                           r.startDate, 
                           r.endDate
            FROM reservations r
            JOIN users u ON r.userID = u.userID
            JOIN tinyhouses t ON r.houseID = t.houseID
            WHERE r.createdAt BETWEEN DATEADD(DAY, -7, GETDATE()) AND GETDATE()
              AND t.ownerID = @OwnerID
            ORDER BY r.createdAt DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@OwnerID", currentUserID);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView2.DataSource = dt;
            }
        }



        private void ilanlarımToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Show();   
            HListingManageForm form = new HListingManageForm(currentUserID);
            form.ShowDialog();
        }

        private void rezervasyonToolStripMenuItem_Click(object sender, EventArgs e)
        {

        
            HRezerManageForm form = new HRezerManageForm(currentUserID);
            form.ShowDialog();
        }

        private void puanlarVeYorumlarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
            HReviewsForm form = new HReviewsForm(currentUserID);
            form.ShowDialog();
        }

        private void ödemeBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Bu ekran henüz hazır değil...");
        }

        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Zaten ana sayfadasınız ! ! !");
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


