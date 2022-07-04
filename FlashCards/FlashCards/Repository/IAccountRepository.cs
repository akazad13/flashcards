using FlashCards.Model;
using Microsoft.AspNetCore.Identity;

namespace FlashCards.Repository
{
    public interface IAccountRepository
    {
        void Add<T>(T entity) where T : class;
        void AddRange<T>(IEnumerable<T> entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<User> GetUserByName(string userName);
        Task<User> GetUserById(int id);
        Task<SignInResult> MatchPassword(User user, string password);
        Task<IList<string>> GetUserRoles(User user);
        Task<IdentityResult> AddToRoles(User user, IEnumerable<string> roles);
        Task<IdentityResult> RemoveRoles(User user, IEnumerable<string> roles);
        Task<IdentityResult> CreateUser(User user);
        Task<IdentityResult> CreateUser(User user, string password);
        Task<IdentityResult> UpdateUser(User user);
        Task<IdentityResult> DeleteUser(User user);
        Task<string> GeneratePhoneNumberToken(User user);
        Task<IdentityResult> VerifyPhoneNumberToken(User user, string token);
        Task<string> GeneratePasswordResetToken(User user);
        Task<IdentityResult> ResetPassword(User user, string token, string password);
        Task<bool> SaveAll();
    }
}