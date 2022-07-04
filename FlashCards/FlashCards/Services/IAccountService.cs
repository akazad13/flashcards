using FlashCards.DTOs;

namespace FlashCards.Services
{
    public interface IAccountService
    {
        Task<AuthResponse> Login(AuthRequest request);
    }
}