using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework2SqlWithCSharp
{
    public class ElectroStore : DbContext
    {
        public ElectroStore()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ElectroStore;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Store)
                .WithMany(s => s.Clients)
                .HasForeignKey(c => c.StoreId);


            modelBuilder.Entity<Product>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ClientId);


            modelBuilder.Entity<Product>()
                .HasOne(p => p.Store)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.StoreId);


            modelBuilder.Entity<Client>().HasData(new Client
            {
                Id = 1,
                Name = "Ivan",
                Surname = "Efim",
                Email = "ivan@gmail.com",
                Birthdate = new DateTime(1990, 1, 1),
                StoreId = 1
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1, 
                Brand = "Apple",
                Model = "X max",
                Price = 14000, 
                Year = 2024, 
                ClientId = 1,
                StoreId = 1
            });
            modelBuilder.Entity<Store>().HasData(new Store
            {
                Id = 1,
                Name = "Jabko"
            });

        }


        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Surname { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        public DateTime? Birthdate { get; set; }

        [ForeignKey("StoreId")]
        public Store Store { get; set; }
        public int StoreId { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Model { get; set; }

        [Required, MaxLength(50)]
        public string Brand { get; set; }

        [Range(2000, 2100)]
        public int Year { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public int ClientId { get; set; }

        [ForeignKey("StoreId")]
        public Store Store { get; set; }
        public int StoreId { get; set; }
    }

    public class Store
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Client> Clients { get; set; } = new List<Client>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
