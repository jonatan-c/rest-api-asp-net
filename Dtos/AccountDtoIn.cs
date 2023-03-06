

namespace web_api.Dtos;

public class AccountDtoIn
{
    public int Id { get; set; }
    public int AccountTypeId { get; set; }
    public int? ClientId { get; set; }
    public decimal Balance { get; set; }
}