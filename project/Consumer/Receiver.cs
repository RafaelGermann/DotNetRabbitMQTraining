using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Consumer;
internal class Receiver
{
    private static void Main(string[] args)
    {
        var factory = new ConnectionFactory { HostName = "localhost", Password = "123456", UserName = "admin" };
        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Teste", exclusive: false, autoDelete: false);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine("Received message {0}", message);
                };
                channel.BasicConsume("Teste",true, consumer);
            }
        }

        Console.WriteLine("Press [enter] to exit the Consumer App...");
        Console.ReadLine();
    }
}