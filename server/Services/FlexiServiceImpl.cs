using core;
using flexiservice;
using Grpc.Core;
using server.Handlers;
using static flexiservice.FlexiRequest;

namespace server.Services
{
    public class FlexiServiceImpl : FlexiService.FlexiServiceBase
    {
        private readonly IFlexiHandlerService _service;
        private readonly ILogger<FlexiServiceImpl> _logger;        

        public FlexiServiceImpl(
            IFlexiHandlerService service,
            ILogger<FlexiServiceImpl> logger)
        {            
            _service = service;
            _logger = logger;
        }


        public override Task<FlexiResponse> SendMessage(FlexiRequest request, ServerCallContext context)
        {
            /* request comes in:
             *  String: Target
             *  OneOf:
             *      JSON
             *      Any
             * 
             * Given the OneOf state, we either deserialize or any unpack the recv
             * into T/object: result
             * 
             * Pass result into the delegate which returns value
             * 
             * serialize value
             * return value
            */

            var response = request.PayloadCase switch
            {
                PayloadOneofCase.JSON   => _service.Get(request.Target, request.JSON),
                PayloadOneofCase.Any    => _service.Get(request.Target, request.Any),
                _                       => throw new Exception()
            };
            return Task.FromResult(response);
        }



    }
}
