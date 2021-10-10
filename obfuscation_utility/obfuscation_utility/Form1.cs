using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using Newtonsoft.Json;

namespace obfuscation_utility
{
    public partial class MainForm : Form
    {
        static int tabNumber = 1;
        protected internal static TabPage currentTab;
        protected internal static int currentTabIndex = 0;
        static DeleteRuleDialog deleteDialog;
        public static Button saveBtn;
        public static Button execQueryBtn;
        List<string> filesForOpen;
        List<string> filesForOpen_names;
        List<List<SQLiteConnection>> DB_connections;
        List<Dictionary<string, List<string>>> DB_info; //pair -table name-table atribs list for EACH WORKPLACE (List structure)
                                                        // List<>
        protected internal static SQLiteConnection currentConnection;
        string ActiveTable;
        string ActiveAtrib;
        public static List<Dictionary<List<string>, List<ObfuscationRule>>> createdRules; //Dictionary<List<ObfuscationRule>,List<string>>> - rules and queries
        public static TabControl DB_tab_panel_obj;
        public static int NewRuleId;
        public static string NewRuleStringform;
        public static int workplaceId;

        public static Button clickedAddRuleBtn = null;


        public MainForm()
        {
            InitializeComponent();
            deleteDialog = new DeleteRuleDialog();
            filesForOpen = new List<string>();
            filesForOpen_names = new List<string>();
            DB_connections = new List<List<SQLiteConnection>>();
            DB_info = new List<Dictionary<string, List<string>>>();
            createdRules = new List<Dictionary<List<string>, List<ObfuscationRule>>>();

            deleteDialog.Visible = false;
            del_rule_btn.Enabled = false;
            execQueryBtn = this.exec_q_btn;
            //  chng_rule_btn.Enabled = false;
            AddNewTab();
            LogObj.LogEvent(LogEventType.Session_Start, OperationStatus.Success, "", "--------------------------------------------------------------------------------");
        }
        public void AddNewTab()
        {
            TabPage DefaultPageLayout = new TabPage();
            Button openFilesBtn = new Button();
            ComboBox openFilesList = new ComboBox();
            ListBox tablesList = new ListBox();
            ListBox AttribList = new ListBox();
            TableLayoutPanel rulesTable = new TableLayoutPanel();
            DB_connections.Add(new List<SQLiteConnection>());
            createdRules.Add(new Dictionary<List<string>, List<ObfuscationRule>>());
            DB_info.Add(new Dictionary<string, List<string>>());

            rulesTable.Name = "rulesTable";
            openFilesBtn.Name = "openFilesBtn";
            openFilesList.Name = "openFilesList";
            tablesList.Name = "tablesList";
            AttribList.Name = "attribList";
            tablesList.SelectedIndexChanged += refreshAttribList;
            AttribList.SelectedIndexChanged += AttribList_SelectedIndexChanged;
            DB_tab_panel_obj = this.DB_tab_panel;

            Label FilesListLabel = new Label();
            Label TableListLabel = new Label();
            Label AttributesListLabel = new Label();
            Label RulesListLabel = new Label();

            FilesListLabel.Text = "Current DB File:";
            TableListLabel.Text = "Found tables:";
            AttributesListLabel.Text = "Found attributes:";
            RulesListLabel.Text = "Rules:";
            openFilesBtn.Text = "Open database files";

            openFilesBtn.Click += OpenFilesBtn_Click;

            openFilesBtn.Size = new Size(DB_tab_panel.Width / 4, 20);
            int nextX_LocationPoint = DB_tab_panel.Width / 4;
            int nextY_LocationPoint = 0;

            DefaultPageLayout.Text = "work_place " + tabNumber;
            DefaultPageLayout.Name = "Page_layout" + tabNumber;
            nextX_LocationPoint += DB_tab_panel.Width / 10;
            FilesListLabel.Location = new Point(nextX_LocationPoint, 0);
            openFilesList.Location = new Point(nextX_LocationPoint += DB_tab_panel.Width / 5, 0);
            openFilesList.Width = DB_tab_panel.Width - nextX_LocationPoint - nextX_LocationPoint / 10;

            nextX_LocationPoint = 0;
            nextY_LocationPoint = openFilesBtn.Height * 2;
            TableListLabel.Location = new Point(nextX_LocationPoint, nextY_LocationPoint);
            tablesList.Location = new Point(nextX_LocationPoint, nextY_LocationPoint += TableListLabel.Height);
            tablesList.Width = (int)((float)DB_tab_panel.Width / 2.1);
            tablesList.Height = (int)((float)DB_tab_panel.Height * 0.55);
            nextX_LocationPoint += tablesList.Width + tablesList.Width / 10;

            nextY_LocationPoint = openFilesBtn.Height * 2;
            AttributesListLabel.Location = new Point(nextX_LocationPoint, nextY_LocationPoint);
            AttribList.Location = new Point(nextX_LocationPoint, nextY_LocationPoint += AttributesListLabel.Height);
            AttribList.Width = (int)((float)DB_tab_panel.Width / 2.25);
            AttribList.Height = (int)((float)DB_tab_panel.Height * 0.55);


            nextX_LocationPoint = 0;
            nextY_LocationPoint = AttribList.Height + AttributesListLabel.Height * 3;
            RulesListLabel.Location = new Point(nextX_LocationPoint, nextY_LocationPoint);

            rulesTable.Location = new Point(nextX_LocationPoint, nextY_LocationPoint += RulesListLabel.Height);
            rulesTable.RowCount = 1;
            rulesTable.ColumnCount = 3;
            rulesTable.Width = DB_tab_panel.Width - DB_tab_panel.Width / 50;
            rulesTable.Height = this.Height / 4;

            rulesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            rulesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            rulesTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            rulesTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            rulesTable.Controls.Add(new Label() { Text = "Id" }, 0, 0);
            rulesTable.Controls.Add(new Label() { Text = "Attribute" }, 1, 0);
            rulesTable.Controls.Add(new Label() { Text = "Rule" }, 2, 0);
            rulesTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            rulesTable.AutoScroll = true;

            DefaultPageLayout.Controls.Add(FilesListLabel);
            DefaultPageLayout.Controls.Add(openFilesBtn);
            DefaultPageLayout.Controls.Add(openFilesList);
            DefaultPageLayout.Controls.Add(TableListLabel);
            DefaultPageLayout.Controls.Add(tablesList);
            DefaultPageLayout.Controls.Add(AttributesListLabel);
            DefaultPageLayout.Controls.Add(AttribList);
            DefaultPageLayout.Controls.Add(RulesListLabel);
            DefaultPageLayout.Controls.Add(rulesTable);
            DB_tab_panel.TabPages.Add(DefaultPageLayout);
            DB_tab_panel.SelectedIndex++;

            add_rule_btn.Enabled = false;
            tabNumber++;

            saveBtn = save_rule_button;

            string FullFileName = "";
            if (!File.Exists("settings.conf"))
            {

                MessageBox.Show(
                    "The settings file was not found. You must specify the path to save the log file."
                     , "Utility start info", MessageBoxButtons.OK, MessageBoxIcon.Error);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "log file (*.log)|*.log";
                sfd.Title = "Log file creation path";
                sfd.FileName = "ObfuscationLog.log";

                DialogResult DRes = sfd.ShowDialog();
            FCreate:
                while (DRes != DialogResult.OK && DRes != DialogResult.Yes)
                {
                    MessageBox.Show(
                     "The settings file was not found. You must specify the path to save the log file."
                      , "Settings File open error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DRes = sfd.ShowDialog();
                }

                if (!sfd.CheckFileExists)
                {
                    FullFileName = Path.GetFullPath(sfd.FileName);
                    string dir = Directory.GetCurrentDirectory();
                    using (File.Create(FullFileName))

                    using (File.Create(dir + "\\settings.conf"))

                        LogObj.SetLogFilePath(FullFileName);
                    LogObj.LogEvent(LogEventType.Settings_Change, OperationStatus.Success, "", "A new path has been set for the log file in the configuration file <settings.conf>.");
                    File.WriteAllText("settings.conf", "[log_file]=" + FullFileName + "\n");
                    File.SetAttributes("settings.conf", FileAttributes.Hidden);
                    LogObj.LogEvent(LogEventType.Settings_Change, OperationStatus.Warning, "<settings.conf> was not exist in program work directory (The directory where the executable file is located)", "");
                    LogObj.LogEvent(LogEventType.Settings_Change, OperationStatus.In_progress, "Start create <settings.conf>", "<settings.conf> was not exist in program work directory (The directory where the executable file is located).");
                    LogObj.LogEvent(LogEventType.Settings_Change, OperationStatus.Success, "", "<settings.conf> was create.New configuration value was write.");
                }
                else
                {
                    MessageBox.Show(
                     "A log file with the same name already exists in the specified directory."
                      , "Settings File open error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DRes = DialogResult.Cancel;
                    LogObj.LogEvent(LogEventType.Settings_Change, OperationStatus.Error, "", "A log file with the same name already exists in the specified directory.");
                    goto FCreate;
                }

            }
            else
            {
                FullFileName = Directory.GetCurrentDirectory();
                string conf_value = File.ReadAllText("settings.conf");
                string logFileConfAtrib = "[log_file]=";
                string path="";
                try
                {
                    path = conf_value.Substring(conf_value.IndexOf(logFileConfAtrib), conf_value.IndexOf("\n"));
                    path = path.Replace(logFileConfAtrib, "");
                }
                catch (Exception)
                {

                    path = Directory.GetCurrentDirectory() + "\\Obfuscation.log";
                    File.Create(path);
                    LogObj.LogEvent(LogEventType.Work_with_settings, OperationStatus.Error, "An error occurred while reading the value " +
                        "of the log file parameter in the configuration file. The configuration file is empty, " +
                        "damaged, or missing. The log file will be created in the following path:"+ path, "Try read config file.");

                    MessageBox.Show(
                   "An error occurred while reading the value " +
                        "of the log file parameter in the configuration file. The configuration file is empty, " +
                        "damaged, or missing. The log file will be created in the following path:" + path
                     , "Error reading configuration file.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                }
               
                LogObj.SetLogFilePath(path);
                LogObj.LogEvent(LogEventType.Work_with_settings, OperationStatus.In_progress, "Start read configuration file.", "Use <settings.conf> .");
                var lines = File.ReadAllLines("settings.conf");
                string logPath = "";
                foreach (var item in lines)
                {
                    if (item.StartsWith("[log_file]"))
                    {
                        logPath = item.Substring(item.IndexOf("="), item.Length - item.IndexOf("="));
                        logPath = logPath.Replace("=", "");

                    }
                }
                if (logPath != "")
                {
                    LogObj.SetLogFilePath(logPath);
                    LogObj.LogEvent(LogEventType.Work_with_settings, OperationStatus.Success, "The path to the log file was found successfully.", "Use <settings.conf> .");

                }
                else
                {
                    LogObj.LogEvent(LogEventType.Work_with_settings, OperationStatus.Error, "The path to the log file was not found.", "Use <settings.conf> .");
                }

            }
            //import_rule_button.Enabled = true;
            LogF_Path_label.Text = "Path to log file:" + LogObj.GetLogFilePath();
            ConfigF_path_label.Text = "Path to config file:" + FullFileName;
        }

        private void AttribList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = ((ListBox)sender);
            if (lb.SelectedIndex >= 0 && lb.SelectedIndex <= lb.Items.Count)
            {
                ActiveAtrib = lb.SelectedItem.ToString();
                add_rule_btn.Enabled = true;
            }
        }

        private void refreshAttribList(object sender, EventArgs e)
        {
            ListBox tables_list = (ListBox)sender;
            ListBox atrib_list = (ListBox)this.DB_tab_panel.SelectedTab.Controls.Find("attribList", true)[0];
            if (tables_list.SelectedIndex >= 0 && tables_list.SelectedIndex <= tables_list.Items.Count)
            {
                ActiveTable = tables_list.SelectedItem.ToString();
                atrib_list.Items.Clear();
                foreach (var item in DB_info[DB_tab_panel.SelectedIndex][tables_list.SelectedItem.ToString()])
                {
                    atrib_list.Items.Add(item);
                }
                add_rule_btn.Enabled = false;
            }
        }

        private void OpenFilesBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "sqlite db files (*.db)|*.db";
            fileDialog.Title = "Select database files";
            fileDialog.CheckFileExists = true;
            fileDialog.CheckPathExists = true;
            fileDialog.ShowDialog();
            filesForOpen.Clear();
            filesForOpen_names.Clear();
            filesForOpen.AddRange(fileDialog.FileNames.ToList<string>());
            filesForOpen_names.AddRange(fileDialog.SafeFileNames.ToList<string>());
            if (this.DB_tab_panel.SelectedTab.Controls.Find("openFilesList", true).Length != 0)
            {
                ComboBox filesL = (ComboBox)this.DB_tab_panel.SelectedTab.Controls.Find("openFilesList", true)[0];
                foreach (var item in filesForOpen_names)
                { filesL.Items.Add(item); }
                if (filesL.Items.Count > 0)
                {
                    filesL.SelectedIndex = 0;
                    openDatabaseFiles();
                    readDatabaseInfo();
                    print_DB_info_ToUI();
                    if (createdRules.Count > 0)
                    {
                        execQueryBtn.Enabled = true;
                    }
                }
            }

        }

        private void print_DB_info_ToUI()
        {
            ListBox tables_list = (ListBox)this.DB_tab_panel.SelectedTab.Controls.Find("tablesList", true)[0];
            ListBox atrib_list = (ListBox)this.DB_tab_panel.SelectedTab.Controls.Find("attribList", true)[0];
            foreach (var infoElement in DB_info[DB_tab_panel.SelectedIndex])
            {
                tables_list.Items.Add(infoElement.Key);
            }
        }

        public static void TestConnection()
        {
            if (currentConnection.State == ConnectionState.Broken)
            {
                LogObj.LogEvent(LogEventType.Connection_broken, OperationStatus.Warning, "", "Connection broken." +
                    ";Connection string:" + currentConnection.ConnectionString + ";File:" + currentConnection.FileName + ";Data Source:" + currentConnection.DataSource +
                    ";Database:" + currentConnection.Database + ";Server Version:" + currentConnection.ServerVersion + ";\n");
                currentConnection.Close();
                LogObj.LogEvent(LogEventType.Connection_closed, OperationStatus.Warning, "", "Connection broken." +
                   ";Connection string:" + currentConnection.ConnectionString + ";File:" + currentConnection.FileName + ";Data Source:" + currentConnection.DataSource +
                   ";Database:" + currentConnection.Database + ";Server Version:" + currentConnection.ServerVersion + ";\n");

            }
            if (currentConnection.State == ConnectionState.Closed)
            { currentConnection.Open(); }
        }
        private void readDatabaseInfo()
        {
            ComboBox filesL = (ComboBox)this.DB_tab_panel.SelectedTab.Controls.Find("openFilesList", true)[0];
            currentConnection = DB_connections[DB_tab_panel.SelectedIndex][filesL.SelectedIndex];
            ////test
            //DB_connections[DB_tab_panel.SelectedIndex][filesL.SelectedIndex].Open();
            //currentConnection.Close();
            //if (DB_connections[DB_tab_panel.SelectedIndex][filesL.SelectedIndex].State == ConnectionState.Closed)
            //{
            //    currentConnection.Close();
            //}
            ////
            TestConnection();


            SQLiteCommand sQCmd = new SQLiteCommand("select name from sqlite_master where type='table'" +
                "and name not like 'sqlite_%';", currentConnection);
            SQLiteDataReader dataReader = sQCmd.ExecuteReader();

            LogObj.LogEvent(LogEventType.Query_start, OperationStatus.Success, "", "Query exec start." +
                 "Command text:" + sQCmd.CommandText + "Command type:" + sQCmd.CommandType + ";Connection string:" + currentConnection.ConnectionString + ";File:" + currentConnection.FileName + ";Data Source:" + currentConnection.DataSource +
                 ";Database:" + currentConnection.Database + ";Server Version:" + currentConnection.ServerVersion + ";\n");


            while (dataReader.Read())
            {
                string queryData = dataReader.GetString(0);
                if (!DB_info[DB_tab_panel.SelectedIndex].ContainsKey(queryData))
                {
                    SQLiteCommand sQCmd1 = new SQLiteCommand("SELECT name FROM PRAGMA_TABLE_INFO('" + queryData + "');", currentConnection);
                    SQLiteDataReader dataReader1 = sQCmd1.ExecuteReader();
                    List<string> tablesAttrib = new List<string>();
                    while (dataReader1.Read())
                    {
                        LogObj.LogEvent(LogEventType.Query_in_progress, OperationStatus.Success, "", "Query exec start." +
               "Command text:" + sQCmd1.CommandText + "Command type:" + sQCmd1.CommandType + ";Connection string:" + currentConnection.ConnectionString + ";File:" + currentConnection.FileName + ";Data Source:" + currentConnection.DataSource +
               ";Database:" + currentConnection.Database + ";Server Version:" + currentConnection.ServerVersion + ";\n");

                        tablesAttrib.Add(dataReader1.GetString(0));
                    }
                    DB_info[DB_tab_panel.SelectedIndex].Add(queryData, tablesAttrib);
                }
            }
            LogObj.LogEvent(LogEventType.Query_was_exec, OperationStatus.Success, "Exec done", "Query was exec." +
     "Command text:" + sQCmd.CommandText + "Command type:" + sQCmd.CommandType + ";Connection string:" + currentConnection.ConnectionString + ";File:" + currentConnection.FileName + ";Data Source:" + currentConnection.DataSource +
     ";Database:" + currentConnection.Database + ";Server Version:" + currentConnection.ServerVersion + ";\n");
            currentConnection.Close();
        }

        private void openDatabaseFiles()
        {
            foreach (var currentFile in filesForOpen)
            {
                if (File.Exists(currentFile))
                {
                    LogObj.LogEvent(LogEventType.File_Open, OperationStatus.Success, "DB file was exist.", "File:" + currentFile);
                    DB_connections[DB_tab_panel.SelectedIndex].Add(
                        new SQLiteConnection(@"Data Source=" + currentFile + "; Version=3;"));
                }
                else
                {
                    LogObj.LogEvent(LogEventType.File_Open, OperationStatus.Error, "DB file was not exist.", "File:" + currentFile);
                    MessageBox.Show(
                        "The file:" + currentFile +
                        " -  does not exist or is not available. It will not" +
                        " be added to the list of open files."
                        , "File open error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    filesForOpen.Remove(currentFile);
                }
            }
        }

        private void add_tab_btn_Click(object sender, EventArgs e)
        {
            AddNewTab();
            rem_tab_btn.Enabled = true;
            add_tab_btn.Enabled = true;
        }

        private void rem_tab_btn_Click(object sender, EventArgs e)
        {
            DB_connections.RemoveAt(DB_tab_panel.SelectedIndex);
            createdRules.RemoveAt(DB_tab_panel.SelectedIndex);
            DB_info.RemoveAt(DB_tab_panel.SelectedIndex);
            DB_tab_panel.TabPages.RemoveAt(DB_tab_panel.SelectedIndex);
            if (DB_tab_panel.TabPages.Count == 0)
            {
                tabNumber = 1;
                rem_tab_btn.Enabled = false;
                add_tab_btn.Enabled = true;

                add_rule_btn.Enabled = false;
                del_rule_btn.Enabled = false;
                execQueryBtn.Enabled = false;
                import_rule_button.Enabled = false;
                save_rule_button.Enabled = false;



            }
            else
            {
                add_rule_btn.Enabled = true;
                del_rule_btn.Enabled = true;
                execQueryBtn.Enabled = true;
                import_rule_button.Enabled = true;
                save_rule_button.Enabled = true;
            }
        }

        int currentRuleId = 0;
        private void add_rule_btn_Click(object sender, EventArgs e)
        {
            if (CheckКuleСonsistency(ActiveAtrib))
            {
                LogObj.LogEvent(LogEventType.Rule_Create, OperationStatus.In_progress, "Rule create start", ";Rule id:" + NewRuleId + ";Rule for:" + NewRuleStringform + ";");

                NewRuleId = currentRuleId;
                NewRuleStringform = ActiveTable + "." + ActiveAtrib;
                TableLayoutPanel RTable = (TableLayoutPanel)DB_tab_panel.SelectedTab.Controls["rulesTable"];
                RTable.Controls.Add(new Label() { Text = NewRuleId + "", Name = "RuleIdLabel" + (currentRuleId) }, 0, RTable.Controls.Count);
                RTable.Controls.Add(new Label() { Text = NewRuleStringform, Name = "RuleAtribLabel" + (currentRuleId), Width = RTable.Width / 3 }, 1, RTable.Controls.Count - 1);
                RTable.Controls.Add(new Button() { Text = "set rule" + "", Name = "RuleIdBtn" + (currentRuleId) }, 2, RTable.Controls.Count - 2);
                clickedAddRuleBtn = (Button)RTable.Controls.Find("RuleIdBtn" + (currentRuleId), true)[0];
                ((Button)RTable.Controls[RTable.Controls.Count - 1]).Click += SetRuleBtnClick;

                del_rule_btn.Enabled = true;
                //chng_rule_btn.Enabled = true;
                currentRuleId++;
            }

        }
        private bool CheckКuleСonsistency(string attrib)
        {
            if (createdRules.Count > 0)
            {
                string msgText = "";
                foreach (var item in createdRules)
                {
                    foreach (var item1 in item.Values)
                    {
                        foreach (var item2 in item1)
                        {
                            if (item2.GetRuleAttrib() == attrib)
                            {
                                LogObj.LogEvent(LogEventType.Rule_Create, OperationStatus.Warning, "The rule has already been created.", msgText);

                                if (item2.GetBindedAtrib != null)
                                {
                                    msgText = "The list of rules already contains a rule for this attribute. " +
                                    "Depending on the logic of the rule you created, " +
                                    "they may contradict. Would you like to add" +
                                    " another rule for this attribute?";
                                }
                                else
                                {
                                    msgText = "There is already a more global rule for" +
                                        " this attribute in the list of rules - that of the where subquery." +
                                        " Depending on the order in which the rules are executed, a global rule may have an overlapping effect. " +
                                        "Would you like to add another rule for this attribute?";
                                }
                                DialogResult dr = MessageBox.Show(msgText,
                                    "The rule has already been created.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == DialogResult.Yes)
                                {
                                    LogObj.LogEvent(LogEventType.Rule_Create, OperationStatus.Success, "Rule is not consistency.The rule has  been created.", msgText);
                                    return true;
                                }
                                else
                                {
                                    LogObj.LogEvent(LogEventType.Rule_Create, OperationStatus.Error, "The rule create was cancel by user.", msgText);

                                    return false;
                                }
                            }
                        }

                    }



                }
                return true;
            }
            else
            {
                return true;
            }
        }
        private void SetRuleBtnClick(object sender, EventArgs e)
        {
            RuleSettingsForm ruleSettingsForm = new RuleSettingsForm();
            ruleSettingsForm.Show();
        }

        private void del_rule_btn_Click(object sender, EventArgs e)
        {
            LogObj.LogEvent(LogEventType.Rule_Delete, OperationStatus.In_progress, "Rule delete start", "");

            currentTab = DB_tab_panel.SelectedTab;
            currentTabIndex = DB_tab_panel.SelectedIndex;
            if (currentTab.Controls["rulesTable"].Controls.Count == 3)
            {
                del_rule_btn.Enabled = false;
                //chng_rule_btn.Enabled = false;
            }
            if (deleteDialog.IsDisposed)
            {
                deleteDialog = new DeleteRuleDialog();
            }
            deleteDialog.Visible = true;
        }

        private void DB_tab_panel_SelectedIndexChanged(object sender, EventArgs e)
        {
            workplaceId = DB_tab_panel.SelectedIndex;
            if (workplaceId < 0)
            {
                workplaceId = 0;
            }
            if (createdRules.Count > workplaceId && createdRules[workplaceId].Count > 0)
            {
                saveBtn.Enabled = true;
                del_rule_btn.Enabled = true;
            }
            else
            {
                saveBtn.Enabled = false;
                del_rule_btn.Enabled = false;
            }
        }

        private void save_rule_button_Click(object sender, EventArgs e)
        {
            if (createdRules.Count > 0)
            {
                LogObj.LogEvent(LogEventType.Rule_Export, OperationStatus.In_progress, "Open SaveRule_DialogForm", "save_rule_button_Click");
                SaveRule_DialogForm srdf = new SaveRule_DialogForm();
                srdf.Show();
            }
            else
            {
                LogObj.LogEvent(LogEventType.Rule_Export, OperationStatus.Error, "No rules have been created yet!", "save_rule_button_Click");

                MessageBox.Show(
                       "No rules have been created yet!"
                       , "Save/Export rule error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void import_rule_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog importFileDialog = new OpenFileDialog();
            importFileDialog.Filter = "sqlite db files (*.json)|*.json";
            importFileDialog.Title = "Select  json rule files";
            importFileDialog.CheckFileExists = true;
            importFileDialog.CheckPathExists = true;
            importFileDialog.ShowDialog();
            importRule(importFileDialog.FileNames);

        }

        private void importRule(string[] fileNames)
        {
            bool hasError_flag = false;
            string errorFiles = "";
            TableLayoutPanel RTable = (TableLayoutPanel)DB_tab_panel.SelectedTab.Controls["rulesTable"];
            for (int i = 0; i < fileNames.Length; i++)
            {
                try
                {
                    KeyValuePair<List<string>, List<ObfuscationRule>> rule = JsonConvert.DeserializeObject<KeyValuePair<List<string>, List<ObfuscationRule>>>(File.ReadAllText(fileNames[i]));
                    createdRules[workplaceId].Add(rule.Key, rule.Value);

                    currentRuleId = createdRules.Count - 1;
                    NewRuleId = currentRuleId;

                    RTable.Controls.Add(new Label() { Text = NewRuleId + "", Name = "RuleIdLabel" + (currentRuleId) }, 0, RTable.Controls.Count);
                    RTable.Controls.Add(new Label() { Text = rule.Value[0].GetRuleTextForm(), Name = "RuleAtribLabel" + (currentRuleId), Width = RTable.Width / 3 }, 1, RTable.Controls.Count - 1);
                    RTable.Controls.Add(new Label() { Text = rule.Key[0], Name = "RuleIdLabel" + (currentRuleId), Width = RTable.Width }, 2, RTable.Controls.Count - 2);

                    LogObj.LogEvent(LogEventType.Rule_import, OperationStatus.Success, "", "Import from:" + fileNames[i] + ";Rule id:" + NewRuleId + ";Rule for:" + rule.Value[0].GetRuleTextForm() + ";Rule:" + rule.Key[0]);

                }
                catch (Exception)
                {
                    LogObj.LogEvent(LogEventType.Rule_import, OperationStatus.Error,
                        "File:" + fileNames[i] + " The file has an inappropriate internal structure. ",
                        "The file is not a rule. File was not imported.The importing of the remaining files will continue.");

                    hasError_flag = true;
                    MessageBox.Show(
                        "File:" + fileNames[i] + " The file has an inappropriate internal structure. " +
                        "The file is not a rule. File was not imported.The importing of the remaining files will continue."
                        , "Import rule error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorFiles += fileNames[i] + "\n";
                }

            }
            if (!hasError_flag)
            {
                LogObj.LogEvent(LogEventType.Rule_import, OperationStatus.Success,
                       "Import rule success", "All files were imported successfully.");
                MessageBox.Show(
                       "All files were imported successfully.", "Import rule success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                LogObj.LogEvent(LogEventType.Rule_import, OperationStatus.Warning,
                     "Import rule warning", "All files except:\n" + errorFiles + "\n were imported successfully.");
                MessageBox.Show("All files except:\n" + errorFiles + "\n were imported successfully.", "Import rule success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            save_rule_button.Enabled = true;
            if (filesForOpen.Count > 0)
            {
                execQueryBtn.Enabled = true;
            }


            currentRuleId++;
        }

        private void chng_rule_btn_Click(object sender, EventArgs e)
        {

        }

        private void exec_q_btn_Click(object sender, EventArgs e)
        {
            if (createdRules.Count > 0)
            {
                if (All_WorkspaceExecFlag_checkBox.Checked)
                {

                    foreach (var item in createdRules)
                    {
                        var d = item.Keys;
                        foreach (var item1 in d)
                        {
                            foreach (var item2 in item1)
                            {
                                try
                                {
                                    SQLiteCommand sQCmd1 = new SQLiteCommand(item2, currentConnection);
                                    int rowsAffected = sQCmd1.ExecuteNonQuery();
                                    LogObj.LogEvent(LogEventType.Query_was_exec, OperationStatus.Success, "", "rows affected:" + rowsAffected + ";\n");
                                }
                                catch (Exception ex)
                                {

                                    LogObj.LogEvent(LogEventType.Query_was_exec, OperationStatus.Error, "", "Error message:" + ex.Message + ";StackTrace:" + ex.StackTrace + "\n");

                                }

                            }

                        }
                        MessageBox.Show("All rules was execute.", "Execute rule success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
                else if (createdRules.Count > currentTabIndex)
                {
                    var d = createdRules[currentTabIndex].Keys;
                    foreach (var item1 in d)
                    {
                        foreach (var item2 in item1)
                        {
                            try
                            {
                                SQLiteCommand sQCmd1 = new SQLiteCommand(item2, currentConnection);
                                int rowsAffected = sQCmd1.ExecuteNonQuery();
                                LogObj.LogEvent(LogEventType.Query_was_exec, OperationStatus.Success, "", "rows affected:" + rowsAffected + ";\n");
                            }
                            catch (Exception ex)
                            {

                                LogObj.LogEvent(LogEventType.Query_was_exec, OperationStatus.Error, "", "Error message:" + ex.Message + ";StackTrace:" + ex.StackTrace + "\n");
                                MessageBox.Show("Execute rule error.\n" + "Error message:" + ex.Message + ";StackTrace:" + ex.StackTrace + "\n"
                                    , "Execute rule error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }

                        }

                    }
                    MessageBox.Show("All rules was execute.", "Execute rule success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No rules created yet.", "Execute rule error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogObj.LogEvent(LogEventType.Query_was_exec, OperationStatus.Error, "No rules created yet.", "Execute rule error.\n");

            }
        }
    }
}

