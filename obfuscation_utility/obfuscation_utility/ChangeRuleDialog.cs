using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace obfuscation_utility
{
    public partial class ChangeRuleDialog : Form
    {
        public ChangeRuleDialog()
        {
            InitializeComponent();
        }

        private void Change_acept_btn_Click(object sender, EventArgs e)
        {
            TableLayoutPanel table = ((TableLayoutPanel)MainForm.currentTab.Controls["rulesTable"]);
            if (table.Controls.Find("RuleIdLabel" + RuleIdPicker.Value, true).Length != 0)
            {
              //TODO
            }
            else
            {
                MessageBox.Show("Not rule id found", "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
