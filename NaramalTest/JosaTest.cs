using System;
using Xunit;
using Naramal.HangulGraphemes;
using System.Globalization;

namespace Naramal.Test
{
    public class JosaTest
    {
        private string JosaProcessExample(string text, string josa0, string josa1)
        {
            bool hasFinalConsonant = Hangul.ExtractFinalConsonant(text[^1]) != FinalConsonant.None;
            bool hasFinalConsonantWithㄹ = Hangul.ExtractFinalConsonant(text[^1]) == FinalConsonant.ㄹ;

            if (josa0 == "으로")
            {
                return hasFinalConsonant && !hasFinalConsonantWithㄹ ? text + josa0 : text + josa1;
            }
            else
            {
                return hasFinalConsonant ? text + josa0 : text + josa1;
            }
        }

        [Theory]
        [InlineData("사과")]
        [InlineData("과일")]
        [InlineData("우주선")]
        [InlineData("비행기")]
        public void JosaProcess(string text)
        {
            bool hasFinalConsonant = Hangul.ExtractFinalConsonant(text[^1]) != FinalConsonant.None;
            bool hasFinalConsonantWithㄹ = Hangul.ExtractFinalConsonant(text[^1]) != FinalConsonant.ㄹ;


            Assert.Equal(JosaProcessExample(text, "이", "가"), Josa.Process(text, "이/가"));
            Assert.Equal(JosaProcessExample(text, "은", "는"), Josa.Process(text, "은/는"));
            Assert.Equal(JosaProcessExample(text, "을", "를"), Josa.Process(text, "을/를"));
            Assert.Equal(JosaProcessExample(text, "과", "와"), Josa.Process(text, "과/와"));
            Assert.Equal(JosaProcessExample(text, "으로", "로"), Josa.Process(text, "으로/로"));
        }

        [Theory]
        [InlineData("비행기")]
        [InlineData("서울")]
        [InlineData("우주선")]
        [InlineData("타우마타와카탕이항아코아우아우오타마테아투리푸카카피키마웅아호로누쿠포카이웨누아키타나타후")]
        public void Format(string text)
        {
            Assert.Equal(JosaProcessExample(text, "은", "는") + " 재밌다.", Josa.Format("{0:은/는} 재밌다.", text));
            Assert.Equal(JosaProcessExample(text, "이", "가") + " 난다.", Josa.Format("{0:이/가} 난다.", text));
            Assert.Equal(JosaProcessExample(text, "으로", "로") + " 가자.", Josa.Format("{0:으로/로} 가자.", text));
        }

        [Theory]
        [InlineData("안경")]
        [InlineData("빨강색")]
        [InlineData("초코")]
        [InlineData("잉어")]
        public void Interpolation(string text)
        {
            Assert.Equal(JosaProcessExample(text, "과", "와") + " 이거는 안 어울리는데.", Josa.Interpolation($"{text:과/와} 이거는 안 어울리는데."));
            Assert.Equal(JosaProcessExample(text, "으로", "로") + " 움직여.", Josa.Interpolation($"{text:으로/로} 움직여."));
            Assert.Equal("이것은 " + JosaProcessExample(text, "이", "") + "다.", Josa.Interpolation($"이것은 {text:이/}다."));
        }

        [Theory]
        [InlineData("콩")]
        [InlineData("햄버거")]
        public void MixedInterpolation(string text)
        {
            int rand = new Random().Next();
            Assert.Equal(JosaProcessExample(text, "이", "가") + $" {rand:n0} 개 있다.", Josa.Interpolation($"{text:이/가} {rand:n0} 개 있다."));
        }

        [Theory]
        [InlineData("벼")]
        [InlineData("밀")]
        public void CultureMixedInterpolation(string text)
        {
            DateTime now = DateTime.Now;

            foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                Assert.Equal(JosaProcessExample(text, "은", "는") + string.Format(cultureInfo, " {0}에 수확되었습니다.", now),
                    Josa.Interpolation(cultureInfo, $"{text:은/는} {now}에 수확되었습니다."));
            }
        }
    }
}
