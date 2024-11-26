using System.Collections;
using LitMotion.Extensions;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class CoroutineTest
    {
        [UnityTest]
        public IEnumerator Test_ToYieldInteraction()
        {
            yield return LMotion.Create(0f, 10f, 3f)
                .BindToUnityLogger()
                .ToYieldInstruction();
        }
    }
}