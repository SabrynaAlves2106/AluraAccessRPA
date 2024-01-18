using AluraAccessRPA.Application.Selenium;
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
    private readonly Navigator _navigator;
    protected readonly IConfiguration _configuration;
    
    public AluraAccessJob(ILogger<JobBase> logger, RepositoryAlura repository,
        IDriverFactoryService driverFactoryService,Navigator navigator,
        IConfiguration configuration):base(logger)
    {
        _repository= repository;
        _driverFactoryService = driverFactoryService;
        _navigator = navigator;
        _configuration = configuration;
    }
    public override async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var url = _configuration["UrlAlura"];
            var course = _configuration["Course"];
            SetupDriver();
            var listCourse =_navigator.StartNavigate(url,course);
            _repository.InsertAll(listCourse);
            var result = _repository.ObterDados();
        }
        catch(Exception ex) 
        {
        }
    }

    public void SetupDriver()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--disable-extensions");
        //options.AddArgument("--headless");  // Se não precisar de uma interface gráfica
        options.AddArgument("--disable-gpu");
        options.AddUserProfilePreference("default_content_setting_values.script_time", 0);
        options.AddArgument("--no-sandbox");
        _driverFactoryService.StartDriver(EDriverType.Chrome, options);
       

    }
}
