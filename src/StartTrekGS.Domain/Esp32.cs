namespace StartTrekGS.src.StartTrekGS.Domain;


public class Esp32
{
    public int IdEsp32 { get; set; }

    // Relacionamento 1:N
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
