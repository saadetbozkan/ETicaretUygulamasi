namespace ETicaretAPI.Application.RequestParameters
{
    public record Pagination
    {
        public int Size { get; set; } = 5;
        public int Page { get; set; } = 0;
    }
}
