

namespace TestCore
{
    /// <summary>
    /// Main class.
    /// </summary>
    public class SignToken
    {

        /// <summary>
        /// Debug.
        /// </summary>
        public static void Debug(string msg)
        {
            Console.WriteLine($"{DateTimeOffset.UtcNow} - {msg}");
        }
        
        /// <summary>
        /// Main entry point./
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                AppStoreConnect.MainTest(args);
            }
            catch (Exception ex)
            {
                Debug($"ERROR: {ex}");
                throw;
            }
        }

    }
}