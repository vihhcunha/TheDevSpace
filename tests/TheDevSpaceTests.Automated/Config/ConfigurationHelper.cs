using Microsoft.Extensions.Configuration;

namespace TheDevSpaceTests.Automated.Config;

internal class ConfigurationHelper
{
    private readonly IConfiguration _configuration;

    public ConfigurationHelper()
    {
        _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }
    public string DomainUrl => _configuration.GetSection("DomainUrl").Value;
    public string HomeUrl => $"{DomainUrl}{_configuration.GetSection("HomeUrl").Value}";
    public string LoginUrl => $"{DomainUrl}{_configuration.GetSection("LoginUrl").Value}";
    public string RegisterUrl => $"{DomainUrl}{_configuration.GetSection("RegisterUrl").Value}";
    public string EmailTest => _configuration.GetSection("EmailTest").Value;
    public string PasswordTest => _configuration.GetSection("PasswordTest").Value;
}
