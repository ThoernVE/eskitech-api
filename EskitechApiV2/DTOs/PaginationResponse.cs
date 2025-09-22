namespace EskitechApi.DTOs
{
    public class PaginationResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public string? FirstPage { get; set; }
        public string? LastPage { get; set; }
        public string? NextPage { get; set; }
        public string? PreviousPage { get; set; }
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}