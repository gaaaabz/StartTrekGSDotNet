namespace StartTrekGS.src.StartTrekGS.Domain;



public class TipoUsuario
{
    public int IdTipoUsuario { get; set; }
    public string NomeTipoUsuario { get; set; } = null!;

    // Relacionamento 1:N
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
