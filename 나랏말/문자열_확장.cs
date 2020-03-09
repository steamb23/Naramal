using System;
using System.Collections.Generic;
using System.Text;

using 문자열 = System.String;
using 문자 = System.Char;
using 정수 = System.Int32;
using 논리 = System.Boolean;

namespace 나랏말
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
        public static 문자열 조사처리(this 문자열 단어, 문자열 조사) => 나랏말.조사.처리(단어, 조사);
    }
}
