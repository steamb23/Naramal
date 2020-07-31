using System.Runtime.CompilerServices;
using Naramal.HangulGraphemes;

namespace Naramal
{
    /// <summary>
    /// 한글의 구성요소를 나타냅니다.
    /// </summary>
    public partial struct Hangul
    {
        #region 상수
        /// <summary>
        /// 현대 한글의 자음 갯수입니다.
        /// </summary>
        public const int ConsonantCount = 19;
        /// <summary>
        /// 현대 한글의 모음 갯수입니다.
        /// </summary>
        public const int VowelCount = 21;
        /// <summary>
        /// 현대 한글의 받침 갯수입니다.
        /// </summary>
        public const int FinalConsonantCount = 28;
        #endregion

        /// <summary>
        /// 변환에 성공했는 지에 대한 여부를 가져옵니다.
        /// </summary>
        public bool IsConverted => Consonant > Consonant.Failed && Vowel > Vowel.Failed && FinalConsonant > FinalConsonant.Failed;

        /// <summary>
        /// 자음입니다.
        /// </summary>
        public Consonant Consonant;
        /// <summary>
        /// 모음입니다.
        /// </summary>
        public Vowel Vowel;
        /// <summary>
        /// 받침입니다.
        /// </summary>
        public FinalConsonant FinalConsonant;

        /// <summary>
        /// 문자를 가져옵니다.
        /// </summary>
        public char Character => (char)this;

        /// <summary>
        /// <see cref="Hangul"/> 구조체의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="Consonant">설정할 자음입니다.</param>
        /// <param name="Vowel">설정할 모음입니다.</param>
        /// <param name="FinalConsonant">설정할 받침입니다. 기본값은 <see cref="FinalConsonant.None"/>입니다.</param>
        public Hangul(Consonant Consonant, Vowel Vowel, FinalConsonant FinalConsonant = FinalConsonant.None)
        {
            this.Consonant = Consonant;
            this.Vowel = Vowel;
            this.FinalConsonant = FinalConsonant;
        }

        /// <summary>
        /// <see cref="Hangul"/> 구조체의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="character"><see cref="Hangul"/> 구조체로 변환될 한글 문자입니다.</param>
        public Hangul(char character)
        {
            if (CheckHangulSyllable(character))
            {
                int unicodeValue = character;
                unicodeValue -= 0xac00;

                var remainder = unicodeValue % (VowelCount * FinalConsonantCount);

                Consonant = (Consonant)(unicodeValue / (VowelCount * FinalConsonantCount));
                Vowel = (Vowel)(remainder / FinalConsonantCount);
                FinalConsonant = (FinalConsonant)(remainder % FinalConsonantCount);
            }
            else
            {
                Consonant = Consonant.Failed;
                Vowel = Vowel.Failed;
                FinalConsonant = FinalConsonant.Failed;
            }
        }
        #region 정적 멤버
        /// <summary>
        /// 주어진 문자가 완성형 한글 영역에 속하는지 확인합니다.
        /// </summary>
        /// <param name="character">확인할 문자입니다.</param>
        /// <returns>완성형 한글이면 참, 아니면 거짓입니다.</returns>
        public static bool CheckHangulSyllable(char character) => CheckHangulSyllable((int)character);

        /// <summary>
        /// 주어진 문자에서 자음만 추출합니다.
        /// </summary>
        /// <param name="character">추출할 문자입니다.</param>
        /// <returns>추출된 자음입니다.</returns>
        public static Consonant ExtractConsonant(char character) => (Consonant)ExtractConsonant((int)character);

        /// <summary>
        /// 주어진 문자에서 모음만 추출합니다.
        /// </summary>
        /// <param name="character">추출할 문자입니다.</param>
        /// <returns>추출된 모음입니다.</returns>
        public static Vowel ExtractVowel(char character) => (Vowel)ExtractVowel((int)character);

        /// <summary>
        /// 한글의 자음, 모음, 받침을 한 문자로 조합합니다.
        /// </summary>
        /// <param name="consonant"></param>
        /// <param name="vowel"></param>
        /// <param name="finalConsonant"></param>
        /// <returns></returns>
        public static char Merge(Consonant consonant, Vowel vowel, FinalConsonant finalConsonant = FinalConsonant.None) => (char)Merge((int)consonant, (int)vowel, (int)finalConsonant);

        /// <summary>
        /// 주어진 문자에서 받침만 추출합니다.
        /// </summary>
        /// <param name="character">추출할 문자입니다.</param>
        /// <returns>추출된 받침입니다.</returns>
        public static FinalConsonant ExtractFinalConsonant(char character) => (FinalConsonant)ExtractFinalConsonant((int)character);

        #region 유니코드값 정수 처리
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool CheckHangulSyllable(int unicodeValue) => unicodeValue >= 0xac00 && unicodeValue <= 0xd7a3;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int ExtractConsonant(int unicodeValue) => CheckHangulSyllable(unicodeValue) ? ((unicodeValue - 0xac00) / (VowelCount * FinalConsonantCount)) : -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int ExtractVowel(int unicodeValue) => CheckHangulSyllable(unicodeValue) ? ((unicodeValue - 0xac00) % (VowelCount * FinalConsonantCount) / FinalConsonantCount) : -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int ExtractFinalConsonant(int unicodeValue) => CheckHangulSyllable(unicodeValue) ? ((unicodeValue - 0xac00) % (VowelCount * FinalConsonantCount) % FinalConsonantCount) : -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int Merge(int consonant, int vowel, int finalConsonant) =>
            consonant >= 0 && vowel >= 0 && finalConsonant >= 0 ?
            0xac00 + consonant * VowelCount * FinalConsonantCount + vowel * FinalConsonantCount + finalConsonant :
            0;
        #endregion
        #endregion

        #region 사용자 정의 구조체 오버라이드
        /// <summary>
        /// 두 한글의 값이 같은지 확인합니다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Equals(Hangul value) =>
                Consonant == value.Consonant &&
                Vowel == value.Vowel &&
                FinalConsonant == value.FinalConsonant;

        /// <summary>
        /// 두 개체 인스턴스가 같은지 확인합니다.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => Equals((Hangul)obj);

        /// <summary>
        /// 이 인스턴스의 해시코드를 반환합니다.
        /// </summary>
        /// <returns>부호 있는 32비트 정수 해시 코드입니다.</returns>
        public override int GetHashCode() => Consonant.GetHashCode() | Vowel.GetHashCode() << 5 | FinalConsonant.GetHashCode() << 5 * 2;
        /// <summary></summary>
        public static bool operator ==(Hangul left, Hangul right)
        {
            return left.Equals(right);
        }
        /// <summary></summary>
        public static bool operator !=(Hangul left, Hangul right)
        {
            return !(left == right);
        }
        /// <summary></summary>
        public static explicit operator char(Hangul value) => Merge(value.Consonant, value.Vowel, value.FinalConsonant);
        /// <summary></summary>
        public static explicit operator Hangul(char value) => new Hangul(value);
        #endregion
    }
}
