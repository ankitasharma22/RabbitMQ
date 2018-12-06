using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQProducer
{
    /// <summary>
    /// consumer -> exchange (mandatory mediator) -> queue
    /// types of exchange: 1. Fan out: broadcasts msg to all queues. Msg contains routing key used for binding between echange and queue, exchange name and queue name In this case, routing key not
    /// needed since msg broadcasted to all queues by default
    /// 2. direct msg: one to one mapping between queue and exchnage.Hence routing key differs. number of one to one mapping = number of routign key 
    /// 3. default: special type of direct exchange: routing key = queue name, exchnage name = ""
    /// 
    /// </summary>
    class Program //to insert into rabbitMQ
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" }; //server = local machine since we have installed rabitmq in our local machine
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //queue declaration
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message); //string 'message' to bytes

                channel.BasicPublish(exchange: "", //direct exchange since exchange = ""
                                     routingKey: "hello", //hello is the name of the queue. Routing key = queue name. If no queue exists with routingKey name, it will create a queue with that name.
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
