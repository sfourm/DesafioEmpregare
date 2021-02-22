using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Empregare.Data;
using Empregare.ViewModel;
using Empregare.Models;

namespace Empregare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Api : ControllerBase
    {
        private readonly Context _context;

        public Api(Context context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public JsonResult Login([FromBody] Login model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var result = from user in _context.UserRegistration
                    //where user.Username == model.UserName && user.Password == model.Password
                    //select user;

                    if (_context.Usuarios.Any(user => user.Email == model.Email && user.Senha == model.Senha))
                    {
                        return new JsonResult("Login Successfully");
                    }
                    // return Ok(new Response { Status = "Success", Message = "Login Successfully" });
                    //return new JsonResult("Login Successfully");
                }
                //catches any server error
                catch (Exception ex)
                {
                    var message = ex.Message;
                    //return message;
                }

            }
            //return Unauthorized();
            return new JsonResult("Login Failed");
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }
    }
}
