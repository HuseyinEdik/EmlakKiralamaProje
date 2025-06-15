using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class AUserManageForm : Form
    {
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";
        int selectedUserId = -1;

        public AUserManageForm()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT userID, firstName, lastName, email, password, roleID, isActive FROM users", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
        }

        private void ClearInputs()
        {
            textBox2.Text = textBox4.Text = textBox3.Text = textBox5.Text = "";
            comboBox1.SelectedIndex = comboBox2.SelectedIndex = -1;
            selectedUserId = -1;
        }

         
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                selectedUserId = Convert.ToInt32(row.Cells["userID"].Value);
                textBox2.Text = row.Cells["firstName"].Value.ToString();
                textBox4.Text = row.Cells["lastName"].Value.ToString();
                textBox3.Text = row.Cells["email"].Value.ToString();
                textBox5.Text = row.Cells["password"].Value.ToString();
                comboBox1.SelectedIndex = Convert.ToInt32(row.Cells["roleID"].Value) - 1;
                comboBox2.Text = (bool)row.Cells["isActive"].Value ? "aktif" : "pasif";
             
        }

        }

        private void button1_Click(object sender, EventArgs e) 
        {
            string search = textBox1.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM users WHERE email LIKE @ara", conn);
                da.SelectCommand.Parameters.AddWithValue("@ara", "%" + search + "%");
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
        }

        private void BtnAnaMenu_Click(object sender, EventArgs e)
        {
            
            AdminMainPage adminMain = new AdminMainPage();
            adminMain.Show();

            this.Hide();

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            LoadUsers();
            ClearInputs();
 
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO users (firstName, lastName, email, password, roleID, isActive ) VALUES (@ad, @soyad, @email, @sifre, @rol, @durum )", conn);
                cmd.Parameters.AddWithValue("@ad", textBox2.Text);
                cmd.Parameters.AddWithValue("@soyad", textBox4.Text);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                cmd.Parameters.AddWithValue("@sifre", textBox5.Text);
                cmd.Parameters.AddWithValue("@rol", comboBox1.SelectedIndex + 1);
                cmd.Parameters.AddWithValue("@durum", comboBox2.Text == "aktif" ? 1 : 0);
                cmd.ExecuteNonQuery();
            }
            LoadUsers();
            ClearInputs();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (selectedUserId == -1)
            {
                MessageBox.Show("Lütfen bir kullanıcı seçin.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE userID=@id", conn);
                cmd.Parameters.AddWithValue("@id", selectedUserId);
                cmd.ExecuteNonQuery();
            }
            LoadUsers();
            ClearInputs();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            if (selectedUserId == -1)
            {
                MessageBox.Show("Lütfen bir kullanıcı seçin.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE users SET firstName=@ad, lastName=@soyad, email=@email, password=@sifre, roleID=@rol, isActive=@durum WHERE userID=@id", conn);
                cmd.Parameters.AddWithValue("@ad", textBox2.Text);
                cmd.Parameters.AddWithValue("@soyad", textBox4.Text);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);
                cmd.Parameters.AddWithValue("@sifre", textBox5.Text);
                cmd.Parameters.AddWithValue("@rol", comboBox1.SelectedIndex + 1);
                cmd.Parameters.AddWithValue("@durum", comboBox2.Text == "aktif" ? 1 : 0);
                cmd.Parameters.AddWithValue("@id", selectedUserId);
                cmd.ExecuteNonQuery();
            }
            LoadUsers();
            ClearInputs();
        }
    }
}

