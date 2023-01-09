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
        
        await Page.ClickAsync("text = Login");
        await Page.FillAsync("#UserName","admin");
        await Page.FillAsync("#Password","password");
        await Page.ClickAsync("text = Log in");
        await Expect(Page.Locator("text= Employee Details")).ToBeVisibleAsync();



    }
}