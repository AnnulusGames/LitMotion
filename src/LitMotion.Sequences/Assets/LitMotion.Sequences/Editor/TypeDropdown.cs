using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace LitMotion.Sequences.Editor
{
    public sealed class TypeDropdownItem : AdvancedDropdownItem
    {
        public readonly Type type;
        public TypeDropdownItem(Type type, string name) : base(name)
        {
            this.type = type;
            // if (type != null) icon = (Texture2D)EditorIcons.CsScriptIcon.image;
        }
    }

    public sealed class TypeDropdown : AdvancedDropdown
    {
        private static readonly float headerHeight = EditorGUIUtility.singleLineHeight * 2f;

        private Type[] types;
        public event Action<TypeDropdownItem> OnItemSelected;

        public static void AddTo(AdvancedDropdownItem root, IEnumerable<Type> types)
        {
            var itemCount = 0;

            var typeArray = types.OrderBy(x => x.FullName);


            foreach (var type in typeArray)
            {
                var attribute = type.GetCustomAttribute<SequenceComponentMenuAttribute>();
                var name = attribute == null ? type.FullName : attribute.MenuName;

                var splittedTypePath = name.Split('/');
                var parent = root;

                if (splittedTypePath.Length > 1)
                {
                    for (int i = 0; (splittedTypePath.Length - 1) > i; i++)
                    {
                        var foundItem = GetItem(parent, splittedTypePath[i]);
                        if (foundItem != null)
                        {
                            parent = foundItem;
                        }
                        else
                        {
                            var newItem = new AdvancedDropdownItem(splittedTypePath[i])
                            {
                                id = itemCount++,
                            };
                            parent.AddChild(newItem);
                            parent = newItem;
                        }
                    }
                }

                var item = new TypeDropdownItem(type, splittedTypePath[^1])
                {
                    id = itemCount++
                };
                parent.AddChild(item);
            }
        }

        static AdvancedDropdownItem GetItem(AdvancedDropdownItem parent, string name)
        {
            foreach (AdvancedDropdownItem item in parent.children)
            {
                if (item.name == name) return item;
            }
            return null;
        }

        public TypeDropdown(IEnumerable<Type> types, int maxLineCount, AdvancedDropdownState state) : base(state)
        {
            SetTypes(types);
            minimumSize = new(minimumSize.x, EditorGUIUtility.singleLineHeight * maxLineCount + headerHeight);
        }

        public void SetTypes(IEnumerable<Type> types)
        {
            this.types = types.ToArray();
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem("Search");
            AddTo(root, types);
            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);
            if (item is TypeDropdownItem dropdownItem)
            {
                OnItemSelected?.Invoke(dropdownItem);
            }
        }
    }
}