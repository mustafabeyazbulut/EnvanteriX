using EnvanteriX.Application.Interfaces.Email;
using System.Reflection;

namespace EnvanteriX.Infrastructure.Email
{
    public class FileEmailTemplateProvider : IEmailTemplateProvider
    {
        private readonly Assembly _assembly;
        private readonly string _baseNamespace;

        public FileEmailTemplateProvider()
        {
            // Bu assembly'i yükle
            _assembly = Assembly.Load("EnvanteriX.Infrastructure");

            // Namespace başlangıcı (kaynak dosyalarının tam adı buna göre belirleniyor)
            _baseNamespace = "EnvanteriX.Infrastructure.Email.Templates";
        }

        public async Task<string?> GetTemplateAsync(string key, CancellationToken ct = default)
        {
            // Örnek: key = "RegisterEmailTemplate"
            // Kaynak adı = EnvanteriX.Infrastructure.Email.Templates.RegisterEmailTemplate.html
            var resourceName = $"{_baseNamespace}.{key}.html";

            using (Stream? stream = _assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new Exception($"Email template resource not found: {resourceName}");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    var template = await reader.ReadToEndAsync();

                    // Eğer template içinde {{CODE}}, {{MINUTE}} gibi değişkenler varsa sonradan replace edebilirsin
                    return template;
                }
            }
        }
    }
}
