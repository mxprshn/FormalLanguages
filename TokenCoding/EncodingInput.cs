using System;
using System.Collections.Generic;
using System.Text;

namespace TokenCoding
{
    public class EncodingInput
    {

        /// <summary>
        /// Кодируемая строка. 
        /// </summary>
        public string Source { get; set; }

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
