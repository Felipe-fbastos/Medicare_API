using System;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class GrauParentesco
    {   
        [Key]
        public int IdGrauParentesco { get; set; }
        public string Descricao { get; set; }

        public List<Responsavel> Responsavel { get; set; } = new(); // Relacionamento 1:N
    }
}