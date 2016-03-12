namespace Web.Models
{
    public sealed class StatusResponse
    {
        public static readonly StatusResponse Locked = new StatusResponse();

        public bool IsAuthenticated { get; set; }

        public string IpAddress { get; set; }
    }
}