/* using NUnit.Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;


namespace PlaywrightDemo;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        //playwright
        using var playwright = await Playwright.CreateAsync();

        //browser
        await using var browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions{
                Headless = false
            }
        );

        //page
        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://www.eaapp.somee.com");
        await page.ClickAsync("text = Login");

        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "Eaapp.jpg"
        });

        await page.FillAsync("#UserName","admin");
        await page.FillAsync("#Password","password");
        await page.ClickAsync("text = Log in");

        var isExist = await page.Locator("text= Employee Details").IsVisibleAsync();
        Assert.IsTrue(isExist);


    }
} */