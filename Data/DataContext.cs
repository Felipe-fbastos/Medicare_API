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

        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Cuidador> Cuidadores { get; set; }
        public DbSet<Responsavel> Responsaveis { get; set; }
        public DbSet<ParceiroUtilizador> ParceiroUtilizadores { get; set; }
        public DbSet<Parceiro> Parceiros { get; set; }
        public DbSet<Posologia> Posologias { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Alarme> Alarmes { get; set; }
        public DbSet<HistoricoPosologia> HistoricosPosologia { get; set; }
        public DbSet<Remedio> Remedios { get; set; }
        public DbSet<TipoUtilizador> TipoUtilizadores { get; set; }
        public DbSet<GrauParentesco> GrauParentesco { get; set; }
        public DbSet<AlarmeStatus> AlarmeStatus { get; set; }
        public DbSet<TipoOrdemGrandeza> TipoOrdemGrandeza { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<FormaPagamento> FormasPagamento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Relacionamentos

            // Chaves primárias compostas e configurações de relacionamento

            // Relacionamento de Cuidador e Utilizador (Chave composta: IdUtilizador, IdCuidador)
            modelBuilder.Entity<Cuidador>()
                .HasKey(ph => new { ph.IdUtilizador, ph.IdCuidador }); // Definindo chave composta

            modelBuilder.Entity<Cuidador>()
                .HasOne(u => u.Utilizador) // Relacionando com Utilizador
                .WithMany(c => c.Cuidadores)
                .HasForeignKey(fkc => new { fkc.IdUtilizador, fkc.IdCuidador })
                .OnDelete(DeleteBehavior.Restrict); // Comportamento de exclusão


            // Relacionamento de Responsável e Utilizador (Chave composta: IdUtilizador, IdResponsavel)
            modelBuilder.Entity<Responsavel>()
                .HasKey(pk => new { pk.IdUtilizador, pk.IdResponsavel });

            modelBuilder.Entity<Responsavel>()
                .HasOne(u => u.Utilizador)
                .WithMany(r => r.Responsaveis)
                .HasForeignKey(fk => new { fk.IdUtilizador, fk.IdResponsavel })
                .OnDelete(DeleteBehavior.Restrict); // Comportamento de exclusão


            modelBuilder.Entity<Responsavel>()
                .HasOne(gp => gp.GrauParentesco) // Relacionamento com GrauParentesco
                .WithMany(re => re.Responsavel)
                .HasForeignKey(fk => fk.IdGrauParentesco)
                .OnDelete(DeleteBehavior.Restrict); // Comportamento de exclusão

            // Relacionamento de ParceiroUtilizador e Parceiro (Chave composta: IdUtilizador, IdParceiro)
            modelBuilder.Entity<ParceiroUtilizador>()
                .HasKey(pu => new { pu.IdUtilizador, pu.IdParceiro }); // Definindo chave composta

            modelBuilder.Entity<ParceiroUtilizador>()
                .HasOne(c => c.Parceiro) // Relacionamento com Parceiro
                .WithMany(p => p.ParceiroUtilizador)
                .HasForeignKey(fk => fk.IdParceiro)
                .OnDelete(DeleteBehavior.Restrict); // Comportamento de exclusão

            modelBuilder.Entity<ParceiroUtilizador>()
                .HasOne(c => c.colaborador) // Relacionamento com Colaborador
                .WithMany(pu => pu.ParceiroUtilizadores)
                .HasForeignKey(fk => fk.IdUtilizador)
                .OnDelete(DeleteBehavior.Restrict); // Comportamento de exclusão

            modelBuilder.Entity<Utilizador>()
                .HasKey(u => u.IdUtilizador);

            // Relacionamento de Utilizador com TipoUtilizador
            modelBuilder.Entity<Utilizador>()
                .HasOne(tp => tp.TipoUtilizador)
                .WithMany(u => u.Utilizadores)
                .HasForeignKey(fk => fk.IdTipoUtilizador)
                .OnDelete(DeleteBehavior.Restrict); // Comportamento de exclusão


            // Remedio (Chave composta: IdGrandeza, IdLaboratorio)
            modelBuilder.Entity<Remedio>()
                .HasKey(r => r.IdRemedio);

            modelBuilder.Entity<Remedio>()
                .HasOne(r => r.Grandeza)
                .WithMany(t => t.Remedios)
                .HasForeignKey(r => r.IdTipoOrdemGrandeza)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Remedio>()
                .HasOne(r => r.laboratorio)
                .WithMany(l => l.Remedios)
                .HasForeignKey(r => r.IdLaboratorio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Remedio>()
                .HasMany(r => r.Posologias)
                .WithOne(p => p.remedio)
                .HasForeignKey(p => p.IdRemedio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Remedio>()
                .HasMany(r => r.HistoricoPosologias)
                .WithOne(h => h.remedio)
                .HasForeignKey(h => h.IdRemedio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Remedio>()
                .HasMany(r => r.Alarmes)
                .WithOne(a => a.Remedio)
                .HasForeignKey(a => a.IdRemedio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Remedio>()
                .HasMany(r => r.Promocoes)
                .WithOne(p => p.remedio)
                .HasForeignKey(p => p.IdRemedio)
                .OnDelete(DeleteBehavior.Restrict);

            // Posologia (Chave composta: IdRemedio, IdUtilizador)
            modelBuilder.Entity<Posologia>()
                .HasKey(p => p.IdPosologia);

            modelBuilder.Entity<Posologia>()
                .HasMany(p => p.HistoricoPosologias)
                .WithOne(h => h.posologia)
                .HasForeignKey(h => h.IdPosologia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Posologia>()
                .HasMany(p => p.Alarmes)
                .WithOne(a => a.Posologia)
                .HasForeignKey(a => a.IdPosologia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Posologia>()
                .HasOne(p => p.remedio)
                .WithMany(r => r.Posologias)
                .HasForeignKey(p => p.IdRemedio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Posologia>()
                .HasOne(p => p.utilizador)
                .WithMany(u => u.Posologias)
                .HasForeignKey(p => p.IdUtilizador)
                .OnDelete(DeleteBehavior.Restrict);

            // HistóricoPosologia (Chave composta: IdPosologia, IdRemedio)
            modelBuilder.Entity<HistoricoPosologia>()
                .HasKey(h => new { h.IdPosologia, h.IdRemedio });

            modelBuilder.Entity<HistoricoPosologia>()
                .HasOne(h => h.posologia)
                .WithMany(p => p.HistoricoPosologias)
                .HasForeignKey(h => h.IdPosologia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistoricoPosologia>()
                .HasOne(h => h.remedio)
                .WithMany(r => r.HistoricoPosologias)
                .HasForeignKey(h => h.IdRemedio)
                .OnDelete(DeleteBehavior.Restrict);

            // AlarmeStatus
            modelBuilder.Entity<AlarmeStatus>()
                .HasKey(at => at.IdAlarmeStatus);

            modelBuilder.Entity<AlarmeStatus>()
                .HasMany(at => at.Alarmes)
                .WithOne(a => a.Status)
                .HasForeignKey(a => a.IdAlarme)
                .OnDelete(DeleteBehavior.Restrict);

            // Alarme (Chave composta: IdPosologia, IdRemedio)
            modelBuilder.Entity<Alarme>()
                .HasKey(a => a.IdAlarme);

            modelBuilder.Entity<Alarme>()
                .HasOne(a => a.Posologia)
                .WithMany(p => p.Alarmes)
                .HasForeignKey(a => a.IdPosologia)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Alarme>()
                .HasOne(a => a.Remedio)
                .WithMany(r => r.Alarmes)
                .HasForeignKey(a => a.IdRemedio)
                .OnDelete(DeleteBehavior.Restrict);

            // Promoção (Chave composta: IdFormaDePagamento, IdColaborador, IdRemedio)
            modelBuilder.Entity<Promocao>()
                .HasKey(p => new { p.IdFormaDePagamento, p.IdColaborador, p.IdRemedio });

            modelBuilder.Entity<Promocao>()
                .HasOne(p => p.formaDePagamento)
                .WithMany(f => f.Promocoes)
                .HasForeignKey(p => p.IdFormaDePagamento)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Promocao>()
                .HasOne(p => p.Colaborador)
                .WithMany(u => u.Promocoes)
                .HasForeignKey(p => p.IdColaborador)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Promocao>()
                .HasOne(p => p.remedio)
                .WithMany(r => r.Promocoes)
                .HasForeignKey(p => p.IdRemedio)
                .OnDelete(DeleteBehavior.Restrict);

            // Forma de Pagamento
            modelBuilder.Entity<FormaPagamento>()
                .HasKey(fp => fp.IdFormaPagamento);

            modelBuilder.Entity<FormaPagamento>()
                .HasMany(fp => fp.Promocoes)
                .WithOne(p => p.formaDePagamento)
                .HasForeignKey(p => p.IdFormaDePagamento)
                .OnDelete(DeleteBehavior.Restrict);
        
            #endregion

            #region Seed de Dados

            // Seed para AlarmeStatus
            modelBuilder.Entity<AlarmeStatus>().HasData(
                new AlarmeStatus { IdAlarmeStatus = 1, Descricao = "Ativo" },
                new AlarmeStatus { IdAlarmeStatus = 2, Descricao = "Inativo" }
            );

            // Seed para TipoUtilizador
            modelBuilder.Entity<TipoUtilizador>().HasData(
                new TipoUtilizador { IdTipoUtilizador = 1, Descricao = "Utilizador" },
                new TipoUtilizador { IdTipoUtilizador = 2, Descricao = "Cuidador" },
                new TipoUtilizador { IdTipoUtilizador = 3, Descricao = "Responsável" },
                new TipoUtilizador { IdTipoUtilizador = 4, Descricao = "Parceiro" }
            );

            // Seed para GrauParentesco
            modelBuilder.Entity<GrauParentesco>().HasData(
                new GrauParentesco { IdGrauParentesco = 1, Descricao = "Pai" },
                new GrauParentesco { IdGrauParentesco = 2, Descricao = "Mãe" },
                new GrauParentesco { IdGrauParentesco = 3, Descricao = "Filho" },
                new GrauParentesco { IdGrauParentesco = 4, Descricao = "Filha" },
                new GrauParentesco { IdGrauParentesco = 5, Descricao = "Avô" },
                new GrauParentesco { IdGrauParentesco = 6, Descricao = "Avó" },
                new GrauParentesco { IdGrauParentesco = 7, Descricao = "Tio" },
                new GrauParentesco { IdGrauParentesco = 8, Descricao = "Tia" },
                new GrauParentesco { IdGrauParentesco = 9, Descricao = "Primo" },
                new GrauParentesco { IdGrauParentesco = 10, Descricao = "Prima" },
                new GrauParentesco { IdGrauParentesco = 11, Descricao = "Sobrinho" },
                new GrauParentesco { IdGrauParentesco = 12, Descricao = "Sobrinha" },
                new GrauParentesco { IdGrauParentesco = 13, Descricao = "Cônjuge" },
                new GrauParentesco { IdGrauParentesco = 14, Descricao = "Companheiro" },
                new GrauParentesco { IdGrauParentesco = 15, Descricao = "Companheira" }
            );

            // Seed para TipoOrdemGrandeza (Unidades de Medida para Medicamentos)
            modelBuilder.Entity<TipoOrdemGrandeza>().HasData(
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 1, Descricao = "Miligrama" }, // mg
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 2, Descricao = "Gramas" }, // g
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 3, Descricao = "Litros" }, // L
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 4, Descricao = "Mililitros" }, // mL
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 5, Descricao = "Centímetros cúbicos" }, // cm³
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 6, Descricao = "Unidades internacionais" }, // UI (unidades específicas para vitaminas e hormônios
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 7, Descricao = "Micrograma" }, // mcg
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 8, Descricao = "Quilograma" }, // kg
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 9, Descricao = "Unidade" }, // Unidade (geral para comprimidos, cápsulas, etc.)
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 10, Descricao = "Pipeta" }, // Quantidade medida com pipeta (ex. gotas)
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 11, Descricao = "Tabletes" }, // Tabletes (geral para formas sólidas)
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 12, Descricao = "Doses" }, // Doses específicas (ex. vacina)
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 13, Descricao = "Miliunidade" }, // Miliunidade, usado para medicamentos biológicos
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 14, Descricao = "Cápsulas" }, // Cápsulas de medicamento
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 15, Descricao = "Soluções" }, // Soluções (medicamento diluído)
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 16, Descricao = "Gotas" }, // Gotas (frequente em medicamentos líquidos)
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 17, Descricao = "Miliquilos" }, // mL (reforçando unidade de líquidos)
                new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 18, Descricao = "Injeções" } // Injeções (para medicamentos parenterais)
            );

            
            #endregion
        }

    }
}