using LibraryApp.Web.Repository;
using LibraryApp.Web.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IUnitOfWorkHttp, UnitOfWorkHttp>(c =>
{
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});

//builder.Services.AddAuthentication("Bearer")
//       .AddJwtBearer(options =>
//       {
//           options.TokenValidationParameters = new TokenValidationParameters
//           {
//               ValidateIssuerSigningKey = true,
//               ValidateIssuer = false,
//               ValidateAudience = false,
//               IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
//             builder.Configuration.GetSection("JWT:Token").Value!))
//           };
//       });

//builder.Services.AddAuthorization();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

app.Run();
