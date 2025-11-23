namespace Lab08Robbiejude.DTOs
{
    public class ClientOrderDto
    {
        public string ClientName { get; set; } = string.Empty;
        public List<OrderDto> Orders { get; set; } = new();
    }
}