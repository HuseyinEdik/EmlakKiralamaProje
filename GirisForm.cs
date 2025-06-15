  
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmlakKiralamaProje
{
    public partial class GirisForm : Form
    {
        string connectionString = "Server=.;Database=tinnyHouse;Integrated Security=True;";

        public GirisForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            
            string email = txtbxE_Mail.Text.Trim();
            string password = txtbxPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen e-posta ve şifre giriniz.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
            SELECT u.userID, u.firstName, r.roleName
            FROM users u
            JOIN roles r ON u.roleID = r.roleID
            WHERE u.email = @Email AND u.password = @Password";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string role = reader["roleName"].ToString();
                    int userID = Convert.ToInt32(reader["userID"]);


                    if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        this.Hide();
                        AdminMainPage admin = new AdminMainPage();
                        admin.Show();
                    }
                    else if (role.Equals("Kiracı", StringComparison.OrdinalIgnoreCase))
                    {
                        this.Hide();
                        UserMainPage kiraci = new UserMainPage(userID);
                        kiraci.Show();
                    }
                    else if (role.Equals("Ev Sahibi", StringComparison.OrdinalIgnoreCase))
                    {
                        HostMainPage evsahibi = new HostMainPage(userID);
                        this.Hide();
                        evsahibi.Show();
                    }
                     
                    else
                    {
                        MessageBox.Show("Bu rol için ekran henüz hazır değil.");
                        this.Show(); 
                    }
                }
                else
                {
                    MessageBox.Show("E-posta veya şifre hatalı.");
                }
            }
        }


         
        private void txtbxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtbxE_Mail_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {



             
            string role = comboBox1.SelectedItem?.ToString();
            string ad =   textBox3.Text.Trim();
            string soyad = textBox4.Text.Trim();
            string email = textBox1.Text.Trim();
            string sifre = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun ! ! ! ");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                int roleID = 0;

                string getRoleIDQuery = "SELECT roleID FROM roles WHERE roleName = @RoleName";

                using (SqlCommand cmd = new SqlCommand(getRoleIDQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@RoleName", role);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        roleID = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz bir rol seçtiniz!");
                        return;
                    }
                }

                string insertQuery = @"INSERT INTO users (roleID,  firstName, lastName, email, password, isActive, createtime)
                               VALUES (@RoleID,  @FirstName, @LastName, @Email, @Password, 1, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@RoleID", roleID);
                    cmd.Parameters.AddWithValue("@FirstName", ad);
                    cmd.Parameters.AddWithValue("@LastName", soyad);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", sifre);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla oluşturuldu.");

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();  
                }
            }
        }

    }

}
 



