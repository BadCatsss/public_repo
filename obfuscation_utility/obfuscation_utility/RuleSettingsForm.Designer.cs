
namespace obfuscation_utility
{
    partial class RuleSettingsForm
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
            this.RuleFor_label = new System.Windows.Forms.Label();
            this.Old_attrib_value_lbl = new System.Windows.Forms.Label();
            this.Modificators_lbl = new System.Windows.Forms.Label();
            this.Mod_enable_checkBox = new System.Windows.Forms.CheckBox();
            this.countryList_comboBox = new System.Windows.Forms.ComboBox();
            this.replacePattern_setting_groupBox = new System.Windows.Forms.GroupBox();
            this.N_pattern_textBox = new System.Windows.Forms.TextBox();
            this.N_Replace_radioButton = new System.Windows.Forms.RadioButton();
            this.LinearReplace_radioButton = new System.Windows.Forms.RadioButton();
            this.state_comboBox = new System.Windows.Forms.ComboBox();
            this.value_lbl = new System.Windows.Forms.Label();
            this.state_lbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.new_atrib_val_groupBox = new System.Windows.Forms.GroupBox();
            this.Modificators_setting_groupBox = new System.Windows.Forms.GroupBox();
            this.coord_t_groupBox = new System.Windows.Forms.GroupBox();
            this.BindLatLon_checkBox = new System.Windows.Forms.CheckBox();
            this.Lon_radioButton = new System.Windows.Forms.RadioButton();
            this.Lat_radioButton = new System.Windows.Forms.RadioButton();
            this.Lon_label = new System.Windows.Forms.Label();
            this.Lon_textBox = new System.Windows.Forms.TextBox();
            this.Lat_label = new System.Windows.Forms.Label();
            this.Lat_textBox = new System.Windows.Forms.TextBox();
            this.coords_by_const_radioButton = new System.Windows.Forms.RadioButton();
            this.worldPart_radioButton = new System.Windows.Forms.RadioButton();
            this.contryList_radioButton = new System.Windows.Forms.RadioButton();
            this.worldPartList_comboBox = new System.Windows.Forms.ComboBox();
            this.old_atrib_val_groupBox = new System.Windows.Forms.GroupBox();
            this.save_rule_button = new System.Windows.Forms.Button();
            this.atribBind_comboBox = new System.Windows.Forms.ComboBox();
            this.AtribBind_groupBox = new System.Windows.Forms.GroupBox();
            this.UpdateAll_checkBox = new System.Windows.Forms.CheckBox();
            this.replacePattern_setting_groupBox.SuspendLayout();
            this.Modificators_setting_groupBox.SuspendLayout();
            this.coord_t_groupBox.SuspendLayout();
            this.AtribBind_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // RuleFor_label
            // 
            this.RuleFor_label.AutoSize = true;
            this.RuleFor_label.Location = new System.Drawing.Point(12, 9);
            this.RuleFor_label.Name = "RuleFor_label";
            this.RuleFor_label.Size = new System.Drawing.Size(47, 13);
            this.RuleFor_label.TabIndex = 0;
            this.RuleFor_label.Text = "Rule for:";
            // 
            // Old_attrib_value_lbl
            // 
            this.Old_attrib_value_lbl.AutoSize = true;
            this.Old_attrib_value_lbl.Location = new System.Drawing.Point(12, 35);
            this.Old_attrib_value_lbl.Name = "Old_attrib_value_lbl";
            this.Old_attrib_value_lbl.Size = new System.Drawing.Size(81, 13);
            this.Old_attrib_value_lbl.TabIndex = 2;
            this.Old_attrib_value_lbl.Text = "Old attrib value:";
            // 
            // Modificators_lbl
            // 
            this.Modificators_lbl.AutoSize = true;
            this.Modificators_lbl.Location = new System.Drawing.Point(9, 154);
            this.Modificators_lbl.Name = "Modificators_lbl";
            this.Modificators_lbl.Size = new System.Drawing.Size(67, 13);
            this.Modificators_lbl.TabIndex = 5;
            this.Modificators_lbl.Text = "Modificators:";
            // 
            // Mod_enable_checkBox
            // 
            this.Mod_enable_checkBox.AutoSize = true;
            this.Mod_enable_checkBox.Location = new System.Drawing.Point(82, 153);
            this.Mod_enable_checkBox.Name = "Mod_enable_checkBox";
            this.Mod_enable_checkBox.Size = new System.Drawing.Size(126, 17);
            this.Mod_enable_checkBox.TabIndex = 6;
            this.Mod_enable_checkBox.Text = "is geographical cords";
            this.Mod_enable_checkBox.UseVisualStyleBackColor = true;
            this.Mod_enable_checkBox.CheckedChanged += new System.EventHandler(this.Mod_enable_checkBox_CheckedChanged);
            // 
            // countryList_comboBox
            // 
            this.countryList_comboBox.Enabled = false;
            this.countryList_comboBox.FormattingEnabled = true;
            this.countryList_comboBox.Location = new System.Drawing.Point(4, 112);
            this.countryList_comboBox.Name = "countryList_comboBox";
            this.countryList_comboBox.Size = new System.Drawing.Size(221, 21);
            this.countryList_comboBox.TabIndex = 8;
            // 
            // replacePattern_setting_groupBox
            // 
            this.replacePattern_setting_groupBox.Controls.Add(this.N_pattern_textBox);
            this.replacePattern_setting_groupBox.Controls.Add(this.N_Replace_radioButton);
            this.replacePattern_setting_groupBox.Controls.Add(this.LinearReplace_radioButton);
            this.replacePattern_setting_groupBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.replacePattern_setting_groupBox.Location = new System.Drawing.Point(325, 325);
            this.replacePattern_setting_groupBox.Name = "replacePattern_setting_groupBox";
            this.replacePattern_setting_groupBox.Size = new System.Drawing.Size(278, 178);
            this.replacePattern_setting_groupBox.TabIndex = 9;
            this.replacePattern_setting_groupBox.TabStop = false;
            this.replacePattern_setting_groupBox.Text = "Replace pattern:";
            // 
            // N_pattern_textBox
            // 
            this.N_pattern_textBox.Location = new System.Drawing.Point(110, 64);
            this.N_pattern_textBox.Name = "N_pattern_textBox";
            this.N_pattern_textBox.Size = new System.Drawing.Size(100, 20);
            this.N_pattern_textBox.TabIndex = 24;
            // 
            // N_Replace_radioButton
            // 
            this.N_Replace_radioButton.AutoSize = true;
            this.N_Replace_radioButton.Location = new System.Drawing.Point(7, 65);
            this.N_Replace_radioButton.Name = "N_Replace_radioButton";
            this.N_Replace_radioButton.Size = new System.Drawing.Size(97, 17);
            this.N_Replace_radioButton.TabIndex = 23;
            this.N_Replace_radioButton.Text = "Each N record:";
            this.N_Replace_radioButton.UseVisualStyleBackColor = true;
            this.N_Replace_radioButton.CheckedChanged += new System.EventHandler(this.N_Replace_radioButton_CheckedChanged);
            // 
            // LinearReplace_radioButton
            // 
            this.LinearReplace_radioButton.AutoSize = true;
            this.LinearReplace_radioButton.Checked = true;
            this.LinearReplace_radioButton.Location = new System.Drawing.Point(6, 31);
            this.LinearReplace_radioButton.Name = "LinearReplace_radioButton";
            this.LinearReplace_radioButton.Size = new System.Drawing.Size(54, 17);
            this.LinearReplace_radioButton.TabIndex = 22;
            this.LinearReplace_radioButton.TabStop = true;
            this.LinearReplace_radioButton.Text = "Linear";
            this.LinearReplace_radioButton.UseVisualStyleBackColor = true;
            this.LinearReplace_radioButton.CheckedChanged += new System.EventHandler(this.LinearReplace_radioButton_CheckedChanged);
            // 
            // state_comboBox
            // 
            this.state_comboBox.FormattingEnabled = true;
            this.state_comboBox.Location = new System.Drawing.Point(48, 55);
            this.state_comboBox.Name = "state_comboBox";
            this.state_comboBox.Size = new System.Drawing.Size(196, 21);
            this.state_comboBox.TabIndex = 1;
            // 
            // value_lbl
            // 
            this.value_lbl.AutoSize = true;
            this.value_lbl.Location = new System.Drawing.Point(263, 58);
            this.value_lbl.Name = "value_lbl";
            this.value_lbl.Size = new System.Drawing.Size(36, 13);
            this.value_lbl.TabIndex = 12;
            this.value_lbl.Text = "value:";
            // 
            // state_lbl
            // 
            this.state_lbl.AutoSize = true;
            this.state_lbl.Location = new System.Drawing.Point(9, 58);
            this.state_lbl.Name = "state_lbl";
            this.state_lbl.Size = new System.Drawing.Size(33, 13);
            this.state_lbl.TabIndex = 13;
            this.state_lbl.Text = "state:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "value:";
            // 
            // new_atrib_val_groupBox
            // 
            this.new_atrib_val_groupBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.new_atrib_val_groupBox.Location = new System.Drawing.Point(48, 91);
            this.new_atrib_val_groupBox.Name = "new_atrib_val_groupBox";
            this.new_atrib_val_groupBox.Size = new System.Drawing.Size(531, 50);
            this.new_atrib_val_groupBox.TabIndex = 10;
            this.new_atrib_val_groupBox.TabStop = false;
            this.new_atrib_val_groupBox.Text = "New atrib value:";
            // 
            // Modificators_setting_groupBox
            // 
            this.Modificators_setting_groupBox.Controls.Add(this.coord_t_groupBox);
            this.Modificators_setting_groupBox.Controls.Add(this.Lon_label);
            this.Modificators_setting_groupBox.Controls.Add(this.Lon_textBox);
            this.Modificators_setting_groupBox.Controls.Add(this.Lat_label);
            this.Modificators_setting_groupBox.Controls.Add(this.Lat_textBox);
            this.Modificators_setting_groupBox.Controls.Add(this.coords_by_const_radioButton);
            this.Modificators_setting_groupBox.Controls.Add(this.worldPart_radioButton);
            this.Modificators_setting_groupBox.Controls.Add(this.contryList_radioButton);
            this.Modificators_setting_groupBox.Controls.Add(this.worldPartList_comboBox);
            this.Modificators_setting_groupBox.Controls.Add(this.countryList_comboBox);
            this.Modificators_setting_groupBox.Enabled = false;
            this.Modificators_setting_groupBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Modificators_setting_groupBox.Location = new System.Drawing.Point(12, 176);
            this.Modificators_setting_groupBox.Name = "Modificators_setting_groupBox";
            this.Modificators_setting_groupBox.Size = new System.Drawing.Size(299, 311);
            this.Modificators_setting_groupBox.TabIndex = 15;
            this.Modificators_setting_groupBox.TabStop = false;
            this.Modificators_setting_groupBox.Text = "New coords:";
            // 
            // coord_t_groupBox
            // 
            this.coord_t_groupBox.Controls.Add(this.BindLatLon_checkBox);
            this.coord_t_groupBox.Controls.Add(this.Lon_radioButton);
            this.coord_t_groupBox.Controls.Add(this.Lat_radioButton);
            this.coord_t_groupBox.Location = new System.Drawing.Point(10, 20);
            this.coord_t_groupBox.Name = "coord_t_groupBox";
            this.coord_t_groupBox.Size = new System.Drawing.Size(264, 61);
            this.coord_t_groupBox.TabIndex = 24;
            this.coord_t_groupBox.TabStop = false;
            this.coord_t_groupBox.Text = "Coord change type:";
            // 
            // BindLatLon_checkBox
            // 
            this.BindLatLon_checkBox.AutoSize = true;
            this.BindLatLon_checkBox.Location = new System.Drawing.Point(126, 29);
            this.BindLatLon_checkBox.Name = "BindLatLon_checkBox";
            this.BindLatLon_checkBox.Size = new System.Drawing.Size(107, 17);
            this.BindLatLon_checkBox.TabIndex = 2;
            this.BindLatLon_checkBox.Text = "Bind Lat and Lon";
            this.BindLatLon_checkBox.UseVisualStyleBackColor = true;
            this.BindLatLon_checkBox.CheckedChanged += new System.EventHandler(this.BindLatLon_checkBox_CheckedChanged);
            // 
            // Lon_radioButton
            // 
            this.Lon_radioButton.AutoSize = true;
            this.Lon_radioButton.Location = new System.Drawing.Point(58, 29);
            this.Lon_radioButton.Name = "Lon_radioButton";
            this.Lon_radioButton.Size = new System.Drawing.Size(43, 17);
            this.Lon_radioButton.TabIndex = 1;
            this.Lon_radioButton.Text = "Lon";
            this.Lon_radioButton.UseVisualStyleBackColor = true;
            this.Lon_radioButton.CheckedChanged += new System.EventHandler(this.Lon_radioButton_CheckedChanged);
            // 
            // Lat_radioButton
            // 
            this.Lat_radioButton.AutoSize = true;
            this.Lat_radioButton.Checked = true;
            this.Lat_radioButton.Location = new System.Drawing.Point(7, 29);
            this.Lat_radioButton.Name = "Lat_radioButton";
            this.Lat_radioButton.Size = new System.Drawing.Size(40, 17);
            this.Lat_radioButton.TabIndex = 0;
            this.Lat_radioButton.TabStop = true;
            this.Lat_radioButton.Text = "Lat";
            this.Lat_radioButton.UseVisualStyleBackColor = true;
            this.Lat_radioButton.CheckedChanged += new System.EventHandler(this.Lat_radioButton_CheckedChanged);
            // 
            // Lon_label
            // 
            this.Lon_label.AutoSize = true;
            this.Lon_label.Location = new System.Drawing.Point(114, 254);
            this.Lon_label.Name = "Lon_label";
            this.Lon_label.Size = new System.Drawing.Size(28, 13);
            this.Lon_label.TabIndex = 22;
            this.Lon_label.Text = "Lon:";
            // 
            // Lon_textBox
            // 
            this.Lon_textBox.Enabled = false;
            this.Lon_textBox.Location = new System.Drawing.Point(148, 250);
            this.Lon_textBox.Name = "Lon_textBox";
            this.Lon_textBox.Size = new System.Drawing.Size(68, 20);
            this.Lon_textBox.TabIndex = 23;
            // 
            // Lat_label
            // 
            this.Lat_label.AutoSize = true;
            this.Lat_label.Location = new System.Drawing.Point(4, 254);
            this.Lat_label.Name = "Lat_label";
            this.Lat_label.Size = new System.Drawing.Size(25, 13);
            this.Lat_label.TabIndex = 16;
            this.Lat_label.Text = "Lat:";
            // 
            // Lat_textBox
            // 
            this.Lat_textBox.Enabled = false;
            this.Lat_textBox.Location = new System.Drawing.Point(35, 251);
            this.Lat_textBox.Name = "Lat_textBox";
            this.Lat_textBox.Size = new System.Drawing.Size(68, 20);
            this.Lat_textBox.TabIndex = 21;
            // 
            // coords_by_const_radioButton
            // 
            this.coords_by_const_radioButton.AutoSize = true;
            this.coords_by_const_radioButton.Location = new System.Drawing.Point(10, 224);
            this.coords_by_const_radioButton.Name = "coords_by_const_radioButton";
            this.coords_by_const_radioButton.Size = new System.Drawing.Size(69, 17);
            this.coords_by_const_radioButton.TabIndex = 20;
            this.coords_by_const_radioButton.Text = "By const:";
            this.coords_by_const_radioButton.UseVisualStyleBackColor = true;
            this.coords_by_const_radioButton.CheckedChanged += new System.EventHandler(this.coords_by_const_radioButton_CheckedChanged);
            // 
            // worldPart_radioButton
            // 
            this.worldPart_radioButton.AutoSize = true;
            this.worldPart_radioButton.Checked = true;
            this.worldPart_radioButton.Location = new System.Drawing.Point(10, 149);
            this.worldPart_radioButton.Name = "worldPart_radioButton";
            this.worldPart_radioButton.Size = new System.Drawing.Size(89, 17);
            this.worldPart_radioButton.TabIndex = 19;
            this.worldPart_radioButton.TabStop = true;
            this.worldPart_radioButton.Text = "By world part:";
            this.worldPart_radioButton.UseVisualStyleBackColor = true;
            this.worldPart_radioButton.CheckedChanged += new System.EventHandler(this.worldPart_radioButton_CheckedChanged);
            // 
            // contryList_radioButton
            // 
            this.contryList_radioButton.AutoSize = true;
            this.contryList_radioButton.Location = new System.Drawing.Point(10, 87);
            this.contryList_radioButton.Name = "contryList_radioButton";
            this.contryList_radioButton.Size = new System.Drawing.Size(93, 17);
            this.contryList_radioButton.TabIndex = 18;
            this.contryList_radioButton.Text = "By country list:";
            this.contryList_radioButton.UseVisualStyleBackColor = true;
            this.contryList_radioButton.CheckedChanged += new System.EventHandler(this.contryList_radioButton_CheckedChanged);
            // 
            // worldPartList_comboBox
            // 
            this.worldPartList_comboBox.FormattingEnabled = true;
            this.worldPartList_comboBox.Location = new System.Drawing.Point(4, 184);
            this.worldPartList_comboBox.Name = "worldPartList_comboBox";
            this.worldPartList_comboBox.Size = new System.Drawing.Size(221, 21);
            this.worldPartList_comboBox.TabIndex = 17;
            // 
            // old_atrib_val_groupBox
            // 
            this.old_atrib_val_groupBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.old_atrib_val_groupBox.Location = new System.Drawing.Point(325, 12);
            this.old_atrib_val_groupBox.Name = "old_atrib_val_groupBox";
            this.old_atrib_val_groupBox.Size = new System.Drawing.Size(254, 42);
            this.old_atrib_val_groupBox.TabIndex = 24;
            this.old_atrib_val_groupBox.TabStop = false;
            this.old_atrib_val_groupBox.Text = "Old atrib value:";
            // 
            // save_rule_button
            // 
            this.save_rule_button.Location = new System.Drawing.Point(332, 288);
            this.save_rule_button.Name = "save_rule_button";
            this.save_rule_button.Size = new System.Drawing.Size(75, 23);
            this.save_rule_button.TabIndex = 25;
            this.save_rule_button.Text = "Done";
            this.save_rule_button.UseVisualStyleBackColor = true;
            this.save_rule_button.Click += new System.EventHandler(this.save_rule_button_Click);
            // 
            // atribBind_comboBox
            // 
            this.atribBind_comboBox.FormattingEnabled = true;
            this.atribBind_comboBox.Location = new System.Drawing.Point(7, 19);
            this.atribBind_comboBox.Name = "atribBind_comboBox";
            this.atribBind_comboBox.Size = new System.Drawing.Size(187, 21);
            this.atribBind_comboBox.TabIndex = 26;
            // 
            // AtribBind_groupBox
            // 
            this.AtribBind_groupBox.Controls.Add(this.atribBind_comboBox);
            this.AtribBind_groupBox.Enabled = false;
            this.AtribBind_groupBox.Location = new System.Drawing.Point(325, 180);
            this.AtribBind_groupBox.Name = "AtribBind_groupBox";
            this.AtribBind_groupBox.Size = new System.Drawing.Size(200, 100);
            this.AtribBind_groupBox.TabIndex = 27;
            this.AtribBind_groupBox.TabStop = false;
            this.AtribBind_groupBox.Text = "Atrib For Bind";
            this.AtribBind_groupBox.Visible = false;
            // 
            // UpdateAll_checkBox
            // 
            this.UpdateAll_checkBox.AutoSize = true;
            this.UpdateAll_checkBox.Location = new System.Drawing.Point(325, 68);
            this.UpdateAll_checkBox.Name = "UpdateAll_checkBox";
            this.UpdateAll_checkBox.Size = new System.Drawing.Size(74, 17);
            this.UpdateAll_checkBox.TabIndex = 28;
            this.UpdateAll_checkBox.Text = "Update all";
            this.UpdateAll_checkBox.UseVisualStyleBackColor = true;
            this.UpdateAll_checkBox.CheckedChanged += new System.EventHandler(this.UpdateAll_checkBox_CheckedChanged);
            // 
            // RuleSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 499);
            this.Controls.Add(this.UpdateAll_checkBox);
            this.Controls.Add(this.AtribBind_groupBox);
            this.Controls.Add(this.save_rule_button);
            this.Controls.Add(this.old_atrib_val_groupBox);
            this.Controls.Add(this.Modificators_setting_groupBox);
            this.Controls.Add(this.new_atrib_val_groupBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.state_lbl);
            this.Controls.Add(this.value_lbl);
            this.Controls.Add(this.replacePattern_setting_groupBox);
            this.Controls.Add(this.Mod_enable_checkBox);
            this.Controls.Add(this.Modificators_lbl);
            this.Controls.Add(this.Old_attrib_value_lbl);
            this.Controls.Add(this.state_comboBox);
            this.Controls.Add(this.RuleFor_label);
            this.Name = "RuleSettingsForm";
            this.Text = "RuleSettingsForm";
            this.replacePattern_setting_groupBox.ResumeLayout(false);
            this.replacePattern_setting_groupBox.PerformLayout();
            this.Modificators_setting_groupBox.ResumeLayout(false);
            this.Modificators_setting_groupBox.PerformLayout();
            this.coord_t_groupBox.ResumeLayout(false);
            this.coord_t_groupBox.PerformLayout();
            this.AtribBind_groupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label RuleFor_label;
        private System.Windows.Forms.Label Old_attrib_value_lbl;
        private System.Windows.Forms.Label Modificators_lbl;
        private System.Windows.Forms.CheckBox Mod_enable_checkBox;
        private System.Windows.Forms.ComboBox countryList_comboBox;
        private System.Windows.Forms.GroupBox replacePattern_setting_groupBox;
        private System.Windows.Forms.TextBox N_pattern_textBox;
        private System.Windows.Forms.RadioButton N_Replace_radioButton;
        private System.Windows.Forms.RadioButton LinearReplace_radioButton;
        private System.Windows.Forms.ComboBox state_comboBox;
        private System.Windows.Forms.Label value_lbl;
        private System.Windows.Forms.Label state_lbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox new_atrib_val_groupBox;
        private System.Windows.Forms.GroupBox Modificators_setting_groupBox;
        private System.Windows.Forms.Label Lon_label;
        private System.Windows.Forms.TextBox Lon_textBox;
        private System.Windows.Forms.Label Lat_label;
        private System.Windows.Forms.TextBox Lat_textBox;
        private System.Windows.Forms.RadioButton coords_by_const_radioButton;
        private System.Windows.Forms.RadioButton worldPart_radioButton;
        private System.Windows.Forms.RadioButton contryList_radioButton;
        private System.Windows.Forms.ComboBox worldPartList_comboBox;
        private System.Windows.Forms.GroupBox old_atrib_val_groupBox;
        private System.Windows.Forms.Button save_rule_button;
        private System.Windows.Forms.GroupBox coord_t_groupBox;
        private System.Windows.Forms.RadioButton Lon_radioButton;
        private System.Windows.Forms.RadioButton Lat_radioButton;
        private System.Windows.Forms.CheckBox BindLatLon_checkBox;
        private System.Windows.Forms.ComboBox atribBind_comboBox;
        private System.Windows.Forms.GroupBox AtribBind_groupBox;
        private System.Windows.Forms.CheckBox UpdateAll_checkBox;
    }
}