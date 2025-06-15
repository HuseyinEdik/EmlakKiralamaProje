using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;


namespace EmlakKiralamaProje
{
    public partial class HListingManageForm : Form
    {
        private int currentUserID;
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";

        public HListingManageForm(int userID)
        {
            InitializeComponent();
            currentUserID = userID;
            numericUpDown1.Maximum = 1000000;  
            LoadListings();
            dataGridView4.CellClick += dataGridView4_CellClick;  
        }

        private void LoadListings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT houseID, title, location, pricePerNight, isActive, createdAt , imagePath FROM tinyhouses WHERE ownerID = @OwnerID";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@OwnerID", currentUserID);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView4.DataSource = dt;
            }
        }

        private void BtnAnaMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        } 

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex >= 0 && dataGridView4.Rows[e.RowIndex].Cells["houseID"].Value != null)
                {
                    DataGridViewRow row = dataGridView4.Rows[e.RowIndex];

                    textBox10.Text = row.Cells["title"].Value?.ToString() ?? "";
                    textBox8.Text = row.Cells["location"].Value?.ToString() ?? "";
                    comboBox5.SelectedItem = Convert.ToInt32(row.Cells["isActive"].Value) == 1 ? "aktif" : "pasif";

                    decimal fiyat = Convert.ToDecimal(row.Cells["pricePerNight"].Value);
                    if (fiyat >= numericUpDown1.Minimum && fiyat <= numericUpDown1.Maximum)
                    {
                        numericUpDown1.Value = fiyat;
                    }
                    else
                    {
                        numericUpDown1.Value = numericUpDown1.Minimum;
                    }


                    string imagePath = row.Cells["imagePath"].Value?.ToString();
                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        pictureBox1.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                return;
            }
            
            }
         

        private void button11_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 10000)
            {
                MessageBox.Show("Fiyat 10 000 TL'den fazla olamaz:(((");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string insertQuery = @"INSERT INTO tinyhouses 
                               (ownerID, title, location, pricePerNight, isActive, createdAt)
                               VALUES 
                               (@OwnerID, @Title, @Location, @Price, @IsActive, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@OwnerID", currentUserID);
                    cmd.Parameters.AddWithValue("@Title", textBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@Location", textBox8.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@IsActive", comboBox5.SelectedItem?.ToString().ToLower() == "aktif" ? 1 : 0);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("İlan başarıyla eklendi.");
                }

                LoadListings();
            }
        }



        private void button10_Click_1(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentRow != null)
            {
                int houseID = Convert.ToInt32(dataGridView4.CurrentRow.Cells["houseID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = @"UPDATE tinyhouses SET title = @Title, location = @Location,
                                            isActive = @IsActive, pricePerNight = @Price
                                            WHERE houseID = @HouseID AND ownerID = @OwnerID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", textBox10.Text.Trim());
                        cmd.Parameters.AddWithValue("@Location", textBox8.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsActive", comboBox5.SelectedItem?.ToString().ToLower() == "aktif" ? 1 : 0);
                        cmd.Parameters.AddWithValue("@Price", numericUpDown1.Value);
                        cmd.Parameters.AddWithValue("@HouseID", houseID);
                        cmd.Parameters.AddWithValue("@OwnerID", currentUserID);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("İlan başarıyla güncellendi.");
                    }

                    LoadListings();
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            LoadListings();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
            HNewListing   newListingForm = new HNewListing(currentUserID);      
            newListingForm.ShowDialog();    
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Bu event henüz hazır değil...");
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Bu event henüz hazır değil...");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}



