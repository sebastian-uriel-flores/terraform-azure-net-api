namespace TerraAzSQLAPI.Services;

public interface IHelloWorldService
{
    string GetHelloWorld();
}

public class HelloWorldService : IHelloWorldService
{
    public string GetHelloWorld() => "Hello World!";
}