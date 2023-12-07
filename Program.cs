using BE_Shop.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;
using BE_Shop.Data;
using BE_Shop.Data.Service;
using BE_Shop.Hubs;

//Cập nhật CSDL
//await DatabaseConnection.CreateDatabase();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { 
		Title = "BE_Shop",
		Description = "Backend of Ecommerce project",
		Version = "v1"
	});
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme()
			{
				Reference = new OpenApiReference()
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header,
			},
			new List<string>()
		}
	});
});
builder.Services.AddSignalR();
// builder.Services.AddScoped<IEmailService, EmailService>();
// builder.Services.AddScoped<ISMSService, SMSService>();

// Add login token.
builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
	x.RequireHttpsMetadata = false;
	x.SaveToken = true;
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		ValidateAudience = false,
		ValidateIssuer = false,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(UserController.key)),
	};
});

// Add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "http://112.78.1.194",
											  "http://misaproject.click")
						  .AllowAnyHeader()
						  .AllowAnyMethod()
						  .AllowCredentials();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
		c.DocExpansion(DocExpansion.List);
	});
}

//app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/hub");

app.Run();
