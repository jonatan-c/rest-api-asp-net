namespace web_api.Data.DataBankModels
{
    public class Administrator
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AdminType { get; set; } = null!;
        public DateTime RegDate { get; set; }
        public string Pwd { get; set; } = null!;

    }
}