namespace excemathApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUserRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string Nickname { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int RightAnswers { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int WrongAnswers { get; private set; }

        public static explicit operator GetUserRequest(User user)
        {
            return new GetUserRequest
            {
                Nickname = user.Nickname,
                RightAnswers = user.RightAnswers,
                WrongAnswers = user.WrongAnswers
            };
        }
    }
}
