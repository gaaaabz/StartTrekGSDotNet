namespace StartTrekGS.src.StartTrekGS.Application.DTO
{
    public class UsuarioResponseDto
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Ativo { get; set; }

        public string? NomeTipoUsuario { get; set; }
        public int? IdEsp32 { get; set; }
    }
}
