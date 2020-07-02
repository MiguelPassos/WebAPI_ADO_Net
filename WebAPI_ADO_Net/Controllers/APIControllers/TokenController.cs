using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI_ADO_Net.Models;
using WebAPI_ADO_Net.Repositories;
using WebAPI_ADO_Net.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_ADO_Net.Controllers.APIControllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration Configuration { get; }

        private IApplicationService ApplicationService { get; }

        public TokenController(IConfiguration configuration, IApplicationService applicationService)
        {
            Configuration = configuration;
            ApplicationService = applicationService;
        }

        // POST api/token
        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostRequestToken([FromBody]ApplicationData app)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("É necessário informar os dados da aplicação.");

                ApplicationData application = ApplicationService.GetApplicationAsync(app.Name, app.Password).Result;

                if (application == null)
                    return NotFound("Aplicação não cadastrada.");

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, application.Name)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var token = new JwtSecurityToken(
                    issuer: Configuration["Issuer"], 
                    audience: Configuration["Audience"], 
                    claims: claims, 
                    expires: DateTime.Now.AddHours(Convert.ToInt16(Configuration["TokenDuration"])), 
                    signingCredentials: creds);

                return Ok(
                    new { 
                        token = new JwtSecurityTokenHandler().WriteToken(token), 
                        expirationDate = token.ValidTo 
                    });
            }
            catch (Exception ex)
            {
                //Dispara uma response com StatusCode 500 de Internal Server Error
                //informando o erro ocorrido.
                return StatusCode(500, ex.Message);
            }
        }
    }
}
