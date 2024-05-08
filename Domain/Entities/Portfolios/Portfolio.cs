namespace Domain.Entities.Portfolios
{
    public class Portfolio
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Photo { get; set; }
        public DateTime InsertTime { get; set; }
    }
}
