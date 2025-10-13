namespace EnvanteriX.Application.Features.Results.ModelResults
{
    public class CreateModelCommandResult
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }

        public CreateModelCommandResult() { }

        public CreateModelCommandResult(int id, string modelName, int brandId)
        {
            Id = id;
            ModelName = modelName;
            BrandId = brandId;
        }
    }
}
