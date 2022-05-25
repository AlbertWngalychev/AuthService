using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
    public class Repository : IRepository, IDisposable
    {
        private readonly AuthServiceContext _context;

        public Repository(AuthServiceContext context)
        {
            _context = context;
        }

        public async Task<Auth?> GetUser(Guid guid)
        {
            return await _context.Auths.FindAsync(new object[] { guid });
        }

        public async Task<Auth?> GetUser(string username)
        {
            return await _context.Auths.Where(a => a.Username == username).FirstOrDefaultAsync();
        }
        public async Task ChangePassword(string username, string newPass)
        {
            Auth? temp = await _context.Auths.Where(x =>
                    x.Username == username &&
                    x.Active == true).FirstOrDefaultAsync();

            if (temp==null)
            {
                throw new ArgumentNullException(nameof(username));
            }

            ChangePassword(temp,newPass);
        }
        public async Task ChangePassword(Guid guid, string newPass)
        {
            Auth? temp = await _context.Auths.Where(x =>
                x.Guid == guid &&
                x.Active == true).FirstOrDefaultAsync();

            if (temp == null)
            {
                throw new ArgumentNullException(nameof(guid));
            }

            ChangePassword(temp, newPass);
        }
        private void ChangePassword(Auth a, string newPass)
        {
            byte[] s = PasswordHash.GenerateSalt();
            byte[] p = newPass.CreatePass(s);
            a.Salt = s;
            a.HashedPass = p;
        }
        public async Task<Auth> CreateUser(string username, string pass)
        {
            byte[] s = PasswordHash.GenerateSalt();

            if (await GetUser(username)!=null)
            {
                throw new ArgumentException(nameof(username));
            }

            var temp = await _context.Auths.AddAsync(new()
            {
                Guid = Guid.NewGuid(),
                Username = username,
                Salt = s,
                HashedPass = pass.CreatePass(s),
                Active = true
            });

            return temp.Entity;
        }

        public async Task DeleteUser(string username)
        {
            Auth? temp = await GetUser(username);
            if (temp==null)
            {
                throw new ArgumentNullException(nameof(username));
            }
            temp.Active = false;
        }

        public async Task DeleteUser(Guid guid)
        {
            Auth? temp = await GetUser(guid);
            if (temp == null)
            {
                throw new ArgumentNullException(nameof(guid));
            }
            temp.Active = false;
        }


        public Task<int> SaveChangedAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }
    }
}
