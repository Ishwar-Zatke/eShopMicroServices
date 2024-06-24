using RabbitMQ.Client;

namespace Catalog.RabbitMQ
{
    public class RabbitMQConnection 
    {
        public IConnection?_connection;
        public RabbitMQConnection()
        {
            InitializingConnection();
        }

        private void InitializingConnection()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
            };
            _connection = factory.CreateConnection();
        }
    }
}
