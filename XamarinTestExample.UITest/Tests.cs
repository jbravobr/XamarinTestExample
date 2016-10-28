using System;
using System.Linq;

using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XamarinTestExample.UITest
{
    //[TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp _app;
        Platform _platform;

        static readonly Func<AppQuery, AppQuery> btnSaveUserInfo =
            c => c.Marked("btnSaveUserInfo");
        
        static readonly Func<AppQuery, AppQuery> bnConfirmUserInfo =
            c => c.Marked("bnConfirmUserInfo");
        
        static readonly Func<AppQuery, AppQuery> btnBackToUserInfo = 
            c => c.Marked("btnBackToUserInfo");

        public Tests(Platform platform)
        {
            _platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(_platform);
        }

        [Test]
        public void AppLaunches()
        {
            _app.Screenshot("Tirando screenshoot.");

            //Localizando o item com a TAG AutomationId com valor txtUsername
            //e aplicando um valor para a mesma
            _app.EnterText(c => c.Marked("txtUsername"), "Rodrigo");

            _app.EnterText(c => c.Marked("txtLastname"), "Amaro");
            _app.EnterText(c => c.Marked("txtBirthday"), "21/07/1983");
            _app.EnterText(c => c.Marked("txtEmail"), "jbravo.br@gmail.com");
            _app.EnterText(c => c.Marked("txtCity"), "Rio de Janeiro");

            _app.Tap(btnSaveUserInfo);

            _app.Screenshot("Delay");
            _app.Screenshot("Delay");

            //var result = _app.Query("lblConfirmMsg");
            //Assert.IsTrue(result.Any(), "A mensagem de confirmação não apareceu.");
        }
    }
}
