using AluraAccessRPA.Domain.Enum;
using AluraAccessRPA.Domain.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace AluraAccessRPA.Infrastructure.Services;

public class DriverFactoryService : IDriverFactoryService
{
    public IWebDriver? Instance => _driver;
    private static IWebDriver? _driver { get; set; }
    public DriverFactoryService() { }

    public IWebDriver StartDriver(EDriverType driverType = EDriverType.Chrome)
    {
        if (_driver is null)
            _driver = Instantiate(driverType);
        return _driver;
    }

    public void SetInstance(IWebDriver? driver) => _driver = driver;

    private static IWebDriver Instantiate(EDriverType driverType)
    {
        //Realiza o download do chrome compativel com a versão da maquina e inicia uma sessão
        return driverType switch
        {
            EDriverType.IE => new InternetExplorerDriver(new WebDriverManager.DriverManager().SetUpDriver(new InternetExplorerConfig(), VersionResolveStrategy.MatchingBrowser)),
            EDriverType.Chrome => new ChromeDriver(new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser)),
            EDriverType.FireFox => new FirefoxDriver(new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser)),
            EDriverType.Edge => new EdgeDriver(new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser)),
            _ => throw new ArgumentException($"drvier '{driverType}' is not supported.")
        };

    }

    public void Quit() => _driver?.Quit();
}
