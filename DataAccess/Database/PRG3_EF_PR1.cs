using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Database
{
    public partial class PRG3_EF_PR1 : DbContext
    {
        public PRG3_EF_PR1()
            : base("name=PRG3_EF_PR1")
        {
        }

        public virtual DbSet<Alquileres> Alquileres { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Copias> Copias { get; set; }
        public virtual DbSet<Peliculas> Peliculas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>()
                .HasMany(e => e.Alquileres)
                .WithRequired(e => e.Clientes)
                .HasForeignKey(e => e.IdCliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Copias>()
                .HasMany(e => e.Alquileres)
                .WithRequired(e => e.Copias)
                .HasForeignKey(e => e.IdCopia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Peliculas>()
                .HasMany(e => e.Copias)
                .WithRequired(e => e.Peliculas)
                .HasForeignKey(e => e.IdPelicula)
                .WillCascadeOnDelete(false);
        }
    }
}
