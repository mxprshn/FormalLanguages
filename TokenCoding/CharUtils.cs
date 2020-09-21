using System;
using System.Collections.Generic;
using System.Text;

namespace TokenCoding
{
    public static class CharUtils
    {
        /// <summary>
        /// Проверяет, является ли символ заглавной латинской буквой.
        /// </summary>
        static public bool IsUpperLatin(this char character) =>
            character >= 'A' && character <= 'Z';

        /// <summary>
        /// Проверяет, является ли символ строчной латинской буквой.
        /// </summary>
        static public bool IsLowerLatin(this char character) =>
            character >= 'a' && character <= 'z';
    }
}
