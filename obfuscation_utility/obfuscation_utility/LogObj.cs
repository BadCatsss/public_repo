using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace obfuscation_utility
{
    enum LogEventType
    { //programe state
        Session_Start,
        File_Open,
        Rule_Create,
        Rule_Change,
        Rule_Delete,
        Rule_import,
        Rule_Export,
        Session_End,
        Settings_Change,
        Work_with_settings,

        //rule state
        Start_Rule_exec,
        Stop_Rule_exec,
       
        //connection state
        Connection_broken,
        Connection_closed,
        Connection_connecting,
        Connection_executing,
        Connection_fetching,
        Connection_open,

        // query state
        Query_start,
        Query_in_progress,
        Query_was_exec


    }

    enum OperationStatus
    {
        Success,
        Error,
        Warning,
        In_progress
    }
    static class LogObj
    {
        static string LogFilePath = "";

        public static string GetLogFilePath() { return LogFilePath; }
        public static void SetLogFilePath(string path)
        {
            LogFilePath = path;
        }
        public static void LogEvent(LogEventType logEvent, OperationStatus status, string StatusAdditionalDesc, string EventAdditionalDesc)
        {
            checkLogFileExist();
            string eventDesc = "";
            switch (logEvent)
            {
                case LogEventType.Settings_Change:
                    eventDesc = "The settings file has been changed.[settings_change]";
                    break;
                case LogEventType.Work_with_settings:
                    eventDesc = "The value of the settings file is used.[settings_use]";
                    break;
                case LogEventType.Session_Start:
                    eventDesc = "The application is running. The working session has begun.[begin]";
                    break;
                case LogEventType.File_Open:
                    eventDesc = "The database file is open.[open db]";
                    break;
                case LogEventType.Rule_Create:
                    eventDesc = "Obfuscation rule created.[create rule]";
                    break;
                case LogEventType.Rule_Change:
                    eventDesc = "Obfuscation rule changed.[changed rule]";
                    break;
                case LogEventType.Rule_Delete:
                    eventDesc = "Obfuscation rule deleted.[deleted rule]";
                    break;
                case LogEventType.Rule_import:
                    eventDesc = "Obfuscation rule imported.[import rule]";
                    break;
                case LogEventType.Rule_Export:
                    eventDesc = "Obfuscation rule exported.[export rule]";
                    break;
                case LogEventType.Session_End:
                    eventDesc = "The application has stopped. The working session is over.[end]";
                    break;
                case LogEventType.Start_Rule_exec:
                    eventDesc = "Rule started.[r_start]";
                    break;
                case LogEventType.Stop_Rule_exec:
                    eventDesc = "Rule stopped.[r_stop]";
                    break;
                case LogEventType.Query_start:
                    eventDesc = "Query exec start.[q_start]";
                    break;
                case LogEventType.Query_in_progress:
                    eventDesc = "Query in progress.[q_in_progr]";
                    break;
                case LogEventType.Query_was_exec:
                    eventDesc = "Query was exec.[q_was_exec]";
                    break;

                default:
                    eventDesc = "Unknow operation.[unknow]";
                    break;
            }
            
            File.AppendAllText(LogFilePath, 
                "[Event Type]=" + logEvent
                + ";[Description]=" + eventDesc
                +";[Additional description]="+EventAdditionalDesc
                + ";[Status]=" + status
                + ";[Status additional info]=" + StatusAdditionalDesc
                + ";[Strat Time UTC]=" + DateTime.UtcNow + ";\n");
        }

        private static void checkLogFileExist()
        {
            if (LogFilePath == "")
            {
                LogFilePath =Directory.GetDirectoryRoot(Directory.GetCurrentDirectory());
                //LogFilePath.Replace("\\","\\\\");
                //File.Create(LogFilePath);
            }
            else
            {
                if (!File.Exists(LogFilePath))
                {
                 FileStream fs=   File.Create(LogFilePath);
                    fs.Close();
                }
            }

        }
    }
}
