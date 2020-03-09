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
    /// 자음을 나타냅니다.
    /// </summary>
    public enum 자음 : 정수
    {
        /// <summary>
        /// 변환에 실패한 값입니다.
        /// </summary>
        실패 = -1,
        /// <summary>
        /// 자음 ㄱ입니다.
        /// </summary>
        ㄱ = 0,
        /// <summary>
        /// 자음 ㄲ입니다.
        /// </summary>
        ㄲ = 1,
        /// <summary>
        /// 자음 ㄴ입니다.
        /// </summary>
        ㄴ = 2,
        /// <summary>
        /// 자음 ㄷ입니다.
        /// </summary>
        ㄷ = 3,
        /// <summary>
        /// 자음 ㄸ입니다.
        /// </summary>
        ㄸ = 4,
        /// <summary>
        /// 자음 ㄹ입니다.
        /// </summary>
        ㄹ = 5,
        /// <summary>
        /// 자음 ㅁ입니다.
        /// </summary>
        ㅁ = 6,
        /// <summary>
        /// 자음 ㅂ입니다.
        /// </summary>
        ㅂ = 7,
        /// <summary>
        /// 자음 ㅃ입니다.
        /// </summary>
        ㅃ = 8,
        /// <summary>
        /// 자음 ㅅ입니다.
        /// </summary>
        ㅅ = 9,
        /// <summary>
        /// 자음 ㅆ입니다.
        /// </summary>
        ㅆ = 10,
        /// <summary>
        /// 자음 ㅇ입니다.
        /// </summary>
        ㅇ = 11,
        /// <summary>
        /// 자음 ㅈ입니다.
        /// </summary>
        ㅈ = 12,
        /// <summary>
        /// 자음 ㅉ입니다.
        /// </summary>
        ㅉ = 13,
        /// <summary>
        /// 자음 ㅊ입니다.
        /// </summary>
        ㅊ = 14,
        /// <summary>
        /// 자음 ㅋ입니다.
        /// </summary>
        ㅋ = 15,
        /// <summary>
        /// 자음 ㅌ입니다.
        /// </summary>
        ㅌ = 16,
        /// <summary>
        /// 자음 ㅍ입니다.
        /// </summary>
        ㅍ = 17,
        /// <summary>
        /// 자음 ㅎ입니다.
        /// </summary>
        ㅎ = 18
    }
    /// <summary>
    /// 모음을 나타냅니다.
    /// </summary>
    public enum 모음 : 정수
    {
        /// <summary>
        /// 변환에 실패한 값입니다.
        /// </summary>
        실패 = -1,
        /// <summary>
        /// 모음 ㅏ입니다.
        /// </summary>
        ㅏ = 0,
        /// <summary>
        /// 모음 ㅐ입니다.
        /// </summary>
        ㅐ = 1,
        /// <summary>
        /// 모음 ㅑ입니다.
        /// </summary>
        ㅑ = 2,
        /// <summary>
        /// 모음 ㅒ입니다.
        /// </summary>
        ㅒ = 3,
        /// <summary>
        /// 모음 ㅓ입니다.
        /// </summary>
        ㅓ = 4,
        /// <summary>
        /// 모음 ㅔ입니다.
        /// </summary>
        ㅔ = 5,
        /// <summary>
        /// 모음 ㅕ입니다.
        /// </summary>
        ㅕ = 6,
        /// <summary>
        /// 모음 ㅖ입니다.
        /// </summary>
        ㅖ = 7,
        /// <summary>
        /// 모음 ㅗ입니다.
        /// </summary>
        ㅗ = 8,
        /// <summary>
        /// 모음 ㅘ입니다.
        /// </summary>
        ㅘ = 9,
        /// <summary>
        /// 모음 ㅙ입니다.
        /// </summary>
        ㅙ = 10,
        /// <summary>
        /// 모음 ㅚ입니다.
        /// </summary>
        ㅚ = 11,
        /// <summary>
        /// 모음 ㅛ입니다.
        /// </summary>
        ㅛ = 12,
        /// <summary>
        /// 모음 ㅜ입니다.
        /// </summary>
        ㅜ = 13,
        /// <summary>
        /// 모음 ㅝ입니다.
        /// </summary>
        ㅝ = 14,
        /// <summary>
        /// 모음 ㅞ입니다.
        /// </summary>
        ㅞ = 15,
        /// <summary>
        /// 모음 ㅟ입니다.
        /// </summary>
        ㅟ = 16,
        /// <summary>
        /// 모음 ㅠ입니다.
        /// </summary>
        ㅠ = 17,
        /// <summary>
        /// 모음 ㅡ입니다.
        /// </summary>
        ㅡ = 18,
        /// <summary>
        /// 모음 ㅢ입니다.
        /// </summary>
        ㅢ = 19,
        /// <summary>
        /// 모음 ㅣ입니다.
        /// </summary>
        ㅣ = 20
    }

    /// <summary>
    /// 받침을 나타냅니다.
    /// </summary>
    public enum 받침 : 정수
    {
        /// <summary>
        /// 변환에 실패한 값입니다.
        /// </summary>
        실패 = -1,
        /// <summary>
        /// 받침이 없다는 것을 알리는 값입니다.
        /// </summary>
        없음 = 0,
        /// <summary>
        /// 받침 ㄱ입니다.
        /// </summary>
        ㄱ = 1,
        /// <summary>
        /// 받침 ㄲ입니다.
        /// </summary>
        ㄲ = 2,
        /// <summary>
        /// 받침 ㄳ입니다.
        /// </summary>
        ㄳ = 3,
        /// <summary>
        /// 받침 ㄴ입니다.
        /// </summary>
        ㄴ = 4,
        /// <summary>
        /// 받침 ㄵ입니다.
        /// </summary>
        ㄵ = 5,
        /// <summary>
        /// 받침 ㄶ입니다.
        /// </summary>
        ㄶ = 6,
        /// <summary>
        /// 받침 ㄷ입니다.
        /// </summary>
        ㄷ = 7,
        /// <summary>
        /// 받침 ㄹ입니다.
        /// </summary>
        ㄹ = 8,
        /// <summary>
        /// 받침 ㄺ입니다.
        /// </summary>
        ㄺ = 9,
        /// <summary>
        /// 받침 ㄻ입니다.
        /// </summary>
        ㄻ = 10,
        /// <summary>
        /// 받침 ㄼ입니다.
        /// </summary>
        ㄼ = 11,
        /// <summary>
        /// 받침 ㄽ입니다.
        /// </summary>
        ㄽ = 12,
        /// <summary>
        /// 받침 ㄾ입니다.
        /// </summary>
        ㄾ = 13,
        /// <summary>
        /// 받침 ㄿ입니다.
        /// </summary>
        ㄿ = 14,
        /// <summary>
        /// 받침 ㅀ입니다.
        /// </summary>
        ㅀ = 15,
        /// <summary>
        /// 받침 ㅁ입니다.
        /// </summary>
        ㅁ = 16,
        /// <summary>
        /// 받침 ㅂ입니다.
        /// </summary>
        ㅂ = 17,
        /// <summary>
        /// 받침 ㅄ입니다.
        /// </summary>
        ㅄ = 18,
        /// <summary>
        /// 받침 ㅅ입니다.
        /// </summary>
        ㅅ = 19,
        /// <summary>
        /// 받침 ㅆ입니다.
        /// </summary>
        ㅆ = 20,
        /// <summary>
        /// 받침 ㅇ입니다.
        /// </summary>
        ㅇ = 21,
        /// <summary>
        /// 받침 ㅈ입니다.
        /// </summary>
        ㅈ = 22,
        /// <summary>
        /// 받침 ㅊ입니다.
        /// </summary>
        ㅊ = 23,
        /// <summary>
        /// 받침 ㅋ입니다.
        /// </summary>
        ㅋ = 24,
        /// <summary>
        /// 받침 ㅌ입니다.
        /// </summary>
        ㅌ = 25,
        /// <summary>
        /// 받침 ㅍ입니다.
        /// </summary>
        ㅍ = 26,
        /// <summary>
        /// 받침 ㅎ입니다.
        /// </summary>
        ㅎ = 27,
    }
}
