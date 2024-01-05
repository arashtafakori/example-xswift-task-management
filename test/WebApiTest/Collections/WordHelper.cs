using Bogus;
using Bogus.DataSets;
using System;
using System.Diagnostics;

namespace WebApiTest
{
    public static class WordHelper
    {
        public static string GetARandomName()
        {
            var text = string.Empty;
            do
            {
                text = new Faker().Company.CompanyName();
            } while (text.Length < 3 || text.Length > 40);

            return text;
        }

        public static string GetARandomSentence()
        {
            var text = string.Empty;
            do
            {
                text = new Faker().Lorem.Sentence();
            } while (text.Length < 10 || text.Length > 1000);

            return text;
        }
    }
}
