﻿namespace EnvanteriX.Application.Features.Results.ModelResults
{
    public class GetModelByIdQueryResult
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
