namespace AluraAccessRPA.Application.Selenium.Controllers;

public class ControllerAlura
{
    private readonly AluraPage _aluraPage;
    public ControllerAlura(AluraPage aluraPage)
    {
        _aluraPage = aluraPage;
    }
    public NavigateResult AccessPage(string url)
    {
        return _aluraPage.AccessPage(url);
    }
    public (NavigateResult navigationResult,IEnumerable<string> Elements) SearchCouse(string course)
    {
        return _aluraPage.SearchCourse(course);
    }
    public (NavigateResult navigateResult,List<CourseInformation> ListCourses) GetInformation(IEnumerable<string> itens)
    {
        
        return _aluraPage.GetInformation(itens);
    }

}
