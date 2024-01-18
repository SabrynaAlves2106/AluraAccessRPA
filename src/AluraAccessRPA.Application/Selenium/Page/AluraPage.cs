namespace AluraAccessRPA.Application.Selenium.Page;

public class AluraPage : ElementsAlura
{
    private readonly IDriverFactoryService _driverFactoryService;
    private IWebDriver _driver => _driverFactoryService.Instance;

    public AluraPage(IDriverFactoryService driverFactoryService, IConfiguration configuration) : base(configuration)
    {
        _driverFactoryService = driverFactoryService;
    }
    public NavigateResult AccessPage(string url)
    {
        try
        {
            //Realiza navegação
            _driver.GoToNoWait(url);
            return new() { IsSuccess = true, Observation = "Acesso a pagina realizado" };
        }
        catch (Exception ex)
        {
            return new() { IsSuccess = false, Observation = "Um erro ocorreu ao tentar acessar a pagina, erro: " + ex.Message };
        }
    }
    public (NavigateResult, IEnumerable<string>) SearchCourse(string course)
    {
        try
        {
            //Realiza a busca do curso e pega o nome de todos os cursos
            _driver.WaitElement(inputSearch).SendKeys(course);
            _driver.WaitElement(btnSearch).ClickNoWait();
            var itens = _driver.WaitElements(getCouses).Select((element, v) =>
            {
                return element.FindElement(By.TagName("h4")).Text;
            }
            ).ToList();



            return (new() { IsSuccess = true, Observation = "Acesso a pagina realizado" }, itens);
        }
        catch (Exception ex)
        {
            return (new() { IsSuccess = false, Observation = "Um erro ocorreu ao tentar acessar a pagina, erro: " + ex.Message }, null);
        }
    }
    public (NavigateResult, List<CourseInformation>) GetInformation(IEnumerable<string> itens)
    {
        try
        {
            var linkSearch = _driver.Url;
            var list = new List<CourseInformation>();
            foreach (var courseName in itens)
            {
                //Captura a descrição antes de entrar no link
                var description = _driver.WaitElement(By.XPath(string.Format(descriptionText,courseName))).Text;

                //Clica no link do curso
                _driver.WaitElement(By.XPath(string.Format(courseLink,courseName))).ClickNoWait();

                //Pega todos os elementos que contém nome dos instrutores
                var instructorElements = _driver.WaitElements(instructorsElements);

                //Valida se o nome do instrutor foi localizado, caso não seja insere comentario "Nome dos instrutores não esta disponivel", caso esteja concatena os casos que são diferente de empty/vazio
                string nameInstructors = instructorElements.Count() > 0 ? string.Join(", ", instructorElements.Select(x => x.Text).Where(x => x != "")) : "Nome dos instrutores não esta disponivel";

                //Pega a carga horaria caso exista
                var workloadElement = _driver.WaitElement(workloadElements, 5);
                var valueWorkload = workloadElement is not null ? workloadElement.Text.Split("\r")[0] : "Carga horaria não esta disponivel";

                //Adiciona as informações na lista
                list.Add(new() { Title = courseName, Teacher = nameInstructors, Description = description, WorkLoad = valueWorkload });

                _driver.GoToNoWait(linkSearch);
            }

            return (new() { IsSuccess = true, Observation = "Acesso a pagina realizado" }, list);
        }
        catch (Exception ex)
        {
            return (new() { IsSuccess = false, Observation = "Um erro ocorreu ao tentar acessar a pagina, erro: " + ex.Message }, null);
        }
        finally
        {
            _driverFactoryService.Quit();
        }
    }
}