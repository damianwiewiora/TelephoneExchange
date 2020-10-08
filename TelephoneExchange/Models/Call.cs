using System;
using System.Collections.Generic;
using System.Text;

namespace TelephoneExchange.Models
{
    public class Call
    {
        public int Id { get; set; }
        public int DurationInSec { get; set; }
        public int AgentId { get; set; }
        public string AgentName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CallStart { get; set; }
        public DateTime CallEnd { get; set; }
        public string PhoneNumber { get; set; }
    }
}
