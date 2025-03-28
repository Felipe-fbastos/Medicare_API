using System.Text.Json.Serialization;


namespace Medicare_API.Models
{
    public class CuidadorDTO
    {
        public required int IdCuidador { get; set; }
        public required int IdUtilizador { get; set; }
        public required DateTime DtInicio { get; set; }
        public DateTime? DtFim { get; set; }
        public required string StCuidador { get; set; }
    }
}
