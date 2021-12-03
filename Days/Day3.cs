using System;
using System.Linq;

namespace AoC2021.Days
{
    public class Day3 : BaseDay
    {
        private char GetCommonBitInColumn(int idx, char[][] binaryArray)
        {
            int bitCount = binaryArray.Select(x => x[idx]).Where(i => i == '1').Count();
            return bitCount >= binaryArray.Length - bitCount ? '1' : '0';
        }
        public override string PartOne(string input)
        {
            var binaryArray = input.Split(Environment.NewLine).Select(x => x.Trim().ToCharArray()).ToArray();
            var common = "";
            var uncommon = "";
            for (int i = 0; i < binaryArray[0].Length; i++)
            {
                var c = GetCommonBitInColumn(i, binaryArray);
                common += c;
                uncommon += (c == '0' ? "1" : "0");
            }
            var gammaRate = Convert.ToInt32(common, 2);
            var eplsilonRate = Convert.ToInt32(uncommon, 2);

            return (gammaRate * eplsilonRate).ToString();
        }

        public override string PartTwo(string input)
        {
            var binaryArray = input.Split(Environment.NewLine).Select(x => x.ToCharArray()).ToArray();
            var oxygenArray = binaryArray;
            var rubberArray = binaryArray;

            for (int i = 0; i < binaryArray[0].Length; i++)
            {
                if (oxygenArray.Length != 1)
                {
                    var commonBit = GetCommonBitInColumn(i, oxygenArray);
                    oxygenArray = oxygenArray.Where(x => x[i] == commonBit).ToArray();
                }
                if (rubberArray.Length != 1)
                {
                    var uncommonBit = GetCommonBitInColumn(i, rubberArray) == '0' ? '1' : '0';
                    rubberArray = rubberArray.Where(x => x[i] == uncommonBit).ToArray();
                }
            }
            var oxygen = Convert.ToInt32(new string(oxygenArray[0]), 2);
            var rubber = Convert.ToInt32(new string(rubberArray[0]), 2);

            return (oxygen * rubber).ToString();
        }
    }
}