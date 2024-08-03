using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Models
{
    public class PhoneBookContext : DbContext
    {

        DbSet<Person> People { get; set; }
        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-ALE5B86;database=PhoneBook;Integrated Security=true;TrustServerCertificate=True;");
        }
    }
}
