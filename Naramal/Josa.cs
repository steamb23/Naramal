using Naramal.HangulGraphemes;
using System;

namespace Naramal
{
    /// <summary>
    /// 한국어의 조사를 나타냅니다.
    /// </summary>
    public class Josa : ICustomFormatter, IFormatProvider
    {

        /// <summary>
        /// 조사 클래스의 인스턴스를 초기화합니다.
        /// </summary>
        public Josa()
        {

        }

        /// <summary>
        /// 조사 클래스의 인스턴스를 초기화합니다. 매개 변수에서 문화권별 서식 지정 정보를 제공합니다.
        /// </summary>
        /// <param name="anotherFormatProvider">문화권별 형식 정보를 제공하는 개체입니다.</param>
        public Josa(IFormatProvider anotherFormatProvider)
        {
            this.AnotherFormatProvider = anotherFormatProvider;
        }

        /// <summary>
        /// 또다른 포맷 제공자의 인스턴스를 가져옵니다.
        /// </summary>
        public IFormatProvider AnotherFormatProvider { get; } = null;

        private static volatile Josa defaultInstance;

        /// <summary>
        /// <see cref="Josa"/> 클래스에 대한 기본 인스턴스를 가져옵니다.
        /// </summary>
        public static Josa Default
        {
            get
            {
                if (defaultInstance == null)
                    defaultInstance = new Josa();
                return defaultInstance;
            }
        }

        /// <summary>
        /// 지정된 단어에 맞는 조사를 자동으로 선택합니다.
        /// </summary>
        /// <param name="word">조사가 붙을 단어입니다.</param>
        /// <param name="josa">단어에 붙일 조사입니다.</param>
        /// <returns>선택된 조사입니다.</returns>
        public static string Select(string word, string josa)
        {
            var finalConsonant = Hangul.ExtractFinalConsonant(word[word.Length - 1]);
            switch (josa)
            {
                case "으로/로":
                    if (finalConsonant == FinalConsonant.None || finalConsonant == FinalConsonant.ㄹ)
                        return "로";
                    else
                        return "으로";
                default:
                    string[] splitedJosa = josa.Split('/');
                    if (finalConsonant == FinalConsonant.None)
                        return splitedJosa[1];
                    else
                        return splitedJosa[0];
            }
        }

        /// <summary>
        /// 지정된 단어에 맞는 조사를 자동으로 선택하고 붙입니다.
        /// </summary>
        /// <param name="word">조사가 붙을 단어입니다.</param>
        /// <param name="josa">단어에 붙일 조사입니다.</param>
        /// <returns>단어와 자동 선택된 조사가 결합된 문자열입니다.</returns>
        public static string Process(string word, string josa) => word + Select(word, josa);

        /// <summary>
        /// 문자열 보간 방식을 사용해 문자열 표현으로 바꿉니다.
        /// </summary>
        /// <param name="formattableString">보간될 문자열입니다.</param>
        /// <returns>보간된 문자열입니다.</returns>
        public static string Interpolation(FormattableString formattableString) => formattableString.ToString(Default);

        /// <summary>
        /// 문자열 보간 방식을 사용해 문자열 표현으로 바꿉니다. 매개 변수에서 문화권별 서식 지정 정보를 제공합니다.
        /// </summary>
        /// <param name="formatProvider">문화권별 형식 정보를 제공하는 개체입니다.</param>
        /// <param name="formattableString">보간될 문자열입니다.</param>
        /// <returns>보간된 문자열입니다.</returns>
        public static string Interpolation(IFormatProvider formatProvider, FormattableString formattableString) => formattableString.ToString(new Josa(formatProvider));

        /// <summary>
        /// 문자열 보간 방식을 사용해 문자열 표현으로 바꿉니다. 매개 변수에서 문화권별 서식 지정 정보를 가진 <see cref="Josa"/> 개체를 제공합니다.
        /// </summary>
        /// <param name="josa">문화권별 형식 정보를 가진 <see cref="Josa"/> 개체입니다.</param>
        /// <param name="formattableString">보간될 문자열입니다.</param>
        /// <returns>보간된 문자열입니다.</returns>
        public static string Interpolation(Josa josa, FormattableString formattableString) => formattableString.ToString(josa);

        /// <summary>
        /// 지정된 문자열에 있는 형식 항목을 지정된 개체의 문자열 표현으로 바꿉니다.
        /// </summary>
        /// <param name="format">조사를 포함하는 복합 형식 문자열입니다.</param>
        /// <param name="arg">형식을 지정할 개체입니다.</param>
        /// <returns>변환된 문자열입니다.</returns>
        public static string Format(string format, object arg) => string.Format(Default, format, arg);

        /// <summary>
        /// 지정된 문자열에 있는 형식 항목을 지정된 배열에 있는 해당 개체의 문자열 표현으로 바꿉니다.
        /// </summary>
        /// <param name="format">조사를 포함하는 복합 형식 문자열입니다.</param>
        /// <param name="args">형식을 지정할 개체의 배열입니다.</param>
        /// <returns></returns>
        public static string Format(string format, params object[] args) => string.Format(Default, format, args);

        /// <summary>
        /// 지정된 문자열에 있는 형식 항목을 지정된 개체의 문자열 표현으로 바꿉니다. 매개 변수에서 문화권별 서식 지정 정보를 제공합니다.
        /// </summary>
        /// <param name="formatProvider">문화권별 형식 정보를 제공하는 개체입니다.</param>
        /// <param name="format">조사를 포함하는 복합 형식 문자열입니다.</param>
        /// <param name="arg">형식을 지정할 개체입니다.</param>
        /// <returns>변환된 문자열입니다.</returns>
        public static string Format(IFormatProvider formatProvider, string format, object arg) => string.Format(new Josa(formatProvider), format, arg);

        /// <summary>
        /// 지정된 문자열에 있는 형식 항목을 지정된 배열에 있는 해당 개체의 문자열 표현으로 바꿉니다. 매개 변수에서 문화권별 서식 지정 정보를 제공합니다.
        /// </summary>
        /// <param name="formatProvider">문화권별 형식 정보를 제공하는 개체입니다.</param>
        /// <param name="format">조사를 포함하는 복합 형식 문자열입니다.</param>
        /// <param name="args">형식을 지정할 개체의 배열입니다.</param>
        /// <returns>변환된 문자열입니다.</returns>
        public static string Format(IFormatProvider formatProvider, string format, params object[] args) => string.Format(new Josa(formatProvider), format, args);

        /// <summary>
        /// 지정된 문자열에 있는 형식 항목을 지정된 개체의 문자열 표현으로 바꿉니다. 매개 변수에서 문화권별 서식 지정 정보를 가진 <see cref="Josa"/> 개체를 제공합니다.
        /// </summary>
        /// <param name="조사">문화권별 형식 정보를 가진 <see cref="Josa"/> 개체입니다.</param>
        /// <param name="format">조사를 포함하는 복합 형식 문자열입니다.</param>
        /// <param name="arg">형식을 지정할 개체입니다.</param>
        /// <returns>변환된 문자열입니다.</returns>

        public static string Format(Josa 조사, string format, object arg) => string.Format(조사, format, arg);

        /// <summary>
        /// 지정된 문자열에 있는 형식 항목을 지정된 배열에 있는 해당 개체의 문자열 표현으로 바꿉니다. 매개 변수에서 문화권별 서식 지정 정보를 가진 <see cref="Josa"/> 개체를 제공합니다.
        /// </summary>
        /// <param name="조사">문화권별 형식 정보를 가진 <see cref="Josa"/> 개체입니다.</param>
        /// <param name="format">조사를 포함하는 복합 형식 문자열입니다.</param>
        /// <param name="args">형식을 지정할 개체의 배열입니다.</param>
        /// <returns>변환된 문자열입니다.</returns>
        public static string Format(Josa 조사, string format, params object[] args) => string.Format(조사, format, args);

        /// <inheritdoc cref="ICustomFormatter.Format(string, object, IFormatProvider)"/>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            // 콜백 유효성 검사
            if (!Equals(formatProvider) || format == null || arg == null)
                return AnotherFormat(AnotherFormatProvider);

            // 포맷 유효성 검사
            // 실패시 범용 포맷으로 변환
            if (!format.Contains("/"))
                return AnotherFormat(AnotherFormatProvider);

            var stringArg = arg.ToString();

            if (!string.IsNullOrWhiteSpace(stringArg))
                return Process(stringArg, format);
            else
                return AnotherFormat(AnotherFormatProvider);


            string AnotherFormat(IFormatProvider anotherFormatProvider)
            {
                if (arg is IFormattable formattableArg)
                    return formattableArg.ToString(format, anotherFormatProvider);
                else
                    return null;
            }
        }

        /// <inheritdoc cref="IFormatProvider.GetFormat(Type)"/>
        public object GetFormat(Type formatType) => formatType == typeof(ICustomFormatter) ? this : null;
    }
}
