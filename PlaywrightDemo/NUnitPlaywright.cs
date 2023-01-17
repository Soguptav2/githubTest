using NUnit.Framework;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Threading.Tasks;


namespace PlaywrightDemo;

public class NUnitPlaywright : PageTest
{
    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync("http://www.eaapp.somee.com");
    }

    [Test]
    public async Task Test1()
    {
        var lnkLogin = Page.Locator("text = Login");
        await lnkLogin.ClickAsync();
        await Page.FillAsync("#UserName","admin");
        await Page.FillAsync("#Password","password");

        var inpLogin = Page.Locator("input",new PageLocatorOptions{HasTextString = "Log in"});
        await inpLogin.ClickAsync();
        await Expect(Page.Locator("text= Employee Details")).ToBeVisibleAsync();



    }
}