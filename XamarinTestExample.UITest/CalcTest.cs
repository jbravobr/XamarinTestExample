using System;
using NUnit.Framework;

namespace XamarinTestExample.UITest
{
    [TestFixture]
    public class CalcTest
    {
        [Test]
        public void Testa_Soma_Com_Letras()
        {
            int num1 = -1;
            int num2 = 2;

            var result = num1 % num2;

            Assert.IsTrue(result >= 0, "Erro");
        }
    }
}
