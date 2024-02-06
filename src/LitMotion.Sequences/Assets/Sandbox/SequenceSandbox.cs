using LitMotion;
using LitMotion.Extensions;
using LitMotion.Sequences;
using UnityEngine;

public class SequenceSandbox : MonoBehaviour
{
    [SerializeField] Transform target1;
    [SerializeField] Transform target2;
    [SerializeField] Transform target3;

    MotionSequence sequence;

    void Start()
    {
        sequence = MotionSequence.CreateBuilder()
            .AppendGroup((target1, target2, target3), (state, motions) =>
            {
                motions.Add(LMotion.Create(-2f, 2f, 1f).WithLoops(3, LoopType.Yoyo).BindToPositionX(state.target1));
                motions.Add(LMotion.Create(-2f, 2f, 1f).WithLoops(2, LoopType.Yoyo).BindToPositionX(state.target2));
                motions.Add(LMotion.Create(-2f, 2f, 1f).BindToPositionX(state.target3));
            })
            .Build()
            .AddTo(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !sequence.IsPlaying())
        {
            sequence.Play();
        }

        if (Input.GetKeyDown(KeyCode.A) && sequence.IsPlaying())
        {
            sequence.Complete();
        }

        if (Input.GetKeyDown(KeyCode.Z) && sequence.IsPlaying())
        {
            sequence.Cancel();
        }
    }
}
