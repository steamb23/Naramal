using System;
using System.Collections.Generic;
using System.Text;
using Naramal.HangulGraphemes;
using Xunit;

namespace Naramal.Test
{
    public class HangulTest
    {
        [Fact]
        public void MakeShortVowel()
        {
            for (int i = 0; i < Hangul.VowelCount; i++)
            {
                Vowel vowel = (Vowel)i;
                Vowel shortVowel = Hangul.MakeShortVowel(vowel);
                switch (vowel)
                {
                    case Vowel.ㅏ:
                        Assert.Equal(Vowel.ㅏ, shortVowel);
                        break;
                    case Vowel.ㅐ:
                        Assert.Equal(Vowel.ㅐ, shortVowel);
                        break;
                    case Vowel.ㅑ:
                        Assert.Equal(Vowel.ㅏ, shortVowel);
                        break;
                    case Vowel.ㅒ:
                        Assert.Equal(Vowel.ㅐ, shortVowel);
                        break;
                    case Vowel.ㅓ:
                        Assert.Equal(Vowel.ㅓ, shortVowel);
                        break;
                    case Vowel.ㅔ:
                        Assert.Equal(Vowel.ㅔ, shortVowel);
                        break;
                    case Vowel.ㅕ:
                        Assert.Equal(Vowel.ㅓ, shortVowel);
                        break;
                    case Vowel.ㅖ:
                        Assert.Equal(Vowel.ㅔ, shortVowel);
                        break;
                    case Vowel.ㅗ:
                        Assert.Equal(Vowel.ㅗ, shortVowel);
                        break;
                    case Vowel.ㅘ:
                        Assert.Equal(Vowel.ㅏ, shortVowel);
                        break;
                    case Vowel.ㅙ:
                        Assert.Equal(Vowel.ㅐ, shortVowel);
                        break;
                    case Vowel.ㅚ:
                        Assert.Equal(Vowel.ㅔ, shortVowel);
                        break;
                    case Vowel.ㅛ:
                        Assert.Equal(Vowel.ㅗ, shortVowel);
                        break;
                    case Vowel.ㅜ:
                        Assert.Equal(Vowel.ㅜ, shortVowel);
                        break;
                    case Vowel.ㅝ:
                        Assert.Equal(Vowel.ㅓ, shortVowel);
                        break;
                    case Vowel.ㅞ:
                        Assert.Equal(Vowel.ㅔ, shortVowel);
                        break;
                    case Vowel.ㅟ:
                        Assert.Equal(Vowel.ㅣ, shortVowel);
                        break;
                    case Vowel.ㅠ:
                        Assert.Equal(Vowel.ㅜ, shortVowel);
                        break;
                    case Vowel.ㅡ:
                        Assert.Equal(Vowel.ㅡ, shortVowel);
                        break;
                    case Vowel.ㅢ:
                        Assert.Equal(Vowel.ㅣ, shortVowel);
                        break;
                    case Vowel.ㅣ:
                        Assert.Equal(Vowel.ㅣ, shortVowel);
                        break;
                }
            }
        }

        [Fact]
        public void Stretch()
        {
            Assert.Equal("으아악", Hangul.Stretch("으악"));
            Assert.Equal("안녀어어어엉", Hangul.Stretch("안녕", 4));
        }
    }
}
