using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.Data.DataBankModels;
using web_api.Dtos;

namespace web_api.Services;


public class LoginService
{
    private readonly MasterContext _context;

    public LoginService(MasterContext context)
    {
        _context = context;
    }

    public async Task<Administrator?> GetAdmin(AdminDto admin)
    {
        return await _context.Administrators
            .SingleOrDefaultAsync(x => x.Email == admin.Email && x.Pwd == admin.Pwd);
    }

}