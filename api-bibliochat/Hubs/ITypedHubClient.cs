using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_bibliochat.Hubs
{
    public interface ITypedHubClient
    {
        Task ContadorSession(string token);
    }
}
