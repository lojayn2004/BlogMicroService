using MassTransit;
using Shared.Events;
using UsersService.Dtos.Users;

using UsersService.Services.Contracts;

namespace UsersService.Consumers;

public class UserCreatedEventConsumer(IUserService _userService): IConsumer<UserCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        try 
        {
            var message = context.Message;
            
            var newUser = new CreateUserDto
            {
                UserId = message.UserId,
                DisplayName = message.UserName,
                Bio = message.Bio,
                AvatarUrl = message.AvatarUrl
            };

            var result = await _userService.CreateAsync(newUser);

            if(!result.IsSuccess) 
            {
                await context.RespondAsync(new UserCreatedResponse { IsSuccess = false, ErrorMessage = result.Message });
                return;
            }
            await context.RespondAsync(new UserCreatedResponse { IsSuccess = true });
        }
        catch (Exception ex)
        {
            await context.RespondAsync(new UserCreatedResponse { IsSuccess = false, ErrorMessage = ex.Message });
        }
    }
}
