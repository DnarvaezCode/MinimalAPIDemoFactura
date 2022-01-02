using APIFacturaV1.DTOs;
using APIFacturaV1.Models;
using APIFacturaV1.Repository.Interfaces;
using APIFacturaV1.Utils;
using AutoMapper;
using MiniValidation;

namespace APIFacturaV1.EndPointExtension
{
    public static class EndPointFactura
    {
        public static void AddEndPointFactura(this WebApplication app)
        {
            app.MapGet("api/factura", async (IFacturaRepository repository, IMapper mapper) =>
            {
                try
                {
                    var facturas = await repository.GetAllOrderAsync();
                    return Results.Ok(mapper.Map<IEnumerable<FacturaDTO>>(facturas));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("api/factura/{id:int}", async (IFacturaRepository repository, IMapper mapper, int id) =>
            {
                try
                {
                    var factura = await repository.GetOrderByIdAsync(id);
                    if (factura is null) return Results.NotFound();
                    return Results.Ok(mapper.Map<FacturaDTO>(factura));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            })
            .WithName(EndPointNames.ObtenerFactura)
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            app.MapPost("api/factura", async (IFacturaRepository repository, IMapper mapper, FacturaDTO facturaDTO) =>
            {
                try
                {
                    if (!MiniValidator.TryValidate(facturaDTO, out var errors)) return Results.BadRequest(errors);
                    var factura = mapper.Map<Factura>(facturaDTO);
                    var idFactura = await repository.AddOrderAsync(factura);
                    if (idFactura == 0) return Results.StatusCode(500);
                    return Results.CreatedAtRoute(EndPointNames.ObtenerFactura, new { id = idFactura }, facturaDTO);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });
        }
    }
}
