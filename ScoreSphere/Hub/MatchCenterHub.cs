using Microsoft.AspNetCore.SignalR;


namespace ScoreSphere.Hubs
{
    public class MatchCenterHub : Hub
    {
        public async Task SendMatchCenterUpdate()
        {
            await Clients.All.SendAsync("RefreshMatchCenter");
        }
    }
}