namespace EnvanteriX.Application.Features.Results.DepartmentResults
{
    public class GetAllDepartmentsQueryResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
