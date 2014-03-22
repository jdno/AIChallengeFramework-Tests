using NUnit.Framework;
using System;

using AIChallengeFramework;

namespace AIChallengeFrameworkTests
{
	[TestFixture ()]
	public class RegionTests
	{
		[SetUp ()]
		public void SetUp ()
		{
			// Deactive logger for tests
			Logger.LogLevel = Logger.Severity.OFF;
		}

		[Test ()]
		public void TestAddNeighbor ()
		{
			Continent c1 = new Continent (0, 2);
			Continent c2 = new Continent (1, 5);

			Region r1 = new Region (0, c1);
			Region r2 = new Region (1, c1);
			Region r3 = new Region (2, c2);

			r1.AddNeighbor (r1);

			Assert.AreEqual (0, r1.Neighbors.Count);

			r1.AddNeighbor (r2);

			Assert.AreEqual (1, r1.Neighbors.Count);
			Assert.IsFalse (r1.IsBorderRegion ());

			r1.AddNeighbor (r3);

			Assert.AreEqual (2, r1.Neighbors.Count);
			Assert.IsTrue (r1.IsBorderRegion ());
			Assert.AreEqual (1, c1.BorderRegions.Count);
		}
	}
}

