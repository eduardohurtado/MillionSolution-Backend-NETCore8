public class PropertyWithOwnerDto
{
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal Price { get; set; }
    public string CodeInternal { get; set; } = null!;
    public int Year { get; set; }

    public OwnerDto Owner { get; set; } = null!;
}
