using Agenda.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> contacts { get; set; }
    }
}
