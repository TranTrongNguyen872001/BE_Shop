using Microsoft.AspNetCore.SignalR;

namespace BE_Shop.Hubs
{
	public class NotificationHub : Hub
	{
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
        public async Task Join(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await Clients.OthersInGroup(group).SendAsync("Joined", Context.ConnectionId);
        }
        public async Task Leave(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
            await Clients.Group(group).SendAsync("Leaved", Context.ConnectionId);
        }
        public async Task SendToAll(object message)
		{
			await Clients.All.SendAsync("Received", new {ClientsId = Context.ConnectionId, Message = message});
		}
		public async Task SendToClient(object message, string ConnectionId)
		{
            await Clients.Client(ConnectionId).SendAsync("Received", new { ClientsId = Context.ConnectionId, Message = message });
        }
		public async Task SendToGroup(object message, string group)
		{
            await Clients.OthersInGroup(group).SendAsync("Received", new { ClientsId = Context.ConnectionId, Message = message });
        }
	}
}