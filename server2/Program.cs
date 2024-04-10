using System.Net;
using System.Net.Sockets;
const string IP = "127.0.0.1";
const int port = 8000;
var tcpListener = new TcpListener(IPAddress.Any, 8000);
try
{
    tcpListener.Start();
    Console.WriteLine($"Сервер {IP}:{port} запущен. Ожидание подключений... ");

    while (true)
    {
        using var tcpClient = await tcpListener.AcceptTcpClientAsync();
        var stream = tcpClient.GetStream();
        using var streamReader = new StreamReader(stream);
        using var streamWriter = new StreamWriter(stream);
        var name = await streamReader.ReadLineAsync();
        Console.WriteLine($"Пользователь с именем {name} подключился к серверу.");
        var otvet = "Привет, " + name + "!!!";
        await streamWriter.WriteLineAsync(otvet);
        await streamWriter.FlushAsync();
        while (true)
        {
            var word = await streamReader.ReadLineAsync();
            if (word == "END")
            {
                Console.WriteLine($"Пользователь с именем {name} отключился от сервера.");
                break;
            }
        }
    }
}
finally
{
    tcpListener.Stop();
}