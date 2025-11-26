namespace DesafioDevNet8.Domain.Entities;

// Classe que representa um vendedor no sistema
public class Vendedor
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    
    // Guarda o total de comissões que o vendedor já ganhou
    public decimal ComissaoTotal { get; set; }

    public Vendedor() { }

    public Vendedor(int id, string nome)
    {
        Id = id;
        Nome = nome;
        ComissaoTotal = 0;
    }
}
