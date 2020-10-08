using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace TelephoneExchange.Services
{
    public class ConsoleService : IConsoleService
    {
        
        public string AddNewAgent(string agentName)
        {
            return "Dodano agenta: "+agentName;
        }

        public string RemoveAgent(string agentName)
        {
            return "Usunięto agenta: " + agentName;
        }

        public string AddNewCall()
        {
            return "Dodano połączenie";
        }

        public string EndCall(string agentName, int durationSec)
        {
            return "Zakończono połączenie agenta: " + agentName + " trwające "+durationSec+"s.";
        }
        public string StartCall(string agentName)
        {
            return "Rozpoczęto połączenie agenta: " + agentName;
        }

        public string CallInfo(int callCount)
        {
            return "W kolejce jest obecnie "+ callCount+" połączeń.";
        }

    }
}
