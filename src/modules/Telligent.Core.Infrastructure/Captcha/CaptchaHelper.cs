namespace Telligent.Core.Infrastructure.Captcha
{
    public static class CaptchaHelper
    {
        private const string Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private static readonly Random Random = new();

        /// <summary>
        /// get random string by length
        /// </summary>
        /// <param name="length">length of string</param>
        /// <returns>random string</returns>
        public static string RandomString(int length)
        {
            return new string(Enumerable.Repeat(Chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
