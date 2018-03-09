using System;
using System.Collections.Generic;
using System.Text;

namespace School.Infrastructure.Business.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Проверяем является ли буква русской
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool ContainsRusSymbol(this string symbol)
        {
            string[] rusKey = new string[] { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };

            symbol = symbol.ToUpper();

            for (int i = 0; i < rusKey.Length; i++)
            {
                if (symbol == rusKey[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
