using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    // 檢查 HTTP Header 的 Authorization 是否有 JWT Bearer Token
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // 設定 JWT Bearer Token 的檢查選項
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            #region  配置驗證發行者

            ValidateIssuer = false, // 是否要啟用驗證發行者
            ValidIssuer = Configuration["JWTConfig:Issuer"],

            ValidateAudience = false, // 是否要啟用驗證接收者
            ValidAudience = builder.Configuration["JWTConfig:Audience"],   // ValidAudience = "" // 如果不需要驗證接收者可以註解

            ValidateLifetime = false, // 是否要啟用驗證有效時間

            ValidateIssuerSigningKey = false, // 是否要啟用驗證金鑰，一般不需要去驗證，因為通常Token內只會有簽章


            // 這裡配置是用來解Http Request內Token加密
            // 如果Secret Key跟當初建立Token所使用的Secret Key不一樣的話會導致驗證失敗
            //ValidIssuer = Configuration["JWTConfig:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTConfig:SignKey"]))

            #endregion
        };
    });

//builder.Services.AddAuthorization();


//處理CORS跨網站呼叫
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

//JWT
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
