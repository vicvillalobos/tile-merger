﻿namespace ImageMerger
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    public class MainForm : Form
    {
        private Button buttonProcessImages;
        private Button buttonQuit;
        private Button buttonSetSource;
        private Button buttonSetTarget;
        private CheckBox checkboxRememberSettings;
        private IContainer components;
        private readonly FolderBrowserDialog dialogFindFolder;
        private readonly SaveFileDialog dialogSetTarget;
        private Label label1;
        private Label label2;
        private Label labelNumberDirection;
        private Label label4;
        private Label labelInformation;
        private Label labelProcess;
        private NumericUpDown numericUpDownColumns;
        private TextBox textboxFilter;
        private TextBox textboxSource;
        private TextBox textboxTarget;
        private RadioButton radioButtonHorizontal;
        private RadioButton radioButtonVertical;
        private GroupBox groupBoxTilingDirection;
        private TextBox paddingTopTB;
        private Label label3;
        private Label label5;
        private TextBox paddingBotTB;
        private Label label6;
        private TextBox paddingLeftTB;
        private Label label7;
        private TextBox paddingRightTB;
        private ToolTip toolTipController;

        public MainForm()
        {
            this.InitializeComponent();
            this.dialogFindFolder = new FolderBrowserDialog();
            this.dialogSetTarget = new SaveFileDialog();
            this.labelInformation.Text = "Image Merger created by John Beech. Merges a folder full of image files and saves them into a single image. Number of columns specifies how many images to put on each row before starting a new one.";
            this.ReadRegistryKey(@"Software\mkv25\Tile Merger\");
            this.UpdateFilecountMessage();
        }

        private void ButtonProcessImages_Click(object sender, EventArgs e)
        {
            this.labelInformation.Text = "Processing...";
            TileMerger merger = new TileMerger();
            string text = this.textboxSource.Text;
            if (text == "Not set")
            {
                text = "";
            }
            string fileTarget = this.textboxTarget.Text;
            if (fileTarget == "Not set")
            {
                fileTarget = "";
            }
            string filter = this.textboxFilter.Text.Trim();
            int columnCount = (int) this.numericUpDownColumns.Value;
            if (fileTarget == "")
            {
                this.ButtonSetTarget_Click(this, new EventArgs());
                fileTarget = this.textboxTarget.Text;
                if (fileTarget == "Not set")
                {
                    fileTarget = "";
                }
            }
            bool horizontalTiling = (this.radioButtonHorizontal.Checked);
            if (merger.ProcessDirectoryToFile(this, text, fileTarget, columnCount, filter, horizontalTiling, new string[] { paddingTopTB.Text, paddingBotTB.Text, paddingLeftTB.Text, paddingRightTB.Text }))
            {
                this.labelInformation.Text = "The image " + this.textboxTarget.Text + " has been created.";
                this.labelProcess.Text = merger.MergedImageCount + " images actually merged";
            }
        }

        private void ButtonQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSetSource_Click(object sender, EventArgs e)
        {
            if (this.dialogFindFolder.ShowDialog(this) == DialogResult.OK)
            {
                this.textboxSource.Text = this.dialogFindFolder.SelectedPath;
            }
        }

        private void ButtonSetTarget_Click(object sender, EventArgs e)
        {
            this.dialogSetTarget.Filter = "PNG Image files (*.png)|*.png|All files (*.*)|*.*";
            if (this.dialogSetTarget.ShowDialog(this) == DialogResult.OK)
            {
                this.textboxTarget.Text = this.dialogSetTarget.FileName;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
                this.components = null;
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textboxSource = new System.Windows.Forms.TextBox();
            this.textboxTarget = new System.Windows.Forms.TextBox();
            this.buttonSetSource = new System.Windows.Forms.Button();
            this.buttonSetTarget = new System.Windows.Forms.Button();
            this.checkboxRememberSettings = new System.Windows.Forms.CheckBox();
            this.buttonProcessImages = new System.Windows.Forms.Button();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.labelNumberDirection = new System.Windows.Forms.Label();
            this.labelInformation = new System.Windows.Forms.Label();
            this.numericUpDownColumns = new System.Windows.Forms.NumericUpDown();
            this.textboxFilter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTipController = new System.Windows.Forms.ToolTip(this.components);
            this.labelProcess = new System.Windows.Forms.Label();
            this.radioButtonHorizontal = new System.Windows.Forms.RadioButton();
            this.radioButtonVertical = new System.Windows.Forms.RadioButton();
            this.groupBoxTilingDirection = new System.Windows.Forms.GroupBox();
            this.paddingTopTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.paddingBotTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.paddingLeftTB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.paddingRightTB = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumns)).BeginInit();
            this.groupBoxTilingDirection.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source directory:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(49, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Target file:";
            // 
            // textboxSource
            // 
            this.textboxSource.BackColor = System.Drawing.Color.LightSteelBlue;
            this.textboxSource.ForeColor = System.Drawing.Color.Black;
            this.textboxSource.Location = new System.Drawing.Point(120, 13);
            this.textboxSource.Name = "textboxSource";
            this.textboxSource.ReadOnly = true;
            this.textboxSource.Size = new System.Drawing.Size(225, 20);
            this.textboxSource.TabIndex = 6;
            this.textboxSource.TextChanged += new System.EventHandler(this.TextboxSource_TextChanged);
            // 
            // textboxTarget
            // 
            this.textboxTarget.BackColor = System.Drawing.Color.LightSteelBlue;
            this.textboxTarget.ForeColor = System.Drawing.Color.Black;
            this.textboxTarget.Location = new System.Drawing.Point(120, 39);
            this.textboxTarget.Name = "textboxTarget";
            this.textboxTarget.ReadOnly = true;
            this.textboxTarget.Size = new System.Drawing.Size(225, 20);
            this.textboxTarget.TabIndex = 7;
            // 
            // buttonSetSource
            // 
            this.buttonSetSource.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonSetSource.Location = new System.Drawing.Point(352, 13);
            this.buttonSetSource.Name = "buttonSetSource";
            this.buttonSetSource.Size = new System.Drawing.Size(52, 23);
            this.buttonSetSource.TabIndex = 1;
            this.buttonSetSource.Text = "Browse";
            this.buttonSetSource.UseVisualStyleBackColor = false;
            this.buttonSetSource.Click += new System.EventHandler(this.ButtonSetSource_Click);
            // 
            // buttonSetTarget
            // 
            this.buttonSetTarget.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonSetTarget.Location = new System.Drawing.Point(351, 37);
            this.buttonSetTarget.Name = "buttonSetTarget";
            this.buttonSetTarget.Size = new System.Drawing.Size(52, 23);
            this.buttonSetTarget.TabIndex = 2;
            this.buttonSetTarget.Text = "Browse";
            this.buttonSetTarget.UseVisualStyleBackColor = false;
            this.buttonSetTarget.Click += new System.EventHandler(this.ButtonSetTarget_Click);
            // 
            // checkboxRememberSettings
            // 
            this.checkboxRememberSettings.AutoSize = true;
            this.checkboxRememberSettings.Location = new System.Drawing.Point(258, 132);
            this.checkboxRememberSettings.Name = "checkboxRememberSettings";
            this.checkboxRememberSettings.Size = new System.Drawing.Size(145, 17);
            this.checkboxRememberSettings.TabIndex = 4;
            this.checkboxRememberSettings.Text = "Remember these settings";
            this.checkboxRememberSettings.UseVisualStyleBackColor = true;
            // 
            // buttonProcessImages
            // 
            this.buttonProcessImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonProcessImages.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonProcessImages.Location = new System.Drawing.Point(330, 295);
            this.buttonProcessImages.Name = "buttonProcessImages";
            this.buttonProcessImages.Size = new System.Drawing.Size(100, 23);
            this.buttonProcessImages.TabIndex = 5;
            this.buttonProcessImages.Text = "Process Images";
            this.buttonProcessImages.UseVisualStyleBackColor = false;
            this.buttonProcessImages.Click += new System.EventHandler(this.ButtonProcessImages_Click);
            // 
            // buttonQuit
            // 
            this.buttonQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonQuit.BackColor = System.Drawing.Color.CornflowerBlue;
            this.buttonQuit.Location = new System.Drawing.Point(12, 295);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(65, 23);
            this.buttonQuit.TabIndex = 8;
            this.buttonQuit.Text = "Quit";
            this.buttonQuit.UseVisualStyleBackColor = false;
            this.buttonQuit.Click += new System.EventHandler(this.ButtonQuit_Click);
            // 
            // labelNumberDirection
            // 
            this.labelNumberDirection.AutoSize = true;
            this.labelNumberDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumberDirection.Location = new System.Drawing.Point(13, 72);
            this.labelNumberDirection.Name = "labelNumberDirection";
            this.labelNumberDirection.Size = new System.Drawing.Size(119, 13);
            this.labelNumberDirection.TabIndex = 10;
            this.labelNumberDirection.Text = "Number of columns:";
            // 
            // labelInformation
            // 
            this.labelInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInformation.AutoSize = true;
            this.labelInformation.Location = new System.Drawing.Point(12, 218);
            this.labelInformation.MaximumSize = new System.Drawing.Size(390, 60);
            this.labelInformation.MinimumSize = new System.Drawing.Size(390, 30);
            this.labelInformation.Name = "labelInformation";
            this.labelInformation.Size = new System.Drawing.Size(390, 30);
            this.labelInformation.TabIndex = 11;
            this.labelInformation.Text = "Program information";
            this.labelInformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDownColumns
            // 
            this.numericUpDownColumns.BackColor = System.Drawing.Color.Lavender;
            this.numericUpDownColumns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownColumns.ForeColor = System.Drawing.Color.Black;
            this.numericUpDownColumns.Location = new System.Drawing.Point(138, 69);
            this.numericUpDownColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownColumns.Name = "numericUpDownColumns";
            this.numericUpDownColumns.Size = new System.Drawing.Size(66, 20);
            this.numericUpDownColumns.TabIndex = 3;
            this.toolTipController.SetToolTip(this.numericUpDownColumns, "How many images to tile before starting a new row");
            this.numericUpDownColumns.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // textboxFilter
            // 
            this.textboxFilter.Location = new System.Drawing.Point(321, 69);
            this.textboxFilter.Name = "textboxFilter";
            this.textboxFilter.Size = new System.Drawing.Size(78, 20);
            this.textboxFilter.TabIndex = 3;
            this.toolTipController.SetToolTip(this.textboxFilter, "Only files containing this filter (lowercase check) will be merged");
            this.textboxFilter.TextChanged += new System.EventHandler(this.TextboxFilter_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(225, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Filename filter:";
            // 
            // labelProcess
            // 
            this.labelProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProcess.AutoSize = true;
            this.labelProcess.Location = new System.Drawing.Point(225, 279);
            this.labelProcess.MaximumSize = new System.Drawing.Size(200, 20);
            this.labelProcess.MinimumSize = new System.Drawing.Size(200, 0);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(200, 13);
            this.labelProcess.TabIndex = 14;
            this.labelProcess.Text = "Number of images";
            this.labelProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButtonHorizontal
            // 
            this.radioButtonHorizontal.AutoSize = true;
            this.radioButtonHorizontal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonHorizontal.Location = new System.Drawing.Point(18, 19);
            this.radioButtonHorizontal.Name = "radioButtonHorizontal";
            this.radioButtonHorizontal.Size = new System.Drawing.Size(83, 17);
            this.radioButtonHorizontal.TabIndex = 17;
            this.radioButtonHorizontal.TabStop = true;
            this.radioButtonHorizontal.Text = "Left to Right";
            this.radioButtonHorizontal.UseVisualStyleBackColor = true;
            this.radioButtonHorizontal.CheckedChanged += new System.EventHandler(this.RadioButtonHorizontal_CheckedChanged);
            // 
            // radioButtonVertical
            // 
            this.radioButtonVertical.AutoSize = true;
            this.radioButtonVertical.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonVertical.Location = new System.Drawing.Point(107, 19);
            this.radioButtonVertical.Name = "radioButtonVertical";
            this.radioButtonVertical.Size = new System.Drawing.Size(92, 17);
            this.radioButtonVertical.TabIndex = 18;
            this.radioButtonVertical.TabStop = true;
            this.radioButtonVertical.Text = "Top to Bottom";
            this.radioButtonVertical.UseVisualStyleBackColor = true;
            this.radioButtonVertical.CheckedChanged += new System.EventHandler(this.RadioButtonVertical_CheckedChanged);
            // 
            // groupBoxTilingDirection
            // 
            this.groupBoxTilingDirection.Controls.Add(this.radioButtonHorizontal);
            this.groupBoxTilingDirection.Controls.Add(this.radioButtonVertical);
            this.groupBoxTilingDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTilingDirection.Location = new System.Drawing.Point(12, 104);
            this.groupBoxTilingDirection.Name = "groupBoxTilingDirection";
            this.groupBoxTilingDirection.Size = new System.Drawing.Size(208, 45);
            this.groupBoxTilingDirection.TabIndex = 19;
            this.groupBoxTilingDirection.TabStop = false;
            this.groupBoxTilingDirection.Text = "Tiling Direction";
            // 
            // paddingTopTB
            // 
            this.paddingTopTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paddingTopTB.Location = new System.Drawing.Point(12, 172);
            this.paddingTopTB.MaximumSize = new System.Drawing.Size(90, 20);
            this.paddingTopTB.MinimumSize = new System.Drawing.Size(60, 20);
            this.paddingTopTB.Name = "paddingTopTB";
            this.paddingTopTB.Size = new System.Drawing.Size(90, 20);
            this.paddingTopTB.TabIndex = 20;
            this.paddingTopTB.Text = "0";
            this.paddingTopTB.TextChanged += new System.EventHandler(this.padding_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Padding Top";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(110, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Padding Bottom";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // paddingBotTB
            // 
            this.paddingBotTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paddingBotTB.Location = new System.Drawing.Point(110, 172);
            this.paddingBotTB.MaximumSize = new System.Drawing.Size(90, 20);
            this.paddingBotTB.MinimumSize = new System.Drawing.Size(60, 20);
            this.paddingBotTB.Name = "paddingBotTB";
            this.paddingBotTB.Size = new System.Drawing.Size(90, 20);
            this.paddingBotTB.TabIndex = 22;
            this.paddingBotTB.Text = "0";
            this.paddingBotTB.TextChanged += new System.EventHandler(this.padding_TextChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "Padding Left";
            // 
            // paddingLeftTB
            // 
            this.paddingLeftTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paddingLeftTB.Location = new System.Drawing.Point(217, 172);
            this.paddingLeftTB.MaximumSize = new System.Drawing.Size(90, 20);
            this.paddingLeftTB.MinimumSize = new System.Drawing.Size(60, 20);
            this.paddingLeftTB.Name = "paddingLeftTB";
            this.paddingLeftTB.Size = new System.Drawing.Size(90, 20);
            this.paddingLeftTB.TabIndex = 24;
            this.paddingLeftTB.Text = "0";
            this.paddingLeftTB.TextChanged += new System.EventHandler(this.padding_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(331, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Padding Right";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // paddingRightTB
            // 
            this.paddingRightTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.paddingRightTB.Location = new System.Drawing.Point(331, 172);
            this.paddingRightTB.MaximumSize = new System.Drawing.Size(90, 20);
            this.paddingRightTB.MinimumSize = new System.Drawing.Size(60, 20);
            this.paddingRightTB.Name = "paddingRightTB";
            this.paddingRightTB.Size = new System.Drawing.Size(90, 20);
            this.paddingRightTB.TabIndex = 26;
            this.paddingRightTB.Text = "0";
            this.paddingRightTB.TextChanged += new System.EventHandler(this.padding_TextChanged);
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(442, 330);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.paddingRightTB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.paddingLeftTB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.paddingBotTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.paddingTopTB);
            this.Controls.Add(this.groupBoxTilingDirection);
            this.Controls.Add(this.labelProcess);
            this.Controls.Add(this.checkboxRememberSettings);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textboxFilter);
            this.Controls.Add(this.numericUpDownColumns);
            this.Controls.Add(this.labelInformation);
            this.Controls.Add(this.labelNumberDirection);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.buttonProcessImages);
            this.Controls.Add(this.buttonSetTarget);
            this.Controls.Add(this.buttonSetSource);
            this.Controls.Add(this.textboxTarget);
            this.Controls.Add(this.textboxSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Tile Merger (Release 1.0.0)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumns)).EndInit();
            this.groupBoxTilingDirection.ResumeLayout(false);
            this.groupBoxTilingDirection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.WriteRegistryKey(@"Software\mkv25\Tile Merger\");
        }

        private void ReadRegistryKey(string keyName)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName) ?? Registry.CurrentUser.CreateSubKey(keyName);
            this.textboxSource.Text = (string) key.GetValue("sourceFolder", "Not set");
            this.textboxTarget.Text = (string) key.GetValue("targetFile", "Not set");
            this.numericUpDownColumns.Value = decimal.Parse((string)key.GetValue("numberOfColumns", "" + ((int)this.numericUpDownColumns.Value)));
            this.textboxFilter.Text = (string)key.GetValue("fileFilter", "");
            this.radioButtonHorizontal.Checked = bool.Parse((string)key.GetValue("horizontalTiling", "true"));
            this.radioButtonVertical.Checked = !this.radioButtonHorizontal.Checked;
            this.checkboxRememberSettings.Checked = bool.Parse((string) key.GetValue("rememberSettings", "false"));
        }

        private void TextboxFilter_TextChanged(object sender, EventArgs e)
        {
            this.UpdateFilecountMessage();
        }

        private void TextboxSource_TextChanged(object sender, EventArgs e)
        {
            this.UpdateFilecountMessage();
        }

        private void UpdateFilecountMessage()
        {
            this.labelProcess.Text = "";
            string text = this.textboxSource.Text;
            string filter = this.textboxFilter.Text.Trim();
            if (Directory.Exists(text))
            {
                List<string> list = ImageLoader.ListFiles(text, filter);
                this.labelProcess.Text = list.Count + " files found to merge";
            }
        }

        private void WriteRegistryKey(string keyName)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true);
            if (this.checkboxRememberSettings.Checked)
            {
                if (key == null)
                {
                    key = Registry.CurrentUser.CreateSubKey(keyName);
                }
                key.SetValue("sourceFolder", this.textboxSource.Text);
                key.SetValue("targetFile", this.textboxTarget.Text);
                key.SetValue("numberOfColumns", this.numericUpDownColumns.Value);
                key.SetValue("fileFilter", this.textboxFilter.Text);
                key.SetValue("horizontalTiling", radioButtonHorizontal.Checked);
                key.SetValue("rememberSettings", this.checkboxRememberSettings.Checked);
            }
            else
            {
                Registry.CurrentUser.DeleteSubKeyTree(keyName);
            }
        }

        private void RedrawForm() {
            if (radioButtonHorizontal.Checked)
            {
                labelNumberDirection.Text = "Number of Columns:";
            }
            else
            {
                labelNumberDirection.Text = "Number of Rows:";
            }
        }

        private void RadioButtonHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            RedrawForm();
        }

        private void RadioButtonVertical_CheckedChanged(object sender, EventArgs e)
        {
            RedrawForm();
        }

        private void padding_TextChanged(object sender, EventArgs e)
        {
            // get textbox from event
            TextBox textbox = (TextBox)sender;

            var numbers = Regex.Matches(textbox.Text, @"[0-9]+");

            if (numbers.Count > 0)
            {
                textbox.Text = numbers[0].Value;
            }
            else
            {
                textbox.Text = "";
            }
        }
    }
}

