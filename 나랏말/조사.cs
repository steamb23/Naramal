using System;
using System.Collections.Generic;
using System.Text;

using 문자열 = System.String;
using 문자 = System.Char;
using 정수 = System.Int32;
using 논리 = System.Boolean;
using System.Globalization;

namespace 나랏말
{
    /// <summary>
    /// 한국어의 조사를 나타냅니다.
    /// </summary>
    public class 조사 : ICustomFormatter, IFormatProvider
    {

        /// <summary>
        /// 조사 클래스의 인스턴스를 초기화합니다.
        /// </summary>
        public 조사()
        {

        }

        /// <summary>
        /// 조사 클래스의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="anotherFormatProvider">같이 사용될 포맷 제공자의 인스턴스입니다.</param>
        public 조사(IFormatProvider anotherFormatProvider)
        {
            this.AnotherFormatProvider = anotherFormatProvider;
        }

        /// <summary>
        /// 또다른 포맷 제공자의 인스턴스를 가져옵니다.
        /// </summary>
        public IFormatProvider? AnotherFormatProvider { get; } = null;

        private static volatile 조사? _인스턴스;
        public static 조사 인스턴스
        {
            get
            {
                if (_인스턴스 == null)
                    _인스턴스 = new 조사();
                return _인스턴스;
            }
        }

        public static 문자열 선택(문자열 단어, 문자열 조사)
        {
            var 받침 = 한글.받침추출(단어[^1]);
            switch (조사)
            {
                case "으로/로":
                    if (받침 == 받침.없음 || 받침 == 받침.ㄹ)
                        return "로";
                    else
                        return "으로";
                default:
                    문자열[] 나누어진_조사 = 조사.Split('/');
                    if (받침 == 받침.없음)
                        return 나누어진_조사[1];
                    else
                        return 나누어진_조사[0];
            }
        }

        public static 문자열 처리(문자열 단어, 문자열 조사) => 단어 + 선택(단어, 조사);

        /// <summary>
        /// 문자열 보간 방식을 사용해 문자열 표현으로 바꿉니다.
        /// </summary>
        /// <param name="문자열">보간될 문자열입니다.</param>
        /// <returns>보간된 문자열입니다.</returns>
        public static 문자열 보간(FormattableString 문자열) => 문자열.ToString(인스턴스);

        /// <summary>
        /// 지정된 포맷 제공자와 문자열 보간 방식을 사용해 문자열 표현으로 바꿉니다.
        /// </summary>
        /// <param name="포맷제공자">포매팅 방법을 제공하는 포맷 제공자의 인스턴스입니다.</param>
        /// <param name="문자열">보간될 문자열입니다.</param>
        /// <returns>보간된 문자열입니다.</returns>
        public static 문자열 보간(IFormatProvider 포맷제공자, FormattableString 문자열) => 문자열.ToString(new 조사(포맷제공자));

        /// <summary>
        /// <see cref="조사"/> 클래스의 인스턴스와 문자열 보간 방식을 사용해 문자열 표현으로 바꿉니다.
        /// </summary>
        /// <param name="조사"><see cref="조사"/> 클래스의 인스턴스입니다.</param>
        /// <param name="문자열">보간될 문자열입니다.</param>
        /// <returns>보간된 문자열입니다.</returns>
        public static 문자열 보간(조사 조사, FormattableString 문자열) => 문자열.ToString(조사);


        public static 문자열 포맷(문자열 format, object arg) => string.Format(인스턴스, format, arg);
        public static 문자열 포맷(문자열 format, params object[] args) => string.Format(인스턴스, format, args);

        public static 문자열 포맷(IFormatProvider formatProvider, string format, object arg) => string.Format(new 조사(formatProvider), format, arg);
        public static 문자열 포맷(IFormatProvider formatProvider, string format, params object[] args) => string.Format(new 조사(formatProvider), format, args);

        public static 문자열 포맷(조사 조사, string format, object arg) => string.Format(조사, format, arg);
        public static 문자열 포맷(조사 조사, string format, params object[] args) => string.Format(조사, format, args);

        /// <inheritdoc cref="ICustomFormatter.Format(문자열, object, IFormatProvider)"/>
        public string? Format(string? format, object arg, IFormatProvider formatProvider)
        {
            // 콜백 유효성 검사
            if (!Equals(formatProvider) || format == null || arg == null)
                return AnotherFormat(AnotherFormatProvider, format, arg);

            // 포맷 유효성 검사
            // 실패시 범용 포맷으로 변환
            if (!format.Contains('/'))
                return AnotherFormat(AnotherFormatProvider, format, arg);

            var stringArg = arg.ToString();

            if (!string.IsNullOrWhiteSpace(stringArg))
                return 처리(stringArg, format);
            else
                return AnotherFormat(AnotherFormatProvider, format, arg);


            static string? AnotherFormat(IFormatProvider? anotherFormatProvider, string format, object arg)
            {
                if (arg is IFormattable formattableArg)
                    return formattableArg.ToString(format, anotherFormatProvider);
                else
                    return null;
            }
        }

        /// <inheritdoc cref="IFormatProvider.GetFormat(Type)"/>
        public object? GetFormat(Type formatType) => formatType == typeof(ICustomFormatter) ? this : null;
    }
}
