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
    /// 한국어의 조사를 나타냅니다.
    /// </summary>
    public class 조사 : ICustomFormatter, IFormatProvider
    {
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

        /// <summary>
        /// String interpolation을 이용해 문자열 표현으로 바꿉니다.
        /// </summary>
        /// <param name="문자열"></param>
        /// <returns></returns>
        public static 문자열 보간(FormattableString 문자열) => 문자열.ToString(인스턴스);

        /// <inheritdoc cref="문자열.Format(문자열, object)"/>
        public static 문자열 포맷(문자열 format, object arg0) => string.Format(인스턴스, format, arg0);
        /// <inheritdoc cref="문자열.Format(문자열, object, object)"/>
        public static 문자열 포맷(문자열 format, object arg0, object arg1) => string.Format(인스턴스, format, arg0, arg1);
        /// <inheritdoc cref="문자열.Format(문자열, object, object, object)"/>
        public static 문자열 포맷(문자열 format, object arg0, object arg1, object arg2) => string.Format(인스턴스, format, arg0, arg1, arg2);
        /// <inheritdoc cref="문자열.Format(문자열, object[])"/>
        public static 문자열 포맷(문자열 format, params object[] args) => string.Format(인스턴스, format, args);

        public string? Format(string format, object arg, IFormatProvider formatProvider)
        {
            // 콜백 유효성 검사
            if (!Equals(formatProvider))
                return null;

            var stringArg = arg.ToString();

            if (!string.IsNullOrWhiteSpace(stringArg))
                return 처리(stringArg, format);
            else
                return null;
        }

        public object? GetFormat(Type formatType) => formatType == typeof(ICustomFormatter) ? this : null;
    }
}
