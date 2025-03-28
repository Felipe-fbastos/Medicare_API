public class UtilizadorUpdateDTO
{
    public int IdUtilizador { get; set; }
    public int IdTipoUtilizador { get; set; }
    public required string CPF { get; set; }
    public required string Nome { get; set; }
    public required string Sobrenome { get; set; }
    public required DateTime DtNascimento { get; set; }
    public required string Email { get; set; }
    public required string Telefone { get; set; }
}