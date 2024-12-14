using ParkingLib.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IParkingRepo, ParkingRepo>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny",
    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
);
    options.AddPolicy("AllowSpecificOrigin",
    builder => builder.WithOrigins("http://zealand.dk").AllowAnyHeader().AllowAnyMethod()
    );
    options.AddPolicy("AllowOnlyGetPut",
   builder => builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("Get", "Put")
   );
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAny");

app.MapControllers();

app.Run();
