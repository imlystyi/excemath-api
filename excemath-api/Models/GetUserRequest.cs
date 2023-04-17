namespace excemathApi.Models
{
   /// <summary>
   /// Представляє модель користувача запиту отримання, описану звичайною моделлю <see cref="User"/>, яка має унікальний псевдонім, кількість правильних та неправильних відповідей.
   /// </summary>
   public class GetUserRequest
   {
      #region Властивості

      /// <inheritdoc cref="User.Nickname"/>
      public string Nickname { get; init; }

      /// <inheritdoc cref="User.RightAnswers"/>
      public int RightAnswers { get; init; }

      /// <inheritdoc cref="User.WrongAnswers"/>
      public int WrongAnswers { get; init; }
    //  public double Rating { get; init; }

      #endregion

      #region Перевизначені оператори

      // Дозволяє звести тип звичайної моделі користувача до моделі користувача запиту отримання.
      public static explicit operator GetUserRequest(User user) => new()
      {
         Nickname = user.Nickname,
         RightAnswers = user.RightAnswers,
         WrongAnswers = user.WrongAnswers,
        // Rating = user.Rating
      };

      #endregion
   }
}
