namespace EnvanteriX.WebUI.Models.ApiUrl
{
    public class CompanyApiUrl : BaseApiUrl
    {
        public string CompanyList { get; set; } = "company/Company-list";
        public string ActiveCompanyList { get; set; } = "company/active-company-list";
        public string GetCompany { get; set; } = "company/get-company/";
        public string CreateCompany { get; set; } = "company/create-company";
        public string UpdateCompany { get; set; } = "company/update-company";
        public string RemoveCompany { get; set; } = "company/remove-company";
    }
}
