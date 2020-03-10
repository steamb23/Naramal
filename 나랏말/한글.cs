using System;
using System.Runtime.CompilerServices;

using 문자열 = System.String;
using 문자 = System.Char;
using 정수 = System.Int32;
using 논리 = System.Boolean;

namespace 나랏말
{
    /// <summary>
    /// 한글의 구성요소를 나타냅니다.
    /// </summary>
    public partial struct 한글
    {
        #region 상수
        /// <summary>
        /// 현대 한글의 자음 갯수입니다.
        /// </summary>
        public const 정수 자음갯수 = 19;
        /// <summary>
        /// 현대 한글의 모음 갯수입니다.
        /// </summary>
        public const 정수 모음갯수 = 21;
        /// <summary>
        /// 현대 한글의 받침 갯수입니다.
        /// </summary>
        public const 정수 받침갯수 = 28;
        #endregion

        /// <summary>
        /// 변환에 성공했는 지에 대한 여부를 가져옵니다.
        /// </summary>
        public 논리 변환성공 => 자음 > 자음.실패 && 모음 > 모음.실패 && 받침 > 받침.실패;

        /// <summary>
        /// 자음입니다.
        /// </summary>
        public 자음 자음;
        /// <summary>
        /// 모음입니다.
        /// </summary>
        public 모음 모음;
        /// <summary>
        /// 받침입니다.
        /// </summary>
        public 받침 받침;

        /// <summary>
        /// 문자를 가져옵니다.
        /// </summary>
        public 문자 문자 => (문자)this;

        /// <summary>
        /// <see cref="한글"/> 구조체의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="자음">설정할 자음입니다.</param>
        /// <param name="모음">설정할 모음입니다.</param>
        /// <param name="받침">설정할 받침입니다. 기본값은 <see cref="받침.없음"/>입니다.</param>
        public 한글(자음 자음, 모음 모음, 받침 받침 = 받침.없음)
        {
            this.자음 = 자음;
            this.모음 = 모음;
            this.받침 = 받침;
        }

        /// <summary>
        /// <see cref="한글"/> 구조체의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="문자"><see cref="한글"/> 구조체로 변환될 한글 문자입니다.</param>
        public 한글(문자 문자)
        {
            if (완성형한글여부(문자))
            {
                정수 유니코드값 = 문자;
                유니코드값 -= 0xac00;

                var 나머지 = 유니코드값 % (모음갯수 * 받침갯수);

                자음 = (자음)(유니코드값 / (모음갯수 * 받침갯수));
                모음 = (모음)(나머지 / 받침갯수);
                받침 = (받침)(나머지 % 받침갯수);
            }
            else
            {
                자음 = 자음.실패;
                모음 = 모음.실패;
                받침 = 받침.실패;
            }
        }
        #region 정적 멤버
        /// <summary>
        /// 주어진 문자가 완성형 한글 영역에 속하는지 확인합니다.
        /// </summary>
        /// <param name="문자">확인할 문자입니다.</param>
        /// <returns>완성형 한글이면 참, 아니면 거짓입니다.</returns>
        public static 논리 완성형한글여부(문자 문자) => 완성형한글여부((정수)문자);

        /// <summary>
        /// 주어진 문자에서 자음만 추출합니다.
        /// </summary>
        /// <param name="문자">추출할 문자입니다.</param>
        /// <returns>추출된 자음입니다.</returns>
        public static 자음 자음추출(문자 문자) => (자음)자음추출((정수)문자);

        /// <summary>
        /// 주어진 문자에서 모음만 추출합니다.
        /// </summary>
        /// <param name="문자">추출할 문자입니다.</param>
        /// <returns>추출된 모음입니다.</returns>
        public static 모음 모음추출(문자 문자) => (모음)모음추출((정수)문자);

        /// <summary>
        /// 한글의 자음, 모음, 받침을 한 문자로 조합합니다.
        /// </summary>
        /// <param name="자음"></param>
        /// <param name="모음"></param>
        /// <param name="받침"></param>
        /// <returns></returns>
        public static 문자 조합(자음 자음, 모음 모음, 받침 받침 = 받침.없음) => (문자)조합((정수)자음, (정수)모음, (정수)받침);

        /// <summary>
        /// 주어진 문자에서 받침만 추출합니다.
        /// </summary>
        /// <param name="문자">추출할 문자입니다.</param>
        /// <returns>추출된 받침입니다.</returns>
        public static 받침 받침추출(문자 문자) => (받침)받침추출((정수)문자);

        #region 유니코드값 정수 처리
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static 논리 완성형한글여부(정수 유니코드값) => 유니코드값 >= 0xac00 && 유니코드값 <= 0xd7a3;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static 정수 자음추출(정수 유니코드값) => 완성형한글여부(유니코드값) ? ((유니코드값 - 0xac00) / (모음갯수 * 받침갯수)) : -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static 정수 모음추출(정수 유니코드값) => 완성형한글여부(유니코드값) ? ((유니코드값 - 0xac00) % (모음갯수 * 받침갯수) / 받침갯수) : -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static 정수 받침추출(정수 유니코드값) => 완성형한글여부(유니코드값) ? ((유니코드값 - 0xac00) % (모음갯수 * 받침갯수) % 받침갯수) : -1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static 정수 조합(정수 자음, 정수 모음, 정수 받침) =>
            자음 >= 0 && 모음 >= 0 && 받침 >= 0 ?
            0xac00 + 자음 * 모음갯수 * 받침갯수 + 모음 * 받침갯수 + 받침 :
            0;
        #endregion
        #endregion

        #region 사용자 정의 구조체 오버라이드
        /// <summary>
        /// 두 한글의 값이 같은지 확인합니다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Equals(한글 value) => 
                자음 == value.자음 &&
                모음 == value.모음 &&
                받침 == value.받침;

        /// <summary>
        /// 두 개체 인스턴스가 같은지 확인합니다.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => Equals((한글)obj);

        /// <summary>
        /// 이 인스턴스의 해시코드를 반환합니다.
        /// </summary>
        /// <returns>부호 있는 32비트 정수 해시 코드입니다.</returns>
        public override int GetHashCode() => 자음.GetHashCode() | 모음.GetHashCode() << 5 | 받침.GetHashCode() << 5 * 2;
        /// <summary></summary>
        public static bool operator ==(한글 left, 한글 right)
        {
            return left.Equals(right);
        }
        /// <summary></summary>
        public static bool operator !=(한글 left, 한글 right)
        {
            return !(left == right);
        }
        /// <summary></summary>
        public static explicit operator 문자(한글 value) => 조합(value.자음, value.모음, value.받침);
        /// <summary></summary>
        public static explicit operator 한글(문자 value) => new 한글(value);
        #endregion
    }
}
