using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_ADO_Net.Models;
using WebAPI_ADO_Net.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_ADO_Net.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private IUserService UserService { get; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                IEnumerable<User> foundUsers = await UserService.GetUsersAsync();

                return Ok(foundUsers);
            }
            catch (Exception ex)
            {
                //Dispara uma response com StatusCode 500 de Internal Server Error
                //informando o erro ocorrido.
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                User user = await UserService.GetUserAsync(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                //Dispara uma response com StatusCode 500 de Internal Server Error
                //informando o erro ocorrido.
                return StatusCode(500, ex.Message);
            }
        }
        
        // POST api/users
        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody]User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("É necessário informar os dados do usuário.");

                bool result = await UserService.InsertUserAsync(user);
                
                if (result == false)
                    return BadRequest("Já existe um usuário com o nome e e-mail informado.");

                //Retorna o status code de registro criado.
                return StatusCode(202);
            }
            catch (Exception ex)
            {
                //Dispara uma response com StatusCode 500 de Internal Server Error
                //informando o erro ocorrido.
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]User user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("É necessário informar o usuário e a nova senha.");

                user = await UserService.UpdateUserAsync(id, user);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                //Dispara uma response com StatusCode 500 de Internal Server Error
                //informando o erro ocorrido.
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("É necessário informar um usuário válido.");

                await UserService.DeleteUserAsync(id);

                return Ok();
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
