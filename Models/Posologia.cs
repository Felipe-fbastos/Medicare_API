using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class Posologia
    {
        public int IdPosologia { get; set; }//PK

        public int IdRemedio { get; set; }  //FK
        public Remedio? Remedio { get; set; }  //FK - Relacionamento com Remedio

        public int IdUtilizador { get; set; }  //FK
        public Utilizador? Utilizador { get; set; }  //FK - Relacionamento com Utilizador

        public DateTime DtInicio { get; set; }
        public DateTime? DtFim { get; set; }
        public int Intervalo { get; set; }
        public int QtdRemedio { get; set; }

        // Relacionamentos
        public List<HistoricoPosologia> HistoricosPosologia { get; set; } = new();
        public List<Alarme> Alarmes { get; set; } = new();

        public Posologia(int idPosologia, int idRemedio, int idUtilizador, DateTime dtInicio, DateTime? dtFim, int intervalo, int qtdRemedio)
        {
            IdPosologia = idPosologia;
            IdRemedio = idRemedio;
            IdUtilizador = idUtilizador;
            DtInicio = dtInicio;
            DtFim = dtFim;
            Intervalo = intervalo;
            QtdRemedio = qtdRemedio;
        }
    }
}