using FlashCards.DataAccess;
using FlashCards.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;

        public AccountRepository(UserManager<User> userManager, SignInManager<User> signInManager, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void AddRange<T>(IEnumerable<T> entity) where T : class
        {
            _context.AddRange(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public Task<User> GetUserByName(string userName)
        {
            return _userManager.Users.SingleOrDefaultAsync(u => u.UserName.ToLower() == userName);
        }

        public Task<User> GetUserById(int id)
        {
            return _userManager.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public Task<SignInResult> MatchPassword(User user, string password)
        {
            return _signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public Task<IList<string>> GetUserRoles(User user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public Task<IdentityResult> AddToRoles(User user, IEnumerable<string> roles)
        {
            return _userManager.AddToRolesAsync(user, roles);
        }

        public Task<IdentityResult> RemoveRoles(User user, IEnumerable<string> roles)
        {
            return _userManager.RemoveFromRolesAsync(user, roles);
        }

        public Task<IdentityResult> CreateUser(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public Task<IdentityResult> CreateUser(User user)
        {
            return _userManager.CreateAsync(user);
        }
        public Task<IdentityResult> UpdateUser(User user)
        {
            return _userManager.UpdateAsync(user);
        }
        public Task<IdentityResult> DeleteUser(User user)
        {
            return _userManager.DeleteAsync(user);
        }


        public Task<string> GeneratePhoneNumberToken(User user)
        {
            return _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
        }

        public Task<IdentityResult> VerifyPhoneNumberToken(User user, string token)
        {
            return _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, token);
        }

        public Task<string> GeneratePasswordResetToken(User user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public Task<IdentityResult> ResetPassword(User user, string token, string password)
        {
            return _userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
