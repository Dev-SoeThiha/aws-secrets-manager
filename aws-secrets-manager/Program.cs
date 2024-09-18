using aws_secrets_manager.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure configuration sources
#region aws secrets manager injection
if (builder.Environment.IsProduction())
{
    // Fetch the secret from AWS Secrets Manager and add it to configuration
    var secret = AwsSecretsManagerService.GetSecretAsDictionaryAsync().GetAwaiter().GetResult();

    // Exit the application with an error code
    if (secret is null)
        Environment.Exit(1);

    // Add the new configuration to the existing configuration
    builder.Configuration.AddInMemoryCollection(secret);
}
#endregion

// Add services to the container.


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
