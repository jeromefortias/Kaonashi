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
using static iText.IO.Util.IntHashtable;

namespace Nexai.Kaonashi.Gui
{
    public partial class EntityEditForm : Form
    {
        private Entity _entity;
        public EntityEditForm(Entity entity)
        {
            _entity = entity;
            InitializeComponent();
            PopulateFormCOnstrols();

        }

        private void PopulateFormCOnstrols()
        {
            Entity entity = _entity;
            // This method can be used to populate dropdowns or other controls if needed.
            if (entity != null)
            {
                txtBoxEntityName.Text = entity.Name;
                textBoxOtherNames.Text = string.Join(", ", entity.AlternativeNames ?? new List<string>());
                comboBoxEntityType.Text = entity.Type;
                textBoxShortDescription.Text = entity.ShortDescription;
                textBoxFullDescription.Text = entity.LongDescription;
                textBoxRelatedEntities.Text = string.Join(", ", entity.RelatedEntities ?? new List<string>());
                trackBarJoy.Value = entity.Joy;
                trackBarFear.Value = entity.Fear;
                trackBarAnger.Value = entity.Anger;
                trackBarSadness.Value = entity.Sadness;
                trackBarDisgust.Value = entity.Disgust;
                dateTimeFrom.Value = entity.DateFrom ?? DateTime.Now;
                dateTimeTo.Value = entity.DateTo ?? DateTime.Now.AddYears(1000);
                textBoxHowTo.Text = entity.HowTo;
                textBoxWithWhat.Text = entity.WithWhat;
                textBoxWithout.Text = entity.WithoutWhat;
                textBoxWhere.Text = entity.Where;
                textBoxWhen.Text = entity.When;
            }
        }

        private void EntityEditForm_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Update the entity object with data from the controls
            _entity.Name = txtBoxEntityName.Text;
            _entity.AlternativeNames = textBoxOtherNames.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(n => n.Trim()).ToList();
            _entity.Type = comboBoxEntityType.Text;
            _entity.ShortDescription = textBoxShortDescription.Text;
            _entity.LongDescription = textBoxFullDescription.Text;
            _entity.RelatedEntities = textBoxRelatedEntities.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(n => n.Trim()).ToList();
            _entity.Joy = trackBarJoy.Value;
            _entity.Fear = trackBarFear.Value;
            _entity.Anger = trackBarAnger.Value;
            _entity.Sadness = trackBarSadness.Value;
            _entity.Disgust = trackBarDisgust.Value;
            _entity.DateFrom = dateTimeFrom.Value;
            _entity.DateTo = dateTimeTo.Value;
            _entity.HowTo = textBoxHowTo.Text;
            _entity.WithWhat = textBoxWithWhat.Text;
            _entity.WithoutWhat = textBoxWithout.Text;
            _entity.Where = textBoxWhere.Text;
            _entity.When = textBoxWhen.Text;
            _entity.Date = DateTime.Now;
            
     

            DataService.EntityUpdate(_entity);
    this.DialogResult = DialogResult.OK;
    this.Close();


            // ...and so on

            DataService.EntityUpdate(_entity);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
