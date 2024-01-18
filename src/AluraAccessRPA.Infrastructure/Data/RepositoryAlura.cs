using AluraAccessRPA.Domain.Entities;
using AluraAccessRPA.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium.DevTools.V85.Profiler;
using System.Data.Common;

namespace AluraAccessRPA.Infrastructure.Data;

public class RepositoryAlura
{
    private readonly AluraDbContext _dbContext;
    public RepositoryAlura(AluraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<CourseInformation> ObterDados()
    {
        return _dbContext.Courses.ToList();
    }

    public void InsertAll(List<CourseInformation> listCourse)
    {
         _dbContext.Courses.AddRange(listCourse);
        _dbContext.SaveChanges();
    }

}
