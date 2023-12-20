using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipherApp.Services
{
    internal class EventLogger
    {
        private static EventLogger? _instance;

        private EventLogger() { }

        public static EventLogger GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EventLogger();
            }

            return _instance;
        }

        public void Log(string message)
        {
            File.AppendAllText("./logs.txt", message + Environment.NewLine);
        }
    }
}
