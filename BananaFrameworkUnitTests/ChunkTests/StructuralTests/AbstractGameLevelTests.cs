using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BananaFramework.Chunks.Structural;
using BananaFramework.Chunks.Logical;

namespace BananaFrameworkUnitTests.ChunkTests.StructuralTests
{
	// Basic classes for determining if deep type adding works
	public class TestLevel : AbstractGameLevel { }
	public class TestGameObject : AbstractGameObject
	{
		public override void Update() { }
		public override void Render() { }
	}
	public class SubTestGameObject : TestGameObject
	{
		public override void Update() { }
		public override void Render() { }
	}
	[TestClass]
	public class AbstractGameLevelTests
	{
		[TestMethod]
		public void TestRegisterGameObject()
		{
			// Create some test objects
			TestLevel level = new TestLevel();
			TestGameObject testObj = new TestGameObject();
			SubTestGameObject subTestObj = new SubTestGameObject();

			// Test basic object registration
			level.RegisterGameObject(testObj);
			Assert.AreEqual(1, level.GetGameObjectsByType<TestGameObject>().Count);
			Assert.AreEqual(testObj, level.GetGameObjectsByType<TestGameObject>()[0]);

			level.RegisterGameObject(subTestObj);
			Assert.AreEqual(1, level.GetGameObjectsByType<TestGameObject>().Count);
			Assert.AreEqual(1, level.GetGameObjectsByType<SubTestGameObject>().Count);
			Assert.AreEqual(subTestObj, level.GetGameObjectsByType<SubTestGameObject>()[0]);

			level = new TestLevel();

			level.RegisterGameObject(subTestObj, true);
			Assert.AreEqual(1, level.GetGameObjectsByType<TestGameObject>().Count);
			Assert.AreEqual(1, level.GetGameObjectsByType<SubTestGameObject>().Count);
			Assert.AreEqual(subTestObj, level.GetGameObjectsByType<SubTestGameObject>()[0]);
			Assert.AreEqual(subTestObj, level.GetGameObjectsByType<TestGameObject>()[0]);
		}
	}
}
