namespace FireNetCSharp.Model
{
    public class Rule
    {
        public string? Family { get; set; }
        public string? Chain { get; set; }
        public string? Protocol { get; set; }
        public int? Dport { get; set; }
        public int? Sport { get; set; }
        public string? Src { get; set; }
        public string? Dst { get; set; }
        public string? Target { get; set; }
    }
}
