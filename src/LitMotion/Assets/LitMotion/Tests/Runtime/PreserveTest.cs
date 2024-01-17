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

        [UnityTest]
        public IEnumerator Test_Preserve()
        {
            using var builder = LMotion.Create(0f, 1f, 0.1f).Preserve();
            yield return builder.RunWithoutBinding().ToYieldInteraction();
            yield return builder.RunWithoutBinding().ToYieldInteraction();
            yield return builder.RunWithoutBinding().ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Preserve_MultipleBuilders()
        {
            using var builder1 = LMotion.Create(0f, 1f, 0.1f).Preserve();
            using var builder2 = LMotion.Create(0f, 1f, 0.1f).Preserve();
            using var builder3 = LMotion.Create(0f, 1f, 0.1f).Preserve();
            yield return builder1.RunWithoutBinding().ToYieldInteraction();
            yield return builder2.RunWithoutBinding().ToYieldInteraction();
            yield return builder3.RunWithoutBinding().ToYieldInteraction();
            yield return builder1.RunWithoutBinding().ToYieldInteraction();
            yield return builder2.RunWithoutBinding().ToYieldInteraction();
            yield return builder3.RunWithoutBinding().ToYieldInteraction();
        }
    }
}