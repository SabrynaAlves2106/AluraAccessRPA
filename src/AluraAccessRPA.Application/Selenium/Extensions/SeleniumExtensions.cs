using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using static System.TimeSpan;
using OpenQA.Selenium.Remote;

namespace AluraAccessRPA.Application.Selenium.Extensions;

public static class SeleniumExtensions
{
    public static IWebElement WaitElement(this IWebDriver driver, By by, int seconds = 10)
    {

        try
        {
            var wait = new WebDriverWait(driver, FromSeconds(seconds));
            var element = wait.Until(d => d.FindElement(by));
            return element;
        }
        catch
        {
            return null;
        }

    }

    public static IEnumerable<IWebElement> WaitElements(this IWebDriver driver, By by, int seconds = 10)
    {
        try
        {
            var wait = new WebDriverWait(driver, FromSeconds(seconds));
            var element = wait.Until(d => d.FindElements(by));
            return element;
        }            //Aplicação vai ignorar exeption de timeOut 
        catch
        {
            return null;
        }
    }

    public static void GoToNoWait(this IWebDriver driver, string url, int seconds = 60)
    {
        driver.Manage().Timeouts().PageLoad = FromSeconds(2);

        try
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().PageLoad = FromSeconds(seconds);
        }
        catch (Exception e)
        {
            driver.Manage().Timeouts().PageLoad = FromSeconds(seconds);
            //Aplicação vai ignorar exeption de timeOut 
        }


    }

    public static void ClickNoWait(this IWebElement element, int seconds = 60)
    {
        var driver = ((IWrapsDriver)element).WrappedDriver;
        driver.Manage().Timeouts().PageLoad = FromSeconds(5);

        var oldUrl = driver.Url;


        try
        {
            element.Click();
        }
        catch (Exception e)
        {
            //Aplicação vai ignorar exeption de timeOut 
        }
        while (driver.Url == oldUrl)
        {

        }


        driver.Manage().Timeouts().PageLoad = FromSeconds(seconds);
    }
    public static void BackNoWait(this IWebDriver driver, int seconds = 60)
    {
        driver.Manage().Timeouts().PageLoad = FromSeconds(5);
        var oldUrl = driver.Url;

        try
        {
            driver.Navigate().Back();
        }
        catch (Exception e)
        {
            //Aplicação vai ignorar exeption de timeOut 
        }
        while (driver.Url == oldUrl)
        {

        }
        driver.Manage().Timeouts().PageLoad = FromSeconds(seconds);


    }
}
