using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTransportation
{
    public class Logger
    {
        private readonly string logPath;
        private readonly object fileLock = new();
        private readonly MainForm form;

        public Logger(string path, MainForm form = null)
        {
            logPath = path;
            this.form = form;
            File.WriteAllText(logPath, "");
        }

        public void Log(string message)
        {
            string log = $"[{DateTime.Now:HH:mm:ss}] {message}";
            lock (fileLock)
            {
                File.AppendAllText(logPath, log + "\n");
            }
            Console.WriteLine(log);
            form?.LogToUI(log);
        }
    }
}
