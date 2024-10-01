using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace User_Registration
{
    public partial class Form1 : Form
    {

        void ClearAll()
        {
            txtID.Clear();
            textBox1.Clear();
            textBox2.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.SelectedIndex = 0;
        }

        public void LoadData(DataGridViewRow row)
        {
            txtID.Text = row.Cells["id"].Value.ToString();
            textBox1.Text = row.Cells["name"].Value.ToString();
            textBox2.Text = row.Cells["phone"].Value.ToString();
            if (row.Cells["gender"].Value.ToString() == "1")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            comboBox1.SelectedItem = row.Cells["country"].Value.ToString();
        }

        private (int id, string name, long phone, int gender, object country) StoreData()
        {
            int id = Convert.ToInt32(txtID.Text);
            int gender;
            var name = textBox1.Text;
            var phone = Convert.ToInt64(textBox2.Text);
            if (radioButton1.Checked == true)
            {
                gender = 1;
            }
            else
            {
                gender = 0;
            }
            var country = comboBox1.SelectedValue;

            return (id, name, phone, gender, country);
        }

        DBcontext dbcontext;
        public Form1()
        {
            InitializeComponent();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var (id, name, phone, gender, country) = StoreData();

            try
            {
                string insertQuery = @"INSERT INTO dbo.user_1 (id, name, phone, gender, country)
                           VALUES (@id, @name, @phone, @gender, @country)";

                var parameters = new Dictionary<string, object>
    {
        {"@id", id},
        {"@name", name},
        {"@phone", phone},
        {"@gender", gender},
        {"@country", country?.ToString() ?? string.Empty}
    };

                dbcontext = new DBcontext();
                dbcontext.ExecuteSql(insertQuery, parameters); // Call with the query and parameters
                MessageBox.Show("Inserted User: " + name);
                ClearAll();
           
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> list = new List<string>() { "Select your country!!!", "Nepal", "India", "America", "Australia" };
            comboBox1.DataSource = list;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void displayDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Display display = new Display();
            display.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            var (id, name, phone, gender, country) = StoreData();

            try
            {
                string updateQuery = @"UPDATE dbo.user_1
                           SET id = @id, name = @name, phone = @phone, gender = @gender, country = @country
                           WHERE id = @id";

                var parameters = new Dictionary<string, object>
    {
        {"@id", id},
        {"@name", name},
        {"@phone", phone},
        {"@gender", gender},
        {"@country", country}
    };

                dbcontext = new DBcontext();
                dbcontext.ExecuteSql(updateQuery, parameters); // Call with the query and parameters
                MessageBox.Show("Updated User: " + name);
                ClearAll();
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
