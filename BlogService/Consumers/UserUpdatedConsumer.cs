using BlogService.Database.Contexts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events;

namespace BlogService.Consumers
{
    public class UserUpdatedConsumer(BlogDbContext blogDbContext) : IConsumer<UserUpdatedEvent>
    {
        public async Task Consume(ConsumeContext<UserUpdatedEvent> context)
        {
            var message = context.Message;
            var blogs = await blogDbContext.Blogs
                .Where(blog => blog.AuthorId == message.UserId)
                .ToListAsync(context.CancellationToken);

            foreach (var blog in blogs)
            {
                blog.AuthorName = message.DisplayName;
                blog.AuthorAvatarUrl = message.AvatarUrl;
            }

            await blogDbContext.SaveChangesAsync(context.CancellationToken);
        }
    }
}
