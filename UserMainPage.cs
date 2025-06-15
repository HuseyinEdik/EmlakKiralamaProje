using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class UserMainPage : Form
    {
        private int currentUserID;
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";

        public UserMainPage(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
            LoadAllListings();
            LoadPopularListings();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void LoadAllListings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT houseID, title, location, pricePerNight, imagePath, ownerID FROM tinyhouses WHERE isActive = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void LoadPopularListings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT TOP 3 t.title, t.location, t.pricePerNight
                    FROM reservations r
                    JOIN tinyhouses t ON r.houseID = t.houseID
                    WHERE r.status = 'onaylandi'
                    ORDER BY r.createdAt DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox10.Text = row.Cells["title"].Value.ToString();
                textBox8.Text = row.Cells["location"].Value.ToString();
                textBox3.Text = row.Cells["pricePerNight"].Value.ToString();

                int ownerID = Convert.ToInt32(row.Cells["ownerID"].Value);
                LoadOwnerName(ownerID);

                string imagePath = row.Cells["imagePath"].Value?.ToString();
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    pictureBox1.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
        }

        private void LoadOwnerName(int ownerID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT firstName + ' ' + lastName AS fullName FROM users WHERE userID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", ownerID);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox2.Text = reader["fullName"].ToString();
                }
                reader.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen bir ilan seçin.");
                return;
            }

            int houseID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["houseID"].Value);
            DateTime start = dateTimePicker3.Value;
            DateTime end = dateTimePicker4.Value;

            if (end <= start)
            {
                MessageBox.Show("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO reservations (userID, houseID, startDate, endDate, status, createdAt)
                    VALUES (@UserID, @HouseID, @Start, @End, 'beklemede', GETDATE())";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", currentUserID);
                cmd.Parameters.AddWithValue("@HouseID", houseID);
                cmd.Parameters.AddWithValue("@Start", start);
                cmd.Parameters.AddWithValue("@End", end);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Rezervasyon başarıyla yapıldı.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string search = textBox1.Text.Trim();
                string query = "SELECT houseID, title, location, pricePerNight, imagePath, ownerID FROM tinyhouses WHERE isActive = 1 AND title LIKE @Search";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@Search", "%" + search + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            LoadAllListings();
        }

        private void yorumVePuanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UPayManageForm form = new UPayManageForm(currentUserID);
            form.ShowDialog();
        }

        private void rezervasyonlarımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            URezerManageForm form = new URezerManageForm(currentUserID);
            form.ShowDialog();
        }

        private void anaSayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadAllListings();
            LoadPopularListings();
        }

        private void profilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            UReviewForm form = new UReviewForm(currentUserID);    
            form.ShowDialog();
        }
    }
}

