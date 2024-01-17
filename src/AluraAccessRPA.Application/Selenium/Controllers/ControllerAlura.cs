using AluraAccessRPA.Application.Selenium.Page;
using AluraAccessRPA.Domain.Entities;

namespace AluraAccessRPA.Application.Selenium.Controllers;

public class ControllerAlura
{
    private readonly AluraPage _aluraPage;
    public NavigateResult AccessPage(string url)
    {
        return _aluraPage.AccessPage(url);
    }
}
