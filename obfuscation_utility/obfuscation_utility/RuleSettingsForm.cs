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
using Newtonsoft.Json;
using System.IO;

namespace obfuscation_utility
{
    enum db_types
    {
        int_type = 0,
        text_type = 1,
        real_type = 2,
        numeric_type = 3
    }
    enum DBMS_Name
    {
        sqlite = 0,
        mysql = 1,
        ms_sql = 2,
        oracle = 3,
        postgreSql = 4
    }
    public partial class RuleSettingsForm : Form
    {
        int RuleId; //ok
        string RuleTextForm; //ok
        string RuleAttrib; //ok
        string atribType;//ok
        string RuleTable;//ok
        object RuleOldValue;//ok
        object RuleNewValue;
        object RuleState;//ok
        List<RuleMoificators> activeModificators;

        string bindedAtrib = null;
        double bindedAtribValue = -100000;

        static string pathToJson_countriesF = Path.Combine(Environment.CurrentDirectory, @"data\", "countries.json");
        static List<CountryObj> countries;
        static Dictionary<string, List<CountryObj>> worldPartSet;
        ObfuscationRule.GeographiclObj geographiclObj = null;
        string replacePattern="";

        bool intTypeFlag = false;
        bool textTypeFlag_ = false;
        bool realTypeFlag = false;
        bool dateTypeFlag = false;
        bool boolTypeFlag = false;

        string OldGeographicalLocation = "";
        string NewGeographicalLocation = "";
        double OldLat = -1;
        double OldLon = -1;
        double LatBound = -1;
        double LonBound = -1;

        List<string> intTypesList;
        List<string> textTypesList;
        List<string> realTypesList;
        List<string> numericTypesList;
        SQLiteCommand qLiteCommand;
        public RuleSettingsForm()
        {
            qLiteCommand = new SQLiteCommand(MainForm.currentConnection);
            InitializeComponent();
            RuleId = MainForm.NewRuleId;
            RuleTextForm = MainForm.NewRuleStringform;
            RuleAttrib = RuleTextForm.Split('.')[1];
            RuleTable = RuleTextForm.Split('.')[0];
            try
            {
                MainForm.TestConnection();

                qLiteCommand.CommandText = "select type from pragma_table_info('" + RuleTable + "') where name='" + RuleAttrib + "';";
                object retVal = qLiteCommand.ExecuteScalar();
                atribType = retVal == null ? "--" : retVal.ToString();

                intTypesList = GetDBTypes(DBMS_Name.sqlite, db_types.int_type).Split(',').ToList<string>();
                intTypesList.AddRange(intTypesList.ConvertAll<string>(d => d.ToLower()));

                textTypesList = GetDBTypes(DBMS_Name.sqlite, db_types.text_type).Split(',').ToList<string>();
                textTypesList.AddRange(textTypesList.ConvertAll<string>(d => d.ToLower()));

                realTypesList = GetDBTypes(DBMS_Name.sqlite, db_types.real_type).Split(',').ToList<string>();
                realTypesList.AddRange(realTypesList.ConvertAll<string>(d => d.ToLower()));

                numericTypesList = GetDBTypes(DBMS_Name.sqlite, db_types.numeric_type).Split(',').ToList<string>();
                numericTypesList.AddRange(numericTypesList.ConvertAll<string>(d => d.ToLower()));
                intTypeFlag = false;
                textTypeFlag_ = false;
                realTypeFlag = false;
                dateTypeFlag = false;
                boolTypeFlag = false;

                if (intTypesList.Contains(atribType) ||
                    textTypesList.Contains(atribType) ||
                    realTypesList.Contains(atribType))
                {
                    old_atrib_val_groupBox.Controls.Add(new TextBox() { Name = "Old_atrib_value_textbox" });
                    new_atrib_val_groupBox.Controls.Add(new TextBox() { Name = "New_atrib_value_textbox" });
                    ((TextBox)old_atrib_val_groupBox.Controls["Old_atrib_value_textbox"]).TextChanged += oldValueChanged;
                    ((TextBox)new_atrib_val_groupBox.Controls["New_atrib_value_textbox"]).TextChanged += newValueChanged;
                    //create stateList
                    if (intTypesList.Contains(atribType))
                    {
                        state_comboBox.Items.Add("equal");
                        state_comboBox.Items.Add("less");
                        state_comboBox.Items.Add("more");
                        state_comboBox.Items.Add("not equal");
                        state_comboBox.Items.Add("less or equal");
                        state_comboBox.Items.Add("more or equal");
                        intTypeFlag = true;
                    }
                    else if (textTypesList.Contains(atribType))
                    {
                        state_comboBox.Items.Add("equal");
                        state_comboBox.Items.Add("not equal");
                        state_comboBox.Items.Add("is_substr");
                        state_comboBox.Items.Add("is_not_substr");
                        textTypeFlag_ = true;
                    }
                    else if (realTypesList.Contains(atribType))
                    {
                        state_comboBox.Items.Add("equal");
                        state_comboBox.Items.Add("less");
                        state_comboBox.Items.Add("more");
                        state_comboBox.Items.Add("not equal");
                        state_comboBox.Items.Add("less or equal");
                        state_comboBox.Items.Add("more or equal");
                        realTypeFlag = true;
                    }
                }

                else if (numericTypesList.Contains(atribType))
                {
                    if (atribType == "DATE" || atribType == "date")
                    {
                        old_atrib_val_groupBox.Controls.Add(new DateTimePicker() { Name = "Old_atrib_value_datePicker", Format = DateTimePickerFormat.Custom });
                        new_atrib_val_groupBox.Controls.Add(new DateTimePicker() { Name = "New_atrib_value_datePicker", Format = DateTimePickerFormat.Custom });
                        ((DateTimePicker)old_atrib_val_groupBox.Controls["Old_atrib_value_datePicker"]).ValueChanged += oldValueChanged;
                        ((DateTimePicker)new_atrib_val_groupBox.Controls["New_atrib_value_datePicker"]).ValueChanged += newValueChanged;
                        state_comboBox.Items.Add("equal");
                        state_comboBox.Items.Add("earlier");
                        state_comboBox.Items.Add("later");
                        dateTypeFlag = true;
                    }
                    else if (atribType == "DATETIME" || atribType == "datetime")
                    {
                        old_atrib_val_groupBox.Controls.Add(new DateTimePicker() { Name = "Old_atrib_value_dateTimePicker", Format = DateTimePickerFormat.Time });
                        new_atrib_val_groupBox.Controls.Add(new DateTimePicker() { Name = "New_atrib_value_dateTimePicker", Format = DateTimePickerFormat.Time });
                        ((DateTimePicker)old_atrib_val_groupBox.Controls["Old_atrib_value_dateTimePicker"]).ValueChanged += oldValueChanged;
                        ((DateTimePicker)new_atrib_val_groupBox.Controls["New_atrib_value_dateTimePicker"]).ValueChanged += newValueChanged;
                        state_comboBox.Items.Add("equal");
                        state_comboBox.Items.Add("earlier");
                        state_comboBox.Items.Add("later");
                        dateTypeFlag = true;
                    }
                    else if (atribType == "BOOLEAN" || atribType == "boolean")
                    {
                        old_atrib_val_groupBox.Controls.Add(new CheckBox() { Name = "Old_atrib_value_checkbox" });
                        new_atrib_val_groupBox.Controls.Add(new CheckBox() { Name = "New_atrib_value_checkbox" });
                        ((CheckBox)old_atrib_val_groupBox.Controls["Old_atrib_value_checkbox"]).CheckedChanged += oldValueChanged;
                        ((CheckBox)new_atrib_val_groupBox.Controls["New_atrib_value_checkbox"]).CheckedChanged += newValueChanged;
                        boolTypeFlag = true;
                    }
                    else
                    {
                        old_atrib_val_groupBox.Controls.Add(new TextBox() { Name = "Old_atrib_value_textbox" });
                        new_atrib_val_groupBox.Controls.Add(new TextBox() { Name = "New_atrib_value_textbox" });
                        old_atrib_val_groupBox.Controls["Old_atrib_value_textbox"].TextChanged += oldValueChanged;
                        new_atrib_val_groupBox.Controls["New_atrib_value_textbox"].TextChanged += newValueChanged;
                        textTypeFlag_ = true;
                    }
                }
                activeModificators = new List<RuleMoificators>();
               
                state_comboBox.SelectedIndexChanged += RuleStateChange;
                state_comboBox.SelectedIndex = 0;
           ;
            }
            catch (Exception)
            {
                throw;
            }

            RuleFor_label.Text = "Rule for: " + RuleTextForm;
            if (File.Exists(pathToJson_countriesF))
            {
                string json = File.ReadAllText(pathToJson_countriesF);
                //json.Replace(".","\\.");
                countries = JsonConvert.DeserializeObject<List<CountryObj>>(json);
                worldPartSet = new Dictionary<string, List<CountryObj>>();
                countryList_comboBox.Items.Clear();
                worldPartList_comboBox.Items.Clear();

                for (int i = 0; i < countries.Count; i++)
                {
                    countryList_comboBox.Items.Add(countries[i].CountryName);
                    if (!worldPartSet.ContainsKey(countries[i].Timezone[0].Substring(0, countries[i].Timezone[0].IndexOf("/"))))
                    {
                        worldPartSet.Add(countries[i].Timezone[0].Substring(0, countries[i].Timezone[0].IndexOf("/")), new List<CountryObj>());
                    }
                    //if (worldPartSet[countries[i].Timezone[0].Substring(0, countries[i].Timezone[0].IndexOf("/"))]==null)
                    //{
                    //    worldPartSet[countries[i].Timezone[0].Substring(0, countries[i].Timezone[0].IndexOf("/"))] = new List<CountryObj>();
                    //}
                    worldPartSet[countries[i].Timezone[0].Substring(0, countries[i].Timezone[0].IndexOf("/"))].Add(countries[i]);
                }
                foreach (var wp in worldPartSet)
                {
                    worldPartList_comboBox.Items.Add(wp.Key);
                }

                SQLiteCommand sQCmd1 = new SQLiteCommand("SELECT name FROM PRAGMA_TABLE_INFO('" + RuleTable + "');", MainForm.currentConnection);
                SQLiteDataReader dataReader1 = sQCmd1.ExecuteReader();
                List<string> tablesAttrib = new List<string>();
                while (dataReader1.Read())
                {
                    tablesAttrib.Add(dataReader1.GetString(0));
                }
                tablesAttrib.Remove(RuleAttrib); //remove the attribute to which we are binding
                foreach (var item in tablesAttrib)
                {
                    atribBind_comboBox.Items.Add(item);
                }

                worldPartList_comboBox.SelectedIndex = 0;
                countryList_comboBox.SelectedIndex = 0;
                atribBind_comboBox.SelectedIndex = 0;
            }
        }

        private void RuleStateChange(object sender, EventArgs e)
        {
            if (intTypeFlag || realTypeFlag)
            {
                switch (state_comboBox.SelectedItem.ToString())
                {
                    case "equal":
                        RuleState = IntRuleState.equal;
                        break;
                    case "less":
                        RuleState = IntRuleState.less;
                        break;
                    case "more":
                        RuleState = IntRuleState.more;
                        break;
                    case "not equal":
                        RuleState = IntRuleState.not_equeal;
                        break;
                    case "less or equal":
                        RuleState = IntRuleState.less_or_equal;
                        break;
                    case "more or equal":
                        RuleState = IntRuleState.more_or_equal;
                        break;
                    default:
                        break;
                }
            }
            else if (textTypeFlag_)
            {
                switch (state_comboBox.SelectedItem.ToString())
                {
                    case "equal":
                        RuleState = StringRuleState.equal;
                        break;
                    case "not equal":
                        RuleState = StringRuleState.not_equeal;
                        break;
                    default:
                        break;
                }
            }
            else if (dateTypeFlag)
            {
                switch (state_comboBox.SelectedItem.ToString())
                {
                    case "equal":
                        RuleState = DateRuleState.equal;
                        break;
                    case "earlier":
                        RuleState = DateRuleState.earlier;
                        break;
                    case "later":
                        RuleState = DateRuleState.later;
                        break;
                    default:
                        break;
                }
            }
            else if (boolTypeFlag)
            {
                switch (state_comboBox.SelectedItem.ToString())
                {
                    case "equal":
                        RuleState = BoolRuleState.equal;
                        break;
                    case "not equal":
                        RuleState = BoolRuleState.not_equeal;
                        break;
                    default:
                        break;
                }
            }
        }

        private void newValueChanged(object sender, EventArgs e)
        {
            if (boolTypeFlag)
            {
                RuleNewValue = ((CheckBox)sender).Checked;
            }
           else if (dateTypeFlag)
            {
                RuleNewValue = ((DateTimePicker)sender).Value;
            }
          else
            {
                RuleNewValue = ((TextBox)sender).Text;
            }
        }

        private void oldValueChanged(object sender, EventArgs e)
        {
            if (boolTypeFlag)
            {
                RuleOldValue = ((CheckBox)sender).Checked;
            }
            else if (dateTypeFlag)
            {
                RuleOldValue = ((DateTimePicker)sender).Value;
            }
            else
            {
                RuleOldValue = ((TextBox)sender).Text;
            }
            //  RuleOldValue
        }

        static string GetDBTypes(DBMS_Name dbms, db_types type)
        {
            switch (dbms)
            {
                case DBMS_Name.sqlite:

                    switch (type)
                    {
                        case db_types.int_type:
                            return "INT,INTEGER,TINYINT,SMALLINT,MEDIUMINT,BIGINT,UNSIGNED BIG INT,INT2,INT8";

                        case db_types.text_type:
                            return "CHAR,CHARACTER,VARCHAR,VARYING CHARACTER,NCHAR,NATIVE CHARACTER,NVARCHAR,TEXT,CLOB";

                        case db_types.real_type:
                            return "REAL,DOUBLE,DOUBLE PRECISION,FLOAT";


                        case db_types.numeric_type:
                            return "NUMERIC,DECIMAL,BOOLEAN,DATE,DATETIME";
                        default:
                            return "CHARACTER,VARCHAR,VARYING CHARACTER,NCHAR,NATIVE CHARACTER,NVARCHAR,TEXT,CLOB";
                    }
                    break;
                case DBMS_Name.mysql:
                    return "";

                case DBMS_Name.ms_sql:
                    return "";
                case DBMS_Name.oracle:
                    return "";
                case DBMS_Name.postgreSql:
                    return "";
                default:
                    return "";
            }
        }

       
        private void save_rule_button_Click(object sender, EventArgs e)
        {
            bool bindOkFlag = true;
            if (Mod_enable_checkBox.Checked)
            {
                if (!activeModificators.Contains(RuleMoificators.is_geographical_coords))
                {
                    activeModificators.Add(RuleMoificators.is_geographical_coords);
                }
                if (contryList_radioButton.Checked)
                {
                    LatBound = (countries[countryList_comboBox.SelectedIndex].Lating[0]);
                    LonBound = (countries[countryList_comboBox.SelectedIndex].Lating[1]);
                    for (int i = 0; i < countries.Count; i++)
                    {
                        if (countries[i].CountryName == countryList_comboBox.SelectedItem.ToString())
                        {
                            string timeZoneString = countries[i].Timezone[0];
                            NewGeographicalLocation = timeZoneString.Substring(0, timeZoneString.IndexOf("/"));
                            break;
                        }
                       
                    }
                }
                else if (worldPart_radioButton.Checked)
                {
                    Random rnd = new Random();
                    int listLength = worldPartSet[worldPartList_comboBox.SelectedItem.ToString()].Count - 1;
                    LatBound = (worldPartSet[worldPartList_comboBox.SelectedItem.ToString()][rnd.Next(0, listLength)].Lating[0]);
                    LonBound = (worldPartSet[worldPartList_comboBox.SelectedItem.ToString()][rnd.Next(0, listLength)].Lating[1]);
                    string timeZoneString = worldPartSet[worldPartList_comboBox.SelectedItem.ToString()][rnd.Next(0, listLength)].Timezone[0];
                    NewGeographicalLocation = timeZoneString.Substring(0, timeZoneString.IndexOf("/"));
                }
                else // by cords
                {
                    if (BindLatLon_checkBox.Checked) // with bind
                    {
                        bindOkFlag = false;

                        double Lat_bound = 1, Lon_bound = 1;
                        Double.TryParse(Lat_textBox.Text, out Lat_bound);
                        LatBound = Lat_bound;

                        Double.TryParse(Lon_textBox.Text, out Lon_bound);
                        LonBound = Lon_bound;
                    }
                    else //without bind
                    {
                        bindOkFlag = true;
                        double bound = 1;
                        if (Lat_radioButton.Checked)
                        {
                            Double.TryParse(Lat_textBox.Text, out bound);
                            LatBound = bound;
                        }
                        if (Lon_radioButton.Checked)
                        {
                            Double.TryParse(Lon_textBox.Text, out bound);
                            LonBound = bound;
                        }
                    }
                }
                if (!UpdateAll_checkBox.Checked)
                {
                    if (RuleOldValue!=null)
                    {
                        if (Lat_radioButton.Checked)
                        {
                            OldLat = Double.Parse(RuleOldValue.ToString());
                        }
                        if (Lon_radioButton.Checked)
                        {
                            OldLon = Double.Parse(RuleOldValue.ToString());
                        }
                    }
                   
                    
                }

                //check bind attrib
                // then   bindOkFlag = true;
                if (BindLatLon_checkBox.Checked)
                {
                    bindOkFlag = false;
                    bindedAtrib = atribBind_comboBox.SelectedItem.ToString();
                    qLiteCommand.CommandText = "select type from pragma_table_info('" + RuleTable + "') where name='" + bindedAtrib + "';";
                    object retVal = qLiteCommand.ExecuteScalar();
                    atribType = retVal == null ? "--" : retVal.ToString();
                    if (intTypesList.Contains(atribType) ||
                         realTypesList.Contains(atribType))
                    {
                        bindOkFlag = true;
                    }
                }

                if (bindOkFlag)
                {
                    geographiclObj = new ObfuscationRule.GeographiclObj(OldLat, OldLon, LatBound, LonBound, NewGeographicalLocation, OldGeographicalLocation);
                    if (Lat_radioButton.Checked)
                    { RuleNewValue = geographiclObj.getNewLat(); }
                    if (Lon_radioButton.Checked)
                    { RuleNewValue = geographiclObj.getNewLon(); }
                    if (BindLatLon_checkBox.Checked)
                    {
                        if (Lat_radioButton.Checked)
                        {
                            RuleNewValue = geographiclObj.getNewLat();
                            bindedAtribValue = geographiclObj.getNewLon();
                        }
                        if (Lon_radioButton.Checked)
                        {
                            RuleNewValue = geographiclObj.getNewLon();
                            bindedAtribValue = geographiclObj.getNewLat();
                        }
                    }

                  
                }
            }
            else { activeModificators.Clear(); }

            if (!UpdateAll_checkBox.Checked)
            {
                if (RuleOldValue != null && RuleNewValue != null)
                {
                    createRule();
                }
                else
                {
                    MessageBox.Show(
                            "Rule attribute new or old value not set yet. Please set this values."
                             , "Attribute value error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (RuleNewValue != null)
                {
                    createRule();
                }
                else
                {
                    MessageBox.Show(
                            "Rule attribute new value not set yet. Please set this value."
                             , "Attribute value error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
          
        }
        private void createRule()
        {
            ObfuscationRule rule = new ObfuscationRule(RuleId, RuleTextForm, RuleAttrib, atribType, RuleTable, RuleOldValue,
               RuleNewValue, RuleState, replacePattern, activeModificators, geographiclObj, bindedAtrib, bindedAtribValue);
            string RuleQuery = ConstructUpdateQuery(rule);
            if (MainForm.createdRules.Count<MainForm.currentTabIndex)
            {
                //shit
                while (MainForm.createdRules.Count < MainForm.currentTabIndex)
                {
                    MainForm.createdRules.Add(new Dictionary<List<string>, List<ObfuscationRule>>());
                }
              
            }
            MainForm.createdRules[MainForm.currentTabIndex].Add(new List<string> { RuleQuery }, new List<ObfuscationRule>() { rule });
            MainForm.saveBtn.Enabled = true;
            MainForm.execQueryBtn.Enabled = true;
            LogObj.LogEvent(LogEventType.Rule_Create, OperationStatus.Success,
                "Rule was vcreate", ";Rule id:" + RuleId + ";Rule for:" + RuleTable+"."+ RuleAttrib + 
                ";"+"Rule:"+ RuleTextForm+";Constructed query:"+ RuleQuery + ";");
          //hide btn
            if (MainForm.clickedAddRuleBtn!=null)
            {
                MainForm.clickedAddRuleBtn.Enabled = false;
                MainForm.clickedAddRuleBtn.Visible = false;
            }
          
            this.Close();
        }
        private string ConstructUpdateQuery(ObfuscationRule rule)
        {
            string whereSubquery = "";
            string bindedAtribQueryPart = "";
            string operation = "";
            string startDate = "1970-01-01";


            if (intTypesList.Contains(atribType) ||
                realTypesList.Contains(atribType))
            {
                switch ((IntRuleState)(rule.GetRuleState()))
                {
                    case IntRuleState.more:
                        operation = ">";
                        break;
                    case IntRuleState.less:
                        operation = "<";
                        break;
                    case IntRuleState.equal:
                        operation = "=";
                        break;
                    case IntRuleState.not_equeal:
                        operation = "!=";
                        break;
                    case IntRuleState.less_or_equal:
                        operation = "<=";
                        break;
                    case IntRuleState.more_or_equal:
                        operation = ">=";
                        break;
                    default:
                        break;
                }
            }
            else if (textTypesList.Contains(atribType))
            {
                switch ((StringRuleState)(rule.GetRuleState()))
                {
                    case StringRuleState.equal:
                        operation = "=";
                        break;
                    case StringRuleState.is_substr:
                        operation=  "LIKE %";
                        break;
                    case StringRuleState.is_not_substr:
                        operation = "NOT LIKE %";
                        break;
                    case StringRuleState.not_equeal:
                        operation = "!=";
                        break;
                    default:
                        break;
                }
            }
            else if (atribType == "DATETIME" || atribType == "datetime")
            {
                switch ((DateRuleState)(rule.GetRuleState()))
                {
                    case DateRuleState.earlier:
                        operation = "BETWEEN " +startDate+" AND ";
                        break;
                    case DateRuleState.equal:
                        operation = "=";
                        break;
                    case DateRuleState.later:
                        operation = "NOT BETWEEN" +startDate+ "AND ";
                        break;
                    default:
                        break;
                }
            }
            else // bool
            {
                switch ((BoolRuleState)(rule.GetRuleState()))
                {
                    case BoolRuleState.equal:
                        operation = "=";
                        break;
                    case BoolRuleState.not_equeal:
                        operation = "!=";
                        break;
                    default:
                        break;
                }
            }
                
            if (!UpdateAll_checkBox.Checked)
            {
                whereSubquery = " WHERE " + rule.GetRuleAttrib() + operation + rule.GetRuleOldValue().ToString()+" ";
                if (operation == "LIKE %" || operation == "NOT LIKE %")
                {
                    whereSubquery += "%";
                }
            }
            if (rule.ActiveModificators.Contains(RuleMoificators.is_geographical_coords))
            {
                if (rule.GetBindedAtrib!=null)
                {
                    bindedAtribQueryPart = "," + rule.GetBindedAtrib + "=" + rule.GetBindedAtribValue;
                }
            }
            if (whereSubquery!="")
            {
                if (replacePattern!="")
                {
                    replacePattern = " AND " + replacePattern;
                }
               
            }
            
           string query= "UPDATE " + rule.GetRuleTable() + " SET " + rule.GetRuleAttrib() + "=" + rule.GetRuleNewValue().ToString()+ bindedAtribQueryPart+ whereSubquery+ replacePattern;
            return query;
        }

        private void Mod_enable_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (intTypeFlag || realTypeFlag)
            {
                Modificators_setting_groupBox.Enabled = !Modificators_setting_groupBox.Enabled;
                new_atrib_val_groupBox.Enabled = !new_atrib_val_groupBox.Enabled;
            }
            else
            {
                MessageBox.Show(
                         "Selected attribute is not a number"
                          , "Attribute modificators set error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Mod_enable_checkBox.Checked = false;
            }
        }

        private void Lat_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!BindLatLon_checkBox.Checked)
            {
                if (coords_by_const_radioButton.Checked)
                {
                    Lat_textBox.Enabled = true;
                    Lon_textBox.Enabled = false;

                }
                else
                {
                    Lat_textBox.Enabled = true;
                    Lon_textBox.Enabled = true;
                }
            }
        }

        private void Lon_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!BindLatLon_checkBox.Checked)
            {
                if (coords_by_const_radioButton.Checked)
                {
                    Lon_textBox.Enabled = true;
                    Lat_textBox.Enabled = false;

                }
            }
            else
            {
                Lat_textBox.Enabled = true;
                Lon_textBox.Enabled = true;
            }
        }

        private void contryList_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            countryList_comboBox.Enabled = !countryList_comboBox.Enabled;
        }

        private void worldPart_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            worldPartList_comboBox.Enabled = !worldPartList_comboBox.Enabled;
        }

        private void BindLatLon_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (BindLatLon_checkBox.Checked)
            {
                AtribBind_groupBox.Enabled = true;
                AtribBind_groupBox.Visible = true;

                Lat_textBox.Enabled = true;
                Lon_textBox.Enabled = true;
            }
            else // only lat or only lon uodate
            {
                AtribBind_groupBox.Enabled = false;
                AtribBind_groupBox.Visible = false;

                Lon_radioButton_CheckedChanged(this, e);
                Lat_radioButton_CheckedChanged(this, e);

            }
        }

        private void coords_by_const_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                Lon_radioButton_CheckedChanged(this, e);
                Lat_radioButton_CheckedChanged(this, e);
            }
            else
            {
                Lat_textBox.Enabled = false;
                Lon_textBox.Enabled = false;
            }

        }


        private void UpdateAll_checkBox_CheckedChanged(object sender, EventArgs e)
        {
           
            old_atrib_val_groupBox.Enabled = !old_atrib_val_groupBox.Enabled;
            state_comboBox.Enabled = !state_comboBox.Enabled;
            RuleStateChange(sender, e);
         
           
        }

        private void LinearReplace_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            replacePattern = "";
        }

        private void N_Replace_radioButton_CheckedChanged(object sender, EventArgs e)
        {
            SQLiteCommand sQCmd1 = new SQLiteCommand("SELECT * FROM PRAGMA_TABLE_INFO('" + RuleTable + "');", MainForm.currentConnection);
            SQLiteDataReader dataReader1 = sQCmd1.ExecuteReader();


            SQLiteCommand sQCmd2 = new SQLiteCommand("SELECT name FROM PRAGMA_TABLE_INFO('" + RuleTable + "');", MainForm.currentConnection);
            SQLiteDataReader dataReader2 = sQCmd2.ExecuteReader();
            bool s = false;
            string PK_name = "";


            while (dataReader1.Read() && dataReader2.Read())
            {
             s = dataReader1.GetBoolean(5);
                if (s)
                { PK_name = dataReader2.GetString(0); }
            }
            int  N_val = 0;
            int.TryParse(N_pattern_textBox.Text, out N_val);
            if (PK_name!="" && N_val>0)
            {
                replacePattern = " WHERE " + PK_name + "%" + N_val + "=0";
            }
            else
            {
                replacePattern = "";
                if (PK_name == "")
                {
                  
                    MessageBox.Show(
                         "PK not found in table."
                          , "Replace pattern error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
                if (N_val <= 0)
                {
                    MessageBox.Show(
                         "Incorrect value of variable for pattern."
                          , "Replace pattern error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
              
            }
          
        }
    }
}
