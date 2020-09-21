using System.Collections.Generic;

namespace TokenCoding
{
    /// <summary>
    /// Класс, представляющий результат кодирования.
    /// </summary>
    public class EncodingResult
    {
        /// <summary>
        /// Закодированная строка.
        /// </summary>
        public string Encoded { get; set; }

        /// <summary>
        /// Словарь кодов нетерминалов.
        /// </summary>
        public Dictionary<string, int> NonTerminals { get; set; }

        /// <summary>
        /// Словарь кодов терминалов.
        /// </summary>
        public Dictionary<string, int> Terminals { get; set; }

        /// <summary>
        /// Словарь кодов семантик.
        /// </summary>
        public Dictionary<string, int> Semantics { get; set; }
    }
}