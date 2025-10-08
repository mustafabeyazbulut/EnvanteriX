namespace EnvanteriX.Application.Features.Results.ModelResults
{
    public class GetAllModelsQueryResult
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
