using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Parceiro
    {
        private int Id { get; set; }
        private string Nome { get; set; }
        private string Apelido { get; set; }
        private string CNPJ { get; set; }
    }
}