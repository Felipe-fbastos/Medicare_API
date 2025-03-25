using System;
using System.ComponentModel.DataAnnotations;

namespace Medicare_API.Models
{
    
    public class Alarme
    {
        [Key]
        public int IdAlarme { get; set; }
        public int IdPosologia { get; set; }  // Chave estrangeira para Posologia
        public Posologia Posologia { get; set; }  // Relacionamento com Posologia
        public int IdRemedio { get; set; }  // Chave estrangeira para Remedio
        public Remedio Remedio { get; set; }  // Relacionamento com Remedio
        public DateTime DtHoraAlarme { get; set; }
        public AlarmeStatus Status { get; set; }  // Usando o enum para representar o status
    }
}
