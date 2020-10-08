using System;
using System.Collections.Generic;
using System.Text;
using TelephoneExchange.Models;

namespace TelephoneExchange.Services
{
    public interface ICallService
    {
        List<Call> GenerateCalls();
    }
}
