
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AluraAccessRPA.Domain.Entities;

public class CourseInformation
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get;set; }
    public string Teacher { get; set; }
    [MaxLength]
    public string Description { get;set; }
    public string WorkLoad { get; set; }
}
