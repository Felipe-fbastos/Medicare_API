using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicare_API.Migrations
{
    /// <inheritdoc />
    public partial class NovaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FORMAS_PAGAMENTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtdParcelas = table.Column<int>(type: "int", nullable: false),
                    QtdMinimaParcelas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORMAS_PAGAMENTO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrauParentesco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrauParentesco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laboratorio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratorio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PARCEIROS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apelido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARCEIROS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_ORDEMGRANDEZA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_ORDEMGRANDEZA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_UTILIZADOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_UTILIZADOR", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "REMEDIO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGrandeza = table.Column<int>(type: "int", nullable: false),
                    IdLaboratorio = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anotacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosagem = table.Column<int>(type: "int", nullable: false),
                    DtRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QtdAlerta = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REMEDIO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REMEDIO_Laboratorio_IdLaboratorio",
                        column: x => x.IdLaboratorio,
                        principalTable: "Laboratorio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_REMEDIO_TIPO_ORDEMGRANDEZA_IdGrandeza",
                        column: x => x.IdGrandeza,
                        principalTable: "TIPO_ORDEMGRANDEZA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UTILIZADOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTipoUtilizador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UTILIZADOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UTILIZADOR_TIPO_UTILIZADOR_IdTipoUtilizador",
                        column: x => x.IdTipoUtilizador,
                        principalTable: "TIPO_UTILIZADOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CUIDADOR",
                columns: table => new
                {
                    IdCuidador = table.Column<int>(type: "int", nullable: false),
                    IdUtilizador = table.Column<int>(type: "int", nullable: false),
                    DtInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DcCuidador = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuCuidador = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Situacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilizadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUIDADOR", x => new { x.IdUtilizador, x.IdCuidador });
                    table.ForeignKey(
                        name: "FK_CUIDADOR_UTILIZADOR_IdCuidador",
                        column: x => x.IdCuidador,
                        principalTable: "UTILIZADOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CUIDADOR_UTILIZADOR_IdUtilizador",
                        column: x => x.IdUtilizador,
                        principalTable: "UTILIZADOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CUIDADOR_UTILIZADOR_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "UTILIZADOR",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PARCEIRO_UTILIZADORES",
                columns: table => new
                {
                    IdParceiro = table.Column<int>(type: "int", nullable: false),
                    IdColaborador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARCEIRO_UTILIZADORES", x => x.IdParceiro);
                    table.ForeignKey(
                        name: "FK_PARCEIRO_UTILIZADORES_PARCEIROS_IdParceiro",
                        column: x => x.IdParceiro,
                        principalTable: "PARCEIROS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PARCEIRO_UTILIZADORES_UTILIZADOR_IdColaborador",
                        column: x => x.IdColaborador,
                        principalTable: "UTILIZADOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "POSOLOGIA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdRemedio = table.Column<int>(type: "int", nullable: false),
                    IdUtilizador = table.Column<int>(type: "int", nullable: false),
                    DtInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Intervalo = table.Column<int>(type: "int", nullable: false),
                    QtdRemedio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSOLOGIA", x => new { x.Id, x.IdRemedio });
                    table.ForeignKey(
                        name: "FK_POSOLOGIA_REMEDIO_IdRemedio",
                        column: x => x.IdRemedio,
                        principalTable: "REMEDIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_POSOLOGIA_UTILIZADOR_IdUtilizador",
                        column: x => x.IdUtilizador,
                        principalTable: "UTILIZADOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PROMOCOES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFormaDePagamento = table.Column<int>(type: "int", nullable: false),
                    formaDePagamentoId = table.Column<int>(type: "int", nullable: false),
                    IdColaborador = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRemedio = table.Column<int>(type: "int", nullable: false),
                    DtInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROMOCOES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PROMOCOES_FORMAS_PAGAMENTO_formaDePagamentoId",
                        column: x => x.formaDePagamentoId,
                        principalTable: "FORMAS_PAGAMENTO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PROMOCOES_REMEDIO_IdRemedio",
                        column: x => x.IdRemedio,
                        principalTable: "REMEDIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PROMOCOES_UTILIZADOR_IdColaborador",
                        column: x => x.IdColaborador,
                        principalTable: "UTILIZADOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RESPONSAVEL",
                columns: table => new
                {
                    IdUtilizador = table.Column<int>(type: "int", nullable: false),
                    IdResponsavel = table.Column<int>(type: "int", nullable: false),
                    IdGrauParentesco = table.Column<int>(type: "int", nullable: false),
                    DtCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Situacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilizadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESPONSAVEL", x => new { x.IdUtilizador, x.IdResponsavel });
                    table.ForeignKey(
                        name: "FK_RESPONSAVEL_GrauParentesco_IdGrauParentesco",
                        column: x => x.IdGrauParentesco,
                        principalTable: "GrauParentesco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RESPONSAVEL_UTILIZADOR_IdResponsavel",
                        column: x => x.IdResponsavel,
                        principalTable: "UTILIZADOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RESPONSAVEL_UTILIZADOR_IdUtilizador",
                        column: x => x.IdUtilizador,
                        principalTable: "UTILIZADOR",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RESPONSAVEL_UTILIZADOR_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "UTILIZADOR",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ALARMES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPosologia = table.Column<int>(type: "int", nullable: false),
                    IdRemedio = table.Column<int>(type: "int", nullable: false),
                    DtHoraAlarme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Situacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALARMES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ALARMES_POSOLOGIA_IdPosologia_IdRemedio",
                        columns: x => new { x.IdPosologia, x.IdRemedio },
                        principalTable: "POSOLOGIA",
                        principalColumns: new[] { "Id", "IdRemedio" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ALARMES_REMEDIO_IdRemedio",
                        column: x => x.IdRemedio,
                        principalTable: "REMEDIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HISTORICO_POSOLOGIA",
                columns: table => new
                {
                    IdPosologia = table.Column<int>(type: "int", nullable: false),
                    IdRemedio = table.Column<int>(type: "int", nullable: false),
                    RemedioId = table.Column<int>(type: "int", nullable: false),
                    SdPosologia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HISTORICO_POSOLOGIA", x => new { x.IdPosologia, x.IdRemedio });
                    table.ForeignKey(
                        name: "FK_HISTORICO_POSOLOGIA_POSOLOGIA_IdPosologia_IdRemedio",
                        columns: x => new { x.IdPosologia, x.IdRemedio },
                        principalTable: "POSOLOGIA",
                        principalColumns: new[] { "Id", "IdRemedio" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HISTORICO_POSOLOGIA_REMEDIO_RemedioId",
                        column: x => x.RemedioId,
                        principalTable: "REMEDIO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALARMES_IdPosologia_IdRemedio",
                table: "ALARMES",
                columns: new[] { "IdPosologia", "IdRemedio" });

            migrationBuilder.CreateIndex(
                name: "IX_ALARMES_IdRemedio",
                table: "ALARMES",
                column: "IdRemedio");

            migrationBuilder.CreateIndex(
                name: "IX_CUIDADOR_IdCuidador",
                table: "CUIDADOR",
                column: "IdCuidador");

            migrationBuilder.CreateIndex(
                name: "IX_CUIDADOR_UtilizadorId",
                table: "CUIDADOR",
                column: "UtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICO_POSOLOGIA_RemedioId",
                table: "HISTORICO_POSOLOGIA",
                column: "RemedioId");

            migrationBuilder.CreateIndex(
                name: "IX_PARCEIRO_UTILIZADORES_IdColaborador",
                table: "PARCEIRO_UTILIZADORES",
                column: "IdColaborador");

            migrationBuilder.CreateIndex(
                name: "IX_POSOLOGIA_IdRemedio",
                table: "POSOLOGIA",
                column: "IdRemedio");

            migrationBuilder.CreateIndex(
                name: "IX_POSOLOGIA_IdUtilizador",
                table: "POSOLOGIA",
                column: "IdUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_PROMOCOES_formaDePagamentoId",
                table: "PROMOCOES",
                column: "formaDePagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PROMOCOES_IdColaborador",
                table: "PROMOCOES",
                column: "IdColaborador");

            migrationBuilder.CreateIndex(
                name: "IX_PROMOCOES_IdRemedio",
                table: "PROMOCOES",
                column: "IdRemedio");

            migrationBuilder.CreateIndex(
                name: "IX_REMEDIO_IdGrandeza",
                table: "REMEDIO",
                column: "IdGrandeza");

            migrationBuilder.CreateIndex(
                name: "IX_REMEDIO_IdLaboratorio",
                table: "REMEDIO",
                column: "IdLaboratorio");

            migrationBuilder.CreateIndex(
                name: "IX_RESPONSAVEL_IdGrauParentesco",
                table: "RESPONSAVEL",
                column: "IdGrauParentesco");

            migrationBuilder.CreateIndex(
                name: "IX_RESPONSAVEL_IdResponsavel",
                table: "RESPONSAVEL",
                column: "IdResponsavel");

            migrationBuilder.CreateIndex(
                name: "IX_RESPONSAVEL_UtilizadorId",
                table: "RESPONSAVEL",
                column: "UtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_UTILIZADOR_IdTipoUtilizador",
                table: "UTILIZADOR",
                column: "IdTipoUtilizador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALARMES");

            migrationBuilder.DropTable(
                name: "CUIDADOR");

            migrationBuilder.DropTable(
                name: "HISTORICO_POSOLOGIA");

            migrationBuilder.DropTable(
                name: "PARCEIRO_UTILIZADORES");

            migrationBuilder.DropTable(
                name: "PROMOCOES");

            migrationBuilder.DropTable(
                name: "RESPONSAVEL");

            migrationBuilder.DropTable(
                name: "POSOLOGIA");

            migrationBuilder.DropTable(
                name: "PARCEIROS");

            migrationBuilder.DropTable(
                name: "FORMAS_PAGAMENTO");

            migrationBuilder.DropTable(
                name: "GrauParentesco");

            migrationBuilder.DropTable(
                name: "REMEDIO");

            migrationBuilder.DropTable(
                name: "UTILIZADOR");

            migrationBuilder.DropTable(
                name: "Laboratorio");

            migrationBuilder.DropTable(
                name: "TIPO_ORDEMGRANDEZA");

            migrationBuilder.DropTable(
                name: "TIPO_UTILIZADOR");
        }
    }
}
