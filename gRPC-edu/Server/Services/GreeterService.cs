using Grpc.Core;
using GrpcGreeter;

namespace Server.Services;

public class GreeterService : Operator.OperatorBase
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<OperateReply> Calculate(OperateRequest request, ServerCallContext context)
    {
        var result = request.Operation switch
        {
            Operation.Plus => request.Argument1 + request.Argument2,
            Operation.Minus => request.Argument1 - request.Argument2,
            Operation.Divide => request.Argument1 / request.Argument2,
            Operation.Multiply => request.Argument1 * request.Argument2,
            _ => throw new ArgumentOutOfRangeException()
        };
        return Task.FromResult(new OperateReply
        {
            Result = result,
        });
    }
}