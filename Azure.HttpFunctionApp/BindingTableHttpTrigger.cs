

using System.Net;
using System.Text.Json;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Azure.HttpFunctionApp;

public class BindingTableHttpTrigger
{
    [Function("BindingTableHttpTrigger")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext,
        [TableInput("Products", Connection = "MyAzureStorage")] TableClient result
        )
    {
        string request = await new StreamReader(req.Body).ReadToEndAsync();
        Product? product = JsonSerializer.Deserialize<Product>(request);

        await result.AddEntityAsync(product);
        

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteString("başarılı");
        return response;
        
    }
}