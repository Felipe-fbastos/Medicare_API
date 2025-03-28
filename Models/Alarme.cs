using System;
using System.ComponentModel.DataAnnotations;

namespace Medicare_API.Models
{

    public class Alarme
    {
        public int IdAlarme { get; set; }//PK

        public int IdPosologia { get; set; } //FK
        public Posologia? Posologia { get; set; } //FK - Relacionamento com Posologia

        public int IdRemedio { get; set; }  //FK
        public Remedio? Remedio { get; set; } //FK - Relacionamento com Remedio

        public DateTime DtHoraAlarme { get; set; }
        public string StAlarme { get; set; }

        public Alarme(int idAlarme, int idPosologia, int idRemedio, DateTime dtHoraAlarme, string stAlarme)
        {
            IdAlarme = idAlarme;
            IdPosologia = idPosologia;
            IdRemedio = idRemedio;
            DtHoraAlarme = dtHoraAlarme;
            StAlarme = stAlarme;
        }
    }
}