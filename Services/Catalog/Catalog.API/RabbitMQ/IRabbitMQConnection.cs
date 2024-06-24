using RabbitMQ.Client;

namespace Catalog.RabbitMQ
{
    public interface IRabbitMQConnection
    {
        IConnection Connection { get; }
    }
}
