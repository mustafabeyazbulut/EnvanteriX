
namespace EnvanteriX.Application.Interfaces.Email
{
    public interface IEmailTemplateProvider
    {
        /// <summary>
        /// Verilen key ile mail template içeriğini döner. Key chtml dosya adı ile eşleşir.
        /// </summary>
        Task<string?> GetTemplateAsync(string key, CancellationToken ct = default);
    }
}
