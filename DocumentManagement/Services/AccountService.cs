using DocumentManagement.BUS;
using DocumentManagement.Models.Entity.Account;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Common;
using DocumentManagement.Models.Entity.User;

namespace DocumentManagement.Services
{
    public class AccountService
    {
        public Token CreateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("soHoav1soHoav1soHoav1soHoav1"));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.AddDays(1)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return new Token {
                JwtToken = tokenString,
                Expiration = DateTime.UtcNow.AddDays(1)
            };
        }
        public bool IsAuthenticate(Account account)
        {
            account.UserName = Utilities.KillSqlInjection(account.UserName);
            account.Password = Utilities.KillSqlInjection(account.Password);
            AccountBUS accountBusiness = new AccountBUS();
            var result = accountBusiness.GetUserByUserName(account);
            if (result.Item == null)
            {
                return false;
            }
            AuthenticationHelper _authenticationHelper = new AuthenticationHelper();
            var userDTO = accountBusiness.GetUserToCheck(result.Item.Id);
            string passwordSalt = userDTO.PasswordSalt;
            string passwordInput = _authenticationHelper.GetMd5Hash(passwordSalt + account.Password);
            string passwordUser = userDTO.Password;

            if (passwordInput.Equals(passwordUser))
                return true;
            else
                return false;
        }

        public string CreateHashedPassword(string password)
        {
            var salt = GenerateRandomSalt();
            string hashedPassword = Hash(password, salt);
            // Concat the password with the salt
            var finalPassword = hashedPassword + "$" + Convert.ToBase64String(salt);
            return finalPassword;
        }

        private byte[] SplitSaltFromPasswordCol(string hashedPassword)
        {
            var saltString = hashedPassword.Split("$")[1];
            byte[] salt = Convert.FromBase64String(saltString);
            return salt;
        }

        private string Hash(string password, byte[] salt)
        {
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashedPassword;
        }

        public byte[] GenerateRandomSalt()
        {
            var validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringLength = 12;
            StringBuilder stringBuilder = new StringBuilder(stringLength);
            for (int i = 0; i < stringLength; i++)
            {
                var randomCharacter = validCharacters[new Random().Next(0, validCharacters.Length)];
                stringBuilder.Append(randomCharacter.ToString());
            }
            return Convert.FromBase64String(stringBuilder.ToString());
        }
    }
}
