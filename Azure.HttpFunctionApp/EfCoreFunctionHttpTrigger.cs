using System.Collections.Generic;
using System.Net;
using Azure.HttpFunctionApp.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Azure.HttpFunctionApp;

public class EfCoreFunctionHttpTrigger
{
    private readonly AppDbContext _appDbContext;

    public EfCoreFunctionHttpTrigger(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [Function("EfCoreFunctionHttpTrigger")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        //TODO: Benzer şekilde ekleme, silme, güncelleme yapılabilir.
        var lessonlist = await _appDbContext.Lessons.ToListAsync();

        
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(lessonlist);
        return response;
    }
}