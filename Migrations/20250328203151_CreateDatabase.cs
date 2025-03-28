using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Medicare_API.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormasPagamento",
                columns: table => new
                {
                    IdFormaPagamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    QtdParcelas = table.Column<int>(type: "int", nullable: false),
                    QtdMinimaParcelas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasPagamento", x => x.IdFormaPagamento);
                });

            migrationBuilder.CreateTable(
                name: "GrausParentesco",
                columns: table => new
                {
                    IdGrauParentesco = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrausParentesco", x => x.IdGrauParentesco);
                });

            migrationBuilder.CreateTable(
                name: "Laboratorios",
                columns: table => new
                {
                    IdLaboratorio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratorios", x => x.IdLaboratorio);
                });

            migrationBuilder.CreateTable(
                name: "Parceiros",
                columns: table => new
                {
                    IdParceiro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeParceiro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApelidoParceiro = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CNPJParceiro = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parceiros", x => x.IdParceiro);
                });

            migrationBuilder.CreateTable(
                name: "TiposOrdemGrandeza",
                columns: table => new
                {
                    IdTipoOrdemGrandeza = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Simbolos = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposOrdemGrandeza", x => x.IdTipoOrdemGrandeza);
                });

            migrationBuilder.CreateTable(
                name: "TiposUtilizador",
                columns: table => new
                {
                    IdTipoUtilizador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposUtilizador", x => x.IdTipoUtilizador);
                });

            migrationBuilder.CreateTable(
                name: "Remedios",
                columns: table => new
                {
                    IdRemedio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoOrdemGrandeza = table.Column<int>(type: "int", nullable: false),
                    IdLaboratorio = table.Column<int>(type: "int", nullable: false),
                    NomeRemedio = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Anotacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dosagem = table.Column<int>(type: "int", nullable: false),
                    DtRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QtdAlerta = table.Column<double>(type: "float", nullable: false),
                    LaboratorioIdLaboratorio = table.Column<int>(type: "int", nullable: true),
                    TipoOrdemGrandezaIdTipoOrdemGrandeza = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remedios", x => x.IdRemedio);
                    table.ForeignKey(
                        name: "FK_Remedios_Laboratorios_IdLaboratorio",
                        column: x => x.IdLaboratorio,
                        principalTable: "Laboratorios",
                        principalColumn: "IdLaboratorio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Remedios_Laboratorios_LaboratorioIdLaboratorio",
                        column: x => x.LaboratorioIdLaboratorio,
                        principalTable: "Laboratorios",
                        principalColumn: "IdLaboratorio");
                    table.ForeignKey(
                        name: "FK_Remedios_TiposOrdemGrandeza_IdTipoOrdemGrandeza",
                        column: x => x.IdTipoOrdemGrandeza,
                        principalTable: "TiposOrdemGrandeza",
                        principalColumn: "IdTipoOrdemGrandeza",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Remedios_TiposOrdemGrandeza_TipoOrdemGrandezaIdTipoOrdemGrandeza",
                        column: x => x.TipoOrdemGrandezaIdTipoOrdemGrandeza,
                        principalTable: "TiposOrdemGrandeza",
                        principalColumn: "IdTipoOrdemGrandeza");
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    IdUtilizador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTipoUtilizador = table.Column<int>(type: "int", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DtNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.IdUtilizador);
                    table.ForeignKey(
                        name: "FK_Utilizadores_TiposUtilizador_IdTipoUtilizador",
                        column: x => x.IdTipoUtilizador,
                        principalTable: "TiposUtilizador",
                        principalColumn: "IdTipoUtilizador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cuidadores",
                columns: table => new
                {
                    IdCuidador = table.Column<int>(type: "int", nullable: false),
                    IdUtilizador = table.Column<int>(type: "int", nullable: false),
                    DtInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DcCuidador = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuCuidador = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StCuidador = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UtilizadorIdUtilizador = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuidadores", x => new { x.IdCuidador, x.IdUtilizador });
                    table.ForeignKey(
                        name: "FK_Cuidadores_Utilizadores_IdCuidador",
                        column: x => x.IdCuidador,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cuidadores_Utilizadores_IdUtilizador",
                        column: x => x.IdUtilizador,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cuidadores_Utilizadores_UtilizadorIdUtilizador",
                        column: x => x.UtilizadorIdUtilizador,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador");
                });

            migrationBuilder.CreateTable(
                name: "ParceirosUtilizadores",
                columns: table => new
                {
                    IdParceiro = table.Column<int>(type: "int", nullable: false),
                    IdColaborador = table.Column<int>(type: "int", nullable: false),
                    ParceiroIdParceiro = table.Column<int>(type: "int", nullable: true),
                    UtilizadorIdUtilizador = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParceirosUtilizadores", x => new { x.IdParceiro, x.IdColaborador });
                    table.ForeignKey(
                        name: "FK_ParceirosUtilizadores_Parceiros_IdParceiro",
                        column: x => x.IdParceiro,
                        principalTable: "Parceiros",
                        principalColumn: "IdParceiro",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParceirosUtilizadores_Parceiros_ParceiroIdParceiro",
                        column: x => x.ParceiroIdParceiro,
                        principalTable: "Parceiros",
                        principalColumn: "IdParceiro");
                    table.ForeignKey(
                        name: "FK_ParceirosUtilizadores_Utilizadores_IdColaborador",
                        column: x => x.IdColaborador,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParceirosUtilizadores_Utilizadores_UtilizadorIdUtilizador",
                        column: x => x.UtilizadorIdUtilizador,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador");
                });

            migrationBuilder.CreateTable(
                name: "Posologias",
                columns: table => new
                {
                    IdPosologia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRemedio = table.Column<int>(type: "int", nullable: false),
                    IdUtilizador = table.Column<int>(type: "int", nullable: false),
                    DtInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Intervalo = table.Column<int>(type: "int", nullable: false),
                    QtdRemedio = table.Column<int>(type: "int", nullable: false),
                    RemedioIdRemedio = table.Column<int>(type: "int", nullable: true),
                    UtilizadorIdUtilizador = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posologias", x => x.IdPosologia);
                    table.ForeignKey(
                        name: "FK_Posologias_Remedios_IdRemedio",
                        column: x => x.IdRemedio,
                        principalTable: "Remedios",
                        principalColumn: "IdRemedio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posologias_Remedios_RemedioIdRemedio",
                        column: x => x.RemedioIdRemedio,
                        principalTable: "Remedios",
                        principalColumn: "IdRemedio");
                    table.ForeignKey(
                        name: "FK_Posologias_Utilizadores_IdUtilizador",
                        column: x => x.IdUtilizador,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posologias_Utilizadores_UtilizadorIdUtilizador",
                        column: x => x.UtilizadorIdUtilizador,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador");
                });

            migrationBuilder.CreateTable(
                name: "Promocoes",
                columns: table => new
                {
                    IdPromocao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFormaPagamento = table.Column<int>(type: "int", nullable: false),
                    FormaPagamentoIdFormaPagamento = table.Column<int>(type: "int", nullable: true),
                    IdColaborador = table.Column<int>(type: "int", nullable: false),
                    ColaboradorIdUtilizador = table.Column<int>(type: "int", nullable: true),
                    IdRemedio = table.Column<int>(type: "int", nullable: false),
                    RemedioIdRemedio = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DtInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocoes", x => x.IdPromocao);
                    table.ForeignKey(
                        name: "FK_Promocoes_FormasPagamento_FormaPagamentoIdFormaPagamento",
                        column: x => x.FormaPagamentoIdFormaPagamento,
                        principalTable: "FormasPagamento",
                        principalColumn: "IdFormaPagamento");
                    table.ForeignKey(
                        name: "FK_Promocoes_Remedios_RemedioIdRemedio",
                        column: x => x.RemedioIdRemedio,
                        principalTable: "Remedios",
                        principalColumn: "IdRemedio");
                    table.ForeignKey(
                        name: "FK_Promocoes_Utilizadores_ColaboradorIdUtilizador",
                        column: x => x.ColaboradorIdUtilizador,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador");
                });

            migrationBuilder.CreateTable(
                name: "Responsaveis",
                columns: table => new
                {
                    IdResponsavel = table.Column<int>(type: "int", nullable: false),
                    IdUtilizador = table.Column<int>(type: "int", nullable: false),
                    IdGrauParentesco = table.Column<int>(type: "int", nullable: false),
                    DcResponsavel = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuResponsavel = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StResponsavel = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    UtilizadorIdUtilizador = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsaveis", x => new { x.IdResponsavel, x.IdUtilizador });
                    table.ForeignKey(
                        name: "FK_Responsaveis_GrausParentesco_IdGrauParentesco",
                        column: x => x.IdGrauParentesco,
                        principalTable: "GrausParentesco",
                        principalColumn: "IdGrauParentesco",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Responsaveis_Utilizadores_IdResponsavel",
                        column: x => x.IdResponsavel,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Responsaveis_Utilizadores_IdUtilizador",
                        column: x => x.IdUtilizador,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Responsaveis_Utilizadores_UtilizadorIdUtilizador",
                        column: x => x.UtilizadorIdUtilizador,
                        principalTable: "Utilizadores",
                        principalColumn: "IdUtilizador");
                });

            migrationBuilder.CreateTable(
                name: "Alarmes",
                columns: table => new
                {
                    IdAlarme = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPosologia = table.Column<int>(type: "int", nullable: false),
                    IdRemedio = table.Column<int>(type: "int", nullable: false),
                    DtHoraAlarme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StAlarme = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PosologiaIdPosologia = table.Column<int>(type: "int", nullable: true),
                    RemedioIdRemedio = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarmes", x => x.IdAlarme);
                    table.ForeignKey(
                        name: "FK_Alarmes_Posologias_IdPosologia",
                        column: x => x.IdPosologia,
                        principalTable: "Posologias",
                        principalColumn: "IdPosologia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alarmes_Posologias_PosologiaIdPosologia",
                        column: x => x.PosologiaIdPosologia,
                        principalTable: "Posologias",
                        principalColumn: "IdPosologia");
                    table.ForeignKey(
                        name: "FK_Alarmes_Remedios_IdRemedio",
                        column: x => x.IdRemedio,
                        principalTable: "Remedios",
                        principalColumn: "IdRemedio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Alarmes_Remedios_RemedioIdRemedio",
                        column: x => x.RemedioIdRemedio,
                        principalTable: "Remedios",
                        principalColumn: "IdRemedio");
                });

            migrationBuilder.CreateTable(
                name: "HistoricosPosologia",
                columns: table => new
                {
                    IdPosologia = table.Column<int>(type: "int", nullable: false),
                    IdRemedio = table.Column<int>(type: "int", nullable: false),
                    SdPosologia = table.Column<int>(type: "int", nullable: false),
                    PosologiaIdPosologia = table.Column<int>(type: "int", nullable: true),
                    RemedioIdRemedio = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosPosologia", x => new { x.IdPosologia, x.IdRemedio });
                    table.ForeignKey(
                        name: "FK_HistoricosPosologia_Posologias_IdPosologia",
                        column: x => x.IdPosologia,
                        principalTable: "Posologias",
                        principalColumn: "IdPosologia",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricosPosologia_Posologias_PosologiaIdPosologia",
                        column: x => x.PosologiaIdPosologia,
                        principalTable: "Posologias",
                        principalColumn: "IdPosologia");
                    table.ForeignKey(
                        name: "FK_HistoricosPosologia_Remedios_IdRemedio",
                        column: x => x.IdRemedio,
                        principalTable: "Remedios",
                        principalColumn: "IdRemedio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricosPosologia_Remedios_RemedioIdRemedio",
                        column: x => x.RemedioIdRemedio,
                        principalTable: "Remedios",
                        principalColumn: "IdRemedio");
                });

            migrationBuilder.InsertData(
                table: "GrausParentesco",
                columns: new[] { "IdGrauParentesco", "Descricao" },
                values: new object[,]
                {
                    { 1, "Pai" },
                    { 2, "Mãe" },
                    { 3, "Filho" },
                    { 4, "Filha" },
                    { 5, "Avô" },
                    { 6, "Avó" },
                    { 7, "Tio" },
                    { 8, "Tia" },
                    { 9, "Primo" },
                    { 10, "Prima" },
                    { 11, "Sobrinho" },
                    { 12, "Sobrinha" },
                    { 13, "Cônjuge" },
                    { 14, "Companheiro" },
                    { 15, "Companheira" }
                });

            migrationBuilder.InsertData(
                table: "TiposOrdemGrandeza",
                columns: new[] { "IdTipoOrdemGrandeza", "Descricao", "Simbolos" },
                values: new object[,]
                {
                    { 1, "Miligrama", "mg" },
                    { 2, "Gramas", "g" },
                    { 3, "Litros", "L" },
                    { 4, "Mililitros", "mL" },
                    { 5, "Centímetros cúbicos", "cm³" },
                    { 6, "Unidades internacionais", "UI" },
                    { 7, "Micrograma", "mcg" },
                    { 8, "Quilograma", "kg" },
                    { 9, "Unidade", "un" },
                    { 10, "Pipeta", "gota" },
                    { 11, "Tabletes", "un" },
                    { 12, "Doses", "Dose" },
                    { 13, "Miliunidade", "mUI" },
                    { 14, "Cápsulas", "un" },
                    { 15, "Soluções", "Sol." },
                    { 16, "Gotas", "gota" },
                    { 17, "Miliquilos", "mL" },
                    { 18, "Injeções", "un" }
                });

            migrationBuilder.InsertData(
                table: "TiposUtilizador",
                columns: new[] { "IdTipoUtilizador", "Descricao" },
                values: new object[,]
                {
                    { 1, "Utilizador" },
                    { 2, "Cuidador" },
                    { 3, "Responsável" },
                    { 4, "Parceiro" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alarmes_IdPosologia",
                table: "Alarmes",
                column: "IdPosologia");

            migrationBuilder.CreateIndex(
                name: "IX_Alarmes_IdRemedio",
                table: "Alarmes",
                column: "IdRemedio");

            migrationBuilder.CreateIndex(
                name: "IX_Alarmes_PosologiaIdPosologia",
                table: "Alarmes",
                column: "PosologiaIdPosologia");

            migrationBuilder.CreateIndex(
                name: "IX_Alarmes_RemedioIdRemedio",
                table: "Alarmes",
                column: "RemedioIdRemedio");

            migrationBuilder.CreateIndex(
                name: "IX_Cuidadores_IdUtilizador",
                table: "Cuidadores",
                column: "IdUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Cuidadores_UtilizadorIdUtilizador",
                table: "Cuidadores",
                column: "UtilizadorIdUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosPosologia_IdRemedio",
                table: "HistoricosPosologia",
                column: "IdRemedio");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosPosologia_PosologiaIdPosologia",
                table: "HistoricosPosologia",
                column: "PosologiaIdPosologia");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosPosologia_RemedioIdRemedio",
                table: "HistoricosPosologia",
                column: "RemedioIdRemedio");

            migrationBuilder.CreateIndex(
                name: "IX_Parceiros_CNPJParceiro",
                table: "Parceiros",
                column: "CNPJParceiro",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParceirosUtilizadores_IdColaborador",
                table: "ParceirosUtilizadores",
                column: "IdColaborador");

            migrationBuilder.CreateIndex(
                name: "IX_ParceirosUtilizadores_ParceiroIdParceiro",
                table: "ParceirosUtilizadores",
                column: "ParceiroIdParceiro");

            migrationBuilder.CreateIndex(
                name: "IX_ParceirosUtilizadores_UtilizadorIdUtilizador",
                table: "ParceirosUtilizadores",
                column: "UtilizadorIdUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Posologias_IdRemedio",
                table: "Posologias",
                column: "IdRemedio");

            migrationBuilder.CreateIndex(
                name: "IX_Posologias_IdUtilizador",
                table: "Posologias",
                column: "IdUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Posologias_RemedioIdRemedio",
                table: "Posologias",
                column: "RemedioIdRemedio");

            migrationBuilder.CreateIndex(
                name: "IX_Posologias_UtilizadorIdUtilizador",
                table: "Posologias",
                column: "UtilizadorIdUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Promocoes_ColaboradorIdUtilizador",
                table: "Promocoes",
                column: "ColaboradorIdUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Promocoes_FormaPagamentoIdFormaPagamento",
                table: "Promocoes",
                column: "FormaPagamentoIdFormaPagamento");

            migrationBuilder.CreateIndex(
                name: "IX_Promocoes_RemedioIdRemedio",
                table: "Promocoes",
                column: "RemedioIdRemedio");

            migrationBuilder.CreateIndex(
                name: "IX_Remedios_IdLaboratorio",
                table: "Remedios",
                column: "IdLaboratorio");

            migrationBuilder.CreateIndex(
                name: "IX_Remedios_IdTipoOrdemGrandeza",
                table: "Remedios",
                column: "IdTipoOrdemGrandeza");

            migrationBuilder.CreateIndex(
                name: "IX_Remedios_LaboratorioIdLaboratorio",
                table: "Remedios",
                column: "LaboratorioIdLaboratorio");

            migrationBuilder.CreateIndex(
                name: "IX_Remedios_TipoOrdemGrandezaIdTipoOrdemGrandeza",
                table: "Remedios",
                column: "TipoOrdemGrandezaIdTipoOrdemGrandeza");

            migrationBuilder.CreateIndex(
                name: "IX_Responsaveis_IdGrauParentesco",
                table: "Responsaveis",
                column: "IdGrauParentesco");

            migrationBuilder.CreateIndex(
                name: "IX_Responsaveis_IdUtilizador",
                table: "Responsaveis",
                column: "IdUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Responsaveis_UtilizadorIdUtilizador",
                table: "Responsaveis",
                column: "UtilizadorIdUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_CPF_IdTipoUtilizador",
                table: "Utilizadores",
                columns: new[] { "CPF", "IdTipoUtilizador" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilizadores_IdTipoUtilizador",
                table: "Utilizadores",
                column: "IdTipoUtilizador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarmes");

            migrationBuilder.DropTable(
                name: "Cuidadores");

            migrationBuilder.DropTable(
                name: "HistoricosPosologia");

            migrationBuilder.DropTable(
                name: "ParceirosUtilizadores");

            migrationBuilder.DropTable(
                name: "Promocoes");

            migrationBuilder.DropTable(
                name: "Responsaveis");

            migrationBuilder.DropTable(
                name: "Posologias");

            migrationBuilder.DropTable(
                name: "Parceiros");

            migrationBuilder.DropTable(
                name: "FormasPagamento");

            migrationBuilder.DropTable(
                name: "GrausParentesco");

            migrationBuilder.DropTable(
                name: "Remedios");

            migrationBuilder.DropTable(
                name: "Utilizadores");

            migrationBuilder.DropTable(
                name: "Laboratorios");

            migrationBuilder.DropTable(
                name: "TiposOrdemGrandeza");

            migrationBuilder.DropTable(
                name: "TiposUtilizador");
        }
    }
}
