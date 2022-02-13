namespace EventBus.RabbitMQ;

public class RabbitMQOptions
{
    public string HostName { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public int? Port { get; set; }

    public string QueueName { get; set; }

    public int RetryCount { get; set; } = 5;
}
