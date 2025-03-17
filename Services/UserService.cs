using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MatchPointBackend.Context;
using MatchPointBackend.Models;
using Microsoft.IdentityModel.Tokens;

namespace MatchPointBackend.Services
{
    public class UserService
    {
        private readonly DataContext _dataContext;

        public UserService(DataContext dataContext){
            _dataContext = dataContext;
        }

        public bool CreateUser(UserDTO newUser){
            bool result = false;

            if(!DoesUserExist(newUser.Email)){
                UserModel userToAdd = new();
                userToAdd.Email = newUser.Email;

                PasswordDTO hashPassword = HashPassword(newUser.Password);
                userToAdd.Hash = hashPassword.Hash;
                userToAdd.Salt = hashPassword.Salt;

                _dataContext.Users.Add(userToAdd);
                result = _dataContext.SaveChanges() != 0;
            }

            return result;

        }

        private bool DoesUserExist(string email){
            return _dataContext.Users.SingleOrDefault(users => users.Email == email ) != null;
        }

        private static PasswordDTO HashPassword(string password){
            byte[] saltBytes = RandomNumberGenerator.GetBytes(64);

            string salt = Convert.ToBase64String(saltBytes);

            string hash;

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 310000, HashAlgorithmName.SHA256)){
                hash = Convert.ToBase64String(deriveBytes.GetBytes(32));
            }

            PasswordDTO hashedPassword = new();
            hashedPassword.Salt = salt;
            hashedPassword.Hash = hash;

            return hashedPassword;
        }

        public string Login(UserDTO user){
            string result = null;

            UserModel foundUser = GetUserByEmail(user.Email);

            if(foundUser == null){
                return result;
            }

            if(VerifyPassword(user.Password, foundUser.Salt, foundUser.Hash)){
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("teamMatchPointSecretRacket"));

                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOtions = new JwtSecurityToken(
                    issuer: "http://localhost:5000",
                    audience: "http://localhost:5000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signingCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOtions);

                result  = tokenString;            
            }

            return result;
        }

        private UserModel GetUserByEmail(string email){
            return _dataContext.Users.SingleOrDefault(users => users.Email == email );
        }

        private static bool VerifyPassword(string password, string salt, string hash){
            byte[] saltBytes = Convert.FromBase64String(salt);

            string newHash;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 310000, HashAlgorithmName.SHA256)){
                newHash = Convert.ToBase64String(deriveBytes.GetBytes(32));
            }

            return hash == newHash;
        }

    }
}