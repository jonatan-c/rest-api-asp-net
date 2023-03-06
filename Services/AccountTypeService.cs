using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.Data.DataBankModels;
using web_api.Services;

namespace web_api.Services;

public class AccountTypeService
{
    private readonly MasterContext _context;

    public AccountTypeService(MasterContext context)
    {
        _context = context;
    }

    public async Task<AccountType?> GetById(int id)
    {
        return await _context.AccountTypes.FindAsync(id);
    }
}