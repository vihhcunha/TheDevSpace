using TheDevSpaceTests.Automated.Config;

namespace TheDevSpaceTests.Automated.Login;

[Collection(nameof(AutomationWebFixtureCollection))]
public class LoginTests
{
    private readonly AutomationWebTestsFixture _testsFixture;
    private readonly LoginPageHelper _pageHelper;

    public LoginTests(AutomationWebTestsFixture fixture)
    {
        _testsFixture = fixture;
        _pageHelper = new LoginPageHelper(fixture._helper);
    }

    [Fact(DisplayName = "User can access")]
    [Trait("Category", "LoginPage")]
    public void LoginPage_AccessLoginPage_UserCanAccess()
    {
        _pageHelper.AccessLoginPage();

        Assert.Contains("Login", _pageHelper.GetPageTitle(), StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName = "Submit empty values - show errors")]
    [Trait("Category", "LoginPage")]
    public void LoginPage_SubmitEmptyValues_ShowErrorMessages()
    {
        _pageHelper.AccessLoginPage();
        _pageHelper.FillEmailField("");
        _pageHelper.FillPasswordField("");
        _pageHelper.PressSubmitButton();

        Assert.Contains("mandatory", _pageHelper.GetSpanEmailValue(), StringComparison.InvariantCultureIgnoreCase);
        Assert.Contains("mandatory", _pageHelper.GetSpanPasswordValue(), StringComparison.InvariantCultureIgnoreCase);
    }

    [Fact(DisplayName = "Login into shop")]
    [Trait("Category", "LoginPage")]
    public void LoginPage_LoginIntoShop_MustWorks()
    {
        _pageHelper.Login();

        Assert.Contains("Technical Articles", _testsFixture._helper.GetElementByXPathWaitingForTitle("/html/body/div[1]/main/div/h2", "Home").Text, StringComparison.InvariantCultureIgnoreCase);
    }
}
