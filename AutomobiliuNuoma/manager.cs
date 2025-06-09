using System;
using System.Windows.Forms;
using System.Data.SQLite;

// Arnas Dimšlys PRM24SONL

// Programa buvo sukurta su C# GUI ir SQLlite 
// Programaje veikia VADYBININKO funkcijos,
// galima pridėti automobilį, ir peržiurėti, tai pat ieškoti duomenų bazėje


namespace AutomobiliuNuoma
{
    public partial class manager : Form
    {

        string dbPath = @"C:\Users\P-Mac\Desktop\SMK\Objektinis programavimas\Duomenu bazes\nuoma.db";
        string connectionString;


        public manager()
        {
            InitializeComponent();
            connectionString = $"Data Source={dbPath}";
        }


        private void LoadAutomobiliai()
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT marke, modelis, vin, numeriai FROM automobiliai";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        string result = "";

                        while (reader.Read())
                        {
                            string marke = reader["marke"].ToString();
                            string modelis = reader["modelis"].ToString();
                            string vin = reader["vin"].ToString();
                            string numeriai = reader["numeriai"].ToString();

                            result += $"Marke: {marke},Modelis: {modelis}, VIN: {vin}, Numeriai: {numeriai}\n";
                        }

                        label3.Text = result;


                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                        System.Data.DataTable table = new System.Data.DataTable();
                        adapter.Fill(table);

                        dataGridView1.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Klaida: " + ex.Message);
            }
        }

        private void LoadSearchAutomobiliai()
        {
            string searchText = textBox1.Text.Trim(); 

            string query = "SELECT marke, modelis, vin, numeriai FROM automobiliai " +
                "WHERE numeriai LIKE @search OR modelis LIKE @search OR marke LIKE @search " +
                "OR vin like @search;";

            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchText + "%");


                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                        System.Data.DataTable table = new System.Data.DataTable();
                        adapter.Fill(table);

                        dataGridView1.DataSource = table;
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Klaida: " + ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManagerAddVehicleForm form2 = new ManagerAddVehicleForm(); // Rodyti automobiliu pridėjimo forma
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e) // Rodyti visus automobilius
        {
            LoadAutomobiliai();
        }

        private void button3_Click(object sender, EventArgs e) // Ieskotjimo mygtukas
        {
            LoadSearchAutomobiliai();
        }
    }
}
