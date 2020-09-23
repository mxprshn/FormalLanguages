﻿using System.Collections.Generic;
using System.Text;

namespace TokenCoding
{
    /// <summary>
    /// Класс, производящий операцию кодирования текстового представления грамматики.
    /// </summary>
    public static class Coder
    {
        private const int EofgramCode = 1000;
        private const int FirstNonTerminalCode = 11;
        private const int FirstTerminalCode = 51;
        private const int FirstSemanticsCode = 101;

        private static Dictionary<char, int> symbols = new Dictionary<char, int>
        {
            { ':', 1 },
            { '(', 2 },
            { ')', 3 },
            { '.', 4 },
            { '*', 5 },
            { ';', 6 },
            { ',', 7 },
            { '#', 8 },
            { '[', 9 },
            { ']', 10 }
        };

        /// <summary>
        /// Состояние просмотра последовательности символов.
        /// </summary>
        private enum State
        {
            // Нейтральное состояние (рассматриваются отдельные символы или пробелы)
            Neutral,
            // Рассматривается слово, начинающееся с кавычки
            InQuotes,
            // Рассматривается слово, состоящее из строчных латинских букв
            InLowerCase,
            // Рассматривается слово, состоящее из заглавных букв
            InUpperCase,
            // Рассматривается слово, начинающееся с '$'
            InSemantics
        }

        public static EncodingResult Encode(string source)
        {
            var firstInput = new EncodingInput
            {
                Source = source,
                Terminals = new Dictionary<string, int>(),
                NonTerminals = new Dictionary<string, int>(),
                Semantics = new Dictionary<string, int>()
            };

            var firstResult = Encode(firstInput);

            var secondInput = new EncodingInput
            {
                Source = source,
                Terminals = new Dictionary<string, int>(),
                NonTerminals = firstResult.NonTerminals,
                Semantics = new Dictionary<string, int>()
            };

            return Encode(secondInput);
        }

        private static EncodingResult Encode(EncodingInput input)
        {
            // Хранит результирующую строку
            var resultBuilder = new StringBuilder("");

            // Хранит просматриваемое слово 
            var currentTokenBuilder = new StringBuilder("");

            var newNonTerminalCode = FirstNonTerminalCode;
            var newTerminalCode = FirstTerminalCode;
            var newSemanticsCode = FirstSemanticsCode;

            var nonTerminals = input.NonTerminals;
            var terminals = input.Terminals;
            var semantics = input.Semantics;
            var source = input.Source;

            var currentState = State.Neutral;

            for (var i = 0; i < source.Length; ++i)
            {
                var currentChar = source[i];

                switch (currentState)
                {
                    case State.Neutral:
                        {
                            // Если просматриваемый символ является пробелом, он не добавляется к строке результата
                            if (char.IsWhiteSpace(currentChar))
                            {
                                break;
                            }

                            if (symbols.ContainsKey(currentChar))
                            {
                                resultBuilder.Append(" " + symbols[currentChar]);
                                break;
                            }

                            if (currentChar == '\'')
                            {
                                currentState = State.InQuotes;
                                break;
                            }

                            if (currentChar.IsLowerLatin())
                            {
                                currentState = State.InLowerCase;
                                currentTokenBuilder.Append(currentChar);
                                break;
                            }

                            if (currentChar.IsUpperLatin())
                            {
                                currentState = State.InUpperCase;
                                currentTokenBuilder.Append(currentChar);
                                break;
                            }

                            if (currentChar == '$')
                            {
                                currentState = State.InSemantics;
                                break;
                            }

                            throw new UnexpectedCharacterException(currentChar);
                        }
                    case State.InQuotes:
                        {
                            // Слово в кавычках закончилось
                            if (currentChar == '\'')
                            {
                                currentState = State.Neutral;
                                var token = currentTokenBuilder.ToString();

                                if (!terminals.ContainsKey(token))
                                {
                                    terminals[token] = newTerminalCode;
                                    ++newTerminalCode;
                                }

                                resultBuilder.Append(" " + terminals[token]);
                                currentTokenBuilder.Clear();
                                break;
                            }

                            currentTokenBuilder.Append(currentChar);
                            break;
                        }
                    case State.InUpperCase:
                        {
                            if (currentChar.IsUpperLatin())
                            {
                                currentTokenBuilder.Append(currentChar);
                                break;
                            }

                            var token = currentTokenBuilder.ToString();

                            // Слово из заглавных букв закончилось
                            if (char.IsWhiteSpace(currentChar))
                            {
                                currentState = State.Neutral;

                                if (!nonTerminals.ContainsKey(token))
                                {
                                    nonTerminals[token] = newNonTerminalCode;
                                    ++newNonTerminalCode;
                                }

                                resultBuilder.Append(" " + nonTerminals[token]);
                                currentTokenBuilder.Clear();
                                break;
                            }

                            // Рассматривается Eofgram
                            if (token == "E" && source.Substring(i - 1, 7) == "Eofgram")
                            {
                                resultBuilder.Append(" " + EofgramCode);
                                i = source.Length;
                                break;
                            }

                            throw new UnexpectedCharacterException(currentChar);
                        }
                    case (State.InLowerCase):
                        {
                            if (currentChar.IsLowerLatin())
                            {
                                currentTokenBuilder.Append(currentChar);
                                break;
                            }

                            // Слово из строчных букв закончилось
                            if (char.IsWhiteSpace(currentChar))
                            {
                                currentState = State.Neutral;
                                var token = currentTokenBuilder.ToString();

                                // Слово является нетерминалом
                                if (source[i + 1] == ':')
                                {
                                    if (!nonTerminals.ContainsKey(token))
                                    {
                                        nonTerminals[token] = newNonTerminalCode;
                                        ++newNonTerminalCode;
                                    }

                                    resultBuilder.Append(" " + nonTerminals[token]);
                                    currentTokenBuilder.Clear();
                                    break;
                                }

                                if (nonTerminals.ContainsKey(token))
                                {
                                    resultBuilder.Append(" " + nonTerminals[token]);
                                    currentTokenBuilder.Clear();
                                    break;
                                }

                                // Слово является терминалом
                                if (!terminals.ContainsKey(token))
                                {
                                    terminals[token] = newTerminalCode;
                                    ++newTerminalCode;
                                }

                                resultBuilder.Append(" " + terminals[token]);
                                currentTokenBuilder.Clear();
                                break;
                            }

                            throw new UnexpectedCharacterException(currentChar);
                        }
                    case (State.InSemantics):
                        {
                            // Слово, начинающееся на '$' закончилось
                            if (char.IsWhiteSpace(currentChar))
                            {
                                currentState = State.Neutral;
                                var token = currentTokenBuilder.ToString();

                                if (!semantics.ContainsKey(token))
                                {
                                    semantics[token] = newSemanticsCode;
                                    ++newSemanticsCode;
                                }

                                resultBuilder.Append(" " + semantics[token]);
                                currentTokenBuilder.Clear();
                                break;
                            }

                            currentTokenBuilder.Append(currentChar);
                            break;
                        }
                }
            }

            return new EncodingResult
            {
                Encoded = resultBuilder.ToString().Trim(),
                Terminals = terminals,
                NonTerminals = nonTerminals,
                Semantics = semantics
            };
        }
    }
}