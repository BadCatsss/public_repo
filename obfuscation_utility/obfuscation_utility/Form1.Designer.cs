
namespace obfuscation_utility
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
            this.DB_tab_panel = new System.Windows.Forms.TabControl();
            this.add_tab_btn = new System.Windows.Forms.Button();
            this.rem_tab_btn = new System.Windows.Forms.Button();
            this.add_rule_btn = new System.Windows.Forms.Button();
            this.del_rule_btn = new System.Windows.Forms.Button();
            this.save_rule_button = new System.Windows.Forms.Button();
            this.import_rule_button = new System.Windows.Forms.Button();
            this.exec_q_btn = new System.Windows.Forms.Button();
            this.All_WorkspaceExecFlag_checkBox = new System.Windows.Forms.CheckBox();
            this.LogF_Path_label = new System.Windows.Forms.Label();
            this.ConfigF_path_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DB_tab_panel
            // 
            this.DB_tab_panel.Location = new System.Drawing.Point(12, 12);
            this.DB_tab_panel.Name = "DB_tab_panel";
            this.DB_tab_panel.SelectedIndex = 0;
            this.DB_tab_panel.Size = new System.Drawing.Size(678, 713);
            this.DB_tab_panel.TabIndex = 0;
            this.DB_tab_panel.SelectedIndexChanged += new System.EventHandler(this.DB_tab_panel_SelectedIndexChanged);
            // 
            // add_tab_btn
            // 
            this.add_tab_btn.Location = new System.Drawing.Point(711, 34);
            this.add_tab_btn.Name = "add_tab_btn";
            this.add_tab_btn.Size = new System.Drawing.Size(77, 23);
            this.add_tab_btn.TabIndex = 0;
            this.add_tab_btn.Text = "+";
            this.add_tab_btn.UseVisualStyleBackColor = true;
            this.add_tab_btn.Click += new System.EventHandler(this.add_tab_btn_Click);
            // 
            // rem_tab_btn
            // 
            this.rem_tab_btn.Location = new System.Drawing.Point(711, 63);
            this.rem_tab_btn.Name = "rem_tab_btn";
            this.rem_tab_btn.Size = new System.Drawing.Size(77, 25);
            this.rem_tab_btn.TabIndex = 1;
            this.rem_tab_btn.Text = "-";
            this.rem_tab_btn.UseVisualStyleBackColor = true;
            this.rem_tab_btn.Click += new System.EventHandler(this.rem_tab_btn_Click);
            // 
            // add_rule_btn
            // 
            this.add_rule_btn.Location = new System.Drawing.Point(707, 416);
            this.add_rule_btn.Name = "add_rule_btn";
            this.add_rule_btn.Size = new System.Drawing.Size(79, 32);
            this.add_rule_btn.TabIndex = 2;
            this.add_rule_btn.Text = "Add rule";
            this.add_rule_btn.UseVisualStyleBackColor = true;
            this.add_rule_btn.Click += new System.EventHandler(this.add_rule_btn_Click);
            // 
            // del_rule_btn
            // 
            this.del_rule_btn.Location = new System.Drawing.Point(706, 454);
            this.del_rule_btn.Name = "del_rule_btn";
            this.del_rule_btn.Size = new System.Drawing.Size(80, 32);
            this.del_rule_btn.TabIndex = 4;
            this.del_rule_btn.Text = "Delete rule";
            this.del_rule_btn.UseVisualStyleBackColor = true;
            this.del_rule_btn.Click += new System.EventHandler(this.del_rule_btn_Click);
            // 
            // save_rule_button
            // 
            this.save_rule_button.Enabled = false;
            this.save_rule_button.Location = new System.Drawing.Point(696, 215);
            this.save_rule_button.Name = "save_rule_button";
            this.save_rule_button.Size = new System.Drawing.Size(106, 26);
            this.save_rule_button.TabIndex = 5;
            this.save_rule_button.Text = "Save/Export rule";
            this.save_rule_button.UseVisualStyleBackColor = true;
            this.save_rule_button.Click += new System.EventHandler(this.save_rule_button_Click);
            // 
            // import_rule_button
            // 
            this.import_rule_button.Location = new System.Drawing.Point(696, 247);
            this.import_rule_button.Name = "import_rule_button";
            this.import_rule_button.Size = new System.Drawing.Size(106, 22);
            this.import_rule_button.TabIndex = 6;
            this.import_rule_button.Text = "Import rule";
            this.import_rule_button.UseVisualStyleBackColor = true;
            this.import_rule_button.Click += new System.EventHandler(this.import_rule_button_Click);
            // 
            // exec_q_btn
            // 
            this.exec_q_btn.Enabled = false;
            this.exec_q_btn.Location = new System.Drawing.Point(706, 492);
            this.exec_q_btn.Name = "exec_q_btn";
            this.exec_q_btn.Size = new System.Drawing.Size(80, 50);
            this.exec_q_btn.TabIndex = 7;
            this.exec_q_btn.Text = "Execute created query";
            this.exec_q_btn.UseVisualStyleBackColor = true;
            this.exec_q_btn.Click += new System.EventHandler(this.exec_q_btn_Click);
            // 
            // All_WorkspaceExecFlag_checkBox
            // 
            this.All_WorkspaceExecFlag_checkBox.AutoSize = true;
            this.All_WorkspaceExecFlag_checkBox.Location = new System.Drawing.Point(696, 548);
            this.All_WorkspaceExecFlag_checkBox.Name = "All_WorkspaceExecFlag_checkBox";
            this.All_WorkspaceExecFlag_checkBox.Size = new System.Drawing.Size(153, 17);
            this.All_WorkspaceExecFlag_checkBox.TabIndex = 8;
            this.All_WorkspaceExecFlag_checkBox.Text = "Execute for all workspasce";
            this.All_WorkspaceExecFlag_checkBox.UseVisualStyleBackColor = true;
            // 
            // LogF_Path_label
            // 
            this.LogF_Path_label.AutoSize = true;
            this.LogF_Path_label.Location = new System.Drawing.Point(41, 730);
            this.LogF_Path_label.Name = "LogF_Path_label";
            this.LogF_Path_label.Size = new System.Drawing.Size(84, 13);
            this.LogF_Path_label.TabIndex = 9;
            this.LogF_Path_label.Text = "Path to Log File:";
            // 
            // ConfigF_path_label
            // 
            this.ConfigF_path_label.AutoSize = true;
            this.ConfigF_path_label.Location = new System.Drawing.Point(41, 759);
            this.ConfigF_path_label.Name = "ConfigF_path_label";
            this.ConfigF_path_label.Size = new System.Drawing.Size(95, 13);
            this.ConfigF_path_label.TabIndex = 10;
            this.ConfigF_path_label.Text = "Path to config File:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 781);
            this.Controls.Add(this.ConfigF_path_label);
            this.Controls.Add(this.LogF_Path_label);
            this.Controls.Add(this.All_WorkspaceExecFlag_checkBox);
            this.Controls.Add(this.exec_q_btn);
            this.Controls.Add(this.import_rule_button);
            this.Controls.Add(this.save_rule_button);
            this.Controls.Add(this.del_rule_btn);
            this.Controls.Add(this.add_rule_btn);
            this.Controls.Add(this.rem_tab_btn);
            this.Controls.Add(this.add_tab_btn);
            this.Controls.Add(this.DB_tab_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button add_tab_btn;
        private System.Windows.Forms.Button rem_tab_btn;
        private System.Windows.Forms.Button add_rule_btn;
        private System.Windows.Forms.Button del_rule_btn;
        protected internal System.Windows.Forms.TabControl DB_tab_panel;
        private System.Windows.Forms.Button import_rule_button;
        public System.Windows.Forms.Button save_rule_button;
        public System.Windows.Forms.Button exec_q_btn;
        private System.Windows.Forms.CheckBox All_WorkspaceExecFlag_checkBox;
        private System.Windows.Forms.Label LogF_Path_label;
        private System.Windows.Forms.Label ConfigF_path_label;
    }
}

