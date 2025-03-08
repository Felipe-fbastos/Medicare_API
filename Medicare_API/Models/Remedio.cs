using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Remedio
    {
        private int Id { get; set; }
        private int IdGrandeza { get; set; }
        private int IdLaboratorio { get; set; }
        private string Nome { get; set; }
        private string Anotacao { get; set; }
        private int Dosagem { get; set; }
        private DateTime DtRegistro { get; set; }
        private double QtdAlerta { get; set; }
    }
}