namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public class CategoryApiUrl:BaseApiUrl
    {
        public string CategoryList { get; set; } = "Category/Category-list";
        public string ActiveCategoryList { get; set; } = "Category/active-Category-list";
        public string GetCategory { get; set; } = "Category/get-Category/";
        public string CreateCategory { get; set; } = "Category/create-Category";
        public string UpdateCategory { get; set; } = "Category/update-Category";
        public string RemoveCategory { get; set; } = "Category/remove-Category";
    }
}
