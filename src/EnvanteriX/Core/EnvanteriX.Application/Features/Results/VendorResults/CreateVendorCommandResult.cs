namespace EnvanteriX.Application.Features.Results.VendorResults
{
    public class CreateVendorCommandResult
    {
        public int Id { get; set; }

        public CreateVendorCommandResult(int id)
        {
            this.Id = id;
        }
    }
}
