
using AluraAccessRPA.Domain.Enum;
using OpenQA.Selenium;

namespace AluraAccessRPA.Domain.Interfaces;

public interface IDriverFactoryService
{
    abstract IWebDriver Instance { get; }
    IWebDriver StartDriver(EDriverType driverType = EDriverType.Chrome, DriverOptions? opts = null);
    void Quit();
}
