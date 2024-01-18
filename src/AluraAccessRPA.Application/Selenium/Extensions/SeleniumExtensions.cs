using System.Data.OleDb;

namespace AluraAccessRPA.Application.Selenium.Extensions;

public static class SeleniumExtensions
{
    public static int pageLoadTimeWait = 5;
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
        }
        catch
        {
            return null;
        }
    }

    public static void GoToNoWait(this IWebDriver driver, string url, int seconds = 60)
    {
        driver.Manage().Timeouts().PageLoad = FromSeconds(pageLoadTimeWait);

        try
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().PageLoad = FromSeconds(seconds);
        }
        catch
        {
            driver.Manage().Timeouts().PageLoad = FromSeconds(seconds);
            //Aplicação vai ignorar exeption de timeOut 
        }


    }

    public static void ClickNoWait(this IWebElement element, int seconds = 60)
    {
        var driver = ((IWrapsDriver)element).WrappedDriver;
        driver.Manage().Timeouts().PageLoad = FromSeconds(pageLoadTimeWait);
        int i = 0;

        var oldUrl = driver.Url;

        try
        {
            element.Click();
        }
        catch
        {
            //Aplicação vai ignorar exeption de timeOut 
        }
        driver.Manage().Timeouts().PageLoad = FromSeconds(seconds);

        if (!driver.WaitChangeLink(oldUrl))
            throw new Exception("Ocorreu uma falha ao clicar no elemento" + element.Text);

    }
    private static bool WaitChangeLink(this IWebDriver driver,string oldUrl)
    {
        int i = 0;
        //Serve apenas para aguardar que seja alterada a URL
        while (true)
        {
            //try catch pois se a pagina não carregar a automação não consegue validar o link
            try
            {
                if (driver.Url == oldUrl && i++ < 10)
                    Thread.Sleep(FromSeconds(1));
                else
                    break;
            }
            catch { }

        }
        if (i == 10)
            return false;
        
        return true;
    }
}
