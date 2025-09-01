using BMG.Identidade.Domain.Entities;

namespace BMG.Identidade.Domain.DTOs
{
    public class RegistrarUsuarioDTO
    {
        public string Login { get; set; }
        public string Nome { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
