using System;
using System.Collections.Generic;
using System.Text;
using TelephoneExchange.Models;

namespace TelephoneExchange.Services
{
    public interface IAgentService
    {
        static List<Agent> Agents { get; set; }
        List<Agent> GetAllAgents();
        Agent GetAgent(int IdAgent);
        bool RemoveAgent(int AgentId);
        Agent GetFreeAgent();
        void AgentDismissal(int agentId);
        Agent AddNewAgent(string Name);
    }
}
