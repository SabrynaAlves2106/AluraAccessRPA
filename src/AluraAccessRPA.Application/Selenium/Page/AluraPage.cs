using AluraAccessRPA.Domain.Entities;
using AluraAccessRPA.Domain.Interfaces;
using OpenQA.Selenium;

namespace AluraAccessRPA.Application.Selenium.Page;

public class AluraPage
{
    private readonly IDriverFactoryService _driverFactoryService;
    private IWebDriver _driver => _driverFactoryService.Instance;
    public NavigateResult AccessPage(string url)
    {
        try
        {
            _driver.Navigate().GoToUrl(url);
            return new() { IsSuccess = true, Observation = "Acesso a pagina realizado" };
        }
        catch (Exception ex)
        {
            return new() { IsSuccess = false, Observation = "Um erro ocorreu ao tentar acessar a pagina, erro: " + ex.Message };
        }
    }
}