namespace StartTrekGS.src.StartTrekGS.Application.DTO
{
    public class ComentarioResponseDto
    {
        public int IdComentario { get; set; }
        public string? TextoComentario { get; set; }
        public bool Ativo { get; set; }

        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; } = null!;

        public int IdTrabalho { get; set; }
        public string NomeTrabalho { get; set; } = null!;
    }
}
