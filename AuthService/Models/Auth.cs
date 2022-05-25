namespace AuthService.Models
{
    public partial class Auth
    {
        public Guid Guid { get; set; }
        public string Username { get; set; } = null!;
        public byte[] Salt { get; set; } = null!;
        public byte[] HashedPass { get; set; } = null!;
        public bool? Active { get; set; }
    }
}
