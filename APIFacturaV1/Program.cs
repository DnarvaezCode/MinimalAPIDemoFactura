using APIFacturaV1.EndPointExtension;
using APIFacturaV1.Context;
using APIFacturaV1.DTOs;
using APIFacturaV1.Models;
using APIFacturaV1.Repository;
using APIFacturaV1.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<APIFacturaContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConetion")));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped(typeof(IStoreProcedureRepository<>), typeof(StoreProcedureRepository<>));

//builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<Producto, ProductoDTO>();
    configuration.CreateMap<ProductoDTO, Producto>();
    configuration.CreateMap<Categoria, CategoriaDTO>();
    configuration.CreateMap<CategoriaDTO, Categoria>();
    configuration.CreateMap<ClienteDTO, Cliente>();
    configuration.CreateMap<Cliente, ClienteDTO>();

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddEndPointCategoria();
app.AddEndPointProducto();
app.AddEndPointCliente();

app.Run();