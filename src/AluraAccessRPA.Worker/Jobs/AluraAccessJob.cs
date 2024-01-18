namespace AluraAccessRPA.Worker.Jobs;

public class AluraAccessJob : JobBase
{
    protected readonly IRepository _repository;
    private IDriverFactoryService _driverFactoryService { get; set; }
    private readonly INavigator _navigator;
    protected readonly IConfiguration _configuration;

    public AluraAccessJob(ILogger<JobBase> logger, IRepository repository,
        IDriverFactoryService driverFactoryService, INavigator navigator,
        IConfiguration configuration) : base(logger)
    {
        _repository = repository;
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

            _logger.LogInformation("Instalando e iniciando o Chrome");
            SetupDriver();
            
            _logger.LogInformation("Começo da navegação");
            var listCourse = _navigator.StartNavigate(url, course);

            _logger.LogInformation("Inserindo as informações retornadas no banco de dados, arquivo db");
            _repository.InsertAll(listCourse);

            _logger.LogInformation("Retornando dados do Banco");
            var result = _repository.ObterDados();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _driverFactoryService.Quit();
        }
        finally
        {
            _driverFactoryService.Quit();
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
