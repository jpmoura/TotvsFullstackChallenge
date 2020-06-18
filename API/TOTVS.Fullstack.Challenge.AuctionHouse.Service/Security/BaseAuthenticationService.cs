using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validations;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Security
{
    /// <summary>
    /// Serviço base de autenticação
    /// </summary>
    public class BaseAuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Serviço de usuário
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="userService">Serviço de usuário</param>
        public BaseAuthenticationService(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            CommonValidator.EnforceNotEmpty(username, nameof(username));
            CommonValidator.EnforceNotEmpty(password, nameof(password));

            User user = await userService.GetByUsernameAsync(username);

            if (user == null)
            {
                throw new ResourceNotFoundException(typeof(User), nameof(username));
            }

            if (!user.IsActive)
            {
                throw new DisabledUserException(user);
            }

            if (password != user.Password)
            {
                throw new InvalidParameterException(password);
            }

            return user;
        }
    }
}
