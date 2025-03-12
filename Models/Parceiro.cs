using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Parceiro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string CNPJ { get; set; }
        public ParceiroUtilizador ParceiroUtilizador { get; set; }
    }
}