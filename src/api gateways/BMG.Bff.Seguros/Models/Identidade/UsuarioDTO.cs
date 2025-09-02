namespace BMG.Bff.Seguros.Models.Identidade
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }

    public enum TipoUsuario
    {
        Cliente = 3,
        Operador = 6,
        Administrador = 9
    }
}
