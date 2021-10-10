using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace obfuscation_utility
{
    public partial class SaveRule_DialogForm : Form
    {
        public SaveRule_DialogForm()
        {
            InitializeComponent();
            for (int i = 0; i < MainForm.createdRules.Count; i++)
            {
                foreach (var val in MainForm.createdRules[i].Keys)
                {
                    foreach (var item in val)
                    {
                        RulesForSave_checkedListBox.Items.Add(item);

                    }
                    
                }
            }


        }

        private void Select_accept_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Json files | *.json";
            sfd.DefaultExt = "json";
            //sfd.FileName = "Obfuscation_rule";
        DialogResult drs=    sfd.ShowDialog();
            string rule;

            if(drs==DialogResult.OK || drs == DialogResult.Yes)
            {
                string filePath = "";
                string fileName = "";
                filePath = sfd.FileName.Substring(0, sfd.FileName.LastIndexOf("\\"));
                filePath += "\\";
                if (!sfd.CheckFileExists)
                {
                    fileName = sfd.FileName.Substring(sfd.FileName.LastIndexOf("\\"), sfd.FileName.Length - sfd.FileName.LastIndexOf("\\"));
                    fileName = fileName.Replace("\\", "");
                }
                else
                {
                    fileName = "New_" + DateTime.Now + "_" + fileName;
                }
                int count = 0;
                foreach (var item in RulesForSave_checkedListBox.CheckedItems)
                {
                    for (int i = 0; i < MainForm.createdRules.Count; i++)
                    {
                        foreach (var item1 in MainForm.createdRules[i])
                        {
                            rule = JsonConvert.SerializeObject(item1, Formatting.Indented);

                            File.WriteAllText(filePath + "rule_" + count + "_" + fileName, rule);
                            LogObj.LogEvent(LogEventType.Rule_Export, OperationStatus.Success, "", "Was export rule:" + filePath + "rule_" + count + "_" + fileName);
                        }
                    }
                    count++;


                }
            }
            else
            {
                drs = sfd.ShowDialog();
            }
           
        }

        private void selectAllR_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                for (int i = 0; i < RulesForSave_checkedListBox.Items.Count; i++)
                {
                    RulesForSave_checkedListBox.SetItemChecked(i, true);
                }
            
            }
            else
            {
                for (int i = 0; i < RulesForSave_checkedListBox.Items.Count; i++)
                {
                    RulesForSave_checkedListBox.SetItemChecked(i, false);
                }
            }
        }
    }
}
