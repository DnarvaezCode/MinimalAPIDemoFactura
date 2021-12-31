using APIFacturaV1.DTOs;
using APIFacturaV1.Models;
using APIFacturaV1.Repository.Interfaces;
using APIFacturaV1.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniValidation;

namespace APIFacturaV1.EndPointExtension
{
    public static class EndPointCliente
    {
        public static void AddEndPointCliente(this WebApplication app)
        {
            app.MapGet("api/cliente", async (IClienteRepository repository, IStoreProcedureRepository<Cliente> storeProcedureRepository, IMapper mapper) =>
            {
                try
                {
                    //var clientes = await repository.ObtenerClientesAsync();
                    var clientes = await storeProcedureRepository.GetDataAsync($"exec {Utilitie.Utilitie.spObtenerClientes}", new object[] { });
                    return Results.Ok(mapper.Map<IEnumerable<ClienteDTO>>(clientes));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("api/cliente/{id}", async ([FromServices] IClienteRepository repository, IStoreProcedureRepository<Cliente> storeProcedureRepository, int id, IMapper mapper) =>
            {
                try
                {
                    //var cliente = await repository.ObtenerClientePorIdAsync(id);
                    var resultados = await storeProcedureRepository.GetDataAsync("spObtenerCliente  @Id", new SqlParameter("@Id", id));
                    var cliente = resultados.FirstOrDefault();
                    if (cliente is null) return Results.NotFound();
                    return Results.Ok(mapper.Map<ClienteDTO>(cliente));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).WithName(EndPointNames.ObtenerCliente);

            app.MapPost("api/cliente", async ([FromServices] IClienteRepository repository, IStoreProcedureRepository<Cliente> storeProcedureRepository, [FromBody] ClienteDTO clienteDTO, IMapper mapper) =>
            {
                try
                {
                    if (!MiniValidator.TryValidate(clienteDTO, out var errors)) return Results.BadRequest(errors);
                    //var cliente = mapper.Map<Cliente>(clienteDTO);
                    //var idCliente = await repository.InsertarClienteAsync(cliente);
                    var idParemeter = Utilitie.Utilitie.ParameterId();
                    int idCliente = await SaveDataAsync(storeProcedureRepository, $"{Utilitie.Utilitie.spInsertarCliente} @Id output,@Nombres,@Apellidos,@Direccion,@Telefono,@Correo, @FechaNacimiento", clienteDTO, idParemeter);
                    if (idCliente == 0) return Results.StatusCode(500);
                    return Results.CreatedAtRoute(EndPointNames.ObtenerCliente, new { id = idCliente }, clienteDTO);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("api/cliente", async ([FromServices] IClienteRepository repository, IStoreProcedureRepository<Cliente> storeProcedureRepository, [FromBody] ClienteDTO clienteDTO, IMapper mapper) =>
            {
                try
                {

                    if (!MiniValidator.TryValidate(clienteDTO, out var errors)) return Results.BadRequest(errors);
                    var cliente = mapper.Map<Cliente>(clienteDTO);
                    //var idCliente = await repository.ModificarClienteAsync(cliente);
                    var idParemeter = Utilitie.Utilitie.ParameterId();
                    int idCliente = await SaveDataAsync(storeProcedureRepository, $"{Utilitie.Utilitie.spActualizarCliente} @Id,@Nombres,@Apellidos,@Direccion,@Telefono,@Correo, @FechaNacimiento", clienteDTO, null);
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

        private static async Task<int> SaveDataAsync(IStoreProcedureRepository<Cliente> storeProcedureRepository, string nameStoreProcedure, ClienteDTO clienteDTO, SqlParameter idParemeter)
        {
            return await storeProcedureRepository.SaveDataAsync(nameStoreProcedure,
                idParemeter,
                new[] {         
                        idParemeter is null ?  new SqlParameter("@Id", clienteDTO.Id) : idParemeter,
                        new SqlParameter("@Nombres", clienteDTO.Nombres),
                        new SqlParameter("@Apellidos", clienteDTO.Apellidos),
                        new SqlParameter("@Direccion", clienteDTO.Direccion),
                        new SqlParameter("@Telefono", clienteDTO.Telefono),
                        new SqlParameter("@Correo", clienteDTO.Correo),
                        new SqlParameter("@FechaNacimiento", clienteDTO.FechaNacimiento)
                });
        }

    }
}
