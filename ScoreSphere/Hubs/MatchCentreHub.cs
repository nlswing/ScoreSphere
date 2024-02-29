using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ScoreSphere.Hubs {
public class MatchCenterHub : Hub
{
    public async Task SendMatchCenterUpdate()
    {
        await Clients.All.SendAsync("RefreshMatchCenter");
    }
}

}
