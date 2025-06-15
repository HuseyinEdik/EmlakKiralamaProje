using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class HRezerManageForm : Form
    {
        private int currentUserID;
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";

        public HRezerManageForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
            LoadReservations();
            dataGridView3.CellClick += dataGridView3_CellClick;
        }
        
        private void UpdateReservationStatus(string newStatus)
        {
            if (dataGridView3.CurrentRow != null)
            {
                int reservationID = Convert.ToInt32(dataGridView3.CurrentRow.Cells["reservationID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE reservations SET status = @Status WHERE reservationID = @ResID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Status", newStatus);
                        cmd.Parameters.AddWithValue("@ResID", reservationID);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Rezervasyon durumu güncellendi: " + newStatus);
                    LoadReservations();
                }
            }
            else { MessageBox.Show("Lütfen güncellemek istediğiniz rezervasyonu seçin."); return; } 
        }




        private void LoadReservations()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT r.reservationID, u.firstName + ' ' + u.lastName AS fullName, t.title, 
                           r.startDate, r.endDate, p.status AS paymentStatus, r.status AS reservationStatus
                    FROM reservations r
                    JOIN users u ON r.userID = u.userID
                    JOIN tinyhouses t ON r.houseID = t.houseID
                    left JOIN payments p ON r.reservationID = p.reservationID
                    WHERE t.ownerID = @OwnerID
                    ORDER BY r.createdAt DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@OwnerID", currentUserID);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView3.AutoGenerateColumns = true;

                dataGridView3.DataSource = dt;
            }
        }

       
            private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];

                textBox9.Text = row.Cells["fullName"].Value?.ToString() ?? "";
                textBox1.Text = row.Cells["title"].Value?.ToString() ?? "";

                if (row.Cells["startDate"].Value != DBNull.Value)
                    startDate.Value = Convert.ToDateTime(row.Cells["startDate"].Value);

                if (row.Cells["endDate"].Value != DBNull.Value)
                    endDate.Value = Convert.ToDateTime(row.Cells["endDate"].Value);

                textBox2.Text = row.Cells["paymentStatus"].Value?.ToString() ?? "Yok";
                textBox3.Text = row.Cells["reservationStatus"].Value?.ToString() ?? "Yok";
            }
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            UpdateReservationStatus("onaylandi");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            UpdateReservationStatus("reddedildi");
        }


        private void button7_Click_1(object sender, EventArgs e)
        {
            LoadReservations();
        }

        private void BtnAnaMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
