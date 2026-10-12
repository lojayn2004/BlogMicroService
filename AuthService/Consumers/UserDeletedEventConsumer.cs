using AuthService.Models;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Shared.Events;

namespace AuthService.Consumers
{
    public class UserDeletedEventConsumer(UserManager<ApplicationUser> _userManager): IConsumer<UserDeletedEvent>
    {
        public async Task Consume(ConsumeContext<UserDeletedEvent> context)
        {
            var message = context.Message;
            var user = await _userManager.FindByIdAsync(message.UserId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}
