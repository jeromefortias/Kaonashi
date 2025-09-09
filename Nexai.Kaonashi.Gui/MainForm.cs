

namespace Nexai.Kaonashi.Gui
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Nexai.Kaonashi.Core.Framework;
    using Nexai.Kaonashi.Core.Helpers;
    using Nexai.Kaonashi.Core.Models;
    using Nexai.Kaonashi.Core.Models.Corpus;
    public partial class MainForm : Form
    {
        private readonly UserControlHelp _helpControl;
        private readonly UserControlEntities _EntitiesControl;
        private SessionManager _session;
        private Config _config;


        public MainForm()
        {
            InitializeComponent();
            _helpControl = new UserControlHelp { Dock = DockStyle.Fill };
            _EntitiesControl = new UserControlEntities { Dock = DockStyle.Fill };
            Config config = ConfigMgt.GetFromFile<Config>("config.json");
            _config = config;
            _session = new SessionManager(_config);
            _session.LogSave("Application started", "Kaonashi.Gui", "Info");
        }

        private void hideSansVisageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelHeader.Visible = false;
        }

        private void viewSansVisageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowControl(_EntitiesControl);
        }

        private void butAsk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBoxAsk.Text))
            {
                if(txtBoxAsk.Text.ToLower().Trim() == "/help") ShowControl(_helpControl);
                else if(txtBoxAsk.Text.ToLower().Trim() == "/entities") ShowControl(_EntitiesControl);
                else { txtBoxAnswer.Text = "???"; }
            }
            else { };


                
        }

        private void ShowControl(UserControl control)
        {
            panel1.SuspendLayout();
            panel1.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panel1.Controls.Add(control);
            panel1.ResumeLayout();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowControl(_helpControl);
        }
    }
}
