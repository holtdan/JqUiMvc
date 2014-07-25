using BEDataAccess;
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

        [TestMethod]
        public void SiteEvents()
        {
            var e = new BEDataAccess.Engine();

            //using (var dc = new BEDataContext())
            //{
            //    var sites = dc.Sites.ToList();
            //    var visTypes = dc.SiteEvents.Where(r => r.SiteID == 1).Select(s=>s.VisitType);
            //}
        }
    }
}
