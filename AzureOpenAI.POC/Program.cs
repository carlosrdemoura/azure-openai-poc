using Azure;
using AzureOpenAI.POC.Services;
using Microsoft.Extensions.Azure;

namespace AzureOpenAI.POC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<OpenAICompletionsService>();

            builder.Services.AddAzureClients(clientBuilder =>
            {
                clientBuilder.AddOpenAIClient(
                    new Uri(builder.Configuration["AzureOpenAI:Url"]),
                    new AzureKeyCredential(builder.Configuration["AzureOpenAI:ApiKey"])
                    );
            });

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
        }
    }
}