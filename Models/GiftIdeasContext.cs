using Microsoft.EntityFrameworkCore;

namespace giftideas.Models
{
    public class GiftIdeasContext : DbContext
    {
        public GiftIdeasContext(DbContextOptions<GiftIdeasContext> options)
            : base(options)
        {
        }

        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<GiftIdea> GiftIdeas { get; set; }

    }
}