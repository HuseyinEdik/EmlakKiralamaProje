using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class HReviewsForm : Form
    {
        private int currentUserID;
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";

        public HReviewsForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
            LoadReviews();
            dataGridView4.CellClick += dataGridView4_CellClick;
        }

        private void LoadReviews()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT rv.reviewID, u.firstName + ' ' + u.lastName AS fullName, t.title AS houseTitle, rv.createdAt, rv.comment
                                 FROM reviews rv
                                 JOIN users u ON rv.userID = u.userID
                                 JOIN tinyhouses t ON rv.houseID = t.houseID
                                 WHERE t.ownerID = @OwnerID
                                 ORDER BY rv.createdAt DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@OwnerID", currentUserID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView4.DataSource = dt;

                dataGridView4.Columns["reviewID"].Visible = false; 
            }
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView4.Rows[e.RowIndex];
                textBox1.Text = row.Cells["fullName"].Value?.ToString();
                textBox2.Text = row.Cells["houseTitle"].Value?.ToString();
                textBox4.Text = row.Cells["createdAt"].Value?.ToString();
                textBox3.Text = row.Cells["comment"].Value?.ToString();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadReviews();
        }

        private void BtnAnaMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

