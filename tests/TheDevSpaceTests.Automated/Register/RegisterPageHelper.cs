using TheDevSpaceTests.Automated.Config;

namespace TheDevSpaceTests.Automated.Register;

internal class RegisterPageHelper
{
    public string PageTitleXPath => "/html/body/div[1]/main/div[1]/h2";
    public string SpanEmailXPath => "/html/body/div[1]/main/div[1]/form/div[1]/span";
    public string SpanNameXPath => "/html/body/div[1]/main/div[1]/form/div[2]/span";
    public string SpanPasswordXPath => "/html/body/div[1]/main/div[1]/form/div[3]/span";
    public string SpanConfirmPasswordXPath => "/html/body/div[1]/main/div[1]/form/div[4]/span";
    public string ToastMessageXPath => "/html/body/div[2]/div/div/div[2]";

    public SeleniumHelper _helper { get; set; }
    public RegisterPageHelper(SeleniumHelper helper)
    {
        _helper = helper;
    }

    public void AccessRegisterPage()
    {
        _helper.GoToUrl(_helper.Configuration.RegisterUrl);
    }

    public string GetPageTitle()
    {
        return _helper.GetElementByXPath(PageTitleXPath).Text;
    }

    public void FillEmailField(string value)
    {
        _helper.FillTextBoxById("Email", value);
    }

    public void FillNameField(string value)
    {
        _helper.FillTextBoxById("Name", value);
    }

    public void FillPasswordField(string value)
    {
        _helper.FillTextBoxById("Password", value);
    }

    public void FillConfirmPasswordField(string value)
    {
        _helper.FillTextBoxById("ConfirmPassword", value);
    }

    public void FillAgeField(string value)
    {
        _helper.FillTextBoxById("Age", value);
    }

    public void FillDescriptionField(string value)
    {
        _helper.FillTextBoxById("Description", value);
    }

    public void FillRoleField(string value)
    {
        _helper.FillTextBoxById("Role", value);
    }

    public void PressSubmitButton()
    {
        _helper.ClickOnButtonById("Register_Submit");
        _helper.ClickOnButtonById("register_button");
    }

    public void PressSubmitAsWriterButton()
    {
        _helper.ClickOnButtonById("Register_Submit");
        _helper.ClickOnButtonById("register_as_writer_button");
    }

    public void PressSubmitRegisterWriterButton()
    {
        _helper.ClickOnButtonById("register_writer");
    }

    public string GetSpanEmailValue()
    {
        return _helper.GetElementByXPath(SpanEmailXPath).Text;
    }

    public string GetSpanNameValue()
    {
        return _helper.GetElementByXPath(SpanNameXPath).Text;
    }

    public string GetSpanPasswordValue()
    {
        return _helper.GetElementByXPath(SpanPasswordXPath).Text;
    }

    public string GetSpanConfirmPasswordValue()
    {
        return _helper.GetElementByXPath(SpanConfirmPasswordXPath).Text;
    }

    public bool ExistSomeToastMessage()
    {
        return _helper.CheckIfElementExistsByXPath(ToastMessageXPath);
    }

    public void Register()
    {
        AccessRegisterPage();
        FillEmailField(_helper.Configuration.EmailTest);
        FillNameField("Test");
        FillPasswordField(_helper.Configuration.PasswordTest);
        FillConfirmPasswordField(_helper.Configuration.PasswordTest);
        PressSubmitButton();

        if (ExistSomeToastMessage() == false) return;

        var faker = new Bogus.Faker();
        var email = faker.Internet.Email();
        var password = faker.Internet.Password();

        FillEmailField(email);
        FillPasswordField(password);
        FillConfirmPasswordField(password);
        PressSubmitButton();
    }

    public void RegisterAsWriter()
    {
        AccessRegisterPage();
        FillEmailField(_helper.Configuration.EmailTest);
        FillNameField("Test");
        FillPasswordField(_helper.Configuration.PasswordTest);
        FillConfirmPasswordField(_helper.Configuration.PasswordTest);
        PressSubmitAsWriterButton();

        if (ExistSomeToastMessage() == false) return;

        var faker = new Bogus.Faker();
        var email = faker.Internet.Email();
        var password = faker.Internet.Password();

        FillEmailField(email);
        FillPasswordField(password);
        FillConfirmPasswordField(password);
        PressSubmitAsWriterButton();

        var age = faker.Random.Number(10, 100);
        var description = faker.Lorem.Paragraphs(3);
        var role = faker.Lorem.Sentence(2);

        FillAgeField(age.ToString());
        FillDescriptionField(description);
        FillRoleField(role);
        PressSubmitRegisterWriterButton();
    }
}
