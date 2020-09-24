using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concordance
{
    public class Concordance
    {
        private static HashSet<char> wordConnectors = new HashSet<char> { '\'', '-', '’' };

        private Dictionary<string, SortedSet<int>> wordMap = new Dictionary<string, SortedSet<int>>();

        public bool AddWord(string word, int lineNumber)
        {
            if (!wordMap.ContainsKey(word))
            {
                wordMap.Add(word, new SortedSet<int>());
            }

            return wordMap[word].Add(lineNumber);
        }

        public void ReadText(string text, bool removeWordConnectors)
        {
            var lines = text.ToLower().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (var i = 0; i < lines.Length; ++i)
            {
                var words = new string(lines[i].Select(c => (removeWordConnectors && wordConnectors.Contains(c)) ||
                    (char.IsPunctuation(c) && !wordConnectors.Contains(c)) ? ' ' : c).ToArray())
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in words)
                {
                    AddWord(word, i + 1);
                }
            }
        }

        public void Print()
        {
            Console.WriteLine("Алфавитный указатель:\n");
            foreach (var word in wordMap.OrderBy(w => w.Key))
            {
                Console.Write($"{word.Key + ":",15}");

                foreach (var lineNumber in word.Value)
                {
                    Console.Write($"{lineNumber,5}");
                }

                Console.WriteLine();
            }
        }
    }
}