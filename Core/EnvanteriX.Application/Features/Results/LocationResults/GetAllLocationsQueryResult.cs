namespace EnvanteriX.Application.Features.Results.LocationResults
{
    public class GetAllLocationsQueryResult
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
