using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using GrpcServices;

using var channel = GrpcChannel.ForAddress("http://localhost:5005");
var client = new DataStreamer.DataStreamerClient(channel);
var call = client.SendStreamData();

int i = 0;
while (true)
{
    if(i<2)
    {
        i++;
        var text = "Thi    s is text of news number " + i+1;
        await call.RequestStream.WriteAsync(new Request
        {
            Message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                Text = text,
                TimeOfMessage = DateTime.UtcNow.ToTimestamp(),
                Source = "source",
                Author = ""
            }
        });
        Console.WriteLine(text);
    }
}
//await call.RequestStream.CompleteAsync();