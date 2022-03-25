using AzureFunctionDemo.Efcore;
using AzureFunctionDemo.Models;
using AzureFunctionDemo.Usecases.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace AzureFunctionDemo
{
    public class FunctionController
    {
        private readonly ILogger _logger;
        private readonly ITransactionVerifier _verifier;

        public FunctionController(ILoggerFactory loggerFactory, ITransactionVerifier verifier)
        {
            _logger = loggerFactory.CreateLogger<FunctionController>();
            _verifier = verifier;
        }

        [Function("Transaction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            var model = await GetRequestObject<TransactionInputDTO>(req);
            decimal remainingBalance = 0;
            try
            {
                if(await _verifier.VerifyAccountBalanceAsync(model))
                {
                    remainingBalance  = await _verifier.RecordTransaction(model);
                }
            } 
            catch (Exception ex)
            {
                return await GetFailedResponseObject<bool>(req, model, ex.Message, "Got Exception");
            }

            return await GetResponseObject<TransactionOutputDTO>(
                req,
                new TransactionOutputDTO
                {
                    Account = model.Account,
                    BalanceAmount = remainingBalance,
                    Direction = model.Direction,
                },
                HttpStatusCode.Created,
                model,
                String.Empty,
                "Transaction Recorded"
                );
        }



        private async Task<T?> GetRequestObject<T>(HttpRequestData req) where T : class
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(requestBody);
        }

        private async Task<HttpResponseData> GetResponseObject<T>(
            HttpRequestData req,
            T responseData,
            HttpStatusCode statusCode,
            object payload,
            string error,
            string message)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            var body = new CommonResponse<T> {
                Code = (int)statusCode,
                Message = "",
                Error = "",
                Payload = payload != null ? JsonConvert.SerializeObject(payload) : string.Empty,
                Data = responseData
            };
            await response.WriteStringAsync(JsonConvert.SerializeObject(body));
            return response;
        }

        private async Task<HttpResponseData> GetFailedResponseObject<T>(
            HttpRequestData req, 
            object payload,
            string error,
            string message)
        {
            return await GetResponseObject<bool>(req, false, HttpStatusCode.BadRequest, payload, error, message) ;
        }
    }
}
