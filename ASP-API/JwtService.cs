using ASP_API.Model.Public;
using ASP_API.Model.Staff;
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

        // student token generation
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

        // admin token generation
        public string GenerateTokenAdmin(AdminCredentials credentials)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));
            var signingkey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {                                               
                new Claim("email", credentials.Email),                
                new Claim("role", credentials.Role),
                new Claim("status", credentials.Status),
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

        // general manager token generation
        public string GenerateToken(GeneralManager generalManager, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));
            var signingkey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", generalManager.Id.ToString()),
                new Claim("firstName", generalManager.FirstName),
                new Claim("lastName", generalManager.LastName),
                new Claim("dob", generalManager.BirthDate.ToString()),
                new Claim("email", generalManager.Email),
                new Claim("status", generalManager.Status.ToString()),
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

        // HR manager token generation
        public string GenerateToken(HRManager hrManager, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));
            var signingkey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", hrManager.Id.ToString()),
                new Claim("firstName", hrManager.FirstName),
                new Claim("lastName", hrManager.LastName),
                new Claim("dob", hrManager.BirthDate.ToString()),
                new Claim("email", hrManager.Email),
                new Claim("status", hrManager.Status.ToString()),
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

        // advisor token generation
        public string GenerateToken(Advisor advisor, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));
            var signingkey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", advisor.Id.ToString()),
                new Claim("firstName", advisor.FirstName),
                new Claim("lastName", advisor.LastName),
                new Claim("dob", advisor.BirthDate.ToString()),
                new Claim("email", advisor.Email),
                new Claim("status", advisor.Status.ToString()),
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

        // reviewer token generation
        public string GenerateToken(Reviewer reviewer, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));
            var signingkey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", reviewer.Id.ToString()),
                new Claim("firstName", reviewer.FirstName),
                new Claim("lastName", reviewer.LastName),                
                new Claim("email", reviewer.Email),
                new Claim("domainId", reviewer.DomainId.ToString()),
                new Claim("status", reviewer.Status.ToString()),
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

        // trainer token generation
        public string GenerateToken(Trainer trainer, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));
            var signingkey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", trainer.Id.ToString()),
                new Claim("firstName", trainer.FirstName),
                new Claim("lastName", trainer.LastName),
                new Claim("email", trainer.Email),
                new Claim("specializedIn", trainer.SpecializedIn.ToString()),
                new Claim("status", trainer.Status.ToString()),
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
