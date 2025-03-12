using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medicare_API.Models;
using Microsoft.EntityFrameworkCore;


namespace Medicare_API.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Utilizador> UTILIZADOR { get; set; }
        public DbSet<TipoUtilizador> TIPO_UTILIZADOR { get; set; }
        public DbSet<TipoOrdemGrandeza> TIPO_ORDEMGRANDEZA { get; set; }
        public DbSet<Responsavel> RESPONSAVEL { get; set; }
        public DbSet<Remedio> REMEDIO { get; set; }
        public DbSet<Promocao> PROMOCOES { get; set; }
        public DbSet<Posologia> POSOLOGIA { get; set; }
        public DbSet<ParceiroUtilizador> PARCEIRO_UTILIZADORES { get; set; }
        public DbSet<Parceiro> PARCEIROS { get; set; }
        public DbSet<Laboratorio> LABORATORIOS { get; set; }
        public DbSet<HistoricoPosologia> HISTORICO_POSOLOGIA { get; set; }
        public DbSet<Laboratorio> GRAU_PARENTESCO { get; set; }
        public DbSet<FormaPagamento> FORMAS_PAGAMENTO { get; set; }
        public DbSet<Cuidador> CUIDADOR { get; set; }
        public DbSet<Alarme> ALARMES { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuidador>()
            .HasKey(ph => new { ph.IdUtilizador, ph.IdCuidador }); //Definindo Chave Composta

            modelBuilder.Entity<Cuidador>().
            HasOne(u => u.Utilizador).
            WithMany(c => c.Cuidadores)
            .HasForeignKey(fkc => new { fkc.IdUtilizador, fkc.IdCuidador });

            modelBuilder.Entity<Responsavel>().
            HasKey(pk => new { pk.IdUtilizador, pk.IdResponsavel });

            modelBuilder.Entity<Responsavel>().
            HasOne(u => u.Utilizador)
            .WithMany(r => r.Responsaveis)
            .HasForeignKey(fk => new { fk.IdUtilizador, fk.IdResponsavel });

            modelBuilder.Entity<ParceiroUtilizador>()
            .HasKey(p => p.IdParceiro);

            modelBuilder.Entity<ParceiroUtilizador>()
            .HasOne(c => c.colaborador)
            .WithMany(p => p.ParceiroUtilizadores)
            .HasForeignKey(fk => new {fk.IdParceiro,fk.IdColaborador} );



            





        }


    }
}