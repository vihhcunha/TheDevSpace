using TheDevSpaceTests.Automated.Config;

namespace TheDevSpaceTests.Automated.Register;

[Collection(nameof(AutomationWebFixtureCollection))]
public class RegisterTests
{
    private readonly AutomationWebTestsFixture _testsFixture;
    private readonly RegisterPageHelper _pageHelper;

    public RegisterTests(AutomationWebTestsFixture fixture)
    {
        _testsFixture = fixture;
        _pageHelper = new RegisterPageHelper(fixture._helper);
    }

    [Fact(DisplayName = "User can access")]
    [Trait("Category", "RegisterPage")]
    public void RegisterPage_AccessLoginPage_UserCanAccess()
    {
        _pageHelper.AccessRegisterPage();

        Assert.Contains("Register", _pageHelper.GetPageTitle(), StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName = "Submit empty values - show errors")]
    [Trait("Category", "RegisterPage")]
    public void RegisterPage_SubmitEmptyValues_ShowErrorMessages()
    {
        _pageHelper.AccessRegisterPage();
        _pageHelper.FillEmailField("");
        _pageHelper.FillNameField("");
        _pageHelper.FillPasswordField("");
        _pageHelper.FillConfirmPasswordField("");
        _pageHelper.PressSubmitButton();

        Assert.Contains("must", _pageHelper.GetSpanEmailValue(), StringComparison.InvariantCultureIgnoreCase);
        Assert.Contains("must", _pageHelper.GetSpanNameValue(), StringComparison.InvariantCultureIgnoreCase);
        Assert.Contains("must", _pageHelper.GetSpanPasswordValue(), StringComparison.InvariantCultureIgnoreCase);
        Assert.Contains("must", _pageHelper.GetSpanConfirmPasswordValue(), StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName = "Register as user")]
    [Trait("Category", "RegisterPage")]
    public void RegisterPage_Register_MustWorks()
    {
        _pageHelper.Register();

        Assert.Contains("Technical Articles", _testsFixture._helper.GetElementByXPathWaitingForTitle("/html/body/div[1]/main/div/h2", "Home").Text, StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName = "Register as user and writer")]
    [Trait("Category", "RegisterPage")]
    public void RegisterPage_RegisterAsWriter_MustWorks()
    {
        _pageHelper.RegisterAsWriter();

        Assert.Contains("Technical Articles", _testsFixture._helper.GetElementByXPathWaitingForTitle("/html/body/div[1]/main/div/h2", "Home").Text, StringComparison.InvariantCultureIgnoreCase);
    }
}
