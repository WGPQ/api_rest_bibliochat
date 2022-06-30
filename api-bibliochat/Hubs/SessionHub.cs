using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Hubs
{
    public class SessionHub:Hub
    {
        public Task OpenSession(string user)
        {
            return Clients.All.SendAsync("ReciveOne",user,"Hola");
        }
    }
}
