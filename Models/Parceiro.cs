using System;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class Parceiro
    {
        [Key]
        public int IdParceiro { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string CNPJ { get; set; }

        public List<ParceiroUtilizador> ParceiroUtilizador { get; set; } = new(); // Relacionamento 1:N
    }

}