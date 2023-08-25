using UseCase2.Models;

var builder =  WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

RegisterServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

void RegisterServices(IServiceCollection services, IConfiguration config)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var stripeConfig = config.GetSection("Stripe");

    services.AddSingleton<StripeConfigurationOptions>(_ => new StripeConfigurationOptions
    {
        ApiKey = stripeConfig.GetValue<string>("ApiKey"),
    });
}
