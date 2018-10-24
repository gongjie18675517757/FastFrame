﻿using FastFrame.Entity.System;
using FastFrame.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FastFrame.UnitTest
{
    [TestClass]
    public class Test_T4Help
    {
        [TestMethod]
        public void Test_GetClassSummary()
        {
            var type = typeof(Entity.System.Foreign);
            var path = @"D:\CoreProject\FastFrame\src\FastFrame\Lib";
            var summany = T4Help.GetClassSummary(type, path);
            Assert.AreEqual("表外键信息", summany);
        }

        [TestMethod]
        public void Test_GetTypeName()
        {
            var name = T4Help.GetTypeName(typeof(Nullable<int>));
            Assert.AreEqual("Nullable<Int32>", name);
            name = T4Help.GetTypeName(typeof(string));
            Assert.AreEqual("String", name);
        } 
    }
}
