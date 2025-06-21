using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PersonalizacaoImobiliaria.Domain.Entities;


namespace PersonalizacaoImobiliaria.Infrastructure.Database.Context;

public class PersonalizacaoImobiliariaContext : DbContext
{
    public DbSet<Personalizacao> Personalizacao { get; set; }
    public DbSet<Solicitacao> Solicitacao { get; set; }
    public DbSet<Unidade> Unidade { get; set; }

    public PersonalizacaoImobiliariaContext(DbContextOptions<PersonalizacaoImobiliariaContext> options)
       : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Host=localhost:5332;Database=postgres;Username=admin;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Solicitacao>()
            .HasMany(s => s.Personalizacoes)
            .WithMany(p => p.Solicitacoes);
    }

}
