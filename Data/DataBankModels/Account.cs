using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace web_api.Data.DataBankModels;

public partial class Account
{
    public int Id { get; set; }

    public int AccountType { get; set; }

    public int? ClientId { get; set; }

    public decimal Balance { get; set; }

    public DateTime RegDate { get; set; }

    [JsonIgnore]
    public virtual AccountType AccountTypeNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<BankTransaction> BankTransactions { get; } = new List<BankTransaction>();

    [JsonIgnore]
    public virtual Client? Client { get; set; }
}
