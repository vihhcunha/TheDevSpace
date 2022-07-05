
namespace TheDevSpaceTests.Automated.Config;

[CollectionDefinition(nameof(AutomationWebFixtureCollection))]
public class AutomationWebFixtureCollection : ICollectionFixture<AutomationWebTestsFixture> { }
public class AutomationWebTestsFixture
{
    public string EmailTest => "test@test.com";
    public string PasswordTest => "Test12#";

    internal SeleniumHelper _helper;
    internal readonly ConfigurationHelper Configuration;

    public AutomationWebTestsFixture()
    {
        Configuration = new ConfigurationHelper();
        _helper = new SeleniumHelper(Configuration, false);
    }

    public void AccessLoginPage()
    {
        _helper.GoToUrl(_helper.Configuration.LoginUrl);
    }

    public void FillEmailField(string value)
    {
        _helper.FillTextBoxById("Input_Email", value);
    }

    public void FillPasswordField(string value)
    {
        _helper.FillTextBoxById("Input_Password", value);
    }

    public void PressSubmitButton()
    {
        _helper.ClickOnButtonById("login-submit");
    }

    public void Login()
    {
        AccessLoginPage();
        FillEmailField(EmailTest);
        FillPasswordField(PasswordTest);
        PressSubmitButton();
    }
}
