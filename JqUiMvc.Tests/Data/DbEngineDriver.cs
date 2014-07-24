using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JqUiMvc.Tests.Data
{
    [TestClass]
    public class DbEngineDriver
    {
        [TestMethod]
        public void First()
        {
            var e = new BEDataAccess.Engine();

            e.Go();
        }
    }
}
