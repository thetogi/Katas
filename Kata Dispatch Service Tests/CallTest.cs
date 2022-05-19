using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoliceStationDispatchService;
using System;
using System.Runtime.CompilerServices;



namespace Kata_Dispatch_Service_Tests
{
    [TestClass]
    public class CallTest
    {
        [TestMethod]
        public void TestCall()
        {
            var call = new Call();
            Assert.IsNotNull(call);

        }
    }
}
