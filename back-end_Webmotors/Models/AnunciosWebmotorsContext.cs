using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using back_end_Webmotors.Models;

namespace back_end_Webmotors.Models
{
    public partial class AnuncioswebmotorsContext : DbContext
    {
        public AnuncioswebmotorsContext()
        {
            
        }

        public AnuncioswebmotorsContext(DbContextOptions<AnuncioswebmotorsContext> options)
            : base(options)
        {
        }

        // Unable to generate entity type for table 'dbo.tb_AnuncioWebmotors'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localdb;Database=teste_webmotors;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");
        }

        public DbSet<back_end_Webmotors.Models.AnuncioWebmotors> AnuncioWebmotors { get; set; }
    }
}
