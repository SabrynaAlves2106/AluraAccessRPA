using AluraAccessRPA.Application.Selenium.Extensions;
using AluraAccessRPA.Domain.Entities;
using AluraAccessRPA.Domain.Interfaces;
using OpenQA.Selenium;
using System.Security;

namespace AluraAccessRPA.Application.Selenium.Page;

public class AluraPage
{
    private readonly IDriverFactoryService _driverFactoryService;
    private IWebDriver _driver => _driverFactoryService.Instance;
    public AluraPage(IDriverFactoryService driverFactoryService) => _driverFactoryService = driverFactoryService;
    public NavigateResult AccessPage(string url)
    {
        try
        {
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

            _driver.WaitElement(By.XPath("//input[@placeholder='O que você quer aprender?']")).SendKeys(course);
            _driver.WaitElement(By.XPath("//button[@class='header__nav--busca-submit']")).ClickNoWait();
            var itens = _driver.WaitElements(By.XPath("//li[@class='busca-resultado']")).Select((element, v) =>
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
            var list = new List<CourseInformation>();
            foreach (var courseName in itens)
            {
                //Captura a descrição antes de entrar no link
                var description = _driver.WaitElement(By.XPath($"//h4[contains(text(),'{courseName}')]/following-sibling::p")).Text;

                //Clica no link do curso
                _driver.WaitElement(By.XPath($"//*[contains(text(),'{courseName}')]")).ClickNoWait();

                //Pega todos os elementos que contém nome dos instrutores
                var instructorElements = _driver.WaitElements(By.XPath("//div/h3[@class = 'instructor-title--name'] | //div/h3[@class = 'formacao-instrutor-nome']"));

                string nameInstructors = instructorElements.Count() >0 ? String.Join(", ", instructorElements.Select(x => x.Text).Where(x=> x != "")) : "Nome dos instrutores não esta disponivel";

                //Pega a carga horaria caso exista
                var workloadElement = _driver.WaitElement(By.XPath("//*[@class = 'formacao__info-conclusao'] | //*[@class='formacao__info-destaque']"),5);
                var valueWorkload = workloadElement is not null ? workloadElement.Text : "Carga horaria não esta disponivel";

                //Adiciona as informações na lista
                list.Add(new() { Title = courseName, Teacher = nameInstructors, Description = description, WorkLoad = valueWorkload });

                _driver.BackNoWait();
            }

            return (new() { IsSuccess = true, Observation = "Acesso a pagina realizado" }, list);
        }
        catch (Exception ex)
        {
            return (new() { IsSuccess = false, Observation = "Um erro ocorreu ao tentar acessar a pagina, erro: " + ex.Message }, null);
        }
        finally
        {
            _driver.Quit();
        }
    }
}