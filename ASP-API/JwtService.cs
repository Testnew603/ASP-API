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

        public string GenerateToken(StudentDetails studentDetails, string type)
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
                new Claim("type", type),
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
