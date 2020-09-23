using NUnit.Framework;
using TokenCoding;

namespace TokenCodingTests
{
    public class CoderTests
    {
        [TestCase("nonterminal : nonterminal 'nontermianal'" +
            "Eofgram",
            ExpectedResult = "11 1 11 51 1000")]

        [TestCase("first : second " +
            "second : third " +
            "third : second " +
            "Eofgram",
            ExpectedResult = "11 1 12 12 1 13 13 1 12 1000")]

        [TestCase("first : first $first " +
            "Eofgram",
            ExpectedResult = "11 1 11 101 1000")]

        [TestCase("first : ******* " +
            "Eofgram",
            ExpectedResult = "11 1 5 5 5 5 5 5 5 1000")]

        [TestCase("Eofgram",
            ExpectedResult = "1000")]

        [TestCase(": * ; () . # [] Eofgram",
            ExpectedResult = "1 5 6 2 3 4 8 9 10 1000")]

        [TestCase("first : ( second )#[ a ] " +
            "second : third * b * ( fourth ) " +
            "third : second * $a " +
            "fourth : c $c [[ first ]] " +
            "Eofgram",
            ExpectedResult = "11 1 2 12 3 8 9 51 10 12 1 13 5 52 5 2" +
            " 14 3 13 1 12 5 101 14 1 53 102 9 9 11 10 10 1000")]

        [TestCase("$a $b $c $d $e Eofgram",
            ExpectedResult = "101 102 103 104 105 1000")]
        public string EncodeTest(string source)
        {
            return Coder.Encode(source).Encoded;
        }
    }
}