using System.Collections.Generic;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace LitMotion.Sequences
{
    [AddComponentMenu("LitMotion/Sequence Player")]
    public sealed class SequencePlayer : MonoBehaviour, IExposedPropertyTable
    {
        public SequenceAsset asset;

        [SerializeField] List<PropertyName> propertyNameList;
        [SerializeField] List<UnityObject> objectList;

        MotionSequence sequence;

        void Start()
        {
            asset.ResolveExposedPropeties(this);
            sequence = asset.CreateSequence();
        }

        public bool IsPlaying() => sequence.IsActive();

        public void Play()
        {
            sequence.Play();
        }

        public void Complete()
        {
            sequence.Complete();
        }

        public void Cancel()
        {
            sequence.Cancel();
        }

        void IExposedPropertyTable.ClearReferenceValue(PropertyName id)
        {
            var index = propertyNameList.IndexOf(id);
            if (index != -1)
            {
                propertyNameList.RemoveAt(index);
                objectList.RemoveAt(index);
            }
        }

        UnityObject IExposedPropertyTable.GetReferenceValue(PropertyName id, out bool idValid)
        {
            var index = propertyNameList.IndexOf(id);
            idValid = index != -1;
            return idValid ? objectList[index] : null;
        }

        void IExposedPropertyTable.SetReferenceValue(PropertyName id, UnityObject value)
        {
            if (PropertyName.IsNullOrEmpty(id)) return;

            var index = propertyNameList.IndexOf(id);
            if (index != -1)
            {
                propertyNameList[index] = id;
                objectList[index] = value;
            }
            else if (value == null)
            {
                propertyNameList.Add(id);
                objectList.Add(value);
            }
        }
    }
}
