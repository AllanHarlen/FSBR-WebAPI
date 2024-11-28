namespace WebApis.Models
{
    public class UsuarioResponse
    {
        public string Auth { get; set; }
        public UsuarioInfo Usuario { get; set; }
    }

    public class UsuarioInfo
    {
        public string Id { get; set; }
        public string Login { get; set; }
    }
}
