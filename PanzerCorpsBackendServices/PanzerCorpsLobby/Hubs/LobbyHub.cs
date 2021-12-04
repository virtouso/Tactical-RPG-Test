using Microsoft.AspNetCore.SignalR;

namespace PanzerCorpsLobby.Hubs
{
    public class LobbyHub:Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


    }
}
