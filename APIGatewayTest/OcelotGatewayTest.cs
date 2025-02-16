using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Assert = Xunit.Assert;


namespace APIGatewayTest;

public class OcelotGatewayTest:IClassFixture<WebApplicationFactory<APIGateway.Program>>
{
    private readonly WebApplicationFactory<APIGateway.Program> _factory;
    private readonly HttpClient _client;
    
    
    public OcelotGatewayTest(WebApplicationFactory<APIGateway.Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task LoginEndpointReturnsToken()
    {
        //Arrange
        var adminCreds = new
        {
            name = "David Goggings",
            password = "stayhard"
        };
        
        var body = JsonContent.Create(adminCreds);
        //Act
        var response = await _client.PostAsync("/auth/login", body);
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}