using Microsoft.AspNetCore.SignalR;

public class RealTimeHub : Hub
{
    public async Task SendRealTimeUpdate(string message)
    {
        // Broadcast the message to all connected clients
        await Clients.All.SendAsync("ReceiveRealTimeUpdate", message);
    }
}