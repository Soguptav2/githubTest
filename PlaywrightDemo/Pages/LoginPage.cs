using Microsoft.Playwright;
using System.Threading.Tasks;


namespace PlaywrightDemo.Pages;

public class LoginPage
{
    private IPage _page;
    private readonly ILocator _lnkLogin;
    private readonly ILocator _txtUsername;
    private readonly ILocator _txtPassword;
    private readonly ILocator _inpLogin;
    private readonly ILocator _lnkEmployeeDetails;

    public LoginPage(IPage page)
    {
        _page = page;
        _lnkLogin = _page.Locator("text = Login");
        _txtUsername = _page.Locator("#UserName");
        _txtPassword = _page.Locator("#Password");
        _inpLogin = _page.Locator("text = Log in");
        _lnkEmployeeDetails = _page.Locator("text = Employee Details");
    }

    public async Task ClickLogin() => await _lnkLogin.ClickAsync();

    public async Task Login(string userName, string passWord)
    {
        await _txtUsername.FillAsync(userName);
        await _txtPassword.FillAsync(passWord);
        await _inpLogin.ClickAsync();
    }

    public async Task<bool> IsEmployeeDetailsExist() => await _lnkEmployeeDetails.IsVisibleAsync();


}