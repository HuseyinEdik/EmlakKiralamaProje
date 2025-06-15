using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class ARezerManageForm : Form
    {
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";
        int selectedReservationId = -1;

        public ARezerManageForm()
        {
            InitializeComponent();
            LoadReservations();
            dataGridView3.CellClick += dataGridView3_CellClick;
        }

        private void LoadReservations()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT r.reservationID, u.firstName + ' ' + u.lastName AS userName, h.title AS listingTitle, r.startDate, r.endDate, r.status, r.createdAt FROM reservations r JOIN users u ON r.userID = u.userID JOIN tinyhouses h ON r.houseID = h.houseID", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView3.DataSource = dt;
            }
        }


        private void ClearInputs()
        {
            textBox9.Text = textBox7.Text = textBox1.Text = "";
            comboBox3.SelectedIndex = comboBox4.SelectedIndex = -1;
            selectedReservationId = -1;
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];
                selectedReservationId = Convert.ToInt32(row.Cells["reservationID"].Value);
                textBox9.Text = row.Cells["userName"].Value.ToString();
                textBox1.Text = row.Cells["listingTitle"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["startDate"].Value);
                dateTimePicker2.Value = Convert.ToDateTime(row.Cells["endDate"].Value);
                comboBox3.Text = row.Cells["status"].Value.ToString();
                textBox7.Text = row.Cells["createdAt"].Value.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e) // Yenile
        {
            LoadReservations();
            ClearInputs();
        }
 
        private void button9_Click(object sender, EventArgs e)  
        {
            if (selectedReservationId == -1)
            {
                MessageBox.Show("Lütfen bir rezervasyon seçin.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE reservations SET status='iptal edildi' WHERE reservationID=@id", conn);
                cmd.Parameters.AddWithValue("@id", selectedReservationId);
                cmd.ExecuteNonQuery();
            }

            LoadReservations();
            ClearInputs();
        }

        private void BtnAnaMenu_Click(object sender, EventArgs e)
        {
            AdminMainPage adminMain = new AdminMainPage();
            adminMain.Show();
            this.Hide();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (selectedReservationId == -1)
            {
                MessageBox.Show("Lütfen bir rezervasyon seçin.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE reservations SET status=@status WHERE reservationID=@id", conn);
                cmd.Parameters.AddWithValue("@status", comboBox3.Text);
                cmd.Parameters.AddWithValue("@id", selectedReservationId);
                cmd.ExecuteNonQuery();
            }

            LoadReservations();
            ClearInputs();
        }
    }
}

