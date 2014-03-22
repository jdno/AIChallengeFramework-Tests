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

using AIChallengeFramework;

namespace AIChallengeFrameworkTests
{
	[TestFixture ()]
	public class MapTests
	{
		[SetUp ()]
		public void SetUp ()
		{
			// Deactive logger for tests
			Logger.LogLevel = Logger.Severity.OFF;
		}

		[Test ()]
		public void TestAddContinent ()
		{
			Map m1 = new Map ();
			Continent c1 = new Continent (0, 2);

			m1.AddContinent (c1);
			Assert.AreEqual (1, m1.Continents.Count);
		}

		[Test ()]
		public void TestAddContinentWithDuplicate ()
		{
			Map m1 = new Map ();
			Continent c1 = new Continent (0, 2);

			m1.AddContinent (c1);
			Assert.AreEqual (1, m1.Continents.Count);

			m1.AddContinent (c1);
			Assert.AreEqual (1, m1.Continents.Count);
		}

		[Test ()]
		public void TestAddRegion ()
		{
			Map m1 = new Map ();
			Continent c1 = new Continent (0, 2);
			Continent c2 = new Continent (1, 5);
			new Region (0, c1);
			Region r2 = new Region (1, c2);

			m1.AddContinent (c1);

			Assert.AreEqual (1, m1.Continents.Count);
			Assert.AreEqual (1, m1.Regions.Count);

			m1.AddRegion (r2);

			Assert.AreEqual (2, m1.Continents.Count);
			Assert.AreEqual (2, m1.Regions.Count);
		}

		[Test ()]
		public void TestAddRegionWithDuplicate ()
		{
			Map m1 = new Map ();
			Continent c1 = new Continent (0, 2);
			Region r1 = new Region (0, c1);

			m1.AddContinent (c1);

			Assert.AreEqual (1, m1.Continents.Count);
			Assert.AreEqual (1, m1.Regions.Count);

			m1.AddRegion (r1);

			Assert.AreEqual (1, m1.Continents.Count);
			Assert.AreEqual (1, m1.Regions.Count);
		}
	}
}

