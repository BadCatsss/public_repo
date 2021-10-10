
namespace obfuscation_utility
{
    partial class SaveRule_DialogForm
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
            this.RulesForSave_checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.selectAllR_checkBox = new System.Windows.Forms.CheckBox();
            this.Select_accept_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RulesForSave_checkedListBox
            // 
            this.RulesForSave_checkedListBox.FormattingEnabled = true;
            this.RulesForSave_checkedListBox.Location = new System.Drawing.Point(12, 40);
            this.RulesForSave_checkedListBox.Name = "RulesForSave_checkedListBox";
            this.RulesForSave_checkedListBox.Size = new System.Drawing.Size(465, 214);
            this.RulesForSave_checkedListBox.TabIndex = 0;
            // 
            // selectAllR_checkBox
            // 
            this.selectAllR_checkBox.AutoSize = true;
            this.selectAllR_checkBox.Location = new System.Drawing.Point(13, 13);
            this.selectAllR_checkBox.Name = "selectAllR_checkBox";
            this.selectAllR_checkBox.Size = new System.Drawing.Size(69, 17);
            this.selectAllR_checkBox.TabIndex = 1;
            this.selectAllR_checkBox.Text = "Select all";
            this.selectAllR_checkBox.UseVisualStyleBackColor = true;
            this.selectAllR_checkBox.CheckedChanged += new System.EventHandler(this.selectAllR_checkBox_CheckedChanged);
            // 
            // Select_accept_button
            // 
            this.Select_accept_button.Location = new System.Drawing.Point(121, 7);
            this.Select_accept_button.Name = "Select_accept_button";
            this.Select_accept_button.Size = new System.Drawing.Size(75, 23);
            this.Select_accept_button.TabIndex = 2;
            this.Select_accept_button.Text = "Done";
            this.Select_accept_button.UseVisualStyleBackColor = true;
            this.Select_accept_button.Click += new System.EventHandler(this.Select_accept_button_Click);
            // 
            // SaveRule_DialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 270);
            this.Controls.Add(this.Select_accept_button);
            this.Controls.Add(this.selectAllR_checkBox);
            this.Controls.Add(this.RulesForSave_checkedListBox);
            this.Name = "SaveRule_DialogForm";
            this.Text = "SaveRule_DialogForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox RulesForSave_checkedListBox;
        private System.Windows.Forms.CheckBox selectAllR_checkBox;
        private System.Windows.Forms.Button Select_accept_button;
    }
}