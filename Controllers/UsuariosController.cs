using Microsoft.AspNetCore.Mvc;
using Empregare.Models;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using System.Net;
using System;
using Empregare.ViewModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Empregare.Data;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Empregare.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Context _context;

        public UsuariosController(Context context)
        {
            _context = context;
        }

        /// [GET] Tela de cadastro
        /// </summary>
        [HttpGet]
        [Route("candidato/cadastro")]
        public IActionResult Cadastro()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"))) return RedirectToAction(nameof(Perfil));

            return View();
        }


        /// [GET] Tela de aviso de envio de email 
        [HttpGet]
        public IActionResult ConfirmarEmail()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"))) return RedirectToAction(nameof(Perfil));
            if (TempData["RegistrarUsuario"] == null) return RedirectToAction(nameof(Login));

            ViewBag.Msg = TempData["Erro"];
            ViewBag.Email = TempData["EmailUser"];
            TempData["RegistrarUsuario"] = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Usuario>(TempData["RegistrarUsuario"].ToString()));
            return View();
        }


        /// [GET] Tela de finalizar cadastro
        [HttpGet]
        public IActionResult FinalizarCadastro(string id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"))) return RedirectToAction(nameof(Perfil));

            if (TempData["token"] != null && id.Equals(TempData["token"].ToString()))
            {
                Usuario registrarUsuario = JsonConvert.DeserializeObject<Usuario>(TempData["RegistrarUsuario"].ToString());
                if (registrarUsuario == null) return RedirectToAction(nameof(Cadastro));

                return View(registrarUsuario);
            }
            return RedirectToAction(nameof(Login));
        }


        /// <summary>
        /// [GET] Tela de login de usuário
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"))) return RedirectToAction(nameof(Perfil));
            return View();
        }


        /// [GET] Tela de dados do usuário logado
        [HttpGet]
        public IActionResult Perfil()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"))) return RedirectToAction(nameof(Login));
            int id = Int32.Parse(HttpContext.Session.GetString("UsuarioId"));
            Usuario usuario = _context.Usuarios.Find(id);
            return View(usuario);
        }


        /// [GET] Editar usuário
        [HttpGet]
        public async Task<IActionResult> EditarPerfil(int? id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"))) return RedirectToAction(nameof(Login));
            if (id == null)
            {
                return NotFound();
            }

            //Se tentar usar id de outro usuario redireciona pro usuario logado
            if (id.ToString() != HttpContext.Session.GetString("UsuarioId").ToString())
            {
                int? idFind = Int32.Parse(HttpContext.Session.GetString("UsuarioId").ToString());
                return View(idFind);
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }


        /// [GET] Edição de senha de acesso
        [HttpGet]
        public async Task<IActionResult> EditarSenha(int? id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"))) return RedirectToAction(nameof(Login));
            if (id == null)
            {
                return NotFound();
            }

            //Se tentar usar id de outro usuario redireciona pro usuario logado
            if (id.ToString() != HttpContext.Session.GetString("UsuarioId").ToString())
            {
                int? idFind = Int32.Parse(HttpContext.Session.GetString("UsuarioId").ToString());
                return View(idFind);
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            EditarSenha senhaEditarSenha = new EditarSenha();
            senhaEditarSenha.UsuarioId = usuario.UsuarioId; 
            return View(senhaEditarSenha);
        }


        //[GET] Recuperar senha
        [HttpGet]
        public IActionResult RecuperarSenha()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId"))) return RedirectToAction(nameof(Perfil));
            return View();
        }


        //[POST] Recuperar senha
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecuperarSenha(RecuperarSenha forgotPasswordModel)
        {
            if (!EmailUsuarioExiste(forgotPasswordModel.Email)) ModelState.AddModelError("Email", "O e-mail não existe");
            ViewBag.MsgSuccess = null;
            if (ModelState.IsValid)
            {
                ViewBag.MsgSuccess = "Foi enviado um e-mail para você";
                SendEmail(forgotPasswordModel.Email, "Usuário", "Recuperação de senha", "Recupere sua senha", "Utilize o link para recuperar o acesso", "https://localhost:44332/Usuarios/");
            }
            return View(forgotPasswordModel);
        }


        ///[POST] Edição de senha de acesso
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarSenha(int id, EditarSenha editarSenhaModel)
        {
            if (id != editarSenhaModel.UsuarioId)
            {
                return NotFound();
            }

            ViewBag.msg = null;
            if (ModelState.IsValid)
            {
                Cryptography cryptography = new Cryptography(MD5.Create());

                var usuario = _context.Usuarios.Find(id);
                usuario.Senha = cryptography.HashGenerate(editarSenhaModel.Senha);
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Perfil));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ViewBag.msg = "Um erro inesperado ocorreu, tente novamente";
                    if (!UsuarioExiste(usuario.UsuarioId))
                    {
                        return NotFound();
                    }
                }
            }
            return View(editarSenhaModel);
        }


        /// [POST] Editar usuário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPerfil(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return NotFound();
            }

            ViewBag.msg = null;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Perfil));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ViewBag.msg = "Um erro inesperado ocorreu, tente novamente";
                    if (!UsuarioExiste(usuario.UsuarioId))
                    {
                        return NotFound();
                    }
                }
            }
            return View(usuario);
        }


        /// <summary>
        /// [POST] Tela de cadastro parte 1 do usuário
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("candidato/cadastro")]
        public IActionResult Cadastro(Cadastro cadastroModel)
        {
            var usuario = new Usuario();
            Cryptography cryptography = new Cryptography(System.Security.Cryptography.MD5.Create());

            //Verifica se email existe
            if (EmailUsuarioExiste(cadastroModel.Email)) ModelState.AddModelError("Email", "O e-mail inserido já existe");

            //Verifica se email existe
            if (TelefoneUsuarioExiste(cadastroModel.Telefone)) ModelState.AddModelError("Telefone", "O telefone inserido já existe");

            //Verifica se a senha a confirmação de senha são iguais
            if (!cryptography.HashVerify(cadastroModel.ConfirmarSenha, cadastroModel.Senha))
            {
                ModelState.AddModelError("Senha", "As senhas não correspondem.");
            }
            //Verifica força da senha
            else if (usuario.VerifyPasswordStrong(cadastroModel.Senha) < 3)
            {
                ModelState.AddModelError("Senha", "A segurança da senha é baixa, tente outra");
            }

            if (ModelState.IsValid)
            {
                usuario.Nome = cadastroModel.Nome;
                usuario.Telefone = cadastroModel.Telefone;
                usuario.Email = cadastroModel.Email;
                usuario.Senha = cryptography.HashGenerate(cadastroModel.Senha);


                //Enviar e-mail pra confirmar email pra cadastro 
                SendEmail(usuario.Email, usuario.Nome, "Confirme seu cadastro", "Verificação de Email", "Você criou uma conta no sistema de login .NET CORE, termine seu registro realizando os passos abaixo.", "https://localhost:44332/Usuarios/FinalizarCadastro?id=");
                TempData["RegistrarUsuario"] = JsonConvert.SerializeObject(usuario);
                TempData["EmailUsuario"] = usuario.Email;
                return RedirectToAction(nameof(ConfirmarEmail));
            }

            TempData["RegistrarUsuario"] = JsonConvert.SerializeObject(usuario);
            return View();
        }


        /// [POST] Tela de finalizar cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarCadastro(Models.Usuario usuario)
        {
            ViewBag.Msg = null;
            try
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                TempData["token"] = null;
                StartSessionLogin(usuario);
                return RedirectToAction(nameof(Perfil));
            }
            catch
            {
                ViewBag.Msg = "Um erro ocorreu, tente novamente";
                return View();
            }
        }

        /// [POST] Tela de login de usuário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login loginModel)
        {
            ViewBag.Erro = null;
            if (ModelState.IsValid)
            {
                if (Login(loginModel.Email, loginModel.Senha))
                {
                    return RedirectToAction(nameof(Perfil));
                }
                ViewBag.Erro = "E-mail ou senha incorretos";
            }
            return View();
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Verifica se o usuario existe pelo ID
        private bool UsuarioExiste(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }


        //Verifica se um email já esta cadastrado
        private bool EmailUsuarioExiste(string email)
        {
            if (String.IsNullOrEmpty(email)) return false;

            Usuario procurarEmail = _context.Usuarios.Where(m => m.Email.Equals(email)).FirstOrDefault();
            if (procurarEmail != null) return true;
            return false;
        }


        //Verifica se o telefone já está cadastrado
        private bool TelefoneUsuarioExiste(string telefone)
        {
            if (String.IsNullOrEmpty(telefone)) return false;

            Usuario procurarTelefone = _context.Usuarios.Where(m => m.Telefone.Equals(telefone)).FirstOrDefault();
            if (procurarTelefone != null) return true;
            return false;
        }

        /// Enviar email pro usuário
        /// <param name="email">E-mail pro remetente</param>
        /// <param name="title">Título do e-mail</param>
        /// <param name="msg">Mensagem do e-mail</param>
        /// <param name="link">Link de recuperação do e-mail</param>
        public void SendEmail(string email, string nome, string title, string assunto, string msg, string link)
        {
            try
            {
                string token = Guid.NewGuid().ToString();

                MailMessage m = new MailMessage(new MailAddress("desafiosmtp@gmail.com", title), new MailAddress(email))
                {
                    Subject = assunto,
                    Body = string.Format(
                        @"<div marginwidth=""0"" marginheight=""0"" style=""margin:0;padding:0;height:100%;width:100%;background-color:#f7f7f7"">
                            <center>
                                 <table border=""0"" cellpadding=""0"" cellspacin =""0"" style=""border-collapse:collapse;width:600px;background-color:#ffffff;border:1px solid #d9d9d9"">
                                    <tbody>
                                        <tr>
                                            <td align=""center"" valign=""top"" style=""font-family:Helvetica,arial,sans-serif;line-height:160%"">
                                                <table align=""center"" border=""0"" cellpaddin=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse:collapse"">
                                                    <tbody>
                                                        <tr>
                                                            <td align = ""center"" style = ""background-color:#ffffff;font-family:Helvetica,arial,sans-serif;line-height:160%;padding-top:20px;padding-bottom:20px;background:#fff"">
                                                                <img src=""https://www.empregare.com/assets/common/images/layoutV2/logo.png?v=202003041329"" alt=""Empregare"" title=""Empregare"" width=""180"" height=""60"" style=""border:0;height:auto;line-height:100%;outline:none;text-decoration:none;max-width:180px;width:180px;height:60px"">
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:160%;width:100%;min-width:100%"">
                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse:collapse;background-color:#ffffff;border-top:1px solid #ffffff;border-bottom:1px solid #ffffff;width:100%;min-width:100%"">
                                                    <tbody>
                                                        <tr>
                                                            <td valign=""top"" style=""font-family:Helvetica,arial,sans-serif;line-height:100%;color:#ffffff;font-size:20px;font-weight:bold;padding-top:0;padding-right:0;padding-bottom:0;padding-left:0;text-align:left;vertical-align:middle;width:100%;min-width:100%"">
                                                                <table height=""90"" align=""center"" valign=""middle"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""border-collapse:collapse;width:100%;min-width:100%"">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td width=""100%"" align=""center"" valign=""middle"" height=""90"" style=""background-color:#606062;background-clip:padding-box;font-size:30px;font-family:Helvetica,arial,sans-serif;text-align:center;color:#ffffff;font-weight:300;padding-left:0px;padding-right:0px;width:100%;vertical-align:middle;min-width:100%"">
                                                                                <span style=""color:#ffffff;font-weight:300"">
                                                                                    {0}
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:160%;color:#404040;font-size:16px;padding-top:20px;padding-bottom:30px;padding-right:72px;padding-left:72px;background:#ffffff"">
                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse:collapse;background-color:#ffffff"">
                                                    <tbody>
                                                        <tr>
                                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:100%;padding-bottom:20px;text-align:center"">
                                                                <div style=""display:block;font-family:Helvetica,Arial,sans-serif;font-style:normal;line-height:160%;letter-spacing:normal;margin-top:0px;margin-right:0;margin-bottom:0;margin-left:0;text-align:center;color:#888888;font-size:12px;background-color:#fffcf4;padding:10px;border:1px solid #ffe8ab;border-radius:5px"">
                                                                Esta mensagem foi enviada pelo software de confirmação de email da EMPREGARE de forma automática.Por favor, não responda este e-mail.
                                                                </div>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:160%;padding-bottom:32px;text-align:center"">
                                                                <h2 style=""display:block;font-family:Helvetica,Arial,sans-serif;font-style:normal;font-weight:bold;line-height:100%;letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:0;margin-left:0;text-align:center;color:#404040;font-size:20px"">
                                                                Olá<strong style=""color:#8dd8f8!important;font-weight:600!important""> {1}!</strong>
                                                                </h2>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:160%;padding-bottom:32px;text-align:center"">
                                                                <p style=""margin:0"">
                                                                    Este é a <b>{2}</b> da <b>EMPREGARE.</b> {3}
                                                                </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:160%;padding-bottom:32px;text-align:center"">
                                                                <div style= ""padding:16px;border:1px solid #AD0000;border-radius:4px;display:block;margin:0 auto;width:90%;font-family:Helvetica,Arial,sans-serif"">
                                                                    <table border= ""0"" width= ""100%"" cellpadding= ""0"" cellspacing= ""0"" >
                                                                        <tbody>
                                                                            <tr>
                                                                                <td style= ""font-family:Helvetica,arial,sans-serif;line-height:160%;padding-bottom:10px;text-align:left"">
                                                                                    <p style= ""margin:0;font-size:14px""> 
                                                                                        Este email é apenas para fins <b>educacionais.</b> Sendo assim de nenhum valor e de nenhum vínculo com a empresa. O uso do mesmo não poderá ser usado para outros fins, sendo extremamente <b>restrito!</b><br>
                                                                                        <strong>Data de criação</strong> 19/02/2021 17:10<br>
                                                                                    </p>
                                                                                </td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:160%;padding-bottom:32px;text-align:center"">
                                                                <p style= ""margin:0"">
                                                                Clique no botão abaixo para acessar sua conta e terminar sua <b>{2}</b>.
                                                                </p>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style=""font-family:Helvetica,arial,sans-serif;padding-bottom:32px;text-align:center"">
                                                                <table height= ""56"" align= ""center"" valign= ""middle"" width= ""90%"" border= ""0"" cellpadding= ""0"" cellspacing= ""0"">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td align=""center"" valign=""middle"" style=""background-color:#8dd8f8;border-top-left-radius:4px;border-bottom-left-radius:4px;border-top-right-radius:4px;border-bottom-right-radius:4px;background-clip:padding-box;font-size:16px;width: 100%;font-family:Helvetica,arial,sans-serif;text-align:center;color:#ffffff;font-weight:300;padding-left: 0px;padding-right: 0px;"">
                                                                                <span style=""color:#ffffff;font-weight:600;display:flex;width:100%"">
                                                                                    <a style=""color:#fff;line-height: 56px;text-align:center;align-items:center;justify-content:center;height:100%;width:100%;text-decoration:none;"" href= ""{4}{5}"" target= ""_blank""> {0} </a>
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style= ""font-family:Helvetica,arial,sans-serif;line-height:160%;padding-bottom:32px;text-align:center"" >
                                                                <p style= ""margin:0;font-size:14px;color:#999"" >
                                                                    Clique no botão acima para {2}.
                                                                </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:160%;margin-bottom:0;padding-bottom:0;text-align:center"">
                                                                <p style=""margin:0;margin-bottom:0;padding-bottom:0"">
                                                                    Atenciosamente,<br>Equipe Empregare
                                                                </p>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:160%;background:#ffffff"">
                                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""border-collapse:collapse;background-color:#ffffff"">
                                                    <tbody>
                                                        <tr>
                                                            <td width=""80%"" style=""font-family:Helvetica,arial,sans-serif;line-height:160%;padding-bottom:16px;text-align:center"">
                                                                <hr  style=""width:80%;border:0;border-top:1px solid #ddd"">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:130%;padding-bottom:16px;text-align:center;padding-left:12%;padding-right:12%"">
                                                                <p style=""margin:0;color:#999;font-size:12px"">
                                                                    Este email é apenas para fins <b>educacionais</b>, não tendo nenhum valor e de uso extremamente <b>restrito!</b><br>
                                                                    Criado por <a href=""samuelnunessergio@gmail.com""><b>Samuel Nunes</b></a>
                                                                </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:160%;padding-bottom:16px;text-align:center"">
                                                                <hr>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style=""font-family:Helvetica,arial,sans-serif;line-height:160%;padding-bottom:40px;text-align:center"">
                                                                <p style=""margin:0;font-size:12px"">
                                                                    <span style=""color:#999"">Simplificamos, otimizamos e inovamos os processos seletivos</span><br>
                                                                    <strong style=""color:#404040""> Empregare 2021</strong>
                                                                </p>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </center>
                        </div>",
                    title, nome, assunto, msg, link, token)
                };

                TempData["token"] = token;

                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("desafiosmtp@gmail.com", "desafiosmtp1234@"),
                    EnableSsl = true
                };
                smtp.Send(m);
            }
            catch
            {
                TempData["Erro"] = "Um erro aconteceu. Tente novamente.";
            }
        }


        //Efetuar o login do usuário
        public bool Login(string email, string senha)
        {
            Cryptography cryptography = new Cryptography(MD5.Create());
            string passwordCript = cryptography.HashGenerate(senha);

            Usuario usuario = _context.Usuarios.Where(p => p.Email.Equals(email) && p.Senha.Equals(passwordCript)).FirstOrDefault();
            if (usuario == null) return false;

            StartSessionLogin(usuario);
            return true;
        }


        //Deslogando o usuário - Remove as sessions existentes
        public void Logout()
        {
            HttpContext.Session.Remove("UsuarioNome");
            HttpContext.Session.Remove("UsuarioEmail");
            HttpContext.Session.Remove("UsuarioId");

        }


        //Inicia a session
        private void StartSessionLogin(Usuario usuario)
        {
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            HttpContext.Session.SetString("UsuarioEmail", usuario.Email);
            HttpContext.Session.SetString("UsuarioId", usuario.UsuarioId.ToString());
        }
    }
}

