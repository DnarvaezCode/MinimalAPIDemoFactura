using APIFacturaV1.DTOs;
using APIFacturaV1.Models;
using APIFacturaV1.Repository.Interfaces;
using APIFacturaV1.Utils;
using AutoMapper;
using FluentValidation;
using MiminalApis.Validators;
using MiniValidation;

namespace APIFacturaV1.EndPointExtension
{
    public static class EndPointCategoria
    {
        public static void AddEndPointCategoria(this WebApplication app)
        {
            app.MapGet("api/categoria", async (IGeneryRepository<Categoria> repository, IMapper mapper) =>
            {
                try
                {
                    var categorias = await repository.ObtenerTodosAsync();
                    return Results.Ok(mapper.Map<IEnumerable<CategoriaDTO>>(categorias));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).RequireAuthorization();

            app.MapGet("api/categoria/{id:int}", async (IGeneryRepository<Categoria> repository, IMapper mapper, int id) =>
            {
                try
                {
                    var categoria = await repository.ObtenerPorIdAsync(id);
                    if (categoria is null) return Results.NotFound();
                    return Results.Ok(mapper.Map<CategoriaDTO>(categoria));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName(EndPointNames.ObtenerCategoria)
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .AllowAnonymous();

            app.MapPost("api/categoria", async (IGeneryRepository<Categoria> repository, IValidator<CategoriaDTO> validator, IMapper mapper, CategoriaDTO categoriaDTO) =>
            {
                try
                {
                    var validationResult = validator.Validate(categoriaDTO);
                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(x => new { ErrorMessage = x.ErrorMessage });
                        return Results.BadRequest(errors);
                    }

                    var categoria = mapper.Map<Categoria>(categoriaDTO);
                    var idCategoria = await repository.InsertarAsync(categoria);
                    if (idCategoria == 0) return Results.StatusCode(500);
                    return Results.CreatedAtRoute(EndPointNames.ObtenerCategoria, new { id = idCategoria }, categoriaDTO);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });//.WithValidator<CategoriaDTO>();

            app.MapPut("api/categoria", async (IGeneryRepository<Categoria> repository, IMapper mapper, CategoriaDTO categoriaDTO) =>
            {
                try
                {
                    var categoria = mapper.Map<Categoria>(categoriaDTO);
                    var idCategoria = await repository.ModificarAsync(categoria);
                    if (idCategoria == 0) return Results.StatusCode(500);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).WithValidator<CategoriaDTO>();

            app.MapDelete("api/categoria", async (IGeneryRepository<Categoria> repository, int id) =>
            {
                try
                {
                    if (await repository.ObtenerPorIdAsync(id) is null) return Results.NotFound();

                    var idCategoria = await repository.EliminarAsync(id);
                    if (idCategoria == 0) return Results.StatusCode(500);

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
