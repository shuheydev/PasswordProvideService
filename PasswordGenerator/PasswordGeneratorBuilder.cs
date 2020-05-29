using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordGenerator
{
    public interface IPasswordGeneratorBuilder
    {
        int RequiredLength { get; }
        int RequiredUniqueChars { get; }
        bool RequireNonAlphanumeric { get; }
        bool RequireLowercase { get; }
        bool RequireUppercase { get; }
        bool RequireDigit { get; }

        IPasswordGeneratorBuilder SetRequiredLength(int length);
        IPasswordGeneratorBuilder SetRequiredUniqueChars(int n);
        IPasswordGeneratorBuilder SetRequireNonAlphanumeric(bool require);
        IPasswordGeneratorBuilder SetRequireLowercase(bool require);
        IPasswordGeneratorBuilder SetRequireUppercase(bool require);
        IPasswordGeneratorBuilder SetRequireDigit(bool require);

        IPasswordGenerator Build();
    }

    public class PasswordGeneratorBuilder : IPasswordGeneratorBuilder
    {
        private int _requiredLength = 6;
        public int RequiredLength
        {
            get => _requiredLength;
            private set
            {
                if (value < 1)
                {
                    _requiredLength = 6;
                }
                else
                {
                    _requiredLength = value;
                }
            }
        }

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

        public IPasswordGenerator Build()
        {
            return new PasswordGenerator(this);
        }
    }
}
