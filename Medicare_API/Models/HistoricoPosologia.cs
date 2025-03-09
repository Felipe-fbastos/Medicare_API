using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class HistoricoPosologia
    {
        private int IdPosologia { get; set; }
        private Posologia Posologia { get; set; }
        private int IdRemedio { get; set; }
        private Remedio Remedio { get; set; }
        private int SdPosologia { get; set; }
    }
}