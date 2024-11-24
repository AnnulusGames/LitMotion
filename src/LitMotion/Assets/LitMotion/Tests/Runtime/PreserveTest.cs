using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class PreserveTest
    {
        [Test]
        public void Test_Error_ReuseBuiler()
        {
            using var builder = LMotion.Create(0f, 1f, 0.1f);

            Assert.Throws<InvalidOperationException>(() =>
            {
                builder.RunWithoutBinding();
                builder.RunWithoutBinding();
            });
        }
    }
}