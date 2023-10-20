using Microsoft.AspNetCore.SignalR;
using BE_Shop.Data;

namespace BE_Shop.Hubs
{
	public class NotificationHub : Hub
	{
		private readonly DatabaseConnection dbContext;
		public NotificationHub(DatabaseConnection dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task SendNotificationToAll(string message)
		{
			await Clients.All.SendAsync("ReceivedNotification", message);
		}
		public async Task SendNotificationToClient(string message, string username)
		{
			var hubConnections = dbContext._User.Where(con => con.UserName == username).ToList();
			foreach (var hubConnection in hubConnections)
			{
				await Clients.Client(hubConnection.UserName).SendAsync("ReceivedPersonalNotification", message, username);
			}
		}
		public async Task SendNotificationToGroup(string message, string group)
		{
			//var hubConnections = dbContext._User.Join(dbContext._User, c => c.Id, o => o.UserName, (c, o) => new { c.Id, c.UserName, o.Role }).Where(o => o.Role == group).ToList();
			//foreach (var hubConnection in hubConnections)
			//{
			//	string username = hubConnection.UserName;
			//	await Clients.Client(hubConnection.UserName).SendAsync("ReceivedPersonalNotification", message, username);
			//	//Call Send Email function here
			//}
		}
		public override Task OnConnectedAsync()
		{
			Clients.Caller.SendAsync("OnConnected");
			return base.OnConnectedAsync();
		}
		//public async Task SaveUserConnection(string username)
		//{
		//	var connectionId = Context.ConnectionId;
		//	HubConnection hubConnect = new HubConnection
		//	{
		//		UserName = username,
		//		ConnectionId = connectionId
		//	};

		//	dbContext.Add(connectionId);
		//	await dbContext.SaveChangesAsync();
		//}
		public override Task OnDisconnectedAsync(Exception? exception)
		{
			var hubConnection = dbContext._User.FirstOrDefault(con => con.UserName == Context.ConnectionId);
			if (hubConnection != null)
			{
				dbContext._User.Remove(hubConnection);
				dbContext.SaveChangesAsync();
			}

			return base.OnDisconnectedAsync(exception);
		}
	}
}