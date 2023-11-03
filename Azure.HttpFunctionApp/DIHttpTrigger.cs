using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Azure.HttpFunctionApp;

public class DIHttpTrigger
{
    private readonly ITestService _testService;

    public DIHttpTrigger(ITestService testService)
    {
        _testService = testService;
    }

    [Function("DIHttpTrigger")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post",Route = "DI")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("DIHttpTrigger");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        var test = _testService.GetTest();

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        await response.WriteStringAsync($"Welcome to Azure Functions! {test}");

        return response;
        
    }
}