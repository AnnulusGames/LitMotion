using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

namespace LitMotion.Sequences.Tests.Runtime
{
    public class SequenceStressTest
    {
        readonly List<MotionSequence> sequences = new(10000);

        [UnityTest, Performance]
        public IEnumerator Test_Update_10000_Sequences()
        {
            PlaySequences(10000);
            yield return Measure.Frames()
                .WarmupCount(3)
                .MeasurementCount(600)
                .Run();
        }

        [Test, Performance]
        public void Test_Create_10000_Sequences()
        {
            Measure.Method(() =>
                {
                    PlaySequences(10000);
                })
                .WarmupCount(0)
                .MeasurementCount(1)
                .Run();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var sequence in sequences) sequence.Cancel();
            sequences.Clear();
        }

        void PlaySequences(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var sequence = MotionSequence.CreateBuilder()
                    .Append(() => LMotion.Create(0f, 1f, 10f).RunWithoutBinding())
                    .Append(() => LMotion.Create(0f, 1f, 10f).RunWithoutBinding())
                    .Append(() => LMotion.Create(0f, 1f, 10f).RunWithoutBinding())
                    .Append(() => LMotion.Create(0f, 1f, 10f).RunWithoutBinding())
                    .Append(() => LMotion.Create(0f, 1f, 10f).RunWithoutBinding())
                    .Build();
                sequences.Add(sequence);
                sequence.Play();
            }
        }
    }
}
