using APIFacturaV1.Models;
using APIFacturaV1.Repository.Interfaces;
using MiniValidation;

namespace APIFacturaV1.EndPointExtension
{
    public static class EndPointCategoria
    {
        public static void AddEndPointCategoria(this WebApplication app)
        {
            app.MapGet("api/categoria", async (IBaseRepository<Categoria> repository) =>
            {
                try
                {
                    return Results.Ok(await repository.ObtenerTodosAsync());
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("api/categoria/{id}", async (IBaseRepository<Categoria> repository, int id) =>
            {
                try
                {
                    var categoria = await repository.ObtenerPorIdAsync(id);
                    if (categoria is null) return Results.NotFound();
                    return Results.Ok(categoria);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("api/categoria", async (IBaseRepository<Categoria> repository, Categoria categoria) =>
            {
                try
                {

                    if (!MiniValidator.TryValidate(categoria, out var errors)) return Results.BadRequest(errors);
                    var resultado = await repository.InsertarAsync(categoria);
                    if (resultado == 0) return Results.StatusCode(500);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("api/categoria", async (IBaseRepository<Categoria> repository, Categoria categoria) =>
            {
                try
                {

                    if (!MiniValidator.TryValidate(categoria, out var errors)) return Results.BadRequest(errors);

                    var resultado = await repository.ModificarAsync(categoria);
                    if (resultado == 0) return Results.StatusCode(500);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapDelete("api/categoria", async (IBaseRepository<Categoria> repository, int id) =>
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
