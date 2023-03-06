using Microsoft.AspNetCore.Mvc;
using web_api.Data;
using web_api.Data.DataBankModels;
using web_api.Services;
using Microsoft.AspNetCore.Authorization;

namespace web_api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    // Constructur need have same name to Class
    private readonly ClientService _service;
    public ClientController(ClientService client)
    {
        _service = client;
    }
    
    //TODO GET ALL
    [HttpGet]
    public async   Task<IEnumerable<Client>> Get()
    {
        return await _service.GetAll();
    }
    
    //TODO GET BY ID
    [HttpGet( "{id}" )]
    public async Task<ActionResult<Client>> GetById(int id)
    {
        var client = await _service.GetById(id);

        if (client == null)
        {
            // return ClientNotFound(id);
            return NotFound(new { message = $"El cliente con ID = {id} no existe." });
        }

        return client;

    }

    //TODO POST
    [Authorize(Policy = "SuperAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(Client client)
    {
       var newClient = await _service.Create(client);

        return CreatedAtAction(nameof(GetById), new { id = newClient.Id }, newClient);
    }

    //TODO PUT
    [Authorize(Policy = "SuperAdmin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Client client)
    {
        if (id != client.Id)
        {
            return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({client.Id} del cuerpo de la solicitud. )" });
        }

        var clientToUpdate = await _service.GetById(id);

        if (clientToUpdate == null)
        {
            // return ClientNotFound(id);
            return NotFound(new { message = $"El cliente con ID = {id} no existe." });
        }

        await _service.Update(id, client);
        return NoContent();

    }

    //TODO DELETE
    [Authorize(Policy = "SuperAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
         var clientToDelete = await _service.GetById(id);

        if (clientToDelete == null)
        {
            // return ClientNotFound(id);
            return NotFound(new { message = $"El cliente con ID = {id} no existe." });
        }

        await _service.Delete(id);
        return Ok();
    }

    // public NotFoundObjectResult ClientNotFound(int id ) 
    // {
    //     return NotFound(new { message = $"El cliente con ID = {id} no existe." });
    // }
}