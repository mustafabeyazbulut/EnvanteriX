namespace EnvanteriX.WebUI.ViewModels.User
{
    public class PaginatedUserViewModel
    {
        public List<UserViewModel> Items { get; set; } = new List<UserViewModel>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
