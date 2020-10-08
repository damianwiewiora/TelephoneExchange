using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using TelephoneExchange.Models;

namespace TelephoneExchange.Services
{
    public class AgentService : IAgentService
    {
        public AgentService()
        {
            Agents = new List<Agent>();
            var a1 = new Agent()
            {
                Id = 1,
                Name = "Agent1"
            };
            var a2 = new Agent()
            {
                Id = 2,
                Name = "Agent2"
            };
            Agents.Add(a1);
            Agents.Add(a2);
        }
        public static List<Agent> Agents { get; set; }

        public List<Agent> GetAllAgents()
        {
            var allAgents = Agents;
            return allAgents;
        }
        public Agent GetAgent(int IdAgent)
        {
            var agent = Agents.FirstOrDefault(i => i.Id == IdAgent);
            if (agent != null)
            {
                return agent;
            }

            throw new Exception("Agent is not found.");
        }

        public Agent GetFreeAgent()
        {
            var freeAgents = Agents.Where(a => a.IsWork == false).ToList();
            if(freeAgents != null && freeAgents.Count > 0)
            {
                var random = new Random();
                var numberRand = random.Next(freeAgents.Count);
                var freeAgent = freeAgents[numberRand];

                freeAgent.IsWork = true;
                return freeAgent;
            }
            return null;
        }

        public void AgentDismissal(int agentId)
        {
            var agent = Agents.FirstOrDefault(a => a.Id == agentId);
            if (agent != null)
            {
                agent.IsWork = false;
            }
        }

        public bool RemoveAgent(int AgentId)
        {
            var agent = Agents.FirstOrDefault(i => i.Id == AgentId);
            if (agent != null)
            {
                Agents.Remove(agent);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Agent AddNewAgent(string Name)
        {
            var lastId = Agents.Max(a => a.Id);
            var newId = lastId + 1;
            var newAgent = new Agent()
            {
                Id = newId,
                Name = Name
            };
            Agents.Add(newAgent);
            return newAgent;
        }

    }
}
