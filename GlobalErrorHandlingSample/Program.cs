using GlobalErrorHandlingSample.Infrastructure;
using GlobalErrorHandlingSample.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("user/test", () =>
{
    int a = Convert.ToInt16("123a");
    return a;
});
//app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseExceptionHandler();
app.Run();