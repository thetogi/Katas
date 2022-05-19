using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoliceStationDispatchService;
using PoliceStationDispatchService.Dispatch;
using Moq;
using System;
using Logger;

namespace Kata_Dispatch_Service_Tests
{
    [TestClass]
    public class PoliceStationTest
    {
        private ILogger _logger;
        private IPoliceStation _policeStation;
        //TODO: this may cause flakey tests
        private Mock<IDispatch> _policeDispatchServiceMock;

        [TestInitialize]
        public void BeforeEachTest()
        {
            _logger = new Mock<ILogger>().Object;
            _policeDispatchServiceMock = new Mock<IDispatch>();
            _policeStation = new PoliceStation(_logger, _policeDispatchServiceMock.Object);
        }

        [TestMethod]
        public void TestPoliceStation()
        {
            Assert.IsNotNull(_policeStation);
        }

        [TestMethod]
        public void TestPoliceStation_RegisterPatrolCar_One()
        {
            _policeStation.RegisterPatrolCar(new Worker());
            Assert.AreEqual(1, _policeStation.PatrolCars.Count);
        }

        [TestMethod]
        public void TestPoliceStation_RegisterPatrolCar_Many()
        {
            var policeDispatchServiceMock = new Mock<IDispatch>();
            for (var i = 0; i < 10; i++)
            {
                _policeStation.RegisterPatrolCar(new Worker());
            }
            Assert.AreEqual(10, _policeStation.PatrolCars.Count);
        }

        [TestMethod]
        public void TestPoliceStation_RegisterPatrolCar_Zero()
        {
            Assert.AreEqual(0, _policeStation.PatrolCars.Count);
        }

        [TestMethod]
        public void TestPoliceStation_DispatchPatrolCar_One()
        {            
            var patrolCarMock = new Mock<IWorker>();
            _policeStation.RegisterPatrolCar(patrolCarMock.Object);
            _policeStation.DispatchPatrolCar();
            Assert.AreEqual(1, _policeStation.PatrolCars.Count);
        }

        [TestMethod]
        public void TestPoliceStation_DispatchPatrolCar_Many()
        {
            for (var i = 0; i < 10; i++)
            {
                var patrolCarMock = new Mock<IWorker>();
                _policeStation.RegisterPatrolCar(patrolCarMock.Object);
            }

            for (var i = 0; i < 10; i++)
            {
                _policeStation.DispatchPatrolCar();
            }
            Assert.AreEqual(1, _policeStation.PatrolCars.Count);
        }

        [TestMethod]
        public void TestPoliceStation_DispatchPatrolCar_Zero()
        {           
            _policeStation.DispatchPatrolCar();
            Assert.AreEqual(0, _policeStation.PatrolCars.Count);
        }

    }
}
