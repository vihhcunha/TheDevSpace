using TheDevSpaceTests.Automated.Config;

namespace TheDevSpaceTests.Automated.Login;

internal class LoginPageHelper
{
    public string PageTitleXPath => "/html/body/div[1]/main/div/h2";
    public string SpanEmailXPath => "/html/body/div[1]/main/div/form/div[1]/span/span";
    public string SpanPasswordXPath => "/html/body/div[1]/main/div/form/div[2]/span/span";
    public string ErrorToast => "/html/body/div[2]/div/div/div[2]";

    public SeleniumHelper _helper { get; set; }
    public LoginPageHelper(SeleniumHelper helper)
    {
        _helper = helper;
    }

    public void AccessLoginPage()
    {
        _helper.GoToUrl(_helper.Configuration.LoginUrl);
    }

    public string GetPageTitle()
    {
        return _helper.GetElementByXPath(PageTitleXPath).Text;
    }

    public void FillEmailField(string value)
    {
        _helper.FillTextBoxById("Email", value);
    }

    public void FillPasswordField(string value)
    {
        _helper.FillTextBoxById("Password", value);
    }

    public void PressSubmitButton()
    {
        _helper.ClickOnButtonById("login_submit");
    }

    public string GetSpanEmailValue()
    {
        return _helper.GetElementByXPath(SpanEmailXPath).Text;
    }

    public string GetSpanPasswordValue()
    {
        return _helper.GetElementByXPath(SpanPasswordXPath).Text;
    }

    public string GetErrorToastValue()
    {
        return _helper.GetElementByXPath(ErrorToast).Text;
    }

    public void Login()
    {
        AccessLoginPage();
        FillEmailField(_helper.Configuration.EmailTest);
        FillPasswordField(_helper.Configuration.PasswordTest);
        PressSubmitButton();
    }
}
