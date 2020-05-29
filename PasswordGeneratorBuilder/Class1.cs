using System;

namespace PasswordGeneratorBuilder
{
    public class PasswordGeneratorBuilder : IPasswordGeneratorBuilder
    {
        public int RequiredLength { get; private set; } = 6;

        public int RequiredUniqueChars { get; private set; } = 1;

        public bool RequireNonAlphanumeric { get; private set; } = true;

        public bool RequireLowercase { get; private set; } = true;

        public bool RequireUppercase { get; private set; } = true;

        public bool RequireDigit { get; private set; } = true;

        public IPasswordGeneratorBuilder SetRequireDigit(bool require)
        {
            this.RequireDigit = require;
            return this;
        }

        public IPasswordGeneratorBuilder SetRequiredLength(int length)
        {
            this.RequiredLength = length;
            return this;
        }

        public IPasswordGeneratorBuilder SetRequiredUniqueChars(int n)
        {
            this.RequiredUniqueChars = n;
            return this;
        }

        public IPasswordGeneratorBuilder SetRequireLowercase(bool require)
        {
            this.RequireLowercase = require;
            return this;
        }

        public IPasswordGeneratorBuilder SetRequireNonAlphanumeric(bool require)
        {
            this.RequireNonAlphanumeric = require;
            return this;
        }

        public IPasswordGeneratorBuilder SetRequireUppercase(bool require)
        {
            this.RequireUppercase = require;
            return this;
        }
    }
}
