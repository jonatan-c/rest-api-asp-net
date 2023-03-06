using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api.Data;
using web_api.Data.DataBankModels;
using Microsoft.EntityFrameworkCore;
using web_api.Dtos;

namespace web_api.Services;

public class AccountService
{
    private readonly MasterContext _context;

    public AccountService(MasterContext context)
    {
        _context = context;
    }   

    //TODO GETALL SERVICE
    public async Task<IEnumerable<AccountDtoOut>> GetAll()
    {
        return await _context.Accounts.Select(a => new AccountDtoOut
        {
            Id = a.Id,
            AccountName = a.AccountTypeNavigation.Name,
            ClientName = a.Client != null ? a.Client.Name : "No Client",
            Balance = a.Balance,
            RegDate = a.RegDate
           
        }).ToListAsync();
    }

        //TODO GETALL SERVICE
    public async Task<AccountDtoOut?> GetDtoById(int id)
    {
        return await _context.Accounts
            .Where(a => a.Id  == id)
            .Select(a => new AccountDtoOut
                {
                    Id = a.Id,
                    AccountName = a.AccountTypeNavigation.Name,
                    ClientName = a.Client != null ? a.Client.Name : "No Client",
                    Balance = a.Balance,
                    RegDate = a.RegDate
                
                }).SingleOrDefaultAsync();
    }

    //tODO GETBYID SERVICE
    public async Task<ActionResult<Account>?> GetById(int id)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account == null)
        {
            return null;
        }

        return account;

    }

    //TODO POST SERVICE
    public async Task<Account> Create(AccountDtoIn newAccountDTO)
    {
        var newAccount = new Account();

        newAccount.AccountType = newAccountDTO.AccountTypeId;
        newAccount.ClientId = newAccountDTO.ClientId;
        newAccount.Balance = newAccountDTO.Balance;

        _context.Accounts.Add(newAccount);
        await _context.SaveChangesAsync();

        return newAccount;
    }

    //TODO PUT SERVICE
    public async Task Update(int id, AccountDtoIn account)
    {
              

        var accountSearch = await  _context.Accounts.FindAsync(id);

        if (accountSearch == null)
        {
            return;
        }

        // accountSearch.AccountType = account.AccountTypeId;
        accountSearch.ClientId = account.ClientId;
        accountSearch.Balance = account.Balance;

        await _context.SaveChangesAsync();
 
    }

    //TODO DELETE SERVICE
    public async Task Delete(int id)
    {
        var account = await _context.Accounts.FindAsync(id);

        if (account == null)
        {
            return;
        }

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
    }
}
