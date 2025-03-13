var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(op =>
{
    op.Cookie.Name = "IsLoggedIn";
    op.IdleTimeout = TimeSpan.FromMinutes(60);
    op.Cookie.IsEssential = true;

});
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();