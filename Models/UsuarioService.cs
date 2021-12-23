using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar()
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.ToList();
            }
        }

        public Usuario Listar(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }

        public void IncluirUsuario(Usuario u)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Add(u);
                bc.SaveChanges();
            }
        }
        public void EditarUsuario(Usuario EditU)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario antigoU = bc.Usuarios.Find(EditU.Id);
                antigoU.Nome = EditU.Nome;
                antigoU.Login = EditU.Login;
                antigoU.Senha = EditU.Senha;
                antigoU.Tipo = EditU.Tipo;

                bc.SaveChanges();
            }
        }
        public void ExcluirUsuario(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();
            }
        }
    }
}