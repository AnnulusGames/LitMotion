using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LitMotion.Animation.Editor
{
    [CustomPropertyDrawer(typeof(LitMotionAnimationComponent), true)]
    public class LitMotionAnimationComponentDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var view = new AnimationComponentView()
            {
                Text = property.displayName
            };

            var endProperty = property.GetEndProperty();
            var isFirst = true;
            while (property.NextVisible(isFirst))
            {
                if (SerializedProperty.EqualContents(property, endProperty)) break;
                isFirst = false;

                view.Add(new PropertyField(property));
            }

            return view;
        }
    }
}