using Customer.API.Data;
using Customer.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CustomerDbContext>(options =>
{
    options.EnableSensitiveDataLogging(true);
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomer, CustomersRepo>();

var factory = new ConnectionFactory() { HostName = builder.Configuration["RabbitMQSettings:HostName"] };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
channel.QueueDeclare(queue: builder.Configuration["RabbitMQSettings:QueueName"], durable: false, exclusive: false, autoDelete: false, arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var productMessage = JsonConvert.DeserializeObject<dynamic>(message);
    // Process the message (e.g., log it, update a cache, etc.)
    if (productMessage.Product == "GET API call")
    {
        // Make an HTTP GET request to Service B's own API endpoint
        using (var httpClient = new HttpClient())
        {
            var apiUrl = "https://localhost:7228/customers/getAllCustomers"; // Replace with your actual API endpoint
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received response from API: {apiResponse}");
            }
            else
            {
                Console.WriteLine($"Failed to call API. Status code: {response.StatusCode}");
                // Handle error scenario
            }
        }
    }
    else
    {
        Console.WriteLine($"Processing Create action for Product: {message}");
    }
    
};
channel.BasicConsume(queue: builder.Configuration["RabbitMQSettings:QueueName"], autoAck: true, consumer: consumer);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
