namespace StartTrekGS.src.StartTrekGS.Application.DTO
{
    public class ComentarioCreateDto
    {
        public string? TextoComentario { get; set; }
        public int IdUsuario { get; set; }
        public int IdTrabalho { get; set; }
    }
}
