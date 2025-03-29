using Medicare_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Medicare_API.Data
{
      public class DataContext : DbContext
      {
            public DbSet<TipoUtilizador> TiposUtilizador { get; set; }
            public DbSet<Utilizador> Utilizadores { get; set; }
            public DbSet<Cuidador> Cuidadores { get; set; }
            public DbSet<GrauParentesco> GrausParentesco { get; set; }
            public DbSet<Responsavel> Responsaveis { get; set; }
            public DbSet<Parceiro> Parceiros { get; set; }
            public DbSet<ParceiroUtilizador> ParceirosUtilizador { get; set; }
            public DbSet<Laboratorio> Laboratorios { get; set; }
            public DbSet<TipoOrdemGrandeza> TiposOrdemGrandeza { get; set; }
            public DbSet<Remedio> Remedios { get; set; }
            public DbSet<Posologia> Posologias { get; set; }
            public DbSet<Alarme> Alarmes { get; set; }
            public DbSet<HistoricoPosologia> HistoricosPosologia { get; set; }
            public DbSet<FormaPagamento> FormasPagamento { get; set; }
            public DbSet<Promocao> Promocoes { get; set; }

            public DataContext(DbContextOptions<DataContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                  #region TipoUtilizador
                  modelBuilder.Entity<TipoUtilizador>(entity =>
                  {
                        entity.ToTable("TiposUtilizador");

                        entity.HasKey(e => e.IdTipoUtilizador);


                        entity.Property(e => e.Descricao)
                        .HasMaxLength(14)
                        .IsRequired();
                  });
                  #endregion

                  #region Utilizador
                  modelBuilder.Entity<Utilizador>(entity =>
                  {
                        entity.ToTable("Utilizadores"); ;
                        entity.HasKey(e => e.IdUtilizador);

                        entity.Property(e => e.CPF)
                        .HasMaxLength(14)
                        .IsRequired();

                        entity.Property(e => e.Nome)
                        .HasMaxLength(40)
                        .IsRequired();

                        entity.Property(e => e.Sobrenome)
                        .HasMaxLength(40)
                        .IsRequired();

                        entity.Property(e => e.SenhaSalt)
                        .IsRequired(false);

                        entity.Property(e => e.SenhaHash)
                        .IsRequired(false);

                        entity.Property(e => e.DtNascimento)
                        .IsRequired();

                        entity.Property(e => e.Email)
                        .HasMaxLength(255)
                        .IsRequired();

                        entity.Property(e => e.Telefone)
                        .HasMaxLength(11)
                        .IsRequired(false);

                        entity.HasOne(e => e.TipoUtilizador)
                        .WithMany(u => u.Utilizadores)
                        .HasForeignKey(e => e.IdTipoUtilizador)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);



                        entity.HasIndex(e => new { e.CPF, e.IdTipoUtilizador }).IsUnique(); // CPF único por tipo de utilizador
                  });
                  #endregion

                  #region Cuidador
                  modelBuilder.Entity<Cuidador>(entity =>
                  {
                        entity.ToTable("Cuidadores");

                        entity.HasKey(e => new { e.IdCuidador, e.IdUtilizador });

                        entity.Property(e => e.DtInicio)
                        .IsRequired();

                        entity.Property(e => e.DtFim)
                        .IsRequired(false);

                        entity.Property(e => e.DcCuidador)
                        .IsRequired();

                        entity.Property(e => e.DuCuidador)
                        .IsRequired();

                        entity.Property(e => e.StCuidador)
                        .HasMaxLength(1)
                        .IsRequired();

                        entity.HasOne(e => e.Utilizador)
                        .WithMany()
                        .HasForeignKey(e => e.IdUtilizador)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.CuidadorUtilizador)
                        .WithMany()
                        .HasForeignKey(e => e.IdCuidador)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                  });
                  #endregion

                  #region GrauParentesco
                  modelBuilder.Entity<GrauParentesco>(entity =>
                   {
                         entity.ToTable("GrausParentesco");

                         entity.HasKey(e => e.IdGrauParentesco);

                         entity.Property(e => e.Descricao)
                         .HasMaxLength(15)
                         .IsRequired();
                   });
                  #endregion

                  #region Responsavel
                  modelBuilder.Entity<Responsavel>(entity =>
                  {
                        entity.ToTable("Responsaveis");

                        entity.HasKey(e => new { e.IdResponsavel, e.IdUtilizador });

                        entity.Property(e => e.DcResponsavel)
                          .IsRequired();

                        entity.Property(e => e.DuResponsavel)
                          .IsRequired();

                        entity.Property(e => e.StResponsavel)
                          .HasMaxLength(1)
                          .IsRequired();

                        entity.HasOne(e => e.Utilizador)
                          .WithMany()
                          .HasForeignKey(e => e.IdUtilizador)
                          .IsRequired()
                          .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.ResponsavelUtilizador)
                          .WithMany()
                          .HasForeignKey(e => e.IdResponsavel)
                          .IsRequired()
                          .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.GrauParentesco)
                          .WithMany(re => re.Responsavel)
                          .HasForeignKey(e => e.IdGrauParentesco)
                          .IsRequired()
                          .OnDelete(DeleteBehavior.Restrict);
                  });
                  #endregion

                  #region Parceiro
                  modelBuilder.Entity<Parceiro>(entity =>
                  {
                        entity.ToTable("Parceiros");

                        entity.HasKey(e => e.IdParceiro);

                        entity.Property(e => e.NomeParceiro)
                      .HasMaxLength(50)
                      .IsRequired();

                        entity.Property(e => e.ApelidoParceiro)
                      .HasMaxLength(25)
                      .IsRequired();

                        entity.Property(e => e.CNPJParceiro)
                      .HasMaxLength(18)
                      .IsRequired();

                        entity.HasIndex(e => e.CNPJParceiro).IsUnique(); // CNPJ único
                  });
                  #endregion


                  #region ParceiroUtilizador
                  modelBuilder.Entity<ParceiroUtilizador>(entity =>
                  {
                        entity.ToTable("ParceirosUtilizadores");

                        entity.HasKey(e => new { e.IdParceiro, e.IdColaborador });

                        entity.HasOne(e => e.Parceiro)
                          .WithMany()
                          .HasForeignKey(e => e.IdParceiro)
                          .IsRequired()
                          .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.Colaborador)
                        .WithMany()
                        .HasForeignKey(e => e.IdColaborador)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                  });
                  #endregion

                  #region  Laboratorio
                  modelBuilder.Entity<Laboratorio>(entity =>
                  {
                        entity.ToTable("Laboratorios");

                        entity.HasKey(e => e.IdLaboratorio);

                        entity.Property(e => e.Nome)
                        .HasMaxLength(50)
                        .IsRequired();
                  });
                  #endregion

                  #region TipoOrdemGrandeza
                  modelBuilder.Entity<TipoOrdemGrandeza>(entity =>
                  {
                        entity.ToTable("TiposOrdemGrandeza");

                        entity.HasKey(e => e.IdTipoOrdemGrandeza);

                        entity.Property(e => e.Descricao)
                        .HasMaxLength(25)
                        .IsRequired();

                        entity.Property(e => e.Simbolos)
                        .HasMaxLength(4)
                        .IsRequired();
                  });
                  #endregion

                  #region Remedio
                  modelBuilder.Entity<Remedio>(entity =>
                  {
                        entity.ToTable("Remedios");

                        entity.HasKey(e => e.IdRemedio);

                        entity.Property(e => e.NomeRemedio)
                        .HasMaxLength(255)
                        .IsRequired();

                        entity.Property(e => e.Anotacao)
                        .IsRequired();

                        entity.Property(e => e.Dosagem)
                        .IsRequired();

                        entity.Property(e => e.DtRegistro)
                        .IsRequired();

                        entity.Property(e => e.QtdAlerta)
                        .IsRequired();

                        entity.HasOne(e => e.TipoOrdemGrandeza)
                        .WithMany()
                        .HasForeignKey(e => e.IdTipoOrdemGrandeza)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.Laboratorio)
                        .WithMany()
                        .HasForeignKey(e => e.IdLaboratorio)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                  });
                  #endregion

                  #region Posologia
                  modelBuilder.Entity<Posologia>(entity =>
                  {
                        entity.ToTable("Posologias");

                        entity.HasKey(e => e.IdPosologia);

                        entity.Property(e => e.DtInicio)
                        .IsRequired();

                        entity.Property(e => e.Intervalo)
                        .IsRequired();

                        entity.Property(e => e.QtdRemedio)
                        .IsRequired();

                        entity.HasOne(e => e.Remedio)
                        .WithMany()
                        .HasForeignKey(e => e.IdRemedio)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.Utilizador)
                        .WithMany()
                        .HasForeignKey(e => e.IdUtilizador)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                  });
                  #endregion

                  #region Alarme
                  modelBuilder.Entity<Alarme>(entity =>
                  {
                        entity.ToTable("Alarmes");

                        entity.HasKey(e => e.IdAlarme);

                        entity.Property(e => e.DtHoraAlarme)
                        .IsRequired();

                        entity.Property(e => e.StAlarme)
                        .HasMaxLength(50)
                        .IsRequired();

                        entity.HasOne(e => e.Posologia)
                        .WithMany()
                        .HasForeignKey(e => e.IdPosologia)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.Remedio)
                        .WithMany()
                        .HasForeignKey(e => e.IdRemedio)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                  });
                  #endregion

                  #region HistoricoPosologia
                  modelBuilder.Entity<HistoricoPosologia>(entity =>
                  {
                        entity.ToTable("HistoricosPosologia");

                        entity.HasKey(e => new { e.IdPosologia, e.IdRemedio });

                        entity.Property(e => e.SdPosologia)
                        .IsRequired();

                        entity.HasOne(e => e.Posologia)
                        .WithMany()
                        .HasForeignKey(e => e.IdPosologia)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);

                        entity.HasOne(e => e.Remedio)
                        .WithMany()
                        .HasForeignKey(e => e.IdRemedio)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                  });
                  #endregion

                  #region FormaPagamento
                  modelBuilder.Entity<FormaPagamento>(entity =>
                  {
                        entity.ToTable("FormasPagamento");

                        entity.HasKey(e => e.IdFormaPagamento);

                        entity.Property(e => e.Descricao)
                        .HasMaxLength(45)
                        .IsRequired();

                        entity.Property(e => e.QtdParcelas)
                        .IsRequired();

                        entity.Property(e => e.QtdMinimaParcelas)
                        .IsRequired();
                  });
                  #endregion

                  #region Promocao
                  modelBuilder.Entity<Promocao>(entity =>
                  {
                        entity.ToTable("Promocoes");
                        entity.HasKey(e => e.IdPromocao);

                        entity.Property(e => e.Descricao)
                        .HasMaxLength(40)
                        .IsRequired();

                        entity.Property(e => e.DtInicio)
                        .IsRequired();

                        entity.Property(e => e.DtFim)
                        .IsRequired();

                        entity.Property(e => e.Valor)
                        .IsRequired();
                  });
                  #endregion

                  base.OnModelCreating(modelBuilder);

                  #region Seed de Dados
                  modelBuilder.Entity<TipoUtilizador>().HasData(
                      new TipoUtilizador { IdTipoUtilizador = 1, Descricao = "Utilizador" },
                      new TipoUtilizador { IdTipoUtilizador = 2, Descricao = "Cuidador" },
                      new TipoUtilizador { IdTipoUtilizador = 3, Descricao = "Responsável" },
                      new TipoUtilizador { IdTipoUtilizador = 4, Descricao = "Parceiro" }
                  );

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

                  modelBuilder.Entity<TipoOrdemGrandeza>().HasData(
                       new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 1, Descricao = "Miligrama", Simbolos = "mg" },
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 2, Descricao = "Gramas", Simbolos = "g" },
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 3, Descricao = "Litros", Simbolos = "L" },
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 4, Descricao = "Mililitros", Simbolos = "mL" },
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 5, Descricao = "Centímetros cúbicos", Simbolos = "cm³" },
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 6, Descricao = "Unidades internacionais", Simbolos = "UI" },
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 7, Descricao = "Micrograma", Simbolos = "mcg" },
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 8, Descricao = "Quilograma", Simbolos = "kg" },
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 9, Descricao = "Unidade", Simbolos = "un" },
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 10, Descricao = "Pipeta", Simbolos = "gota" }, // Quantidade medida com pipeta (ex. gotas)
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 11, Descricao = "Tabletes", Simbolos = "un" }, // Tabletes (geral para formas sólidas)
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 12, Descricao = "Doses", Simbolos = "Dose" }, // Doses específicas (ex. vacina)
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 13, Descricao = "Miliunidade", Simbolos = "mUI" }, // Miliunidade, usado para medicamentos biológicos
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 14, Descricao = "Cápsulas", Simbolos = "un" }, // Cápsulas de medicamento
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 15, Descricao = "Soluções", Simbolos = "Sol." }, // Soluções (medicamento diluído)
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 16, Descricao = "Gotas", Simbolos = "gota" }, // Gotas (frequente em medicamentos líquidos)
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 17, Descricao = "Miliquilos", Simbolos = "mL" }, // mL (reforçando unidade de líquidos)
                  new TipoOrdemGrandeza { IdTipoOrdemGrandeza = 18, Descricao = "Injeções", Simbolos = "un" } // Injeções (para medicamentos parenterais)
              );

                  #endregion
            }
      }
}