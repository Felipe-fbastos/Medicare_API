using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Medicare_API.Models
{
    public class Laboratorio
    {
        [Key]
        public int IdLaboratorio { get; set; }
        public string Nome { get; set; }
        public List<Remedio> Remedios { get; set; } = new();  // Relacionamento com Remedios
    }
}
