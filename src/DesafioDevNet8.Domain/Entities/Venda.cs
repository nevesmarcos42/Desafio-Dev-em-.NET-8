namespace DesafioDevNet8.Domain.Entities;

// Representa uma venda no sistema
public class Venda
{
    public int Id { get; set; }
    public int VendedorId { get; set; }
    public decimal Valor { get; set; }
    
    // Comissão calculada com base no valor da venda
    public decimal Comissao { get; set; }
    
    public DateTime DataVenda { get; set; }

    public Venda() { }

    public Venda(int id, int vendedorId, decimal valor)
    {
        Id = id;
        VendedorId = vendedorId;
        Valor = valor;
        DataVenda = DateTime.Now;
    }
}
