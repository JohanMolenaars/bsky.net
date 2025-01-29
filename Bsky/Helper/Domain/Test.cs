namespace Bsky.Net.Helper.Domain
{
    public static class Test
    {
        /// <summary>
        /// A static list of Top-Level Domains (TLDs) including:
        /// - Generic TLDs (e.g., .com, .org, .net)
        /// - Infrastructure TLD (.arpa)
        /// - Sponsored TLDs (e.g., .aero, .biz, .coop)
        /// - Country code TLDs (e.g., .us, .uk, .ca)
        /// </summary>
        private static List<string> TLDs = new List<string>  {
            ".com", ".org", ".net", ".int", ".edu", ".gov", ".mil", // Generic TLDs
            ".arpa", // Infrastructure TLD
            ".aero", ".biz", ".coop", ".info", ".museum", ".name", ".pro", // Sponsored TLDs
            ".asia", ".cat", ".jobs", ".mobi", ".tel", ".travel", // Sponsored TLDs
            ".ac", ".ad", ".ae", ".af", ".ag", ".ai", ".al", ".am", ".ao", ".aq", ".ar", ".as", ".at", ".au", ".aw", ".ax", ".az", // Country code TLDs
            ".ba", ".bb", ".bd", ".be", ".bf", ".bg", ".bh", ".bi", ".bj", ".bm", ".bn", ".bo", ".br", ".bs", ".bt", ".bv", ".bw", ".by", ".bz",
            ".ca", ".cc", ".cd", ".cf", ".cg", ".ch", ".ci", ".ck", ".cl", ".cm", ".cn", ".co", ".cr", ".cu", ".cv", ".cw", ".cx", ".cy", ".cz",
            ".de", ".dj", ".dk", ".dm", ".do", ".dz",
            ".ec", ".ee", ".eg", ".er", ".es", ".et", ".eu",
            ".fi", ".fj", ".fk", ".fm", ".fo", ".fr",
            ".ga", ".gb", ".gd", ".ge", ".gf", ".gg", ".gh", ".gi", ".gl", ".gm", ".gn", ".gp", ".gq", ".gr", ".gs", ".gt", ".gu", ".gw", ".gy",
            ".hk", ".hm", ".hn", ".hr", ".ht", ".hu",
            ".id", ".ie", ".il", ".im", ".in", ".io", ".iq", ".ir", ".is", ".it",
            ".je", ".jm", ".jo", ".jp",
            ".ke", ".kg", ".kh", ".ki", ".km", ".kn", ".kp", ".kr", ".kw", ".ky", ".kz",
            ".la", ".lb", ".lc", ".li", ".lk", ".lr", ".ls", ".lt", ".lu", ".lv", ".ly",
            ".ma", ".mc", ".md", ".me", ".mg", ".mh", ".mk", ".ml", ".mm", ".mn", ".mo", ".mp", ".mq", ".mr", ".ms", ".mt", ".mu", ".mv", ".mw", ".mx", ".my", ".mz",
            ".na", ".nc", ".ne", ".nf", ".ng", ".ni", ".nl", ".no", ".np", ".nr", ".nu", ".nz",
            ".om",
            ".pa", ".pe", ".pf", ".pg", ".ph", ".pk", ".pl", ".pm", ".pn", ".pr", ".ps", ".pt", ".pw", ".py",
            ".qa",
            ".re", ".ro", ".rs", ".ru", ".rw",
            ".sa", ".sb", ".sc", ".sd", ".se", ".sg", ".sh", ".si", ".sj", ".sk", ".sl", ".sm", ".sn", ".so", ".sr", ".ss", ".st", ".su", ".sv", ".sx", ".sy", ".sz",
            ".tc", ".td", ".tf", ".tg", ".th", ".tj", ".tk", ".tl", ".tm", ".tn", ".to", ".tp", ".tr", ".tt", ".tv", ".tw", ".tz",
            ".ua", ".ug", ".uk", ".us", ".uy", ".uz",
            ".va", ".vc", ".ve", ".vg", ".vi", ".vn", ".vu",
            ".wf", ".ws",
            ".ye", ".yt",
            ".za", ".zm", ".zw"
        };

        /// <summary>
        /// Determines whether the specified string is a valid domain.
        /// </summary>
        /// <param name="str">The domain string to validate.</param>
        /// <returns>
        /// <c>true</c> if the specified string is a valid domain; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidDomain(string str)
        {
            return TLDs.Exists(tld =>
            {
                int i = str.LastIndexOf(tld);
                if (i == -1)
                {
                    return false;
                }
                return str[i - 1] == '.' && i == str.Length - tld.Length;
            });
        }
    }
}
