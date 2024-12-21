using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class MotionSettingsTest
    {
        [UnityTest]
        public IEnumerator Test_Create()

        {
            var settings = new MotionSettings<float, NoOptions>
            {
                StartValue = 0f,
                EndValue = 1f,
                Duration = 1f,
                ImmediateBind = true,
            };

            var x = -1f;

            LMotion.Create(settings)
                .Bind(v => x = v);

            Assert.That(x, Is.EqualTo(0f));
            yield return new WaitForSeconds(settings.Duration);
            Assert.That(x, Is.EqualTo(1f));
        }
    }
}
