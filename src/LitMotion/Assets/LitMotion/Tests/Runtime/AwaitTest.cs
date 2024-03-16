using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools.Utils;

namespace LitMotion.Tests.Runtime
{
    public class AwaitTest
    {
        [Test]
        public async Task Test_AwaitManyTimes()
        {
            var value = 0f;
            var startValue = 0f;
            var endValue = 10f;

            for (int i = 0; i < 50; i++)
            {
                await LMotion.Create(startValue, endValue, 0.1f)
                    .Bind(x => value = x);
                Assert.That(value, Is.EqualTo(10f).Using(FloatEqualityComparer.Instance));
            }
        }
    }
}