
namespace obfuscation_utility
{
    partial class DeleteRuleDialog
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
            this.Delete_acept_btn = new System.Windows.Forms.Button();
            this.RuleIdPicker = new System.Windows.Forms.NumericUpDown();
            this.ruleId_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RuleIdPicker)).BeginInit();
            this.SuspendLayout();
            // 
            // Delete_acept_btn
            // 
            this.Delete_acept_btn.Location = new System.Drawing.Point(47, 62);
            this.Delete_acept_btn.Name = "Delete_acept_btn";
            this.Delete_acept_btn.Size = new System.Drawing.Size(151, 23);
            this.Delete_acept_btn.TabIndex = 0;
            this.Delete_acept_btn.Text = "Delete";
            this.Delete_acept_btn.UseVisualStyleBackColor = true;
            this.Delete_acept_btn.Click += new System.EventHandler(this.Delete_acept_btn_Click);
            // 
            // RuleIdPicker
            // 
            this.RuleIdPicker.Location = new System.Drawing.Point(78, 12);
            this.RuleIdPicker.Name = "RuleIdPicker";
            this.RuleIdPicker.Size = new System.Drawing.Size(120, 20);
            this.RuleIdPicker.TabIndex = 1;
            // 
            // ruleId_label
            // 
            this.ruleId_label.AutoSize = true;
            this.ruleId_label.Location = new System.Drawing.Point(13, 18);
            this.ruleId_label.Name = "ruleId_label";
            this.ruleId_label.Size = new System.Drawing.Size(46, 13);
            this.ruleId_label.TabIndex = 2;
            this.ruleId_label.Text = "Rule ID:";
            // 
            // DeleteRuleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 115);
            this.Controls.Add(this.ruleId_label);
            this.Controls.Add(this.RuleIdPicker);
            this.Controls.Add(this.Delete_acept_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DeleteRuleDialog";
            this.Text = "DeleteRuleDialog";
            ((System.ComponentModel.ISupportInitialize)(this.RuleIdPicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Delete_acept_btn;
        private System.Windows.Forms.NumericUpDown RuleIdPicker;
        private System.Windows.Forms.Label ruleId_label;
    }
}