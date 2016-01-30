using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace MassiveTest.DataLoader
{
    /// <summary>
    /// Logs messages to file and/or to the console
    /// </summary>
    static class Logger
    {
        // log file name
        private static string logFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "app.log");
        
        // sync object to enable logging from concurrent threads
        private static readonly object syncRoot = new Object();

        // "FirstMessage" flag, if not set log file recreated
        private static bool isFirstMessage = true;

        // flag for control console output
        public static bool AllowWriteToConsole = true;

        private static void writeLine(string msg, string category, bool logToConsole = false, bool logToFile = true, bool noTimeStampForEntry = false, ConsoleColor conColor = ConsoleColor.Gray)
        {
            write(msg, category, true, logToConsole, logToFile, noTimeStampForEntry, conColor);
        }

        // main log function
        private static void write(string msg, string category, bool addNewLine, bool logToConsole = false, bool logToFile = true, bool noTimeStampForEntry = false, ConsoleColor conColor = ConsoleColor.Gray)
        {
            lock (syncRoot)
            {
                // log to file
                if (logToFile)
                {
                    if (isFirstMessage)
                    {
                        File.Delete(logFileName);
                        isFirstMessage = false;
                    }
                    string ts = String.Format("{0} [{1}]: ", DateTime.Now.ToString("G"), category);
                    File.AppendAllText(logFileName, String.Format("{0}{1}{2}", (noTimeStampForEntry ? "" : ts), msg, (addNewLine ? Environment.NewLine : "")));
                }
                // log to console if allowed
                if (logToConsole && AllowWriteToConsole)
                {
                    ConsoleColor c = Console.ForegroundColor;
                    Console.ForegroundColor = conColor;
                    if (addNewLine)
                        Console.WriteLine(msg);
                    else
                        Console.Write(msg);
                    Console.ForegroundColor = c;
                }
            }
        }

        public static void WriteLine(string msg, bool logToConsole = false, bool logToFile = true, bool noTimeStampForEntry = false, ConsoleColor conColor = ConsoleColor.Gray)
        {
            writeLine(msg, "Info", logToConsole, logToFile, noTimeStampForEntry, conColor);
        }

        public static void Write(string msg, bool logToConsole = false, bool logToFile = true, bool noTimeStampForEntry = false, ConsoleColor conColor = ConsoleColor.Gray)
        {
            write(msg, "Info", false, logToConsole, logToFile, noTimeStampForEntry, conColor);
        }

        public static void WriteLineSuccess(string msg, bool logToConsole = false, bool logToFile = true, bool noTimeStampForEntry = false)
        {
            writeLine(msg, "Info", logToConsole, logToFile, noTimeStampForEntry, ConsoleColor.Green);
        }

        public static void WriteSuccess(string msg, bool logToConsole = false, bool logToFile = true, bool noTimeStampForEntry = false)
        {
            write(msg, "Info", false, logToConsole, logToFile, noTimeStampForEntry, ConsoleColor.Green);
        }

        public static void WriteLineError(string msg, bool logToConsole = false, bool logToFile = true, bool noTimeStampForEntry = false)
        {
            writeLine(msg, "Error", logToConsole, logToFile, noTimeStampForEntry, ConsoleColor.Red);
        }

        public static void WriteError(string msg, bool logToConsole = false, bool logToFile = true, bool noTimeStampForEntry = false)
        {
            write(msg, "Error", false, logToConsole, logToFile, noTimeStampForEntry, ConsoleColor.Red);
        }

        public static void WriteException(string msg, Exception e)
        {
            writeLine(msg + " Exception: " + e.Message + "\n" + e.StackTrace, "Error");
        }


        public static void WriteLineSuccessEx(string msg)
        {
            WriteSuccess("  Success. ", true);
            WriteLine(msg, true, true, true);
        }

        public static void WriteLineErrorEx(string msg)
        {
            WriteError("  Failed. ", true);
            WriteLine(msg, true, true, true);
        }
    }
}
