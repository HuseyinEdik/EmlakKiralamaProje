using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EmlakKiralamaProje
{
    public partial class UReviewForm : Form
    {
        private int currentUserID;
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";

        public UReviewForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
            LoadReviews();
            LoadReservations();
            dataGridViewReservations.CellClick += dataGridView2_CellClick;
        }

        private void LoadReviews()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                 string query = @"SELECT rv.reviewID, t.title, rv.comment, rv.createdAt
                                  FROM reviews rv
                                  JOIN tinyhouses t ON rv.houseID = t.houseID
                                  ORDER BY rv.createdAt DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@UserID", currentUserID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewComments.DataSource = dt;

                dataGridViewComments.Columns["reviewID"].Visible = false;
                dataGridViewComments.Columns["createdAt"].Visible = false;
            }
        }

        private void LoadReservations()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT r.reservationID, t.houseID, t.title, t.location 
                         FROM reservations r
                         JOIN tinyhouses t ON r.houseID = t.houseID
                         WHERE r.userID = @UserID AND r.status = 'kabul edildi'";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@UserID", currentUserID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewReservations.DataSource = dt;

                dataGridViewReservations.Columns["reservationID"].Visible = true;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewReservations.Rows[e.RowIndex];
                textBoxTitle.Text = row.Cells["title"].Value?.ToString();
                textBoxLocation.Text = row.Cells["location"].Value?.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridViewReservations.CurrentRow != null)
            {
                int reservationID = Convert.ToInt32(dataGridViewReservations.CurrentRow.Cells["reservationID"].Value);
                string comment = textBoxComment.Text.Trim();

                if (string.IsNullOrWhiteSpace(comment))
                {
                    MessageBox.Show("Yorum alanı boş bırakılamaz.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string insertQuery = @"INSERT INTO reviews (houseID, userID,  comment, createdAt)
                       VALUES (@HouseID, @UserID,   @Comment, GETDATE())";

                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    int houseID = Convert.ToInt32(dataGridViewReservations.CurrentRow.Cells["houseID"].Value);

                    cmd.Parameters.AddWithValue("@HouseID", houseID);
                    cmd.Parameters.AddWithValue("@UserID", currentUserID);
                 
                    cmd.Parameters.AddWithValue("@Comment", comment);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Yorum başarıyla eklendi.");
                LoadReviews();
                textBoxComment.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            this.Close();
            UserMainPage mainPage = new UserMainPage(currentUserID);        
                mainPage.Show();
        }

        private void dataGridViewReservations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadReservations();
        }
    }
}

