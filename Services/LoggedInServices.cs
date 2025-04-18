using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MatchPointBackend.Context;
using MatchPointBackend.Models;
using MatchPointBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MatchPointBackend.Services
{
    public class LoggedInServices
    {
        private readonly DataContext _dataContext;
        // private readonly IConfiguration _config;

        public LoggedInServices(DataContext dataContext, IConfiguration config)
        {
            _dataContext = dataContext;
            // _config = config;
        }

        public async Task<bool> EditUsernameAsync(UserUsernameChangeDTO user)
        {
            UserModel foundUser = await GetUserByUsername(user.Username);
            UserModel checkUsername = await GetUserByUsername(user.NewUsername);

            if (foundUser == null) return false;
            if (checkUsername != null) return false;

            foundUser.Username = user.NewUsername;

            _dataContext.Users.Update(foundUser);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        public async Task<bool> EditPasswordAsync(UserLoginDTO user)
        {
            UserModel foundUser = await GetUserByUsername(user.Username);

            if (foundUser == null) return false;

            PasswordDTO hashPassword = HashPassword(user.Password);
            foundUser.Hash = hashPassword.Hash;
            foundUser.Salt = hashPassword.Salt;

            _dataContext.Users.Update(foundUser);
            return await _dataContext.SaveChangesAsync() != 0;
        }

        private static PasswordDTO HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(64);

            string salt = Convert.ToBase64String(saltBytes);

            string hash;

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 310000, HashAlgorithmName.SHA256))
            {
                hash = Convert.ToBase64String(deriveBytes.GetBytes(32));
            }

            PasswordDTO hashedPassword = new();
            hashedPassword.Salt = salt;
            hashedPassword.Hash = hash;

            return hashedPassword;
        }
        private async Task<UserModel> GetUserByUsername(string userName) => await _dataContext.Users.SingleOrDefaultAsync(user => user.Username == userName);

        public async Task<bool> DeleteProfile(string user)
        {
            UserModel userToDelete = await GetUserByUsername(user);
            if (userToDelete == null) return false;

            userToDelete.IsDeleted = true;
            _dataContext.Users.Update(userToDelete);

            return await _dataContext.SaveChangesAsync() != 0;

        }

        private async Task<CourtModel> GetLocations(string courtName) => _dataContext.Locations.SingleOrDefault(location => location.CourtName == courtName);
        public async Task<bool> AddLocation(CourtModel location)
        {
            CourtModel addedLocations = await GetLocations(location.CourtName);
            CourtModel newLocations = await GetLocations(location.NewLocations.LocationName);
            if (addedLocations == null) return false;
            if (newLocations == addedLocations) return false;

            addedLocations.CourtName = newLocations.NewLocations.LocationName;

            _dataContext.Locations.Update(addedLocations);
            return await _dataContext.SaveChangesAsync() != 0;
        }

    }
}