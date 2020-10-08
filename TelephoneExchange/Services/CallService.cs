using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelephoneExchange.Models;

namespace TelephoneExchange.Services
{
    public class CallService : ICallService
    {
        private readonly IGenerator generator;
        public CallService(IGenerator generator)
        {
            this.generator = generator;
        }
       
        public List<Call> GenerateCalls()
        {
            var callList = new List<Call>();
            var callsNumber = generator.GenerateNumber();

            for (int i = 0; i < callsNumber; i++)
            {
                var newCall = new Call()
                {
                    Id = i,
                    DurationInSec = generator.GenerateNumber()
                };
                callList.Add(newCall);
            }

            return callList;
        }

    }
}
