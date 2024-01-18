namespace AluraAccessRPA.Domain.Interfaces;

public interface INavigator
{
    List<CourseInformation> StartNavigate(string url, string course);
}
