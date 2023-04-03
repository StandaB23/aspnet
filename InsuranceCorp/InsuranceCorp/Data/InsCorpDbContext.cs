using InsuranceCorp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace InsuranceCorp.Data;
public partial class InsCorpDbContext : DbContext
{
    public InsCorpDbContext()
    {
    }

    public InsCorpDbContext(DbContextOptions<InsCorpDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Contract> Contracts { get; set; }
    public virtual DbSet<Person> Persons { get; set; }

    public DbSet<RequestLog> LogRequests { get; set; }
    public DbSet<ErrorLog> LogErrors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=InsCorpDb;Trusted_Connection=True;MultipleActiveResultSets=true");
    }

}
