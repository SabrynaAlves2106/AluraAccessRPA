using AluraAccessRPA.Infrastructure.Data;
using AluraAccessRPA.Worker.Common;


namespace AluraAccessRPA.Worker.Jobs;

public class AluraAccessJob : JobBase
{
    RepositoryAlura _repository { get; init; }
    public AluraAccessJob(ILogger<JobBase> logger, RepositoryAlura repository):base(logger)
    {
        _repository= repository;
    }
    public override async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var result = _repository.ObterDados();
        }
        catch(Exception ex) 
        {
        }
    }
}
