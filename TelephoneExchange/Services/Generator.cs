using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelephoneExchange.Services
{
    public class Generator : IGenerator
    {

        public int GenerateNumber()
        {

            var timeCall = IntervalNumberGenerator(10, 20);
            return timeCall;
        }

        private int IntervalNumberGenerator(int startNumber, int endNumber)
        {
            Random rnd = new Random();
            int result = rnd.Next(startNumber, endNumber);

            return result;
        }

        

    }
}
