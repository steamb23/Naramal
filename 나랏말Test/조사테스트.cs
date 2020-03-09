using System;
using System.Globalization;
using Xunit;

namespace 나랏말.Test
{
    public class 조사테스트
    {
        private string 조사비교용(string 문자열, string 조사0, string 조사1)
        {
            bool 받침있음 = 한글.받침추출(문자열[^1]) != 받침.없음;
            bool ㄹ받침있음 = 한글.받침추출(문자열[^1]) == 받침.ㄹ;

            if (조사0 == "으로")
            {
                return 받침있음 && !ㄹ받침있음 ? 문자열 + 조사0 : 문자열 + 조사1;
            }
            else
            {
                return 받침있음 ? 문자열 + 조사0 : 문자열 + 조사1;
            }
        }
        [Theory]
        [InlineData("사과")]
        [InlineData("과일")]
        [InlineData("우주선")]
        [InlineData("비행기")]
        public void 조사처리(string 문자열)
        {
            bool 받침있음 = 한글.받침추출(문자열[^1]) != 받침.없음;
            bool ㄹ받침있음 = 한글.받침추출(문자열[^1]) == 받침.ㄹ;


            Assert.Equal(조사비교용(문자열, "이", "가"), 조사.처리(문자열, "이/가"));
            Assert.Equal(조사비교용(문자열, "은", "는"), 조사.처리(문자열, "은/는"));
            Assert.Equal(조사비교용(문자열, "을", "를"), 조사.처리(문자열, "을/를"));
            Assert.Equal(조사비교용(문자열, "과", "와"), 조사.처리(문자열, "과/와"));
            Assert.Equal(조사비교용(문자열, "으로", "로"), 조사.처리(문자열, "으로/로"));
        }

        [Theory]
        [InlineData("비행기")]
        [InlineData("서울")]
        [InlineData("우주선")]
        [InlineData("타우마타와카탕이항아코아우아우오타마테아투리푸카카피키마웅아호로누쿠포카이웨누아키타나타후")]
        public void 포맷(string 문자열)
        {
            Assert.Equal(조사비교용(문자열, "은", "는") + " 재밌다.", 조사.포맷("{0:은/는} 재밌다.", 문자열));
            Assert.Equal(조사비교용(문자열, "이", "가") + " 난다.", 조사.포맷("{0:이/가} 난다.", 문자열));
            Assert.Equal(조사비교용(문자열, "으로", "로") + " 가자.", 조사.포맷("{0:으로/로} 가자.", 문자열));
        }

        [Theory]
        [InlineData("안경")]
        [InlineData("빨강색")]
        [InlineData("초코")]
        [InlineData("잉어")]
        public void 보간(string 문자열)
        {
            Assert.Equal(조사비교용(문자열, "과", "와") + " 이거는 안 어울리는데.", 조사.보간($"{문자열:과/와} 이거는 안 어울리는데."));
            Assert.Equal(조사비교용(문자열, "으로", "로") + " 움직여.", 조사.보간($"{문자열:으로/로} 움직여."));
            Assert.Equal("이것은 " +조사비교용(문자열, "이", "") + "다.", 조사.보간($"이것은 {문자열:이/}다."));
        }

        [Theory]
        [InlineData("콩")]
        [InlineData("햄버거")]
        public void 혼합보간(string 문자열)
        {
            int rand = new Random().Next();
            Assert.Equal(조사비교용(문자열, "이", "가") + $" {rand:n0} 개 있다.", 조사.보간($"{문자열:이/가} {rand:n0} 개 있다."));
        }
        
        [Theory]
        [InlineData("벼")]
        [InlineData("밀")]
        public void 혼합보간_문화권(string 문자열)
        {
            DateTime now = DateTime.Now;

            foreach(var cultureInfo in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                Assert.Equal(조사비교용(문자열, "은", "는") + string.Format(cultureInfo, " {0}에 수확되었습니다.", now),
                    조사.보간(cultureInfo, $"{문자열:은/는} {now}에 수확되었습니다."));
            }
        }
    }
}
