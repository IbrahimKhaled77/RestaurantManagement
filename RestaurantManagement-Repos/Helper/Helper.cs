

using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.Model.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurants_Repository.Helper
{
    public static class  Helper
    {



        public static string TokenCustomer(Customer customer) {


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("LongSecrectStringForModulekodestartppopopopsdfjnshbvhueFGDKJSFBYJDSAGVYKDSGKFUYDGASYGFskc vhHJVCBYHVSKDGHASVBCL");
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserId",customer.CustomerId.ToString()),
                        new Claim("Name",customer.Name)
                }),
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptior);
            string finalToken = tokenHandler.WriteToken(token);
            customer.IsLoggedIn = true;
            customer.AccessKey = finalToken;
            customer.AccesskeyExpireDate = tokenDescriptior.Expires;


            return finalToken;



        }

        public static string TokenEmployee(Employee employee)
        {


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("LongSecrectStringForModulekodestartppopopopsdfjnshbvhueFGDKJSFBYJDSAGVYKDSGKFUYDGASYGFskc vhHJVCBYHVSKDGHASVBCL");
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("UserId",employee.EmployeeId.ToString()),
                        new Claim("Name",employee.Name)
                }),
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptior);
            string finalToken = tokenHandler.WriteToken(token);
            employee.IsLoggedIn = true;
            employee.AccessKey = finalToken;
            employee.AccesskeyExpireDate = tokenDescriptior.Expires;


            return finalToken;



        }



    }
}
