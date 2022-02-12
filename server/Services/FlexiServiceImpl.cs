using core;
using flexiservice;
using Grpc.Core;
using server.Handlers;

namespace server.Services
{
    public class FlexiServiceImpl : FlexiService.FlexiServiceBase
    {
        private readonly IDictionary<string, FlexiHandlerDelegate> _delegates;
        private readonly ILogger<FlexiServiceImpl> _logger;        

        public FlexiServiceImpl(
            ILogger<FlexiServiceImpl> logger)
        {
            _logger = logger;

            var builder = new FlexiHandlerBuilder();
            builder.Generate(typeof(SomeHandlers));
            _delegates = builder.DelegateMap;
        }



        public override Task<FlexiResponse> SendMessage(FlexiRequest request, ServerCallContext context)
        {
            if (!_delegates.TryGetValue(request.Target, out var del))
            {
                throw new Exception("invalid target");
            }
            return Task.FromResult(del(request));
        }



    }
}
