using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api.Data;
using web_api.Data.DataBankModels;
using Microsoft.EntityFrameworkCore;

namespace web_api.Services;

public class ClientService
{    
    private readonly MasterContext _context;

    public ClientService(MasterContext context)
    {
        _context = context;
    }
    

    //TODO GETALL SERVICE
    public async Task<IEnumerable<Client>> GetAll()
    {
        return await _context.Clients.ToListAsync();
    }

    //tODO GETBYID SERVICE
    public async Task<ActionResult<Client>?> GetById(int id)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
        {
            return null;
        }

        return client;

    }

    //TODO POST SERVICE
    public async Task<Client> Create(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return client;
    }

    //TODO PUT SERVICE
    public async Task Update(int id, Client client)
    {
      

        var clientSearch = await  _context.Clients.FindAsync(id);

        if (clientSearch == null)
        {
            return;
        }

        clientSearch.Name = client.Name;
        clientSearch.Email = client.Email;
        clientSearch.PhoneNumber = client.PhoneNumber;

       await  _context.SaveChangesAsync();
         
    }

    //TODO DELETE SERVICE
    public async Task Delete(int id)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
        {
            return;
        }

        _context.Clients.Remove(client);
        await  _context.SaveChangesAsync();
    }
 }