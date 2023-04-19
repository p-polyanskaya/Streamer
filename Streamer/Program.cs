using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using GrpcServices;

using var channel = GrpcChannel.ForAddress("http://localhost:5005");
var client = new DataStreamer.DataStreamerClient(channel);
var call = client.SendStreamData();

while (true)
{
    await call.RequestStream.WriteAsync(new Request
    {
        Message = new Message()
        {
            Id = "",
            MessageText = "",
            TimeOfMessage = DateTime.UtcNow.ToTimestamp(),
            Source = "",
            UserName = ""
        }
    });
}
await call.RequestStream.CompleteAsync();