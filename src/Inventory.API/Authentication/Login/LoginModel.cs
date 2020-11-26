namespace Inventory.API.Authentication.Login
{
    /// <summary>
    /// Model to login the user into the application
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// User login name
        /// </summary>
        public string? Username { get; set; }
        /// <summary>
        /// User password
        /// </summary>
        public string? Password { get; set; }
    }
}