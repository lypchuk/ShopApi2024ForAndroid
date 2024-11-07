namespace ShopApi2024.DTOs.Account
{
    //acoount regostration dto
    public class AccountRegistrationDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public IFormFile? Image { get; set; }
        public bool IsAdmin { get; set; } = false;

    }
}

