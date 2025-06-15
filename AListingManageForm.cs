using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace EmlakKiralamaProje
{
    public partial class AListingManageForm : Form
    {
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";
        int selectedHouseId = -1;

        public AListingManageForm()
        {
            InitializeComponent();

            numericUpDown1.Maximum = 100000;
            numericUpDown1.Minimum = 0;

            LoadListings();
            dataGridView4.CellClick += dataGridView4_CellClick;
            button10.Click += button10_Click;
            button11.Click += button11_Click;
            button6.Click += button6_Click;
        }

        private void LoadListings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT houseID, title, ownerID, location, isActive, pricePerNight, imagePath FROM tinyhouses", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView4.DataSource = dt;
            }
        }

        private void ClearInputs()
        {
            textBox10.Text = textBox6.Text = textBox8.Text = "";
            comboBox5.SelectedIndex = -1;
            numericUpDown1.Value = 0;
            pictureBox1.Image = null;
            selectedHouseId = -1;
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView4.Rows[e.RowIndex];
                selectedHouseId = Convert.ToInt32(row.Cells["houseID"].Value);
                textBox10.Text = row.Cells["title"].Value.ToString();
                textBox6.Text = row.Cells["ownerID"].Value.ToString();
                textBox8.Text = row.Cells["location"].Value.ToString();
                comboBox5.Text = Convert.ToBoolean(row.Cells["isActive"].Value) ? "aktif" : "pasif";

                if (row.Cells["pricePerNight"].Value != DBNull.Value)
                    numericUpDown1.Value = Convert.ToDecimal(row.Cells["pricePerNight"].Value);

                // Görsel yükleme işlemi
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

        private void BtnAnaMenu_Click(object sender, EventArgs e)
        {
            AdminMainPage adminMain = new AdminMainPage();
            adminMain.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (selectedHouseId == -1)
            {
                MessageBox.Show("Lütfen bir ilan seçin.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM tinyhouses WHERE houseID=@id", conn);
                cmd.Parameters.AddWithValue("@id", selectedHouseId);
                cmd.ExecuteNonQuery();
            }

            LoadListings();
            ClearInputs();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (selectedHouseId == -1)
            {
                MessageBox.Show("Lütfen bir ilan seçin.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE tinyhouses SET title=@title, location=@location, isActive=@active, pricePerNight=@price WHERE houseID=@id", conn);
                cmd.Parameters.AddWithValue("@title", textBox10.Text);
                cmd.Parameters.AddWithValue("@location", textBox8.Text);
                cmd.Parameters.AddWithValue("@active", comboBox5.Text == "aktif" ? 1 : 0);
                cmd.Parameters.AddWithValue("@price", numericUpDown1.Value);
                cmd.Parameters.AddWithValue("@id", selectedHouseId);
                cmd.ExecuteNonQuery();
            }

            LoadListings();
            ClearInputs();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadListings();
            ClearInputs();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}


