using RabbitMQ.Client;
using System.Text;

namespace Producer;
internal class Sender
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory { HostName = "localhost", Password = "123456", UserName = "admin" };
        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Teste", exclusive: false, autoDelete: false);
                string message = "NEW MESSAGE RABBITMQ";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", "Teste", null, body);
                Console.WriteLine("Sending message {0}", message);
            }
        }

        Console.WriteLine("Press [enter] to exit the sender App...");
        Console.ReadLine();
    }
}