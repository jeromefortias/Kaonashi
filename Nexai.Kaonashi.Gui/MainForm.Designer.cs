﻿namespace Nexai.Kaonashi.Gui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            filesToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            windowsToolStripMenuItem = new ToolStripMenuItem();
            viewSansVisageToolStripMenuItem = new ToolStripMenuItem();
            hideSansVisageToolStripMenuItem = new ToolStripMenuItem();
            panelHeader = new Panel();
            txtBoxAnswer = new TextBox();
            butAsk = new Button();
            txtBoxAsk = new TextBox();
            pictureBox1 = new PictureBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            panel1 = new Panel();
            helpToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { filesToolStripMenuItem, editToolStripMenuItem, toolsToolStripMenuItem, windowsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1904, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // filesToolStripMenuItem
            // 
            filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            filesToolStripMenuItem.Size = new Size(42, 20);
            filesToolStripMenuItem.Text = "&Files";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(47, 20);
            toolsToolStripMenuItem.Text = "&Tools";
            // 
            // windowsToolStripMenuItem
            // 
            windowsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewSansVisageToolStripMenuItem, hideSansVisageToolStripMenuItem });
            windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            windowsToolStripMenuItem.Size = new Size(68, 20);
            windowsToolStripMenuItem.Text = "&Windows";
            // 
            // viewSansVisageToolStripMenuItem
            // 
            viewSansVisageToolStripMenuItem.Name = "viewSansVisageToolStripMenuItem";
            viewSansVisageToolStripMenuItem.Size = new Size(180, 22);
            viewSansVisageToolStripMenuItem.Text = "Entities mgt";
            viewSansVisageToolStripMenuItem.Click += viewSansVisageToolStripMenuItem_Click;
            // 
            // hideSansVisageToolStripMenuItem
            // 
            hideSansVisageToolStripMenuItem.Name = "hideSansVisageToolStripMenuItem";
            hideSansVisageToolStripMenuItem.Size = new Size(180, 22);
            hideSansVisageToolStripMenuItem.Text = "Hide Sans Visage";
            hideSansVisageToolStripMenuItem.Click += hideSansVisageToolStripMenuItem_Click;
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(txtBoxAnswer);
            panelHeader.Controls.Add(butAsk);
            panelHeader.Controls.Add(txtBoxAsk);
            panelHeader.Controls.Add(pictureBox1);
            panelHeader.Location = new Point(0, 27);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1904, 251);
            panelHeader.TabIndex = 1;
            // 
            // txtBoxAnswer
            // 
            txtBoxAnswer.BorderStyle = BorderStyle.FixedSingle;
            txtBoxAnswer.Location = new Point(586, 16);
            txtBoxAnswer.Multiline = true;
            txtBoxAnswer.Name = "txtBoxAnswer";
            txtBoxAnswer.Size = new Size(1306, 142);
            txtBoxAnswer.TabIndex = 3;
            // 
            // butAsk
            // 
            butAsk.Location = new Point(1417, 185);
            butAsk.Name = "butAsk";
            butAsk.Size = new Size(133, 49);
            butAsk.TabIndex = 2;
            butAsk.Text = "Ask";
            butAsk.UseVisualStyleBackColor = true;
            butAsk.Click += butAsk_Click;
            // 
            // txtBoxAsk
            // 
            txtBoxAsk.BorderStyle = BorderStyle.None;
            txtBoxAsk.Location = new Point(236, 185);
            txtBoxAsk.Multiline = true;
            txtBoxAsk.Name = "txtBoxAsk";
            txtBoxAsk.Size = new Size(1175, 49);
            txtBoxAsk.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 16);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(206, 218);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 1019);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1904, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(70, 17);
            toolStripStatusLabel1.Text = "Loaded well";
            // 
            // panel1
            // 
            panel1.Location = new Point(0, 284);
            panel1.Name = "panel1";
            panel1.Size = new Size(1904, 732);
            panel1.TabIndex = 3;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Controls.Add(panelHeader);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(1920, 1080);
            Name = "MainForm";
            Text = "Nexai - GUI - Version Kaonashi";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem filesToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem windowsToolStripMenuItem;
        private ToolStripMenuItem viewSansVisageToolStripMenuItem;
        private ToolStripMenuItem hideSansVisageToolStripMenuItem;
        private Panel panelHeader;
        private PictureBox pictureBox1;
        private TextBox txtBoxAsk;
        private Button butAsk;
        private TextBox txtBoxAnswer;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel panel1;
        private ToolStripMenuItem helpToolStripMenuItem;
    }
}