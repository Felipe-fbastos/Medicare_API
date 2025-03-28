using System;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class HistoricoPosologia
    {
        public int IdPosologia { get; set; }  // PK - FK
        public Posologia? Posologia { get; set; }  //FK - Relacionamento com Posologia

        public int IdRemedio { get; set; }  // PK - FK
        public Remedio? Remedio { get; set; }  //FK - Relacionamento com Remedio

        public int SdPosologia { get; set; }

        public HistoricoPosologia(int idPosologia, int idRemedio, int sdPosologia)
        {
            IdPosologia = idPosologia;
            IdRemedio = idRemedio;
            SdPosologia = sdPosologia;
        }

    }
}
