﻿namespace AluraAccessRPA.Infrastructure.Services;

public class DriverFactoryService : IDriverFactoryService
{
    public IWebDriver? Instance => _driver;
    private static IWebDriver? _driver { get; set; }
    public DriverFactoryService() { }

    public IWebDriver StartDriver(EDriverType driverType = EDriverType.Chrome, DriverOptions? opts = null)
    {
        if (_driver is null)
            _driver = Instantiate(driverType, opts);

        return _driver;
    }

    public void SetInstance(IWebDriver? driver) => _driver = driver;

    private static IWebDriver Instantiate(EDriverType driverType, DriverOptions? opts)
    {
        //Realiza o download do chrome compativel com a versão da maquina e inicia uma sessão
        return driverType switch
        {
            EDriverType.IE => new InternetExplorerDriver(new WebDriverManager.DriverManager().SetUpDriver(new InternetExplorerConfig(), VersionResolveStrategy.MatchingBrowser), opts as InternetExplorerOptions),
            EDriverType.Chrome => new ChromeDriver(new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser), opts as ChromeOptions),
            EDriverType.FireFox => new FirefoxDriver(new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser), opts as FirefoxOptions),
            EDriverType.Edge => new EdgeDriver(new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser), opts as EdgeOptions),
            _ => throw new ArgumentException($"drvier '{driverType}' is not supported.")
        };

    }

    public void Quit()
    {
        _driver?.Quit();
        _driver= null;
    }
}
