using System;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class HistoricoPosologia
    {
        [Key]
        public int IdPosologia { get; set; }  // Chave estrangeira para Posologia
        public Posologia posologia { get; set; }  // Relacionamento com Posologia
        public int IdRemedio { get; set; }  // Chave estrangeira para Remedio
        public Remedio remedio { get; set; }  // Relacionamento com Remedio
        public int SdPosologia { get; set; }  // Código de estado ou indicador de situação
    }
}
