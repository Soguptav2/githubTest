using NUnit.Framework;
using FluentAssertions;
using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightDemo.Pages;
using System.Web;
using System;


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

    [Test]
    public async Task TestWithPOM()
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
        
        LoginPageUpgraded loginpage = new LoginPageUpgraded(page);
        await loginpage.ClickLogin();
        await loginpage.Login("admin","password");

        var isExist = await loginpage.IsEmployeeDetailsExist();
        Assert.IsTrue(isExist);
    }

    [Test]
    public async Task TestNetwork()
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
        
        LoginPageUpgraded loginpage = new LoginPageUpgraded(page);
        await loginpage.ClickLogin();

        //wait for response - Way1
        var waitForResponse = page.WaitForResponseAsync("**/Employee");
        //while button is clicked
        await loginpage.ClickEmployeeList();
        //give the response details
        var getResponse = await waitForResponse;

        //wait for response - Way2
        var response = await page.RunAndWaitForResponseAsync(async() =>
        {
            await loginpage.ClickEmployeeList();
        }, x => x.Url.Contains("/Employee") && x.Status == 200);

        await loginpage.Login("admin","password");

        var isExist = await loginpage.IsEmployeeDetailsExist();
        Assert.IsTrue(isExist);
    }

    [Test]
    public async Task Flipkart()
    {
        //playwright
        using var playwright = await Playwright.CreateAsync();

        //browser
        await using var browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions{
                Headless = false
            }
        );

        //context
        var context = await browser.NewContextAsync();
        //page
        var page = await context.NewPageAsync();
        await page.GotoAsync("https://www.flipkart.com/", new PageGotoOptions
        {
            WaitUntil = WaitUntilState.NetworkIdle
        });
        
        await page.Locator("//button[@class='_2KpZ6l _2doB4z']").ClickAsync();
        await page.Locator("a", new PageLocatorOptions
        {
            HasTextString = "Login"
        }).ClickAsync();

         //wait for response - Way2
        var request = await page.RunAndWaitForRequestAsync(async() =>
        {
            await page.Locator("//button[@class='_2KpZ6l _2doB4z']").ClickAsync();
        }, x => x.Url.Contains("flipkart.d1.sc.omtrdc.net") && x.Method == "GET");

        var returnData = HttpUtility.UrlDecode(request.Url);

        returnData.Should().Contain("Account Login:Displayed Exit");
    }

     [Test]
    public async Task FlipkartNetworkInterception()
    {
        //playwright
        using var playwright = await Playwright.CreateAsync();

        //browser
        await using var browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions{
                Headless = false
            }
        );

        //context
        var context = await browser.NewContextAsync();
        //page
        var page = await context.NewPageAsync();

        page.Request += (_, request) => Console.WriteLine("REQUEST: "+request.Method+" - "+request.Url);
        page.Response += (_, response) => Console.WriteLine("RESPONSE: "+response.Status+" - "+response.Url+" - "+response.StatusText);

        await page.RouteAsync("**/*", async route =>
        {
            if(route.Request.ResourceType == "image")
                await route.AbortAsync();
            else
                await route.ContinueAsync();
        });

        await page.GotoAsync("https://www.flipkart.com/", new PageGotoOptions
        {
            WaitUntil = WaitUntilState.NetworkIdle
        });
        

    }

}