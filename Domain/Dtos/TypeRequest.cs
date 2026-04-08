namespace Domain.Dtos
{
    public class TypeRequest
    {
        public required string Name { get; set; }
        public int? MainTypeId { get; set; }
    }
}
