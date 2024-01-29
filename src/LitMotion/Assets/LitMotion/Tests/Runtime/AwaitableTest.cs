#if UNITY_2023_1_OR_NEWER
using System.Threading.Tasks;
using NUnit.Framework;

namespace LitMotion.Tests.Runtime
{
    public class AwaitableTest
    {
        [Test]
        public async Task Test_Awaitable()
        {
            var value = 0f;
            var handle = LMotion.Create(0f, 10f, 1f).Bind(x => value = x);
            await handle.ToAwaitable();
            Assert.That(value, Is.EqualTo(10f));
        }
    }
}
#endif
