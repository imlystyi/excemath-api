#nullable disable

namespace excemathApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AddUserRequest
    {
        /// <inheritdoc cref="User.Nickname"/>
        public string Nickname { get; set; }

        /// <inheritdoc cref="User.Password"/>
        public string Password { get; set; }
    }
}
