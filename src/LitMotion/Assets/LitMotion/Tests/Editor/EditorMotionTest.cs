using UnityEngine;
using LitMotion.Editor;
using System.Collections;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Editor
{
    public class EditorMotionTest
    {
        [UnityTest]
        public IEnumerator Test_1()
        {
            bool completed = false;
            LMotion.Create(0f, 100f, 1f)
                .WithOnComplete(() => completed = true)
                .Bind(x => Debug.Log(x));

            while (!completed) yield return null;
        }
    }
}