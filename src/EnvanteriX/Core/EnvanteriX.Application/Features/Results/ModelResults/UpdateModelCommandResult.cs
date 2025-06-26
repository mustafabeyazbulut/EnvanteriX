namespace EnvanteriX.Application.Features.Results.ModelResults
{
    public class UpdateModelCommandResult
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }

        public UpdateModelCommandResult() { }

        public UpdateModelCommandResult(int id, string modelName, int brandId)
        {
            Id = id;
            ModelName = modelName;
            BrandId = brandId;
        }
    }
}
