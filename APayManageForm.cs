using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class APayManageForm : Form
    {
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";
        int selectedPaymentId = -1;

        public APayManageForm()
        {
            InitializeComponent();
            numericUpDown2.Maximum = 100000;
            LoadPayments();
            dataGridView5.CellClick += dataGridView5_CellClick;
            button13.Click += button13_Click;  
            button12.Click += button12_Click; 
        }

        private void LoadPayments()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT 
                        p.paymentID, 
                        u.firstName + ' ' + u.lastName AS userName, 
                        h.title AS houseTitle, 
                        p.amount, 
                        p.paymentDate, 
                        p.status 
                    FROM payments p
                    JOIN reservations r ON p.reservationID = r.reservationID
                    JOIN users u ON r.userID = u.userID
                    JOIN tinyhouses h ON r.houseID = h.houseID", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;
            }
        }

        private void ClearInputs()
        {
            textBox11.Text = textBox12.Text = "";
            numericUpDown2.Value = 0;
            comboBox6.SelectedIndex = -1;
            selectedPaymentId = -1;
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView5.Rows[e.RowIndex];
                selectedPaymentId = Convert.ToInt32(row.Cells["paymentID"].Value);
                textBox11.Text = row.Cells["userName"].Value.ToString();
                textBox12.Text = row.Cells["houseTitle"].Value.ToString();
                numericUpDown2.Value = Convert.ToDecimal(row.Cells["amount"].Value);
                dateTimePicker5.Value = Convert.ToDateTime(row.Cells["paymentDate"].Value);
                comboBox6.Text = row.Cells["status"].Value.ToString();
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (selectedPaymentId == -1)
            {
                MessageBox.Show("Lütfen bir ödeme seçin.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE payments SET status=@status WHERE paymentID=@id", conn);
                cmd.Parameters.AddWithValue("@status", comboBox6.Text);
                cmd.Parameters.AddWithValue("@id", selectedPaymentId);
                cmd.ExecuteNonQuery();
            }

            LoadPayments();
            ClearInputs();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            LoadPayments();
            ClearInputs();
        }

        private void BtnAnaMenu_Click(object sender, EventArgs e)
        {
            AdminMainPage adminMain = new AdminMainPage();
            adminMain.Show();
            this.Hide();
        }
    }
}

