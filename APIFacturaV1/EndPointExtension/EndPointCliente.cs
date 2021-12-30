using APIFacturaV1.DTOs;
using APIFacturaV1.Models;
using APIFacturaV1.Repository.Interfaces;
using APIFacturaV1.Util;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;

namespace APIFacturaV1.EndPointExtension
{
    public static class EndPointCliente
    {
        public static void AddEndPointCliente(this WebApplication app)
        {
            app.MapGet("api/cliente", async (IClienteRepository repository, IMapper mapper) =>
            {
                try
                {
                    var clientes = await repository.ObtenerClientesAsync();
                    return Results.Ok(mapper.Map<IEnumerable<ClienteDTO>>(clientes));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("api/cliente/{id}", async ([FromServices] IClienteRepository repository, int id, IMapper mapper) =>
            {
                try
                {
                    var cliente = await repository.ObtenerClientePorIdAsync(id);
                    if (cliente is null) return Results.NotFound();
                    return Results.Ok(mapper.Map<ClienteDTO>(cliente));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).WithName(EndPointNames.ObtenerCliente);

            app.MapPost("api/cliente", async ([FromServices] IClienteRepository repository, [FromBody] ClienteDTO clienteDTO, IMapper mapper) =>
            {
                try
                {
                    if (!MiniValidator.TryValidate(clienteDTO, out var errors)) return Results.BadRequest(errors);
                    var cliente = mapper.Map<Cliente>(clienteDTO);
                    var idCliente = await repository.InsertarClienteAsync(cliente);
                    if (idCliente == 0) return Results.StatusCode(500);
                    return Results.CreatedAtRoute(EndPointNames.ObtenerCliente, new { id = idCliente }, clienteDTO);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("api/cliente", async ([FromServices] IClienteRepository repository, [FromBody] ClienteDTO clienteDTO, IMapper mapper) =>
            {
                try
                {

                    if (!MiniValidator.TryValidate(clienteDTO, out var errors)) return Results.BadRequest(errors);
                    var cliente = mapper.Map<Cliente>(clienteDTO);
                    var idCliente = await repository.ModificarClienteAsync(cliente);
                    if (idCliente == 0) return Results.StatusCode(500);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapDelete("api/cliente", async ([FromServices] IClienteRepository repository, int id) =>
            {
                try
                {
                    if (await repository.ObtenerClientePorIdAsync(id) is null) return Results.NotFound();
                    var idCliente = await repository.EliminarClienteAsync(id);
                    if (idCliente == 0) return Results.StatusCode(500);
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
