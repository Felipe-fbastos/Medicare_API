using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicare_API.Models
{
    public class Utilizador
    {
        private int Id { get; set; }        
        private int IdTipoUtilizador { get; set; }
        private string Nome { get; set; }
        private string Sobrenome { get; set; }
        private string CPF { get; set; }
        private string Email { get; set; }
        private string Telefone { get; set; }
    }
}