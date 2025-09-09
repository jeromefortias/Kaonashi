using Nexai.Kaonashi.Core.Models.Corpus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nexai.Kaonashi.Gui
{
    public partial class UserControlEntities : UserControl
    {
        public UserControlEntities()
        {
            InitializeComponent();
            DataService.PopulateSampleData();
        }

        private void UserControlEntities_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData(string searchTerm = null)
        {
            var entities = DataService.EntitiesSearch(searchTerm);
            dataGrid.DataSource = entities;

            // Customize the DataGridView's appearance and columns
            dataGrid.AutoGenerateColumns = true; // Set to false if you want to manually define columns
            dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGrid.BackgroundColor = SystemColors.ControlLight;
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Hide columns that are not relevant for the main view
            // dataGrid.Columns["LongDescription"].Visible = false;
            // dataGrid.Columns["AlternativeNames"].Visible = false;
            // ...and so on for other complex properties.
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void EntitiesSearchButton_Click(object sender, EventArgs e)
        {
            LoadData(searchTextBox.Text);
        }

        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedEntity = dataGrid.Rows[e.RowIndex].DataBoundItem as Entity;
                if (selectedEntity != null)
                {
                    var editForm = new EntityEditForm(selectedEntity);
                    editForm.Show();
                    
                }
            }
        }
    }
}
