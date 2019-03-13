using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using Microsoft.International.Converters;

namespace BBSObserver.Scheduler
{
    public class Transformer
    {
        public static readonly char NONE = 'N';
        public static readonly char ONE = 'Ⅰ';
        public static readonly char TWO = 'Ⅱ';
        public static readonly char THREE = 'Ⅲ';

        /// <summary>
        /// 受け取った文字列をギリシャ数字はローマ数字に、全角英語は半角英語に変換する
        /// </summary>
        /// <param name="source">変換する文字列</param>
        /// <returns>変換された文字列</returns>
        public static string Transform(string source)
        {
            char target = NONE;
            char change = NONE;
            if (source.Contains(ONE))
            {
                target = ONE;
                change = '1';
            }else if (source.Contains(TWO))
            {
                target = TWO;
                change = '2';
            }else if (source.Contains(THREE))
            {
                target = THREE;
                change = '3';
            }
            else
            {
                change = NONE;
            }

            string result = source;

            if (change != NONE)
            {
                result = result.Replace(target, change);
            }

            result = KanaConverter.HalfwidthKatakanaToKatakana(result);
            if (result.Contains("ｰ"))
            {
                result = result.Replace('ｰ', 'ー');
            }

            var str = Regex.Replace(result, "[ａ-ｚ]", p => ((char)(p.Value[0] - 'ａ' + 'a')).ToString());
            result = Regex.Replace(str, "[Ａ-Ｚ]", p => ((char)(p.Value[0] - 'Ａ' + 'A')).ToString());

            return result;
        }
    }
}