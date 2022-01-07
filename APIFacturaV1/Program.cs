using APIFacturaV1.EndPointExtension;
using APIFacturaV1.Context;
using APIFacturaV1.DTOs;
using APIFacturaV1.Models;
using APIFacturaV1.Repository;
using APIFacturaV1.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<APIFacturaContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConetion")));
builder.Services.AddScoped(typeof(IGeneryRepository<>), typeof(GeneryRepository<>));
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped(typeof(IStoreProcedureRepository<>), typeof(StoreProcedureRepository<>));
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();
//builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<Producto, ProductoDTO>();
    configuration.CreateMap<ProductoDTO, Producto>();
    configuration.CreateMap<Categoria, CategoriaDTO>();
    configuration.CreateMap<CategoriaDTO, Categoria>();
    configuration.CreateMap<ClienteDTO, Cliente>();
    configuration.CreateMap<Cliente, ClienteDTO>();
    configuration.CreateMap<Factura, FacturaDTO>();
    configuration.CreateMap<FacturaDTO, Factura>();
    configuration.CreateMap<DetalleFactura, DetalleFacturaDTO>();
    configuration.CreateMap<DetalleFacturaDTO, DetalleFactura>();
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddDefaultTokenProviders()
                    .AddEntityFrameworkStores<APIFacturaContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option => option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    });

builder.Services.AddAuthorization();
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddEndPointUser();
app.AddEndPointCategoria();
app.AddEndPointProducto();
app.AddEndPointCliente();
app.AddEndPointFactura();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();

app.Run();