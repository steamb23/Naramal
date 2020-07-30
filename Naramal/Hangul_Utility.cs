using Naramal.HangulGraphemes;

namespace Naramal
{
    public partial struct Hangul
    {
        /// <summary>
        /// 지정한 문자의 받침을 지정한 받침으로 치환합니다.
        /// </summary>
        /// <param name="character">받침이 치환될 문자입니다.</param>
        /// <param name="finalConsonant">치환할 받침입니다.</param>
        /// <returns>받침이 치환된 문자입니다.</returns>
        public static char ReplaceFinalConsonant(char character, FinalConsonant finalConsonant) => (char)ReplaceFinalConsonant(character, (int)finalConsonant);

        /// <summary>
        /// 지정한 텍스트에서 마지막 문자의 받침을 지정한 받침으로 치환합니다.
        /// </summary>
        /// <param name="text">마지막 문자의 받침이 치환될 문자열입니다.</param>
        /// <param name="finalConsonant">치환할 받침입니다.</param>
        /// <returns>마지막 문자의 받침이 치환된 문장입니다.</returns>
        public static string ReplaceFinalConsonant(string text, FinalConsonant finalConsonant)
        {
            var lastCharacter = text[text.Length - 1];
            text = text.Substring(0, text.Length - 1);
            return text + ReplaceFinalConsonant(lastCharacter, finalConsonant);
        }

        /// <summary>
        /// 마지막 문자를 길게 늘입니다. 늘여진 문자의 모음은 단모음화됩니다.
        /// </summary>
        /// <param name="text">늘여질 문장입니다.</param>
        /// <param name="amount">문장을 늘일 양입니다.</param>
        /// <param name="splitㅢ">ㅢ를 ㅡㅣ소리로 변환할지 여부를 나타내는 값입니다.</param>
        /// <returns>늘여진 문장입니다.</returns>
        public static string Stretch(string text, int amount = 1, bool splitㅢ = false)
        {
            if (amount < 1)
                return text;

            var lastCharacterPosition = text.Length - 1;
            var lastCharacter = text[lastCharacterPosition];

            var hangul = (Hangul)lastCharacter;
            // 변환에 실패했으면 일반 문장 반환
            if (!hangul.IsConverted)
                return text;

            // 마지막 문자 제거
            text = text.Substring(0, lastCharacterPosition);

            // 받침만 빼고 다시 추가한다.
            // 만약 만약 ㅢ 분리를 하면 ㅢ를 ㅡ로 바꾼다.
            if (hangul.Vowel == Vowel.ㅢ && splitㅢ)
                text += Merge(hangul.Consonant, Vowel.ㅡ);
            else
                text += Merge(hangul.Consonant, hangul.Vowel);

            // 단모음화
            hangul.Vowel = MakeShortVowel(hangul.Vowel);

            for (var i = 0; i < amount; i++)
            {
                // 마지막 받침 추가
                if (i >= amount - 1)
                {
                    text += Merge(Consonant.ㅇ, hangul.Vowel, hangul.FinalConsonant);
                }
                // 받침 생략
                else
                {
                    text += Merge(Consonant.ㅇ, hangul.Vowel);
                }
            }

            return text;
        }

        /// <summary>
        /// 이중모음을 단모음으로 바꿉니다.
        /// </summary>
        /// <param name="vowel">단모음으로 변환할 모음입니다.</param>
        /// <returns>단모음입니다.</returns>
        /// <remarks>
        /// <para>특수 규칙인 (ㅟ, ㅢ) → ㅣ, (ㅚ) → ㅔ 는 필요시 호출자측에서 적절한 분기를 통해 다른 규칙을 적용하시는 것이 좋습니다. 특히 'ㅢ'의 경우에는 'ㅔ'로 발음되는 경우도 있으나 음이 늘어질경우 'ㅣ'로 변화하는 특성이 강해 'ㅣ'로 변경하도록 하였습니다.</para>
        /// </remarks>
        public static Vowel MakeShortVowel(Vowel vowel)
        {
            // 자체 연구한 규칙으로 구현됨
            switch (vowel)
            {
                case Vowel.ㅑ:
                case Vowel.ㅒ:
                case Vowel.ㅕ:
                case Vowel.ㅖ:
                    vowel -= 2;
                    break;
                case Vowel.ㅛ:
                case Vowel.ㅠ:
                    vowel -= 4;
                    break;
                case Vowel.ㅘ:
                case Vowel.ㅙ:
                    vowel -= 9;
                    break;
                case Vowel.ㅝ:
                case Vowel.ㅞ:
                    vowel -= 10;
                    break;
                // 아래는 특수 규칙
                case Vowel.ㅟ:
                case Vowel.ㅢ:
                    vowel = Vowel.ㅣ;
                    break;
                case Vowel.ㅚ:
                    vowel = Vowel.ㅔ;
                    break;
            }
            return vowel;
        }

        internal static int ReplaceFinalConsonant(int unicodeValue, int finalConsonantValue)
        {
            var extractedFinalConsonant = ExtractFinalConsonant(unicodeValue);
            if (extractedFinalConsonant == -1)
                return unicodeValue;
            else
                return unicodeValue - extractedFinalConsonant + finalConsonantValue;
        }
    }
}
