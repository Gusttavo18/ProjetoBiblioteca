using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        //Inserção------------------
        public IActionResult RegistrarUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View();
        }
        [HttpPost]
        public IActionResult RegistrarUsuarios(Usuario novoUser)
        {
            novoUser.Senha = Criptografo.TextoCriptografado(novoUser.Senha);
            new UsuarioService().IncluirUsuario(novoUser);
            return RedirectToAction("CadastroRealizado");
        }
        public IActionResult CadastroRealizado()
        {
            return View();
        }
        //Lista de usuários--------------------
        public IActionResult ListaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);

            return View(new UsuarioService().Listar());
        }
        //Edição de usuários-----------------------------
        public IActionResult editarUsuario(int id)
        {
            Usuario u = new UsuarioService().Listar(id);
            return View(u);
        }
        [HttpPost]
        public IActionResult editarUsuario(Usuario userEditado)
        {
            new UsuarioService().EditarUsuario(userEditado);
            return RedirectToAction("ListaDeUsuarios");
        }
        //Exclusão de usuários---------------------------
        public IActionResult ExcluirUsuario(int id)
        {
            return View(new UsuarioService().Listar(id));
        }
        [HttpPost]
        public IActionResult ExcluirUsuario(string decisao, int id)
        {
            if (decisao == "EXCLUIR")
            {
                ViewData["Mensagem"] = "Exclusão do usuário" + new UsuarioService().Listar(id).Nome + "realizada com sucesso";
                new UsuarioService().ExcluirUsuario(id);
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
            else
            {
                ViewData["Mensagem"] = "Exclusão cancelada";
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
        }
        //Funções extras---------------------------------
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }
    }
}