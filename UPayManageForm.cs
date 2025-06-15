using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class UPayManageForm : Form
    {
        private int currentUserID;
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";

        public UPayManageForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
            LoadPayments();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void LoadPayments()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT p.paymentID, t.title AS houseTitle, p.amount, p.paymentDate, p.status
                    FROM payments p
                    JOIN reservations r ON p.reservationID =  r.reservationID
                    JOIN tinyhouses t ON r.houseID = t.houseID
                     WHERE r.userID = @UserID 
                    ORDER BY p.paymentDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@UserID", currentUserID);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["houseTitle"].Value?.ToString();
                textBox2.Text = row.Cells["amount"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["paymentDate"].Value?.ToString(), out DateTime paymentDate))
                {
                    dateTimePicker2.Value = paymentDate;
                }

                textBox3.Text = row.Cells["status"].Value?.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
             
            if (dataGridView1.CurrentRow != null)
            {
                int paymentID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["paymentID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE payments SET status = 'odendi' WHERE paymentID = @PayID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PayID", paymentID);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Ödeme başarıyla yapıldı.");
                    LoadPayments();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadPayments();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
