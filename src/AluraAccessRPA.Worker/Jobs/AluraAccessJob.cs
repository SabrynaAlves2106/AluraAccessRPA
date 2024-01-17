using AluraAccessRPA.Domain.Enum;
using AluraAccessRPA.Domain.Interfaces;
using AluraAccessRPA.Infrastructure.Data;
using AluraAccessRPA.Worker.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace AluraAccessRPA.Worker.Jobs;

public class AluraAccessJob : JobBase
{
    protected readonly RepositoryAlura _repository;
    private IWebDriver _driver { get; set; }
    private IDriverFactoryService _driverFactoryService { get; set; }

    
    public AluraAccessJob(ILogger<JobBase> logger, RepositoryAlura repository,
        IDriverFactoryService driverFactoryService):base(logger)
    {
        _repository= repository;
        _driverFactoryService = driverFactoryService;
    }
    public override async Task Execute(IJobExecutionContext context)
    {
        try
        {
            SetupDriver();
            var result = _repository.ObterDados();
        }
        catch(Exception ex) 
        {
        }
    }

    public void SetupDriver()
    {
        _driverFactoryService.StartDriver(EDriverType.Chrome);
       

    }
}
