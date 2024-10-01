using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_Registration
{
    public partial class Display : Form
    {
        private void RefreshDataGrid()
        {
            dbcontext = new DBcontext();
            string query = @"SELECT * FROM dbo.user_1";
            var result = dbcontext.getAll(query);
            dataGridView1.DataSource = result.Tables[0];
        }


        DBcontext dbcontext;
        public Display()
        {
            InitializeComponent();
        }

        private void Display_Load(object sender, EventArgs e)
        {
            dbcontext = new DBcontext();
            string query = @"Select * From dbo.user_1";
            var result = dbcontext.getAll(query);

            dataGridView1.DataSource = result.Tables[0];



        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                Form1 editForm = new Form1();
                editForm.LoadData(selectedRow);
                this.Close();
                editForm.ShowDialog();

                

                // Refresh the DataGridView after editing
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("Please select a row to edit.");
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            // Close the form
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Get the ID of the selected user
                int userId = Convert.ToInt32(selectedRow.Cells["id"].Value);

                // Confirmation before deletion
                var confirmResult = MessageBox.Show("Are you sure you want to delete this user?",
                                                     "Confirm Delete",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        // Create the delete query
                        string query = @"DELETE FROM dbo.user_1 WHERE id = @id";

                        // Define parameters for the query
                        var parameters = new Dictionary<string, object>
                {
                    {"@id", userId}
                };

                        // Execute the delete command
                        dbcontext = new DBcontext();
                        dbcontext.ExecuteSql(query, parameters);

                        // Refresh the DataGridView to reflect the changes
                        RefreshDataGrid();

                        // Notify the user
                        MessageBox.Show("User deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting user: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

    }
}

