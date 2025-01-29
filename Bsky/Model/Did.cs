
using Bsky.Net.Helper;
using System;

namespace Bsky.Net.Model
{
    public class Did
    {
        private readonly string _did;

        public Did(string value)
        {
            value.ThrowIfNull();
            string? parsed = default;
            if (!Parser.IsValidDid(value) && !Parser.TryParseDidFromJwt(value, out parsed))
            {
                throw new ArgumentException("Invalid Did", nameof(value));
            }

            _did = parsed ?? value;
        }

        public override string ToString() => _did;

        public string Type => _did!.Split(':')[1];
    }
}
