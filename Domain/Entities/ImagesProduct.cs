namespace Domain.Entities
{
    public class ImagesProduct : BaseEntity
    {
        public int ProductId  { get; set; }
        public required string Path { get; set; }
        public int? Order {  get; set; }
    }
}
