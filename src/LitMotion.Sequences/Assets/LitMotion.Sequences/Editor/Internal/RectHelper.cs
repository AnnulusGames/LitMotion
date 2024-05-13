using UnityEngine;

namespace LitMotion.Sequences.Editor
{
    internal static class RectHelper
    {
        public static Rect Expand(this Rect rect, float value)
        {
            rect.xMin -= value;
            rect.xMax += value;
            rect.yMin -= value;
            rect.yMax += value;
            return rect;
        }

        public static Rect ExpandX(this Rect rect, float value)
        {
            rect.xMin -= value;
            rect.xMax += value;
            return rect;
        }

        public static Rect ExpandY(this Rect rect, float value)
        {
            rect.yMin -= value;
            rect.yMax += value;
            return rect;
        }

        public static Rect AddX(this Rect rect, float value)
        {
            rect.x += value;
            return rect;
        }

        public static Rect AddY(this Rect rect, float value)
        {
            rect.y += value;
            return rect;
        }

        public static Rect AddXMin(this Rect rect, float value)
        {
            rect.xMin += value;
            return rect;
        }

        public static Rect AddXMax(this Rect rect, float value)
        {
            rect.xMax += value;
            return rect;
        }

        public static Rect SetWidth(this Rect rect, float value)
        {
            rect.width = value;
            return rect;
        }

        public static Rect SetHeight(this Rect rect, float value)
        {
            rect.height = value;
            return rect;
        }
    }
}