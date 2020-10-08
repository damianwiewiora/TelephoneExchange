﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TelephoneExchange.Models
{
    public class Agent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsWork { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}