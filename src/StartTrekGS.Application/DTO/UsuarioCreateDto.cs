namespace StartTrekGS.src.StartTrekGS.Application.DTO
{
    public class UsuarioCreateDto
    {
        public string NomeUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public byte[]? Foto { get; set; }

        public int? IdTipoUsuario { get; set; }
        public int? IdEsp32 { get; set; }
    }
}
