using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Azure.HttpFunctionApp;

public static class MyHttpTrigger
{
    //query ile parametre alma
    [Function("MyHttpTrigger")]
    public static HttpResponseData GetProducts([HttpTrigger(AuthorizationLevel.Function, "get", "post",Route = "Products")] HttpRequestData req,
        FunctionContext executionContext)
    {
        // var logger = executionContext.GetLogger("MyHttpTrigger");
        // logger.LogInformation("C# HTTP trigger function processed a request.");

        //Sabit değişkenimizi okuduk.
        string? myApi = Environment.GetEnvironmentVariable("MyApi");
        
        //Query'den aldık
        var name = req.Query["name"];

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString($"TEST : {name} {myApi}");

        return response;
        
    }
    
    
    //Path üzerinden Id alma
    [Function("MyHttpTrigger1")]
    public static HttpResponseData GetProductById([HttpTrigger(AuthorizationLevel.Function, "get", "post",Route = "ProductById/{id}")] HttpRequestData req,
        FunctionContext executionContext,int id)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        response.WriteString($"{id}");

        return response;
        
    }
    //Body üzerinden veri alma
    [Function("MyHttpTrigger3")]
    public static HttpResponseData PostProduct([HttpTrigger(AuthorizationLevel.Function,  "post",Route = "PostProduct")] HttpRequestData req,
        FunctionContext executionContext,int id)
    {
        var requestBody = new StreamReader(req.Body).ReadToEnd();
        var objectProduct = JsonSerializer.Deserialize<Product>(requestBody);
        
        var response = req.CreateResponse(HttpStatusCode.OK);

        response.WriteAsJsonAsync(objectProduct);

        return response;
        
    }
    
    
}