namespace StartTrekGS.src.StartTrekGS.Domain;


public class Categoria
{
    public int IdCategoria { get; set; }
    public string NomeCategoria { get; set; } = null!;
    public string DescricaoCategoria { get; set; } = null!;

    // 1 Categoria tem vários Trabalhos
    public ICollection<Trabalho> Trabalhos { get; set; } = new List<Trabalho>();
}
