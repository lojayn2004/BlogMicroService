using BlogService.Database.Contexts;
using BlogService.Repos.Implementations;
using MassTransit;
using Shared.Events;

namespace BlogService.Consumers
{
    public class UserDeletedConsumer(BlogDbContext _blogDbContext): IConsumer<UserDeletedEvent>
    {
        public async Task Consume(ConsumeContext<UserDeletedEvent> context)
        {
            var message = context.Message;

            _blogDbContext.Blogs.RemoveRange(_blogDbContext.Blogs.Where(b => b.AuthorId == message.UserId));
            await _blogDbContext.SaveChangesAsync();
            

        }
    }
}
