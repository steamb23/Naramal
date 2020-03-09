using System;
using System.Collections.Generic;
using System.Text;

using 문자열 = System.String;
using 문자 = System.Char;
using 정수 = System.Int32;
using 논리 = System.Boolean;

namespace 나랏말
{
    public partial struct 한글
    {
        /// <summary>
        /// 지정한 문자의 받침을 지정한 받침으로 치환합니다.
        /// </summary>
        /// <param name="문자">받침이 치환될 문자입니다.</param>
        /// <param name="받침">치환할 받침입니다.</param>
        /// <returns>받침이 치환된 문자입니다.</returns>
        public static 문자 받침치환(문자 문자, 받침 받침) => (문자)받침치환(문자, (정수)받침);

        /// <summary>
        /// 지정한 텍스트에서 마지막 문자의 받침을 지정한 받침으로 치환합니다.
        /// </summary>
        /// <param name="문장">마지막 문자의 받침이 치환될 문자열입니다.</param>
        /// <param name="받침">치환할 받침입니다.</param>
        /// <returns>마지막 문자의 받침이 치환된 문장입니다.</returns>
        public static 문자열 받침치환(문자열 문장, 받침 받침)
        {
            var 마지막문자 = 문장[^1];
            문장 = 문장[0..^1];
            return 문장 + 받침치환(마지막문자, 받침);
        }
        public static 문자열 문장늘이기(문자열 문장, 정수 늘일량 = 1, 논리 ㅢ분리 = false)
        {
            if (늘일량 < 1)
                return 문장;

            var 마지막문자위치 = 문장.Length - 1;
            var 마지막문자 = 문장[마지막문자위치];

            var 한글 = (한글)마지막문자;
            // 변환에 실패했으면 일반 문장 반환
            if (!한글.변환성공)
                return 문장;

            // 마지막 문자 제거
            문장 = 문장.Substring(0, 마지막문자위치);

            // 받침만 빼고 다시 추가한다.
            // 만약 만약 ㅢ 분리를 하면 ㅢ를 ㅡ로 바꾼다.
            if (한글.모음 == 모음.ㅢ && ㅢ분리)
                문장 += 조합(한글.자음, 모음.ㅡ);
            else
                문장 += 조합(한글.자음, 한글.모음);

            // 단모음화
            한글.모음 = 단모음화(한글.모음);

            for (var i = 0; i < 늘일량; i++)
            {
                // 마지막 받침 추가
                if (i >= 늘일량 - 1)
                {
                    문장 += 조합(자음.ㅇ, 한글.모음, 한글.받침);
                }
                // 받침 생략
                else
                {
                    문장 += 조합(자음.ㅇ, 한글.모음);
                }
            }

            return 문장;
        }

        /// <summary>
        /// 이중모음을 단모음으로 바꿉니다.
        /// </summary>
        /// <param name="모음"></param>
        /// <returns></returns>
        /// <remarks>
        /// <para>특수 규칙인 (ㅟ, ㅢ) → ㅣ, (ㅚ) → ㅔ 는 필요시 호출자측에서 적절한 분기를 통해 다른 규칙을 적용하시는 것이 좋습니다. 특히 'ㅢ'의 경우에는 'ㅔ'로 발음되는 경우도 있으나 음이 늘어질경우 'ㅣ'로 변화하는 특성이 강해 'ㅣ'로 변경하도록 하였습니다.</para>
        /// </remarks>
        public static 모음 단모음화(모음 모음)
        {
            switch (모음)
            {
                case 모음.ㅑ:
                case 모음.ㅒ:
                case 모음.ㅕ:
                case 모음.ㅖ:
                    모음 -= 2;
                    break;
                case 모음.ㅛ:
                case 모음.ㅠ:
                    모음 -= 4;
                    break;
                case 모음.ㅘ:
                case 모음.ㅙ:
                    모음 -= 9;
                    break;
                case 모음.ㅝ:
                case 모음.ㅞ:
                    모음 -= 10;
                    break;
                // 아래는 특수 규칙
                case 모음.ㅟ:
                case 모음.ㅢ:
                    모음 = 모음.ㅣ;
                    break;
                case 모음.ㅚ:
                    모음 = 모음.ㅔ;
                    break;
            }
            return 모음;
        }

        internal static 정수 받침치환(정수 유니코드값, 정수 받침값)
        {
            var 추출된받침 = 받침추출(유니코드값);
            if (추출된받침 == -1)
                return 유니코드값;
            else
                return 유니코드값 - 추출된받침 + 받침값;
        }
    }
}
