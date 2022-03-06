using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuthmeLogin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string dbsaltkodu;
        public static string usersalt;
        public static string hashlisitring;

        private void shalisifreyialma()
        {
            string connectionString22 = "datasource=;port=;username=;password=;database=;";
            string query22 = "SELECT * FROM Hesaplar WHERE username like '" + textBox1.Text + "'";

            MySqlConnection databaseConnection22 = new MySqlConnection(connectionString22);
            MySqlCommand commandDatabase22 = new MySqlCommand(query22, databaseConnection22);
            commandDatabase22.CommandTimeout = 60;
            MySqlDataReader reader22;

            try
            {
                databaseConnection22.Open();
                reader22 = commandDatabase22.ExecuteReader();

                if (reader22.HasRows)
                {
                    while (reader22.Read())
                    {
                        dbsaltkodu = reader22["password"].ToString();
                    }
                }

                databaseConnection22.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Merkezine Bağlanırken hata oluştu " + ex.Message);
            }

            usersalt = dbsaltkodu.Substring(int.Parse("5"), int.Parse("16"));

            Byte[] bytes = Encoding.UTF8.GetBytes(textBox2.Text);
            System.Security.Cryptography.SHA256Managed sha265hashstring = new System.Security.Cryptography.SHA256Managed();

            Byte[] hash = sha265hashstring.ComputeHash(bytes);
            string hashstring = string.Empty;
            foreach (byte x in hash)
            {
                hashstring += string.Format("{0:x2}", x);
            }

            hashlisitring = hashstring + usersalt;

            Byte[] bytes2 = Encoding.UTF8.GetBytes(hashlisitring);
            System.Security.Cryptography.SHA256Managed sha265hashstring2 = new System.Security.Cryptography.SHA256Managed();

            Byte[] hash2 = sha265hashstring.ComputeHash(bytes2);
            string hashstring2 = string.Empty;
            foreach (byte x in hash2)
            {
                hashstring2 += string.Format("{0:x2}", x);
            }
            string connectionString33 = "datasource=;port=;username=;password=;database=;";
            string query33 = "SELECT * FROM Hesaplar WHERE username='" + textBox1.Text + "'AND password='" + "$SHA$" + usersalt + "$" + hashstring2 + "'";

            MySqlConnection databaseConnection33 = new MySqlConnection(connectionString33);
            MySqlCommand commandDatabase33 = new MySqlCommand(query33, databaseConnection33);
            commandDatabase33.CommandTimeout = 60;
            MySqlDataReader reader33;

            try
            {
                databaseConnection33.Open();
                reader33 = commandDatabase33.ExecuteReader();

                if (reader33.HasRows)
                {
                    while (reader33.Read())
                    {
                        Form2 ans = new Form2();
                        ans.Show();
                        Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya parola hatalı", "Giriş Başarısız!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                databaseConnection33.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Merkezine Bağlanırken hata oluştu " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shalisifreyialma();
        }
    }
}
