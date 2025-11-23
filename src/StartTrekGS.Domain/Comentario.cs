namespace StartTrekGS.src.StartTrekGS.Domain;


public class Comentario
{
    public int IdComentario { get; set; }
    public string? TextoComentario { get; set; }


    public bool Ativo { get; set; }

    // FK  Usuario
    public int IdUsuario { get; set; }
    public Usuario Usuario { get; set; } = null!;

    // FK  Trabalho
    public int IdTrabalho { get; set; }
    public Trabalho Trabalho { get; set; } = null!;
}
