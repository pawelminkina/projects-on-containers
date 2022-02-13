using RabbitMQ.Client;

namespace EventBus.RabbitMQ.PersistentConnection;

public interface IRabbitMQPersistentConnection : IDisposable
{
    bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel();
}