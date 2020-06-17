using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Contracts.Services.Security;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Core;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Models.Security;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.Service.Security
{
    /// <summary>
    /// Serviço de autenticação de usuário que produz um JWT como resultado da autenticação
    /// </summary>
    public class JwtAuthenticationService : BaseAuthenticationService, IJwtAuthenticationService
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="userService">Serviço de usuário</param>
        public JwtAuthenticationService(IUserService userService) : base(userService)
        {
        }

        public new async Task<JwtAuthenticationResult> AuthenticateAsync(string username, string password)
        {
            User user = await base.AuthenticateAsync(username, password);

            SecurityToken jwt = GenerateJwt(user);

            return new JwtAuthenticationResult
            {
                Expires = jwt.ValidTo,
                Name = user.Name,
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                UserId = user.Id,
                Username = user.Username
            };
        }

        /// <summary>
        /// Gera um JWT para um determinado usuário
        /// </summary>
        /// <param name="user">Usuário do token</param>
        /// <returns>JWT</returns>
        private SecurityToken GenerateJwt(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("obterUmaChaveDaConfiguracao"); // pegar string das configurações
            DateTime expirationDate = DateTime.UtcNow.AddDays(1);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.Id),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Expiration, expirationDate.ToString())
                }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}
