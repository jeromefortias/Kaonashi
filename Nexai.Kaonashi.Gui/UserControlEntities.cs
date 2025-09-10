using Nexai.Kaonashi.Core.Framework;
using Nexai.Kaonashi.Core.Helpers;
using Nexai.Kaonashi.Core.Models;
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
        private SessionManager _session;
        private Config _config;
        private List<Entity> _entities = new List<Entity>();

        public UserControlEntities()
        {
            InitializeComponent();
            Config config = ConfigMgt.GetFromFile<Config>("config.json");
            _config = config;
            _session = new SessionManager(_config);
          
        }

        private void UserControlEntities_Load(object sender, EventArgs e)
        {
            LoadData(null);
        }

        private void LoadData(string searchTerm )
        {
          
            try
            {
                _entities = _session.EntitySearchByName(searchTerm);
                dataGrid.DataSource = _entities;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error loading entities: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _session.LogSave($"Error loading entities: {ex.Message}", "Kaonashi.Gui", "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var editForm = new EntityEditForm(null);
            editForm.Show();
        }

        private void EntitiesSearchButton_Click(object sender, EventArgs e)
        {
            LoadData(searchTextBox.Text);
        }

        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
         
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
