namespace ConfiguringMiddlewares.Middleware
{
    public class TimingMiddleware
    {
        //middlewares are components that are used to process requests and responses in the request pipeline.
        //Middlewares are executed in the order they are added to the pipeline, and they can perform various
        //tasks such as authentication, logging, routing, and more


        private readonly ILogger<TimingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public TimingMiddleware(ILogger<TimingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            //startTime variable stores the exact time of when this execution started
            var startTime = DateTime.UtcNow;
            await _next.Invoke(context);
            _logger.LogInformation($"Timing: {context.Request.Path} : {(DateTime.Now - startTime).TotalMilliseconds} milliseconds");
        }
    }
        public static class TimingExtensions
        {
            public static IApplicationBuilder UseTiming(this IApplicationBuilder app)
            {
                // We can directly do this in program.cs file we are just doing this for loose coupling of the code
               return app.UseMiddleware<TimingMiddleware>();
            }
        }
}
