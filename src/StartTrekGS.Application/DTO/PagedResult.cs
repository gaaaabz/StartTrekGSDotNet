namespace StartTrekGS.src.StartTrekGS.Application.DTO
{
    public class PagedResult<T>
    {
        public int Pagina { get; init; }
        public int Tamanho { get; init; }
        public int TotalRegistros { get; init; }
        public IEnumerable<T> Dados { get; init; } = Enumerable.Empty<T>();
    }
}
