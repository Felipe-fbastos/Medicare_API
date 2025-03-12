using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class HistoricoPosologia
    {
        public int IdPosologia { get; set; }
        public Posologia Posologia { get; set; }
        public int IdRemedio { get; set; }
        public Remedio Remedio { get; set; }
        public int SdPosologia { get; set; }
    }
}