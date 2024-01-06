using ASP_API.Model.Public;
using ASP_API.Model.Student;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASP_API
{
    public class JwtService
    {
        private readonly string key = string.Empty;
        private readonly int duration;

        public JwtService(IConfiguration configuration)
        {
            key = configuration["Jwt:Key"]!;
            duration = int.Parse(configuration["Jwt:Duration"]!);
        }

        public string GenerateToken(StudentDetails studentDetails, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));
            var signingkey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", studentDetails.Id.ToString()),
                new Claim("firstName", studentDetails.FirstName),
                new Claim("lastName", studentDetails.LastName),
                new Claim("dob", studentDetails.BirthDate.ToString()),
                new Claim("email", studentDetails.Email),
                new Claim("status", studentDetails.Status.ToString()), 
                new Claim("role", role),
            };

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.Now.AddMinutes(this.duration),
                signingkey);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public string GenerateTokenAdmin(AdminCredentials adminCredentials, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));
            var signingkey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {                                               
                new Claim("email", adminCredentials.Email),                
                new Claim("role", role),
            };

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.Now.AddMinutes(this.duration),
                signingkey);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
