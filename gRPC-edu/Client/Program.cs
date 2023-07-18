using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeterClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:5058");
var client = new Operator.OperatorClient(channel);
var reply = await client.CalculateAsync(
    new OperateRequest { Argument1 = 12, Operation = Operation.Divide, Argument2 = 3});
Console.WriteLine("Result: " + reply.Result);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();