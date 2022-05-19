using Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoliceStationDispatchService;
using PoliceStationDispatchService.Dispatch;
using Moq;

namespace Kata_Dispatch_Service_Tests
{
    [TestClass]
    public class DispatchTest
    {
        private IDispatch CreateDispatch()
        {
            var logger = new Mock<ILogger>();
            Dispatch dispatch = new Dispatch(logger.Object);
            return dispatch;
        }
        private IDispatch createDispatchWithCalls(int numberOfCalls)
        {
            var dispatch = CreateDispatch();
            for (int i = 0; i < numberOfCalls; i++)
            {
                dispatch.queueCall(new Call());
            }

            return dispatch;
        }

        [TestMethod]
        public void TestDispatch()
        {
            var dispatch = CreateDispatch();
            Assert.IsNotNull(dispatch);

        }

        [TestMethod]
        public void TestDispatchQueueEmpty()
        {
            var dispatch = CreateDispatch();
            Assert.AreEqual(0, dispatch.callCount());
        }

        [TestMethod]
        public void TestDispatchOneQueueCall()
        {
            var dispatch = CreateDispatch();
            var call = new Call();
            dispatch.queueCall(call);
            Assert.AreEqual(1, dispatch.callCount());
        }

        [TestMethod]
        public void TestDispatchManyQueueCalls()
        {
            var dispatch = createDispatchWithCalls(5);
            Assert.AreEqual(5, dispatch.callCount());
        }

        [TestMethod]
        public void TestDispatchDequeueZeroCall()
        {
            var dispatch = createDispatchWithCalls(0);
            dispatch.DequeueCall();
            Assert.AreEqual(0, dispatch.callCount());
        }

        [TestMethod]
        public void TestDispatchDequeueZeroCallReturnsNull()
        {
            var dispatch = createDispatchWithCalls(0);
            var call = dispatch.DequeueCall();
            Assert.IsNull(call);
        }

        [TestMethod]
        public void TestDispatchDequeueOneCall()
        {
            var dispatch = createDispatchWithCalls(1);
            dispatch.DequeueCall();
            Assert.AreEqual(0, dispatch.callCount());
        }

        [TestMethod]
        public void TestDispatchDequeueOneCallOnlyDequeuesOneCall()
        {
            var queueCount = 3;
            var dispatch = createDispatchWithCalls(queueCount);
            dispatch.DequeueCall();
            Assert.AreEqual(queueCount - 1, dispatch.callCount());
        }


        [TestMethod]
        public void TestDispatchDequeueCallAdheresToFirstInFirstOut()
        {

            var dispatch = CreateDispatch();
            Call callOne = new Call();
            Call callTwo= new Call();
            Call callThree = new Call();

            dispatch.queueCall(callOne);
            dispatch.queueCall(callTwo);
            dispatch.queueCall(callThree);

            var returnedCall = dispatch.DequeueCall();
            Assert.AreEqual(callOne.Id, returnedCall.Id);
        }

        [TestMethod]
        public void TestDispatchDequeueAllCalls()
        { 
            var dispatch = createDispatchWithCalls(5);

            for(var c = 0; c < 5; c++)
            {
                dispatch.DequeueCall();
            }

            Assert.AreEqual(0, dispatch.callCount());
        }

        [TestMethod]
        public void TestDispatchDequeueManyCalls()
        {
            var dispatch = createDispatchWithCalls(5);

            for (var c = 0; c < 3; c++)
            {
                dispatch.DequeueCall();
            }

            Assert.AreEqual(2, dispatch.callCount());
        }

        [TestMethod]
        public void TestDispatchEnqueueCallAfterDequeueCall()
        {
            var dispatch = CreateDispatch();
            dispatch.queueCall(new Call());
            dispatch.queueCall(new Call());
            dispatch.queueCall(new Call());
            dispatch.DequeueCall();
            dispatch.queueCall(new Call());

            Assert.AreEqual(3, dispatch.callCount());
        }
    }
}
