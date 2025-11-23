namespace StartTrekGS.src.StartTrekGS.Domain
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string NomeUsuario { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Senha { get; set; } = null!;


        public byte[]? Foto { get; set; }

   
        public bool Ativo { get; set; }

    
        public int? IdTipoUsuario { get; set; }
        public TipoUsuario? TipoUsuario { get; set; }

        public int? IdEsp32 { get; set; }
        public Esp32? Esp32 { get; set; }

        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
