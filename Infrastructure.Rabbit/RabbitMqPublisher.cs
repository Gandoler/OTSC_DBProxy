using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMqPublisher
{
    private readonly string _hostname;
    private readonly string _queueName;
    private readonly string _userName;
    private readonly string _password;

    public RabbitMqPublisher(IConfiguration configuration)
    {
        _hostname = configuration["RabbitMQ:Host"];
        _queueName = configuration["RabbitMQ:QueueName"];
        _userName = configuration["RabbitMQ:UserName"];
        _password = configuration["RabbitMQ:Password"];
    }

    public async void Publish<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _hostname,
            UserName = _userName,
            Password = _password
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        await channel.BasicPublishAsync()
        await channel.BasicPublish(exchange: "",
            routingKey: _queueName,
            basicProperties: null,
            body: messageBody);
    }
}