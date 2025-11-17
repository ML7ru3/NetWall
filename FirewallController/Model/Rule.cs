namespace FirewallController.Model
{
    public class Rule
    {
        public string? Family { get; set; } = "ipv4"; // "ipv4" or "ipv6"
        public string? Chain { get; set; } // e.g. INPUT, OUTPUT
        public string? Protocol { get; set; } // tcp, udp, icmp
        public int? Dport { get; set; }
        public int? Sport { get; set; }
        public string? Src { get; set; }
        public string? Dst { get; set; }
        public string? Target { get; set; } // ACCEPT, DROP
    }
}
