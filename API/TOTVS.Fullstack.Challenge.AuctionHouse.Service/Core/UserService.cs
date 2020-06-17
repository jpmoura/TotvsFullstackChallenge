using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Persistence.Repositories.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Helper;
using TOTVS.Fullstack.Challenge.AuctionHouse.Service.Validations;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Core
{
    /// <summary>
    /// Classe de serviços de usuário
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Repositório de usuário
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Validor de paginação
        /// </summary>
        private readonly IValidator<Pagination> paginationValidator;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="userRepository">Repositório de usuário</param>
        /// <param name="paginationValidator">Validador de paginação</param>
        public UserService(IUserRepository userRepository, IValidator<Pagination> paginationValidator)
        {
            this.userRepository = userRepository;
            this.paginationValidator = paginationValidator;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            CommonValidator.EnforceNotEmpty(id, nameof(id));
            return await userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            CommonValidator.EnforceNotEmpty(username, nameof(username));
            return await userRepository.GetByUsernameAsync(username);
        }

        public IQueryable<User> List(Pagination pagination)
        {
            paginationValidator.ValidateAndThrow(pagination);
            return userRepository.List(pagination);
        }
    }
}
