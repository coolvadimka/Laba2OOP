using System.Net.Sockets;
TcpClient tcpClient = new TcpClient();
const string IP = "127.0.0.1";
const int port1 = 7000;
const int port2 = 8000;
Console.WriteLine("Введите своё имя: ");
string word = Console.ReadLine();
await tcpClient.ConnectAsync(IP, port1);
Console.WriteLine($"Подключение к сверверу {IP}:{port1} установлено.");
var stream = tcpClient.GetStream();
var streamReader = new StreamReader(stream);
var streamWriter = new StreamWriter(stream);
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
//tcpClient.Close();


tcpClient = new TcpClient();
await tcpClient.ConnectAsync(IP, port2);
Console.WriteLine($"Подключение к сверверу {IP}:{port2} установлено.");
stream = tcpClient.GetStream();
streamReader = new StreamReader(stream);
streamWriter = new StreamWriter(stream);
await streamWriter.WriteLineAsync(word);
await streamWriter.FlushAsync();
otvet = await streamReader.ReadLineAsync();
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