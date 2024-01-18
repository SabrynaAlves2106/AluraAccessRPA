using AluraAccessRPA.Application.Selenium.Controllers;
using AluraAccessRPA.Domain.Entities;

namespace AluraAccessRPA.Application.Selenium;

public class Navigator
{
    protected readonly ControllerAlura _controllerAlura;
    public Navigator(ControllerAlura controllerAlura) =>_controllerAlura = controllerAlura; 
    
    public List<CourseInformation> StartNavigate(string url,string course)
    {
        var resultAccess =_controllerAlura.AccessPage(url);
        
        if (!resultAccess.IsSuccess)
            throw new Exception(resultAccess.Observation);

        var searchResult = _controllerAlura.SearchCouse(course);

        if (!searchResult.navigationResult.IsSuccess)
            throw new Exception(searchResult.navigationResult.Observation);

        var getInformation = _controllerAlura.GetInformation(searchResult.Elements);

        return getInformation.Item2;
    }
}
