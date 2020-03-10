using System;
using Xunit;

namespace 나랏말.Test
{
    public class 한글테스트
    {
        [Fact]
        public void 단모음화()
        {
            for(int i = 0; i < 한글.모음갯수; i++)
            {
                모음 모음 = (모음)i;
                모음 단모음 = 한글.단모음화(모음);
                switch (모음)
                {
                    case 모음.ㅏ:
                        Assert.Equal(모음.ㅏ, 단모음);
                        break;
                    case 모음.ㅐ:
                        Assert.Equal(모음.ㅐ, 단모음);
                        break;
                    case 모음.ㅑ:
                        Assert.Equal(모음.ㅏ, 단모음);
                        break;
                    case 모음.ㅒ:
                        Assert.Equal(모음.ㅐ, 단모음);
                        break;
                    case 모음.ㅓ:
                        Assert.Equal(모음.ㅓ, 단모음);
                        break;
                    case 모음.ㅔ:
                        Assert.Equal(모음.ㅔ, 단모음);
                        break;
                    case 모음.ㅕ:
                        Assert.Equal(모음.ㅓ, 단모음);
                        break;
                    case 모음.ㅖ:
                        Assert.Equal(모음.ㅔ, 단모음);
                        break;
                    case 모음.ㅗ:
                        Assert.Equal(모음.ㅗ, 단모음);
                        break;
                    case 모음.ㅘ:
                        Assert.Equal(모음.ㅏ, 단모음);
                        break;
                    case 모음.ㅙ:
                        Assert.Equal(모음.ㅐ, 단모음);
                        break;
                    case 모음.ㅚ:
                        Assert.Equal(모음.ㅔ, 단모음);
                        break;
                    case 모음.ㅛ:
                        Assert.Equal(모음.ㅗ, 단모음);
                        break;
                    case 모음.ㅜ:
                        Assert.Equal(모음.ㅜ, 단모음);
                        break;
                    case 모음.ㅝ:
                        Assert.Equal(모음.ㅓ, 단모음);
                        break;
                    case 모음.ㅞ:
                        Assert.Equal(모음.ㅔ, 단모음);
                        break;
                    case 모음.ㅟ:
                        Assert.Equal(모음.ㅣ, 단모음);
                        break;
                    case 모음.ㅠ:
                        Assert.Equal(모음.ㅜ, 단모음);
                        break;
                    case 모음.ㅡ:
                        Assert.Equal(모음.ㅡ, 단모음);
                        break;
                    case 모음.ㅢ:
                        Assert.Equal(모음.ㅣ, 단모음);
                        break;
                    case 모음.ㅣ:
                        Assert.Equal(모음.ㅣ, 단모음);
                        break;
                }
            }
        }

        [Fact]
        public void 문장늘이기()
        {
            Assert.Equal("으아악", 한글.문장늘이기("으악"));
            Assert.Equal("안녀어어어엉", 한글.문장늘이기("안녕", 4));
        }
    }
}
