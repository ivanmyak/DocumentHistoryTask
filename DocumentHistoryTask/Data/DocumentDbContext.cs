using DocumentHistoryTask.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DocumentHistoryTask.Data
{
    public class DocumentDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options) { }

        public DbSet<Document> Documents { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
    }


}
