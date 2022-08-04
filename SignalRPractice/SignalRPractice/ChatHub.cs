using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SignalRPractice.DAL;
using SignalRPractice.Models;
using System;
using System.Threading.Tasks;

namespace SignalRPractice
{
    public class ChatHub:Hub
    {
       
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;


        public ChatHub( UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, DateTime.Now.ToString("dd.MM.yyyy"));
        }
        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                AppUser user=_userManager.FindByNameAsync(Context.User.Identity.Name).Result;
                user.ConnectId = Context.ConnectionId;
                _context.SaveChanges();

                Clients.All.SendAsync("Userconnect", user.Id);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                AppUser user = _userManager.FindByNameAsync(Context.User.Identity.Name).Result;
                user.ConnectId =null;
                _context.SaveChanges();
                Clients.All.SendAsync("Userdisconnect", user.Id);
            }
           
            return base.OnDisconnectedAsync(exception); 
        }
    }
}
