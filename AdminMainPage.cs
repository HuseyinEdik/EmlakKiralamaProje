using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class AdminMainPage : Form
    {
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";

        public AdminMainPage()
        {
            InitializeComponent();
            LoadDashboardStats();
            LoadLastReservations();
        }

        private void LoadDashboardStats()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmdUsers = new SqlCommand("SELECT COUNT(*) FROM users", conn);
                SqlCommand cmdHouses = new SqlCommand("SELECT COUNT(*) FROM tinyhouses", conn);
                SqlCommand cmdReservations = new SqlCommand("SELECT COUNT(*) FROM reservations", conn);
                SqlCommand cmdRevenue = new SqlCommand("SELECT ISNULL(SUM(amount), 0) FROM payments WHERE status = 'ödendi'", conn);

                int userCount = (int)cmdUsers.ExecuteScalar();
                int houseCount = (int)cmdHouses.ExecuteScalar();
                int reservationCount = (int)cmdReservations.ExecuteScalar();
                decimal totalRevenue = (decimal)cmdRevenue.ExecuteScalar();

                label1.Text = "Toplam Kullanıcı Sayısı: " + userCount;
                label2.Text = "Toplam İlan Sayısı: " + houseCount;
                label3.Text = "Toplam Rezervasyon Sayısı: " + reservationCount;
                
            }
        }

        private void LoadLastReservations()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT TOP 5 r.reservationID, u.firstName + ' ' + u.lastName AS Kullanici,
                           th.title AS Ilan, r.startDate, r.endDate, r.status
                    FROM reservations r
                    JOIN users u ON r.userID = u.userID
                    JOIN tinyhouses th ON r.houseID = th.houseID
                    ORDER BY r.createdAt DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void kullanıcıYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AUserManageForm userForm = new AUserManageForm();
            userForm.Show();
            this.Hide();
        }

        private void rezervasyonYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ARezerManageForm rezerForm = new ARezerManageForm();
            rezerForm.Show();
            this.Hide();
        }

        private void ilanYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {
             this.Hide();   
            AListingManageForm listingForm = new AListingManageForm();  
            listingForm.Show();
        }

        private void ödemeYönetimiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            APayManageForm payForm = new APayManageForm();      
            payForm.Show();

            this.Hide();


        }

        private void istatisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("İstatistik ve raporlama ekranı henüz hazır değil.");
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("Zaten Ana Sayfadasınız.");
        }

         

        private void AMainPageGBX_Enter(object sender, EventArgs e)
        {

        }

        
    }
}

