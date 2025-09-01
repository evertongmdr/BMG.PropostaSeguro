namespace BMG.Identidade.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
