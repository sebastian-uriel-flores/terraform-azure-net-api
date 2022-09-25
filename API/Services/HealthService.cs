namespace DemoAPIAzure.Services;

public interface IHealthService
{
    string CheckHealth();
}

public class HealthService : IHealthService
{
    public string CheckHealth() => "API running";
}