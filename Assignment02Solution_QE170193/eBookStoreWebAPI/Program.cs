using BusinessObject.Models;
using DataAccess.Repository;
using DataAccess.Repository.Interface;
using DataAccess.Services;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddOData(options =>
{
    var build = new ODataConventionModelBuilder();

    build.EntitySet<Author>("Author");
    build.EntitySet<Book>("Book");
    build.EntitySet<BookAuthor>("BookAuthor");
    build.EntitySet<Publisher>("Publisher");
    build.EntitySet<Role>("Role");
    build.EntitySet<User>("User");

    options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(null)
        .AddRouteComponents("odata", build.GetEdmModel());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Assignment2API", Version = "v1" })
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<EBookStoreDBContext>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();


builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookAuthorService, BookAuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseODataBatching();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
