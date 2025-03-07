﻿
namespace AluraAccessRPA.Application.Selenium;

public class Navigator: INavigator
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
        if (!getInformation.navigateResult.IsSuccess)
            throw new Exception(getInformation.navigateResult.Observation);

        return getInformation.ListCourses;
    }
}
