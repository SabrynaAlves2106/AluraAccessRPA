namespace AluraAccessRPA.Application.Selenium.Elements;

public class ElementsAlura
{

    public ElementsAlura(IConfiguration configuration)
        =>SetValues(configuration);
    //Busca de cursos
    public By inputSearch { get; set; } 
    public By btnSearch { get; set; }
    public By getCouses { get; set; }

    //Captura de informações
    public string descriptionText { get; set; }
    public string courseLink { get; set; }
    public By instructorsElements { get; set; }
    public By workloadElements { get; set; }

    private void SetValues(IConfiguration config)
    {
        inputSearch = By.XPath(config["Search:Input"]);
        btnSearch = By.XPath(config["Search:BtnSearch"]);
        getCouses = By.XPath(config["Search:GetCourses"]);

        descriptionText = config["GetInfo:Description"];
        courseLink = config["GetInfo:CourseLink"];
        instructorsElements = By.XPath(config["GetInfo:InstructorElement"]);
        workloadElements = By.XPath(config["GetInfo:WorkLoadElement"]);
    }
}
