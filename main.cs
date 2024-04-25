using System.Net;
using System.Net.Sockets;
using System.Text;

IPAddress localAddress = IPAddress.Parse("235.5.5.11");
Console.Write("Введіть своє ім'я: ");
string? username = Console.ReadLine();
var localPort = 8001;
var remotePort = 8001;
Console.WriteLine();

Task.Run(ReceiveMessageAsync);
await SendMessageAsync();

async Task SendMessageAsync()
{
    using UdpClient sender = new UdpClient();
    Console.WriteLine("Щоб надіслати повідомлення, введіть повідомлення та натисніть Enter");
    while (true)
    {
        var message = Console.ReadLine(); 
        if (string.IsNullOrWhiteSpace(message)) break;
        message = $"{username}: {message}";
        byte[] data = Encoding.UTF8.GetBytes(message);
        await sender.SendAsync(data, new IPEndPoint(localAddress, remotePort));
    }
}
async Task ReceiveMessageAsync()
{
    using UdpClient receiver = new UdpClient(localPort);
    while (true)
    {
        var result = await receiver.ReceiveAsync();
        var message = Encoding.UTF8.GetString(result.Buffer);
        Console.WriteLine(message);
    }
}
