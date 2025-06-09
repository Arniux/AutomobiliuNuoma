using System;
using System.Windows.Forms;
using System.Data.SQLite;

namespace AutomobiliuNuoma
{
    public partial class ManagerAddVehicleForm : Form
    {
        string dbPath = @"C:\Users\P-Mac\Desktop\SMK\Objektinis programavimas\Duomenu bazes\nuoma.db";
        string connectionString;

        public ManagerAddVehicleForm()
        {
            InitializeComponent();
            connectionString = $"Data Source={dbPath}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string marke = textBox1.Text;
            string modelis = textBox2.Text;
            string vin = textBox3.Text;
            string numeriai = textBox4.Text;

 
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO automobiliai (marke, modelis, vin, numeriai) VALUES (@marke, @modelis, @vin, @numeriai)";
                        cmd.Parameters.AddWithValue("@marke", marke);
                        cmd.Parameters.AddWithValue("@modelis", modelis);
                        cmd.Parameters.AddWithValue("@vin", vin);
                        cmd.Parameters.AddWithValue("@numeriai", numeriai);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Irašyta sėkmingai!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Klaida: " + ex.Message);
            }
        }
    }
}
