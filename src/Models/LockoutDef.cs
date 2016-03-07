namespace Models
{
    public sealed class LockoutDef
    {
        public string Key { get; set; }

        public int Threshold { get; set; }

        public int Expiration { get; set; }
    }
}