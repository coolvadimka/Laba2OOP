using System.Net.Sockets;
using TcpClient tcpClient = new TcpClient();
const string IP = "127.0.0.1";
const int port = 7000;
Console.WriteLine("Введите своё имя: ");
string word = Console.ReadLine();
await tcpClient.ConnectAsync(IP, port);
Console.WriteLine($"Подключение к сверверу {IP}:{port} установлено.");
var stream = tcpClient.GetStream();
using var streamReader = new StreamReader(stream);
using var streamWriter = new StreamWriter(stream);
await streamWriter.WriteLineAsync(word);
await streamWriter.FlushAsync();
var otvet = await streamReader.ReadLineAsync();
Console.WriteLine($"{otvet}");
while (true)
{
    string temp = Console.ReadLine();
    if (temp == "stop")
    {
        break;
    }
}
await streamWriter.WriteLineAsync("END");
await streamWriter.FlushAsync();