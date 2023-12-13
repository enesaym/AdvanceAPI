using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdvanceApi.Extensions
{
	public static class JwtTokenExtension
	{
		public static string GenerateJwtToken(this IConfiguration configuration, string username)
		{
			var issuer = configuration["JwtIssuer"];
			var audience = configuration["JwtAudience"];


			var desc = new SecurityTokenDescriptor()
			{
				Expires = DateTime.Now.AddMinutes(20),
				Subject = new ClaimsIdentity(new Claim[]
				{
					//buraya claimler eklenecek roller eklenebilir
				}),
				Issuer = issuer,
				Audience = audience,
				NotBefore = DateTime.Now,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["apisecretkey"])), SecurityAlgorithms.HmacSha256)
			};

			var tokenhandler = new JwtSecurityTokenHandler();
			var token = tokenhandler.CreateToken(desc);
			var tokenForUser = tokenhandler.WriteToken(token);

			return tokenForUser;
		}
	}
}
