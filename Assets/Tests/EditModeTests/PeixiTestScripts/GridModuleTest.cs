using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Peixi;
using UniRx;

namespace Tests
{
    public class GridModuleTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GridModule_HasKey_false()
        {
            var grid = new GridModule<int, bool>();
            grid.AddData(8, true);
            Assert.IsFalse(grid.CheckHasTheKey(2));
            
        }
        [Test]
        public void GridModule_HasKey_true()
        {
            var grid = new GridModule<int, bool>();
            grid.AddData(3, true);
            Assert.IsTrue(grid.CheckHasTheKey(3));
            Assert.IsTrue(grid.RemoveData(3));
        }
        [Test]
        public void GridModule_RemoveKey_false()
        {
            var grid = new GridModule<int, string>();
            grid.AddData(9, "nine");
            grid.RemoveData(9);
            Assert.IsFalse(grid.CheckHasTheKey(9));
        }
        [Test]
        public void GridModule_OnDataAdded_alice()
        {
            var grid = new GridModule<int, string>();

            grid.OnDataAdded
                .Subscribe(x =>
                {
                    Assert.AreEqual("alice", x.Value);
                    
                });
            grid.AddData(0, "alice");
        }
        [Test]
        public void GridModule_OnDataRemoved_44()
        {
            var grid = new GridModule<string, int>();

            grid.OnDataRemoved
                .Subscribe(y =>
                {
                    Assert.AreEqual(44, y.Value);
                });

            grid.AddData("44", 44);
            grid.RemoveData("44");
        }
    }
}
