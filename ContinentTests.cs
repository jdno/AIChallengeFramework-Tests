//
//  Copyright 2014  jdno
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using NUnit.Framework;
using System;
using System.Collections.Generic;

using AIChallengeFramework;

namespace AIChallengeFrameworkTests
{
	[TestFixture ()]
	public class ContinentTests
	{
		[SetUp ()]
		public void SetUp ()
		{
			// Deactive logger for tests
			Logger.LogLevel = Logger.Severity.OFF;
		}

		[Test ()]
		public void TestAddRegion ()
		{
			Continent c1 = new Continent (0, 2);

			// AddRegion() is called during initialization
			new Region (0, c1);

			Assert.AreEqual (1, c1.Regions.Count);
			Assert.AreEqual (0, c1.BorderRegions.Count);
		}

		[Test ()]
		public void TestAddRegionWithDuplicate ()
		{
			Continent c1 = new Continent (0, 2);

			// AddRegion() is called during initialization
			Region r1 = new Region (0, c1);

			Assert.AreEqual (1, c1.Regions.Count);

			c1.AddRegion (r1);

			Assert.AreEqual (1, c1.Regions.Count);
		}

		[Test ()]
		public void TestOwnedBy ()
		{
			Continent c1 = new Continent (0, 2);
			Region r1 = new Region (0, c1);

			Assert.AreEqual (r1.Owner, c1.OwnedBy ());
		}

		[Test ()]
		public void TestOwnedByNoOne ()
		{
			Continent c1 = new Continent (0, 2);
			new Region (0, c1);

			Region r2 = new Region (1, c1);
			r2.Owner = "Russia maybe?";

			Assert.AreEqual ("unkown", c1.OwnedBy ());
		}

		[Test ()]
		public void TestNumberOfBorderTerritories ()
		{
			Continent c1 = new Continent (0, 2);
			Continent c2 = new Continent (1, 5);

			Region r1 = new Region (0, c1);
			Region r2 = new Region (1, c1);
			Region r3 = new Region (2, c1);
			Region r4 = new Region (3, c2);

			r1.AddNeighbor (r2);
			r1.AddNeighbor (r3);
			r2.AddNeighbor (r3);
			r1.AddNeighbor (r4);

			Assert.AreEqual (1, c1.NumberOfBorderTerritories ());
		}

		[Test ()]
		public void TestNumberOfInvasionPaths ()
		{
			Continent c1 = new Continent (0, 2);
			Continent c2 = new Continent (1, 5);

			Region r1 = new Region (0, c1);
			Region r2 = new Region (1, c1);
			Region r3 = new Region (2, c1);
			Region r4 = new Region (3, c2);

			r1.AddNeighbor (r2);
			r1.AddNeighbor (r3);
			r2.AddNeighbor (r3);
			r1.AddNeighbor (r4);

			Assert.AreEqual (1, c1.NumberOfInvasionPaths ());

			r3.AddNeighbor (r4);

			Assert.AreEqual (2, c1.NumberOfInvasionPaths ());
		}

		[Test ()]
		public void TestEquals ()
		{
			Continent c1 = new Continent (0, 2);
			Continent c2 = new Continent (0, 3);
			Continent c3 = new Continent (1, 5);
			Continent c4 = new Continent (1, 5);

			Assert.AreEqual (c1, c2);
			Assert.AreNotEqual (c1, c3);
			Assert.AreEqual (c3, c4);
		}

		[Test ()]
		public void TestHashCode ()
		{
			List<Continent> continents = new List<Continent> ();
			Continent c1 = new Continent (0, 2);
			Continent c2 = new Continent (1, 4);

			continents.Add (c1);

			Assert.AreEqual (1, continents.Count);
			Assert.AreSame (c1, continents [0]);

			continents.Add (c2);

			Assert.AreEqual (2, continents.Count);
			Assert.AreSame (c2, continents [1]);

			int i = 0;

			foreach (Continent c in continents) {
				i++;
			}

			Assert.AreEqual (2, i);
		}
	}
}

