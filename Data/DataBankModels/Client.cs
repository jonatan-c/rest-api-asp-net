using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace web_api.Data.DataBankModels;

public partial class Client
{
    public int Id { get; set; }

    [MaxLength(30 , ErrorMessage = "El name debe ser menor de 30 caracteres.")]
    public string Name { get; set; } = null!;

    [MaxLength(30 , ErrorMessage = "El numero de telefono debe ser menor de 30 caracteres.")]
    public string PhoneNumber { get; set; } = null!;

    [EmailAddress(ErrorMessage = "El email no es valido.")]
    public string? Email { get; set; }

    public DateTime RegDate { get; set; }

    // [JsonIgnore]
    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
