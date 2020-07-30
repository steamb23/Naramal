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
            bool hasFinalConsonantWith�� = Hangul.ExtractFinalConsonant(text[^1]) == FinalConsonant.��;

            if (josa0 == "����")
            {
                return hasFinalConsonant && !hasFinalConsonantWith�� ? text + josa0 : text + josa1;
            }
            else
            {
                return hasFinalConsonant ? text + josa0 : text + josa1;
            }
        }

        [Theory]
        [InlineData("���")]
        [InlineData("����")]
        [InlineData("���ּ�")]
        [InlineData("�����")]
        public void JosaProcess(string text)
        {
            bool hasFinalConsonant = Hangul.ExtractFinalConsonant(text[^1]) != FinalConsonant.None;
            bool hasFinalConsonantWith�� = Hangul.ExtractFinalConsonant(text[^1]) != FinalConsonant.��;


            Assert.Equal(JosaProcessExample(text, "��", "��"), Josa.Process(text, "��/��"));
            Assert.Equal(JosaProcessExample(text, "��", "��"), Josa.Process(text, "��/��"));
            Assert.Equal(JosaProcessExample(text, "��", "��"), Josa.Process(text, "��/��"));
            Assert.Equal(JosaProcessExample(text, "��", "��"), Josa.Process(text, "��/��"));
            Assert.Equal(JosaProcessExample(text, "����", "��"), Josa.Process(text, "����/��"));
        }

        [Theory]
        [InlineData("�����")]
        [InlineData("����")]
        [InlineData("���ּ�")]
        [InlineData("Ÿ�츶Ÿ��ī�����׾��ھƿ�ƿ��Ÿ���׾�����Ǫīī��Ű������ȣ�δ�����ī�̿�����ŰŸ��Ÿ��")]
        public void Format(string text)
        {
            Assert.Equal(JosaProcessExample(text, "��", "��") + " ��մ�.", Josa.Format("{0:��/��} ��մ�.", text));
            Assert.Equal(JosaProcessExample(text, "��", "��") + " ����.", Josa.Format("{0:��/��} ����.", text));
            Assert.Equal(JosaProcessExample(text, "����", "��") + " ����.", Josa.Format("{0:����/��} ����.", text));
        }

        [Theory]
        [InlineData("�Ȱ�")]
        [InlineData("������")]
        [InlineData("����")]
        [InlineData("�׾�")]
        public void Interpolation(string text)
        {
            Assert.Equal(JosaProcessExample(text, "��", "��") + " �̰Ŵ� �� ��︮�µ�.", Josa.Interpolation($"{text:��/��} �̰Ŵ� �� ��︮�µ�."));
            Assert.Equal(JosaProcessExample(text, "����", "��") + " ������.", Josa.Interpolation($"{text:����/��} ������."));
            Assert.Equal("�̰��� " + JosaProcessExample(text, "��", "") + "��.", Josa.Interpolation($"�̰��� {text:��/}��."));
        }

        [Theory]
        [InlineData("��")]
        [InlineData("�ܹ���")]
        public void MixedInterpolation(string text)
        {
            int rand = new Random().Next();
            Assert.Equal(JosaProcessExample(text, "��", "��") + $" {rand:n0} �� �ִ�.", Josa.Interpolation($"{text:��/��} {rand:n0} �� �ִ�."));
        }

        [Theory]
        [InlineData("��")]
        [InlineData("��")]
        public void CultureMixedInterpolation(string text)
        {
            DateTime now = DateTime.Now;

            foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                Assert.Equal(JosaProcessExample(text, "��", "��") + string.Format(cultureInfo, " {0}�� ��Ȯ�Ǿ����ϴ�.", now),
                    Josa.Interpolation(cultureInfo, $"{text:��/��} {now}�� ��Ȯ�Ǿ����ϴ�."));
            }
        }
    }
}
