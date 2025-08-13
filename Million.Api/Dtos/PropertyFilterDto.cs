public class PropertyFilterDto
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int Skip { get; set; } = 0;
    public int Limit { get; set; } = 50;
}
