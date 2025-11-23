namespace StartTrekGS.src.StartTrekGS.Application.DTO
{
    public class UsuarioUpdateDto
    {
        public string? NomeUsuario { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public byte[]? Foto { get; set; }

        public int? IdTipoUsuario { get; set; }
        public int? IdEsp32 { get; set; }
        public bool? Ativo { get; set; }
    }
}
