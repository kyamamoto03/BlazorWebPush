using PushService.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddWebPush(config =>
{

    config.PublicKey = "BD4ldrURU9tPMSWtq-iqG4D6i2m4_IpbvNEsmJxakVgbSV-fxKBhJHouPnkPwRsDI4Yu_gg745t7OjYWLBwAEfA";
    config.PrivateKey = "milFOZRTb164mxVLA_IgGmFyoxzIjK8Dt5XCuhkduR8";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
