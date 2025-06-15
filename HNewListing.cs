using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmlakKiralamaProje
 
{ 

    public partial class HNewListing : Form
    {

        private string selectedImagePath = "";
        private string imageSaveFolder = @"C:\Users\Hüsocell\Desktop\images.emlakkiralama\";  

        private int currentUserID;

        public HNewListing(int userID)
        {
            currentUserID = userID; 
            InitializeComponent();

            numericUpDown1.Maximum = 10000;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            HListingManageForm form = new HListingManageForm( currentUserID);    
                form.Show();    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox10.Text) || string.IsNullOrWhiteSpace(textBox8.Text) || string.IsNullOrWhiteSpace(selectedImagePath))
            {
                MessageBox.Show("Lütfen tüm alanları ve resmi doldurun.");
                return;
            }

            using (SqlConnection conn = new SqlConnection("Server=.;Database=tinnyHouse;Integrated Security=True;"))
            {
                conn.Open();
                string query = @"INSERT INTO tinyhouses 
                               (ownerID, title, location, pricePerNight, isActive, createdAt, imagePath)
                                  VALUES 
                               (@OwnerID, @Title, @Location, @Price, @IsActive, GETDATE(), @ImagePath)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OwnerID", currentUserID);
                    cmd.Parameters.AddWithValue("@Title", textBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@Location", textBox8.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@IsActive", comboBox5.SelectedItem?.ToString().ToLower() == "aktif" ? 1 : 0);
                    cmd.Parameters.AddWithValue("@ImagePath", selectedImagePath);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("İlan başarıyla eklendi.");
                this.Close();  
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.GetFileName(ofd.FileName);
                string targetPath = Path.Combine(imageSaveFolder, fileName);

                try
                {
                    if (!Directory.Exists(imageSaveFolder))
                        Directory.CreateDirectory(imageSaveFolder);

                    if (!File.Exists(targetPath))
                        File.Copy(ofd.FileName, targetPath);

                    pictureBox1.Image = Image.FromFile(targetPath);
                    selectedImagePath = targetPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Görsel yükleme hatası: " + ex.Message);
                }
            }
        }

    }
}
 
