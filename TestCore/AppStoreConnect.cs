using System.Security.Cryptography;
using Jose;

namespace TestCore;

public class AppStoreConnect
{
        /// <summary>
        /// Debug.
        /// </summary>
        public static void Debug(string msg)
        {
            Console.WriteLine($"{msg}");
        }
        
        /// <summary>
        /// Main entry point./
        /// </summary>
        public static void MainTest(string[] args)
        {
            try
            {
                if (args.Length < 3)
                {
                    throw new Exception("Expected Arguments are: [Key ID], [Issuer ID], [Private Key File (*.p8)]");
                }

                if (!File.Exists(args[2])) throw new Exception($"The private key file must exist: {args[2]}");
                string key = File.ReadAllText(args[2]).Replace("-----", "").Replace("BEGIN PRIVATE KEY", "").Replace("END PRIVATE KEY", "").Replace("\r", "").Replace("\n", "");
                string token = GenerateSignedJWTToken(args[0], args[1], key);
                Debug(token);
            }
            catch (Exception ex)
            {
                Debug($"ERROR: {ex}");
                throw;
            }
        }
        
        
    
        /// <summary>
        /// Generate JWT.
        /// </summary>
        public static string GenerateSignedJWTToken(string keyID, string issuerID, string privateKey)
        {
            //Debug($"GenerateSignedJWTToken:-- START, keyID: {keyID}, issuerID: {issuerID}, privateKey: {privateKey}");
            var header = new Dictionary<string, object>()
            {
                { "alg", "ES256" },
                { "kid", $"{keyID}" },
                { "typ", "JWT" }
            };
    
            //var scope = new string[1] { "GET /v1/apps?filter[platform]=IOS" };
            var payload = new Dictionary<string, object>
            {
                { "iss", $"{issuerID}" },
                { "iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds() },
                { "exp", DateTimeOffset.UtcNow.AddMinutes(15).ToUnixTimeSeconds() },
                { "aud", "appstoreconnect-v1" },
                //{ "scope", scope }
            };
    
            CngKey key = CngKey.Import(Convert.FromBase64String(privateKey), CngKeyBlobFormat.Pkcs8PrivateBlob);
            string token = JWT.Encode(payload, key, JwsAlgorithm.ES256, header);
            return token;
        }
    
}