using System;
using System.Collections.Generic;
using System.Text;

namespace Naramal
{
    /// <summary>
    /// 문자열에 대한 확장을 나타냅니다.
    /// </summary>
    public static class StringExtend
    {
        /// <summary>
        /// 단어와 조사를 조합합니다.
        /// </summary>
        /// <param name="단어">조합될 단어입니다.</param>
        /// <param name="조사">조합될 조사입니다.</param>
        /// <returns></returns>
        public static string 조사처리(this string 단어, string 조사) => Naramal.Josa.Process(단어, 조사);
    }
}
