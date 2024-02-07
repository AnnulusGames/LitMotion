using System.Threading.Tasks;
using NUnit.Framework;

namespace LitMotion.Sequences.Tests.Runtime
{
    public class SequenceAsyncTest
    {
        [Test]
        public async Task Test_ToValueTask_AwaitForComplete()
        {
            var sequenceCompleted = false;

            var sequence = MotionSequence.CreateBuilder()
                .Append(() => LMotion.Create(0f, 1f, 1f).RunWithoutBinding())
                .Build();

            sequence.OnCompleted += () => sequenceCompleted = true;

            for (int i = 0; i < 10; i++)
            {
                sequence.Play();
                await sequence.ToValueTask();

                Assert.That(sequenceCompleted, Is.True);

                sequenceCompleted = false;
            }
        }
    }
}
