using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class URezerManageForm : Form
    {
        
        private int currentUserID;
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";

        public URezerManageForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
            LoadReservations();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void LoadReservations()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT r.reservationID, t.title AS houseTitle, r.startDate, r.endDate, r.status
                    FROM reservations r
                    JOIN tinyhouses t ON r.houseID = t.houseID
                    WHERE r.userID = @UserID
                    ORDER BY r.createdAt DESC";

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
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["startDate"].Value);
                dateTimePicker2.Value = Convert.ToDateTime(row.Cells["endDate"].Value);
                textBox2.Text = row.Cells["status"].Value?.ToString();
                dataGridView1.Columns["reservationID"].Visible = false; 


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int reservationID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["reservationID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE reservations SET status = 'iptal edildi' WHERE reservationID = @ResID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ResID", reservationID);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Rezervasyon iptal edildi.");
                    LoadReservations();
                }
            }
        }
            
        private void button2_Click(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();  
           UserMainPage anasayfa = new UserMainPage(currentUserID); 
                anasayfa.Show();    
        }
    }
}
