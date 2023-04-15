using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BLL.Abstractions.Interfaces;
using Core.Models;
using DAL.Abstractions.Interfaces;
using System.Text;
using System.Security.Cryptography;

namespace BLL.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        public UserService(IRepository<User> repository) :
            base(repository)
        {
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var allUsers = await GetAll();
            User user = allUsers.Where(x => x.Username == username && (x.PasswordHash == Hash(password))).First();
            if (user == null) {
                throw new Exception($"Failed to find the user");
            }
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var allUsers = await GetAll();
            return allUsers.Where(x => x.Username == username).ToList().First();
        }

        public async Task<List<User>> GetUsersByRole(string role)
        {
            var allUsers = await GetAll();
            return allUsers.Where(x => x.Role.ToString() == role).ToList();
        }

        public async Task UpdatePassword(Guid userId, string newPassword)
        {
            var user = await GetById(userId);
            user.PasswordHash = Hash(newPassword);
        }

        public async Task ResetPassword(Guid userId)
        {   
            var user = await GetById(userId);
            await Console.Out.WriteLineAsync("Enter the old password: ");
            if(user.PasswordHash == Hash(Console.ReadLine()))
            {
                await Console.Out.WriteAsync("Enter the new password: ");
                await UpdatePassword(userId, Console.ReadLine());
                return;
            }
            else
            {
                throw new Exception($"You entered the wrong password :(");
            }
        }

        public async Task LockUser(Guid userId)
        {
            var user = await GetById(userId);
            user.IsLocked = true;
        }

        public async Task UnlockUser(Guid userId)
        {
            var user = await GetById(userId);
            user.IsLocked = false;
        }
        public string Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}