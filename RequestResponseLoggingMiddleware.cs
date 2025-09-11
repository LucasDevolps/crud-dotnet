using System.Diagnostics;

namespace EstoqueApi.Application
{
    public sealed class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, 
                                                ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            var requestLog = new RequestLog
            {
                Timestamp = DateTime.UtcNow,
                RequestMethod = context.Request.Method,
                RequestPath = context.Request.Path,
                RequestHeaders = System.Text.Json.JsonSerializer.Serialize(context.Request.Headers)
            };

            string requestBody = await ReadRequestBody(context.Request);
            requestLog.RequestBody = requestBody;

            var originalbodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();

            context.Response.Body = responseBody;

            try
            {
                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                string responseBodyStreamReader = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                requestLog.StatusCode = context.Response.StatusCode;
                requestLog.ResponseBody = responseBodyStreamReader;
                requestLog.ResponseHeaders = GetHeadersAsString(context.Response.Headers);

                await responseBody.CopyToAsync(originalbodyStream);
            }
            finally
            {
                stopwatch.Stop();
                requestLog.ResponseTimeMs = stopwatch.ElapsedMilliseconds;
            }

        }
        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, System.Text.Encoding.UTF8, false, 1024, true);
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }
        private string GetHeadersAsString(IHeaderDictionary headers)
        {
            return System.Text.Json.JsonSerializer.Serialize(headers);
        }
    }
}
