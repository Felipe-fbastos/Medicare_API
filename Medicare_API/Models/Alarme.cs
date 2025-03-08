using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Alarme
    {
    
        private int Id { get; set; }
        private int IdPosologia { get; set; }
        private int IdRemedio { get; set; }
        private DateTime DtHoraAlarme { get; set; }
        private string Situacao { get; set; }
        
    }
}