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
    public partial class DeleteRuleDialog : Form
    {
        public DeleteRuleDialog()
        {
            InitializeComponent();
        }

        private void Delete_acept_btn_Click(object sender, EventArgs e)
        {
            TableLayoutPanel table=((TableLayoutPanel)MainForm.currentTab.Controls["rulesTable"]);
            if (table.Controls.Find("RuleIdLabel" + RuleIdPicker.Value, true).Length!=0)
            {
             int startDelIndex=   table.Controls.IndexOf(table.Controls.Find("RuleIdLabel" + RuleIdPicker.Value, true)[0])-1;
               
                try
                {
                    string id, rule_texr_form, rule;
                    if (MainForm.createdRules.Count>0 && MainForm.createdRules[MainForm.createdRules.Count-1] !=null)
                    {
                        MainForm.createdRules.RemoveAt(MainForm.createdRules.Count - 1);
                    }
                
                    id = RuleIdPicker.Value.ToString();
                    rule_texr_form = Controls[startDelIndex].Text;
                    table.Controls.RemoveAt(startDelIndex);
                    rule = Controls[startDelIndex].Text;
                    table.Controls.RemoveAt(startDelIndex);
                    table.Controls.RemoveAt(startDelIndex);
                    table.Controls.RemoveAt(startDelIndex);
                    if (MainForm.createdRules.Count==0)
                    {
                        MainForm.execQueryBtn.Enabled = false;
                    }
                    LogObj.LogEvent(LogEventType.Rule_Delete, OperationStatus.Success, "Rule was delete", ";Rule id:" + id + ";Rule for:" + rule_texr_form + ";Rule:" + rule);
                }
                catch (Exception ex)
                {
                    LogObj.LogEvent(LogEventType.Rule_Delete, OperationStatus.Error, "Rule delete error", "Exception message:"+ex.Message+";Stacktrace:"+ex.StackTrace) ;
                }

            }
            else
            {
                LogObj.LogEvent(LogEventType.Rule_Delete, OperationStatus.Error, "Rule delete error. Rule id error", "Not rule id found");

                MessageBox.Show("Not rule id found","ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
    }
}
