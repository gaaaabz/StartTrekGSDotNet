namespace StartTrekGS.src.StartTrekGS.Domain;


public class Trabalho
{
    public int IdTrabalho { get; set; }
    public string NomeTrabalho { get; set; } = null!;
    public string DescricaoTrabalho { get; set; } = null!;

    public int IdCategoria { get; set; }
    public Categoria Categoria { get; set; } = null!;

    // Relacionamento 1:N
    public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
}
