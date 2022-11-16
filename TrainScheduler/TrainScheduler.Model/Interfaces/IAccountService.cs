using System.Threading.Tasks;
using TrainScheduler.Model.Models;
using TrainScheduler.Model.ViewModels;

namespace TrainScheduler.Model.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResult> RegisterAsync(RegisterModel registerModel);

        Task<AuthResult> SignInAsync(LoginModel loginModel);

        Task SignOut();
    }
}
