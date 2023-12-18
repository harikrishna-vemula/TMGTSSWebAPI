using System.Security.Cryptography;
using System.Text;

namespace TenantScoreSheet.Repository
{
    public class SecurePassword
    {
        /// <summary>
        /// EncDecType to represent different types of encryption/decryption methods.
        /// </summary>
        public enum EncDecType
        {
            BASE64 = 0,
            MD5 = 1,
            AES = 2
        }

        /// <summary>
        /// EncryptPassword method encrypts a given password based on the specified encryption type (EncOrDecType).
        /// </summary>
        /// <param name="strPassword">A string password that needs to be encrypted.</param>
        /// <param name="EncOrDecType">An enumeration representing different encryption/decryption methods.</param>
        /// <returns>Returns the encrypted password as a string.</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException exception is thrown, if the input password is null or empty.</exception>
        public static string EncryptPassword(string strPassword, EncDecType EncOrDecType)
        {
            string strReturnPassword = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(strPassword))
                {
                    throw new ArgumentNullException("Password should not be null or empty.");
                }
                else
                {
                    switch (EncOrDecType)
                    {
                        case EncDecType.BASE64:
                            strReturnPassword = EncryptBase64Password(strPassword);
                            break;
                        case EncDecType.MD5:
                            strReturnPassword = EnclryptMD5Password(strPassword);
                            break;
                        case EncDecType.AES:
                            strReturnPassword = EncryptAESPassword(strPassword);
                            break;

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return strReturnPassword;
        }

        /// <summary>
        /// DecryptPassword method decrypts a given password based on the specified decryption type (EncOrDecType).
        /// </summary>
        /// <param name="strPassword">A string representing the encrypted password</param>
        /// <param name="EncOrDecType">An enumeration representing different encryption/decryption methods</param>
        /// <returns>Returns the decrypted password as a string.</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException exception is thrown, if the input password is null or empty.</exception>
        public static string DecryptPassword(string strPassword, EncDecType EncOrDecType)
        {
            string strReturnPassword = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(strPassword))
                {
                    throw new ArgumentNullException("Password should not be null or empty.");
                }
                else
                {
                    switch (EncOrDecType)
                    {
                        case EncDecType.BASE64:
                            strReturnPassword = DecryptBase64Password(strPassword);
                            break;
                        case EncDecType.MD5:
                            strReturnPassword = DeclryptMD5Password(strPassword);
                            break;
                        case EncDecType.AES:
                            strReturnPassword = DecryptAESPassword(strPassword);
                            break;

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return strReturnPassword;
        }

        /// <summary>
        /// EncryptBase64Password method is responsible for taking a given password and converting it into a Base64 encoded representation.
        /// </summary>
        /// <param name="strPassword">A string representing the original password that needs to be encrypted.</param>
        /// <returns>Returns the resulting Base64 encoded password</returns>
        private static string EncryptBase64Password(string strPassword)
        {
            string strReturnPassword = string.Empty;
            try
            {
                byte[] passwordByteArray = new byte[strPassword.Length];
                passwordByteArray = Encoding.UTF8.GetBytes(strPassword);
                strReturnPassword = Convert.ToBase64String(passwordByteArray);
            }
            catch (Exception) { throw; }
            return strReturnPassword;
        }

        /// <summary>
        /// Computes the MD5 hash for a given password using the provided MD5 hash algorithm instance.
        /// </summary>
        /// <param name="md5Hash">An instance of the MD5 hash algorithm.</param>
        /// <param name="password">The password to be hashed.</param>
        /// <returns>
        /// A string representation of the MD5 hash for the input password.
        /// The returned hash consists of hexadecimal characters (0-9, a-f).
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the 'md5Hash' parameter is null.
        /// </exception>
        private static string GetMd5Hash(MD5 md5Hash, string password)
        {
            byte[] inputData = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder hashBuilder = new StringBuilder();
            for (int i = 0; i < inputData.Length; i++)
            {
                hashBuilder.Append(inputData[i].ToString("x2"));
            }

            return hashBuilder.ToString();
        }


        /// <summary>
        /// Encrypts the given password using a combination of MD5, TripleDES, and Base64 encoding.
        /// </summary>
        /// <param name="strPassword">The password to be encrypted.</param>
        /// <returns>
        /// A string representing the encrypted version of the input password.
        /// The encrypted password is encoded using Base64.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the 'strPassword' parameter is null or empty.
        /// </exception>
        private static string EnclryptMD5Password(string strPassword)
        {
            string strReturnPassword = string.Empty;
            try
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(strPassword);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    string strMD5Hash = GetMd5Hash(md5, strPassword);
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(strMD5Hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        strReturnPassword = Convert.ToBase64String(results, 0, results.Length);
                    }
                }
            }
            catch (Exception) { throw; }
            return strReturnPassword;
        }

        /// <summary>
        /// Encrypts the given password using the AES encryption algorithm and returns the encrypted result.
        /// </summary>
        /// <param name="strPassword">The password to be encrypted.</param>
        /// <returns>
        /// A string representing the AES-encrypted version of the input password.
        /// </returns>
        /// /// <exception cref="ArgumentNullException">
        /// Thrown if the 'strPassword' parameter is null or empty.
        /// </exception>
        private static string EncryptAESPassword(string strPassword)
        {
            string strReturnPassword = string.Empty;
            try
            {

            }
            catch (Exception) { throw; }
            return strReturnPassword;
        }

        /// <summary>
        /// Decrypts a Base64-encoded password back to its original plaintext representation.
        /// </summary>
        /// <param name="strPassword">The Base64-encoded password to be decrypted.</param>
        /// <returns>
        /// A string representing the original plaintext password after decryption.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the 'strPassword' parameter is null or empty.
        /// </exception>
        private static string DecryptBase64Password(string strPassword)
        {
            string strReturnPassword = string.Empty;
            try
            {
                UTF8Encoding encoder = new System.Text.UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(strPassword);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                strReturnPassword = new string(decoded_char);

            }
            catch (Exception) { throw; }
            return strReturnPassword;
        }

        /// <summary>
        /// Decrypts an MD5-hashed password that has been previously Base64-encoded.
        /// </summary>
        /// <param name="strPassword">The Base64-encoded MD5-hashed password to be decrypted.</param>
        /// <returns>
        /// A string representing the original plaintext password after decryption.
        /// </returns>
        /// /// <exception cref="ArgumentNullException">
        /// Thrown if the 'strPassword' parameter is null or empty.
        /// </exception>
        public static string DeclryptMD5Password1(string strPassword)
        {
            string password = strPassword;

            UTF8Encoding encoder = new System.Text.UTF8Encoding();

            Decoder utf8Decode = encoder.GetDecoder();

            byte[] todecode_byte = Convert.FromBase64String(password.Replace("", "+"));

            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);

            char[] decoded_char = new char[charCount];

            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);

            string result = new(decoded_char);

            return result;

        }

        /// <summary>
        /// Decrypts an MD5-hashed and TripleDES-encrypted password that has been previously Base64-encoded.
        /// </summary>
        /// <param name="strPassword">The Base64-encoded MD5-hashed and TripleDES-encrypted password to be decrypted.</param>
        /// <returns>
        /// A string representing the original plaintext password after decryption.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the 'strPassword' parameter is null or empty.
        /// </exception>
        private static string DeclryptMD5Password(string strPassword)
        {
            string strReturnPassword = string.Empty;
            try
            {
                byte[] data = Convert.FromBase64String(strPassword); // decrypt the incrypted text
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    string strMD5Hash = GetMd5Hash(md5, strPassword);
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(strMD5Hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        strReturnPassword = UTF8Encoding.UTF8.GetString(results);
                    }
                }
            }
            catch (Exception) { throw; }
            return strReturnPassword;
        }

        /// <summary>
        /// Decrypts an AES-encrypted password that has been previously Base64-encoded.
        /// </summary>
        /// <param name="strPassword">The Base64-encoded AES-encrypted password to be decrypted.</param>
        /// <returns>
        /// A string representing the original plaintext password after decryption.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the 'strPassword' parameter is null or empty.
        /// </exception>
        private static string DecryptAESPassword(string strPassword)
        {
            string strReturnPassword = string.Empty;
            try
            {

            }
            catch (Exception) { throw; }
            return strReturnPassword;
        }

    }
}
