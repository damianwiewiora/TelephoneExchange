using System;
using System.Collections.Generic;
using System.Text;

namespace TelephoneExchange.Services
{
    public interface IConsoleService
    {
        string AddNewAgent(string agentName);
        string RemoveAgent(string agentName);
        string AddNewCall();
        string EndCall(string agentName, int durationSec);
        string StartCall(string agentName);
        string CallInfo(int callCount);
    }
}
