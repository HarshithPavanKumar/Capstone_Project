using CapstoneProject_1.Data;
using CapstoneProject_1.DTO;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_1
{
    public class userjwt
    {
        public readonly AppDbContext cs;
        public readonly IConfiguration ic;
        public userjwt(AppDbContext cs, IConfiguration ic)
        {
            this.cs = cs;
            this.ic = ic;
        }
        public async Task<string> Authenticate(LoginRequestdto req)
        {
            if (string.IsNullOrWhiteSpace(req.email) || string.IsNullOrWhiteSpace(req.password))
                return null;

            var userinfo = await cs.Athletes.FirstOrDefaultAsync(s => s.Email == req.email);
            if (userinfo == null || userinfo.password != req.password) // Replace with hashed comparison
                return null;


            // Create claims from user info
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userinfo.Email),
            new Claim("UserId", userinfo.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ic["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: ic["Jwt:Issuer"],
                audience: ic["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: creds);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

    }
}
