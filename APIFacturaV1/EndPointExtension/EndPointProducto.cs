using APIFacturaV1.DTOs;
using APIFacturaV1.Models;
using APIFacturaV1.Repository.Interfaces;
using APIFacturaV1.Specification;
using AutoMapper;
using MiniValidation;

namespace APIFacturaV1.EndPointExtension
{
    public static class EndPointProducto
    {
        public static void AddEndPointProducto(this WebApplication app)
        {
            app.MapGet("api/producto", async (IBaseRepository<Producto> repository, IMapper mapper) =>
            {
                try
                {
                    var spec = new ProductoCategoriaSpecification();
                    var producto = await repository.ObtenerTodosAsync(spec);
                    return Results.Ok(mapper.Map<IEnumerable<ProductoDTO>>(producto));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("api/producto/{id}", async (IBaseRepository<Producto> repository, int id) =>
            {
                try
                {
                    var spec = new ProductoCategoriaSpecification(id);
                    var producto = await repository.ObtenerEntidadConEspecificacion(spec);

                    if (producto is null) return Results.NotFound();
                    return Results.Ok(producto);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("api/producto", async (IBaseRepository<Producto> repository, Producto producto) =>
            {
                try
                {
                    if (!MiniValidator.TryValidate(producto, out var errors)) return Results.BadRequest(errors);
                    var resultado = await repository.InsertarAsync(producto);
                    if (resultado == 0) return Results.StatusCode(500);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("api/producto", async (IBaseRepository<Producto> repository, Producto producto) =>
            {
                try
                {

                    if (!MiniValidator.TryValidate(producto, out var errors)) return Results.BadRequest(errors);

                    var resultado = await repository.ModificarAsync(producto);
                    if (resultado == 0) return Results.StatusCode(500);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapDelete("api/producto", async (IBaseRepository<Producto> repository, int id) =>
            {
                try
                {
                    if (await repository.ObtenerPorIdAsync(id) is null) return Results.NotFound();

                    var resultado = await repository.EliminarAsync(id);
                    if (resultado == 0) return Results.StatusCode(500);

                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });
        }
    }
}
