using Microsoft.Playwright;
using System.Threading.Tasks;


namespace PlaywrightDemo.Pages;

public class LoginPageUpgraded
{
    private IPage _page;
    public LoginPageUpgraded(IPage page) => _page = page;

    private ILocator LnkLogin => _page.Locator("text = Login");
    private ILocator TxtUsername => _page.Locator("#UserName");
    private ILocator TxtPassword => _page.Locator("#Password");
    private ILocator InpLogin => _page.Locator("text = Log in");
    private ILocator LnkEmployeeDetails => _page.Locator("text = Employee Details");
    private ILocator LnkEmployeeList => _page.Locator("text = Employee List");
    public async Task ClickLogin()
    {
        await _page.RunAndWaitForNavigationAsync(async() => 
        {
            await LnkLogin.ClickAsync();
        }, new PageRunAndWaitForNavigationOptions
        {
            UrlString = "**/Login"
        });
    }  

    public async Task ClickEmployeeList() => await LnkEmployeeList.ClickAsync();
    public async Task Login(string userName, string passWord)
    {
        await TxtUsername.FillAsync(userName);
        await TxtPassword.FillAsync(passWord);
        await InpLogin.ClickAsync();
    }

    public async Task<bool> IsEmployeeDetailsExist() => await LnkEmployeeDetails.IsVisibleAsync();


}