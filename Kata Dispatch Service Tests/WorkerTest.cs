using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoliceStationDispatchService;
using PoliceStationDispatchService.Dispatch;
using Moq;
using System;
using System.Threading;

namespace Kata_Dispatch_Service_Tests
{
    [TestClass]
    public class WorkerTest
    {

        [TestInitialize]
        public void BeforeEachTest()
        {
           
        }

        [TestMethod]
        public void TestWorker()
        {        
            IWorker worker = new Worker();
            Assert.IsNotNull(worker);
        }

        [TestMethod]
        public void TestWorker_IsIdle_True()
        {
            WorkerTestHarness worker = new WorkerTestHarness();
            Assert.IsTrue(worker.GetIsIdle());
        }

        [TestMethod]
        public void TestHandleCall_IsIdle_False()
        {
            WorkerTestHarness worker = new WorkerTestHarness();
            Call incomingCall = new Call();
            worker.HandleCall(incomingCall);
            Assert.IsFalse(worker.GetIsIdle());
        }

        [TestMethod]
        public void TestHandleCall_IsIdle_Wait_IsTrue()
        {
            WorkerTestHarness worker = new WorkerTestHarness();
            ICall incomingCall = new Call();
            worker.HandleCall(incomingCall);
            worker.ForceWork();
            Assert.IsTrue(worker.GetIsIdle());
        }

        [TestMethod]
        public void TestHandleCall_evReady_Fires()
        {
            var fire = false;
            WorkerTestHarness worker = new WorkerTestHarness();
            worker.evReady +=  (a,b) => { fire = true; };
            ICall incomingCall = new Call();
            worker.HandleCall(incomingCall);
            worker.ForceWork();
            Assert.IsTrue(fire);
        }

        [TestMethod]
        public void TestHandleCall_nullCall_NoException()
        {
            WorkerTestHarness worker = new WorkerTestHarness();
            worker.ForceWork();
            Assert.IsTrue(true);
        }

       
        //does workedcall go up by one?
        //does an exception happen when call is null?
    }
}
