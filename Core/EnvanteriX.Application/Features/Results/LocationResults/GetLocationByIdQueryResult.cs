namespace EnvanteriX.Application.Features.Results.LocationResults
{
    public class GetLocationByIdQueryResult
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Room { get; set; }
        public string Description { get; set; }
    }
}
