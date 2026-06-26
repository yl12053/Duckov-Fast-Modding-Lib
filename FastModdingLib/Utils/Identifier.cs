using System;

namespace FastModdingLib.Utils
{
    public class Identifier: IEquatable<Identifier>
    {
        private string _domain;
        private string _path;
        
        public string Domain
        {
            get => _domain;
        }
        public string Path
        {
            get => _path;
        }

        public Identifier(string domain, string path)
        {
            if (domain.Contains(":"))
            {
                throw new ArgumentException(":(Colon) is not accepted in domain");
            }
            if (domain.Equals(""))
            {
                throw new ArgumentException("Domain must not be empty");
            }
            if (path.Contains(":"))
            {
                throw new ArgumentException(":(Colon) is not accepted in path");
            }
            if (path.Equals(""))
            {
                throw new ArgumentException("Path must not be empty");
            }
            _domain = domain;
            _path = path;
        }

        public Identifier(string raw)
        {
            if (raw == "")
            {
                throw new ArgumentException("Resource Location must not be a empty string");
            }
            string[] splitted = raw.Split(":");
            if (splitted.Length <= 1)
            {
                throw new ArgumentException(":(Colon) missing");
            }
            if (splitted.Length >= 3)
            {
                throw new ArgumentException(":(Colon) too much!");
            }

            string domain = splitted[0];
            string path = splitted[1];
            if (domain.Contains(":"))
            {
                throw new ArgumentException(":(Colon) is not accepted in domain");
            }
            if (domain.Equals(""))
            {
                throw new ArgumentException("Domain must not be empty");
            }
            if (path.Contains(":"))
            {
                throw new ArgumentException(":(Colon) is not accepted in path");
            }
            if (path.Equals(""))
            {
                throw new ArgumentException("Path must not be empty");
            }
            _domain = domain;
            _path = path;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null) return false;
            return obj is Identifier other &&
                   Equals(other);
        }

        public bool Equals(Identifier other)
        {
            return Path.Equals(other.Path) && Domain.Equals(other.Domain);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Path, Domain);
        }
    }
}