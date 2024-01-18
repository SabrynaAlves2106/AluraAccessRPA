namespace AluraAccessRPA.Domain.Interfaces;

public interface IRepository
{
    IEnumerable<CourseInformation> ObterDados();
    void InsertAll(List<CourseInformation> listCourse);
}
