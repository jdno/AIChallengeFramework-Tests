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
	public class StateTests
	{
		[SetUp ()]
		public void SetUp ()
		{
			// Deactive logger for tests
			Logger.LogLevel = Logger.Severity.DEBUG;
		}

		[Test ()]
		public void TestInitialization ()
		{
			State state = new State ();

			Assert.IsNotNull (state.MyBot);
			Assert.IsNotNull (state.EnemyBot);
			Assert.IsNotNull (state.CompleteMap);
			Assert.IsNotNull (state.VisibleMap);
		}

		[Test ()]
		public void TestCheckRewards ()
		{
			State state = new State ();
			state.MyBot.Name = "player1";
			state.EnemyBot.Name = "player2";

			Continent c1 = new Continent (0, 2);
			Continent c2 = new Continent (1, 5);
			Continent c3 = new Continent (2, 3);

			Region r1 = new Region (0, c1);
			r1.Owner = "player1";
			Region r2 = new Region (1, c2);
			r2.Owner = "player2";
			Region r3 = new Region (2, c3);
			r3.Owner = "player1";
			Region r4 = new Region (3, c3);
			r4.Owner = "player2";

			state.VisibleMap.AddContinent (c1);
			state.VisibleMap.AddContinent (c2);
			state.VisibleMap.AddContinent (c3);

			Assert.AreEqual (3, state.VisibleMap.Continents.Count);
			Assert.AreEqual (4, state.VisibleMap.Regions.Count);
			Assert.AreEqual (5, state.MyBot.ArmiesPerTurn);
			Assert.AreEqual (5, state.EnemyBot.ArmiesPerTurn);

			state.CheckRewards ();

			Assert.IsTrue (state.MyBot.OwnsContinent (c1));
			Assert.IsTrue (state.EnemyBot.OwnsContinent (c2));
			Assert.AreEqual (7, state.MyBot.ArmiesPerTurn);
			Assert.AreEqual (10, state.EnemyBot.ArmiesPerTurn);
			Assert.IsFalse (state.MyBot.OwnsContinent (c3));
			Assert.IsFalse (state.EnemyBot.OwnsContinent (c3));

			r2.Owner = "player1";
			r3.Owner = "player2";
			state.CheckRewards ();

			Assert.IsTrue (state.MyBot.OwnsContinent (c1));
			Assert.IsTrue (state.MyBot.OwnsContinent (c2));
			Assert.IsFalse (state.EnemyBot.OwnsContinent (c2));
			Assert.IsTrue (state.EnemyBot.OwnsContinent (c3));
			Assert.AreEqual (12, state.MyBot.ArmiesPerTurn);
			Assert.AreEqual (8, state.EnemyBot.ArmiesPerTurn);
		}

		[Test ()]
		public void TestProcessAttackTransferMove ()
		{
			State state = new State ();

			Continent c1 = new Continent (0, 2);
			Region r1 = new Region (0, c1);
			Region r2 = new Region (1, c1);
			state.VisibleMap.AddContinent (c1);

			r1.Owner = "player1";
			r2.Owner = "player1";

			r1.Armies = 5;
			r2.Armies = 2;

			AttackTransferMove move = new AttackTransferMove ("player", r1, r2, 3);
			state.ProcessAttackTransferMove (move);

			Assert.AreEqual (2, r1.Armies);
			Assert.AreEqual (5, r2.Armies);
		}

		[Test ()]
		public void TestProcessPlaceArmiesMove ()
		{
			State state = new State ();

			Continent c1 = new Continent (0, 2);
			Region r1 = new Region (0, c1);
			state.VisibleMap.AddContinent (c1);

			PlaceArmiesMove move = new PlaceArmiesMove ("player1", r1, 4);
			state.ProcessPlaceArmiesMove (move);

			Assert.AreEqual (6, r1.Armies);
		}

		[Test ()]
		public void TestUpdateMap ()
		{
			State state = new State ();

			Continent c1 = new Continent (0, 2);
			Continent c2 = new Continent (1, 5);
			Region r1 = new Region (0, c1);
			Region r2 = new Region (1, c1);
			Region r3 = new Region (2, c2);
			Region r4 = new Region (3, c2);
			r1.AddNeighbor (r2);
			r2.AddNeighbor (r3);
			r2.AddNeighbor (r4);
			r3.AddNeighbor (r3);
			state.CompleteMap.AddContinent (c1);
			state.CompleteMap.AddContinent (c2);

			state.UpdateMap (0, "player1", 3);

			Assert.AreEqual (1, state.VisibleMap.Regions.Count);
			Assert.AreNotEqual (r1, state.VisibleMap.Regions [0]);
			Assert.AreEqual (0, state.VisibleMap.Regions [0].Neighbors.Count);

			state.UpdateMap (2, "player1", 4);

			Assert.AreEqual (2, state.VisibleMap.Regions.Count);

			state.UpdateMap (1, "player1", 6);

			Assert.AreEqual (3, state.VisibleMap.Regions.Count);
			Assert.AreEqual (2, state.VisibleMap.Regions [1].Neighbors.Count);
		}
	}
}

