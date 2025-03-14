using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Azure.Core.GeoJson;
using Medicare_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing;




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
            //Relacionamento de Cuidador e Utilizador
            modelBuilder.Entity<Cuidador>()
            .HasKey(ph => new { ph.IdUtilizador, ph.IdCuidador }); //Definindo Chave Composta

            modelBuilder.Entity<Cuidador>().
            HasOne(u => u.Utilizador)
            .WithMany()
            .HasForeignKey(fkc => fkc.IdUtilizador);

            modelBuilder.Entity<Cuidador>()
            .HasOne(c => c.cuidador)
            .WithMany()
            .HasForeignKey(fkc => fkc.IdCuidador)
            .OnDelete(DeleteBehavior.Restrict);

            //Relacionamento de Reponsavel e Utilizador
            //Relacionamento de Reponsavel e Grau Parentesco
            modelBuilder.Entity<Responsavel>().
            HasKey(pk => new { pk.IdUtilizador, pk.IdResponsavel });

            modelBuilder.Entity<Responsavel>().
            HasOne(u => u.Utilizador)
            .WithMany()
            .HasForeignKey(fk =>  fk.IdUtilizador)
            ;

            modelBuilder.Entity<Responsavel>()
            .HasOne(r => r.responsavel)
            .WithMany()
            .HasForeignKey(fkc => fkc.IdResponsavel)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Responsavel>()
            .HasOne(gp => gp.GrauParentesco)
            .WithMany(re => re.Responsavel)
            .HasForeignKey(fk => fk.IdGrauParentesco);

            //Relacionamento de Parceiro Utilizador e Parceiro
            //Relacionamento de Parceiro Utilizador e Utilizador
            modelBuilder.Entity<ParceiroUtilizador>()
            .HasKey(p => p.IdParceiro);

            modelBuilder.Entity<ParceiroUtilizador>()
            .HasOne(c => c.Parceiro)
            .WithMany(p => p.ParceiroUtilizador)
            .HasForeignKey(fk => fk.IdParceiro);

            modelBuilder.Entity<ParceiroUtilizador>()
            .HasOne(c => c.colaborador)
            .WithMany(pu => pu.ParceiroUtilizadores)
            .HasForeignKey(fk => fk.IdColaborador);

            ////Relacionamento de Utilizador com Tipo Utilizador
            modelBuilder.Entity<Utilizador>()
            .HasOne(tp => tp.TipoUtilizador)
            .WithMany(u => u.Utilizadores)
            .HasForeignKey(fk => fk.IdTipoUtilizador);

            //Relacionamento de Posologia e Utilizador
            //Relacionamento de Posologia e Remédio
            //Relacionamento de Posologia e Historico Posologia
            modelBuilder.Entity<Posologia>()
            .HasOne(u => u.Utilizador)
            .WithMany(po => po.Posologias)
            .HasForeignKey(fk => fk.IdUtilizador);

            modelBuilder.Entity<Posologia>()
            .HasKey(pk => new { pk.Id, pk.IdRemedio }); //Definindo Chave Composta

            modelBuilder.Entity<Posologia>()
            .HasOne(re => re.Remedio)
            .WithMany(p => p.Posologias)
            .HasForeignKey(fk => fk.IdRemedio);

            modelBuilder.Entity<HistoricoPosologia>()
            .HasKey(pk => new { pk.IdPosologia, pk.IdRemedio });

            modelBuilder.Entity<HistoricoPosologia>()
            .HasOne(p => p.Posologia)
            .WithMany(h => h.HistoricoPosologias)
            .HasForeignKey(fkc => new { fkc.IdPosologia, fkc.IdRemedio });

            //Relacionamento de Alarme e Posologia
            //Relacionamento de Alarme e Remédio
            modelBuilder.Entity<Alarme>()
            .HasKey(pk => pk.Id); 

            modelBuilder.Entity<Alarme>()
            .HasOne(p => p.Posologia)
            .WithMany(a => a.Alarmes)
            .HasForeignKey(a => new { a.IdPosologia, a.IdRemedio });

            modelBuilder.Entity<Alarme>()
            .HasOne(p => p.Remedio)
            .WithMany(a => a.Alarmes)
            .HasForeignKey(fk => fk.IdRemedio);

            //Relacionamento de Remédio e laboratório
            //Relacionamento de Remédio e Tipo Grandeza
            modelBuilder.Entity<Remedio>()
            .HasOne(la => la.laboratorio)
            .WithMany(re => re.Remedios)
            .HasForeignKey(fk => fk.IdLaboratorio);

            modelBuilder.Entity<Remedio>()
            .HasOne(ga => ga.grandeza)
            .WithMany(re => re.Remedios)
            .HasForeignKey(fk => fk.IdGrandeza);

            //Relacionamento de Promoção e Remédio
            //Relacionamento de Promoção e Colaboradores(Utilizador)
            //Relacionamento de Promoção e Forma de Pagamento
            modelBuilder.Entity<Promocao>()
            .HasKey(pk => pk.Id);

            modelBuilder.Entity<Promocao>()
            .HasOne(re => re.remedio)
            .WithMany(po => po.Promocoes)
            .HasForeignKey(fkre => fkre.IdRemedio);

            modelBuilder.Entity<Promocao>()
            .HasOne(c => c.colaborador)
            .WithMany(po => po.Promocoes)
            .HasForeignKey(fkre => fkre.IdColaborador);

/*
            modelBuilder.Entity<Promocao>()
            .HasOne(p => p.formaDePagamento) 
            .WithMany(f => f.Promocoes)    
            .HasForeignKey(p => p.IdFormaDePagamento); 
*/

            




















        }


    }
}