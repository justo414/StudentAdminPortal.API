using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repositories;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

IWebHostEnvironment environment = builder.Environment;

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddCors((options) =>
{
    options.AddPolicy("angularApplication", (builder) =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .WithMethods("GET", "POST", "PUT", "DELETE")
        .WithExposedHeaders("*");
    });
});
builder.Services.AddControllers();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddDbContext<StudentAdminContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("StudentAdminPortalDb")));

builder.Services.AddScoped<IStudentRepository, SqlStudentRepository>();
builder.Services.AddScoped<IImageRepository, LocalStorageImageRepository>();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}


app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Resources")),
    RequestPath = "/Resources"
});

app.UseRouting();

app.UseCors("angularApplication");

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
 