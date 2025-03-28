using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class Remedio
    {
        public int IdRemedio { get; set; }

        public int IdTipoOrdemGrandeza { get; set; }
        public TipoOrdemGrandeza? TipoOrdemGrandeza { get; set; }

        public int IdLaboratorio { get; set; }
        public Laboratorio? Laboratorio { get; set; }

        public string NomeRemedio { get; set; }
        public string Anotacao { get; set; }
        public int Dosagem { get; set; }
        public DateTime DtRegistro { get; set; }
        public double QtdAlerta { get; set; }

        public List<Posologia> Posologias { get; set; } = new();
        public List<HistoricoPosologia> HistoricosPosologia { get; set; } = new();
        public List<Alarme> Alarmes { get; set; } = new();
        public List<Promocao> Promocoes { get; set; } = new();

        public Remedio( int idTipoOrdemGrandeza, int idLaboratorio, string nomeRemedio, string anotacao, int dosagem, DateTime dtRegistro, double qtdAlerta)
        {
            
            IdTipoOrdemGrandeza = idTipoOrdemGrandeza;
            IdLaboratorio = idLaboratorio;
            NomeRemedio = nomeRemedio;
            Anotacao = anotacao;
            Dosagem = dosagem;
            DtRegistro = dtRegistro;
            QtdAlerta = qtdAlerta;
        }
    }
}
