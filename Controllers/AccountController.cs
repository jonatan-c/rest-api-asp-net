using Microsoft.AspNetCore.Mvc;
using web_api.Data;
using web_api.Data.DataBankModels;
using web_api.Dtos;
using web_api.Services;
using Microsoft.AspNetCore.Authorization;


namespace web_api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    // Constructur need have same name to Class
    private readonly ClientService _clientService;
    private readonly AccountService _accountService;
    private readonly AccountTypeService _accountTypeService;
    public AccountController(
        ClientService clientService
        , AccountService accountService
        , AccountTypeService accountTypeService
        )
    {
       this._accountService = accountService;
         this._clientService = clientService;
            this._accountTypeService = accountTypeService;
    }
    
    //TODO GET ALL
    [HttpGet]
    public async   Task<IEnumerable<AccountDtoOut>> Get()
    {
        return await _accountService.GetAll();
    }
    
    //TODO GET BY ID
    [HttpGet( "{id}" )]
    public async Task<ActionResult<AccountDtoOut>> GetById(int id)
    {
        var client = await _accountService.GetDtoById(id);

        if (client == null)
        {
            // return ClientNotFound(id);
            return NotFound(new { message = $"El account con ID = {id} no existe." });
        }

        return client;

    }

    //TODO POST
    [Authorize(Policy = "SuperAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create(AccountDtoIn account)
    {
       var newAccount = await _accountService.Create(account);

        return CreatedAtAction(nameof(GetById), new { id = newAccount.Id }, newAccount);
    }

    //TODO PUT
    [Authorize(Policy = "SuperAdmin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AccountDtoIn account)
    {
        if (id != account.Id)
        {
            return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({account.Id} del cuerpo de la solicitud. )" });
        }

        var clientToUpdate = await _accountService.GetById(id);

        if (clientToUpdate == null)
        {
            // return ClientNotFound(id);
            return NotFound(new { message = $"El cliente con ID = {id} no existe." });
        }

        await _accountService.Update(id, account);
        return NoContent();

    }

    //TODO DELETE
    [Authorize(Policy = "SuperAdmin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
         var clientToDelete = await _accountService.GetById(id);

        if (clientToDelete == null)
        {
            // return ClientNotFound(id);
            return NotFound(new { message = $"El cliente con ID = {id} no existe." });
        }

        await _accountService.Delete(id);
        return Ok();
    }

    // public NotFoundObjectResult ClientNotFound(int id ) 
    // {
    //     return NotFound(new { message = $"El cliente con ID = {id} no existe." });
    // }
}