using System;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class Promocao
    {
        public int IdPromocao { get; set; }//PK

        public int IdFormaPagamento { get; set; }  //FK
        public FormaPagamento? FormaPagamento { get; set; }  // Relacionamento com FormaPagamento

        public int IdColaborador { get; set; }  //FK
        public Utilizador? Colaborador { get; set; }  // Relacionamento com Utilizador(Colaborador)

        public int IdRemedio { get; set; }  // FK
        public Remedio? Remedio { get; set; }  // Relacionamento com Remedio

        public string Descricao { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public double Valor { get; set; }

        public Promocao(int idFormaPagamento, int idColaborador, int idRemedio, string descricao, DateTime dtInicio, DateTime dtFim, double valor)
        {
            IdFormaPagamento = idFormaPagamento;
            IdColaborador = idColaborador;
            IdRemedio = idRemedio;
            Descricao = descricao;
            DtInicio = dtInicio;
            DtFim = dtFim;
            Valor = valor;
        }
    }
}
