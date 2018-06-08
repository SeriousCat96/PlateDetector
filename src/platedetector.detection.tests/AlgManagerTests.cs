using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Platedetector.Detection.Tests
{
    [TestClass]
    public class AlgManagerTests
    {
        [TestMethod]
        public void Select_ChangeAlgorithmFromDummyToFakeWithDummyAtFirstPosition_SelectedAlgorithmIsFake()
        {
            // arrange
            var manager = new AlgManager(new DummyAlgProvider(), new FakeAlgProvider());
            var expectedType = typeof(FakeAlg);

            //act
            manager.Select(typeof(FakeAlg));
            var result = manager.SelectedAlgorithm.GetType();


            Assert.AreEqual(expectedType, result);
        }

        [TestMethod]
        public void Select_ChangeAlgorithmFromFakeToDummyWithDummyAtFirstPosition_SelectedAlgorithmIsDummy()
        {
            // arrange
            var manager = new AlgManager(new DummyAlgProvider(), new FakeAlgProvider());
            var expectedType = typeof(DummyAlg);
            var t = typeof(AlgManager);

            PropertyInfo info = t.GetProperty("SelectedAlgorithm");
            info.SetValue(manager, manager.Algorithms[1]);

            //act
            manager.Select(typeof(DummyAlg));
            var result = manager.SelectedAlgorithm.GetType();


            Assert.AreEqual(expectedType, result);
        }

        [TestMethod]
        public void Select_ChangeAlgorithmFromDummyToFakeWithFakeNotExist_ThrowsArgumentException()
        {
            // arrange
            var manager = new AlgManager(new DummyAlgProvider());

            //act
            var action = new Action(() => 
            {
                manager.Select(typeof(FakeAlg));
            });


            Assert.ThrowsException<ArgumentException>(action);
        }
    }
}

