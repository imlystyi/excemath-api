using Microsoft.AspNetCore.Routing.Constraints;

namespace excemathApi.Models
{
   public class RatingOfUser
   {
      public string Nickname { get; set; }

      /// <inheritdoc cref="User.RightAnswers"/>
      public int RightAnswers { get; init; }

      /// <inheritdoc cref="User.WrongAnswers"/>
      public int WrongAnswers { get; init; }
      //public double Rating { get; init; }

   }
}
