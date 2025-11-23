namespace StartTrekGS.src.StartTrekGS.Application.DTO
{
    public class LoginResponseDto
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Ativo { get; set; }

        public string Token { get; set; } = null!; // caso você use JWT
    }
}
