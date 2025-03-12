using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Laboratorio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Remedio> Remedios { get; set; }
    }
}