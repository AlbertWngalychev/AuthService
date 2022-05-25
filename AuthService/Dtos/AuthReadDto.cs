namespace AuthService.Dtos
{
    public class AuthReadDto
    {
        public Guid Guid { get; set; }
        public string Username { get; set; } = null!;
        public byte[] Salt { get; set; } = null!;
        public byte[] HashedPass { get; set; } = null!;
        public bool? Active { get; set; }
    }
}
