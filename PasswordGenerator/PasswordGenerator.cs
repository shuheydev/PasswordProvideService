using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator
{
    public interface IPasswordGenerator
    {
        string Generate();
    }

    public class PasswordGenerator : IPasswordGenerator
    {
        //パスワード文字列を生成
        private static readonly string lowerCase = "abcdefghijklmnopqrspuvwxyz";
        private static readonly string upperCase = lowerCase.ToUpper();
        private static readonly string numeric = "0123456789";
        private static readonly string nonAlphaNumeric = @"@;:#$%&{}[]~^";

        public IPasswordGeneratorBuilder builder;

        public PasswordGenerator(IPasswordGeneratorBuilder passwordGeneratorBuilder)
        {
            this.builder = passwordGeneratorBuilder;
        }

        public string Generate()
        {
            //パスワードに使用する文字種を取得
            List<string> candidates = GetCharCandidates();


            //パスワードを構成する文字列の空きスロット数
            var slot = builder.RequiredLength;
            //使用する文字の種類の数
            var divNum = candidates.Count;

            //文字種の構成割合を決定
            var allocRatio = Divide(slot, divNum);

            //それぞれの文字種から構成割合に基づいてピックアップ
            var passwordRough = string.Join("", candidates.Select((x, i) => PickupCharsFromString(x, allocRatio[i])));

            //それらをシャッフルしてパスワードとする.
            var shuffled = Shuffle(passwordRough);

            return shuffled;
        }

        public List<string> GetCharCandidates()
        {
            List<string> candidates = new List<string>();

            if (builder.RequireDigit)
            {
                candidates.Add(numeric);
            }
            if (builder.RequireLowercase)
            {
                candidates.Add(lowerCase);
            }
            if (builder.RequireUppercase)
            {
                candidates.Add(upperCase);
            }
            if (builder.RequireNonAlphanumeric)
            {
                candidates.Add(nonAlphaNumeric);
            }

            if (candidates.Count == 0)
            {
                candidates.Add(numeric);
                candidates.Add(lowerCase);
                candidates.Add(upperCase);
                candidates.Add(nonAlphaNumeric);
            }

            return candidates;
        }

        public string Shuffle(string word)
        {
            var splitted = word.ToList();
            var c = word.Length;
            var r = new Random();

            for (var i = 0; i < c; i++)
            {
                var pos1 = r.Next(0, c - 1);
                var pos2 = r.Next(0, c - 1);

                (splitted[pos1], splitted[pos2]) = (splitted[pos2], splitted[pos1]);
            }

            return string.Join("", splitted);
        }

        /// <summary>
        /// a個をbつにわける
        /// ランダムで
        /// </summary>
        /// <param name="a"></param>
        /// <param name="div"></param>
        /// <returns></returns>
        public List<int> Divide(int slot, int divNum)
        {
            if(divNum==0)
            {
                throw new ArgumentException($"{nameof(divNum)} must be greater than 0");
            }

            if(slot<divNum)
            {
                throw new ArgumentException($"{nameof(divNum)} must be greater equal to {nameof(slot)}");
            }

            var ratio = new List<int>();

            var r = new Random();

            int max = slot;
            for (int i = 1; i <= divNum; i++)
            {
                max = slot - (divNum - i);

                if(i==divNum)
                {
                    ratio.Add(max);
                    break;
                }

                int ocupy = r.Next(1, max);
                ratio.Add(ocupy);
                slot = slot - ocupy;
            }

            return ratio;
        }

        public string PickupCharsFromString(string s, int n)
        {
            var r = new Random();
            var c = s.Length;

            var picked = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                picked.Append(s[r.Next(c)]);
            }

            return picked.ToString();
        }
    }
}
