
namespace obfuscation_utility
{
    partial class ChangeRuleDialog
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
            this.ruleId_label = new System.Windows.Forms.Label();
            this.RuleIdPicker = new System.Windows.Forms.NumericUpDown();
            this.Change_acept_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RuleIdPicker)).BeginInit();
            this.SuspendLayout();
            // 
            // ruleId_label
            // 
            this.ruleId_label.AutoSize = true;
            this.ruleId_label.Location = new System.Drawing.Point(18, 28);
            this.ruleId_label.Name = "ruleId_label";
            this.ruleId_label.Size = new System.Drawing.Size(46, 13);
            this.ruleId_label.TabIndex = 5;
            this.ruleId_label.Text = "Rule ID:";
            // 
            // RuleIdPicker
            // 
            this.RuleIdPicker.Location = new System.Drawing.Point(83, 22);
            this.RuleIdPicker.Name = "RuleIdPicker";
            this.RuleIdPicker.Size = new System.Drawing.Size(120, 20);
            this.RuleIdPicker.TabIndex = 4;
            // 
            // Change_acept_btn
            // 
            this.Change_acept_btn.Location = new System.Drawing.Point(52, 72);
            this.Change_acept_btn.Name = "Change_acept_btn";
            this.Change_acept_btn.Size = new System.Drawing.Size(151, 23);
            this.Change_acept_btn.TabIndex = 3;
            this.Change_acept_btn.Text = "Change rule";
            this.Change_acept_btn.UseVisualStyleBackColor = true;
            this.Change_acept_btn.Click += new System.EventHandler(this.Change_acept_btn_Click);
            // 
            // ChangeRuleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 130);
            this.Controls.Add(this.ruleId_label);
            this.Controls.Add(this.RuleIdPicker);
            this.Controls.Add(this.Change_acept_btn);
            this.Name = "ChangeRuleDialog";
            this.Text = "ChangeRuleDialog";
            ((System.ComponentModel.ISupportInitialize)(this.RuleIdPicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ruleId_label;
        private System.Windows.Forms.NumericUpDown RuleIdPicker;
        private System.Windows.Forms.Button Change_acept_btn;
    }
}