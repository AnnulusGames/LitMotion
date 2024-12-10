using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

namespace LitMotion.Animation.Editor
{
    internal static class SerializedPropertyExtensions
    {
        public static bool TryGetAttribute<TAttribute>(this SerializedProperty property, out TAttribute result) where TAttribute : Attribute
        {
            return TryGetAttribute(property, false, out result);
        }

        public static bool TryGetAttribute<TAttribute>(this SerializedProperty property, bool inherit, out TAttribute result) where TAttribute : Attribute
        {
            TAttribute attribute = GetAttribute<TAttribute>(property, inherit);
            result = attribute;
            return attribute != null;
        }

        public static TAttribute GetAttribute<TAttribute>(this SerializedProperty property, bool inherit = false) where TAttribute : Attribute
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            return property.GetFieldInfo().GetCustomAttribute<TAttribute>(inherit);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this SerializedProperty property, bool inherit) where TAttribute : Attribute
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            return property.GetFieldInfo().GetCustomAttributes<TAttribute>(inherit);
        }

        public static float GetHeight(this SerializedProperty property)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }

        public static float GetHeight(this SerializedProperty property, bool includeChildren)
        {
            return EditorGUI.GetPropertyHeight(property, includeChildren);
        }

        public static float GetHeight(this SerializedProperty property, GUIContent label, bool includeChildren)
        {
            return EditorGUI.GetPropertyHeight(property, label, includeChildren);
        }

        public static T GetValue<T>(this SerializedProperty property)
        {
            return GetNestedObject<T>(property.propertyPath, GetSerializedPropertyRootObject(property));
        }

        public static bool SetValue<T>(this SerializedProperty property, T value)
        {
            object obj = GetSerializedPropertyRootObject(property);
            var fieldStructure = property.propertyPath.Split('.');
            for (int i = 0; i < fieldStructure.Length - 1; i++)
            {
                obj = GetFieldOrPropertyValue<object>(fieldStructure[i], obj);
            }
            var fieldName = fieldStructure.Last();

            return SetFieldOrPropertyValue(fieldName, obj, value);
        }

        static readonly Regex IndexerRegex = new(@"[^0-9]+");

        public static FieldInfo GetFieldInfo(this SerializedProperty property)
        {
            object target = property.serializedObject.targetObject;
            var splits = property.propertyPath.Split('.');

            var fieldInfo = ReflectionHelper.GetField(target.GetType(), splits[0], includingBaseNonPublic: true);
            target = fieldInfo.GetValue(target);

            for (var i = 1; i < splits.Length; i++)
            {
                if (target == null) return null;

                if (splits[i] == "Array")
                {
                    i++;
                    if (i >= splits.Length) continue;

                    var index = int.Parse(IndexerRegex.Replace(splits[i], string.Empty));
                    var targetType = target.GetType();

                    if (targetType.IsArray) target = (target as Array).GetValue(index);
                    else target = (target as IList)[index];

                    i++;
                    if (i >= splits.Length) continue;

                    targetType = target.GetType();
                    fieldInfo = ReflectionHelper.GetField(targetType, splits[i], includingBaseNonPublic: true);
                }
                else
                {
                    var targetType = target.GetType();
                    fieldInfo = ReflectionHelper.GetField(targetType, splits[i], includingBaseNonPublic: true);
                }

                target = fieldInfo?.GetValue(target);
            }

            return fieldInfo;
        }

        public static Type GetPropertyType(this SerializedProperty property, bool isCollectionType = false)
        {
            var fieldInfo = property.GetFieldInfo();

            if (isCollectionType && property.propertyType != SerializedPropertyType.String)
                return fieldInfo.FieldType.IsArray ?
                    fieldInfo.FieldType.GetElementType() :
                    fieldInfo.FieldType.GetGenericArguments()[0];
            return fieldInfo.FieldType;
        }

        public static object SetManagedReferenceType(this SerializedProperty property, Type type)
        {
            var obj = (type != null) ? Activator.CreateInstance(type) : null;
            property.managedReferenceValue = obj;
            return obj;
        }

        public static string GetManagedReferenceFieldTypeName(this SerializedProperty property)
        {
            var typeName = property.managedReferenceFieldTypename;
            var splitIndex = typeName.IndexOf(' ');
            return typeName[(splitIndex + 1)..];
        }

        public static Type GetManagedReferenceFieldType(this SerializedProperty property)
        {
            var typeName = property.managedReferenceFieldTypename;
            var splitIndex = typeName.IndexOf(' ');
            var assembly = Assembly.Load(typeName[..splitIndex]);
            return assembly.GetType(typeName[(splitIndex + 1)..]);
        }

        static UnityEngine.Object GetSerializedPropertyRootObject(SerializedProperty property)
        {
            return property.serializedObject.targetObject;
        }

        static T GetNestedObject<T>(string path, object obj, bool includeAllBases = false)
        {
            var parts = path.Split('.');
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];

                if (part == "Array")
                {
                    var regex = new Regex(@"[^0-9]");
                    var countText = regex.Replace(parts[i + 1], "");
                    if (!int.TryParse(countText, out var index))
                    {
                        index = -1;
                    }

                    obj = GetElementAtOrDefault(obj, index);

                    i++;
                }
                else
                {
                    obj = GetFieldOrPropertyValue<object>(part, obj, includeAllBases);
                }
            }
            return (T)obj;
        }

        static object GetElementAtOrDefault(object arrayOrListObj, int index)
        {
            if (arrayOrListObj is IEnumerable<object> referenceEnumerable)
            {
                return referenceEnumerable.ElementAtOrDefault(index);
            }

            if (arrayOrListObj is IList valueList)
            {
                object result;
                if (index < 0 || index >= valueList.Count)
                {
                    Type listType = valueList.GetType();
                    Type elementType = listType.IsArray ? listType.GetElementType() : listType.GetGenericArguments()[0];
                    result = Activator.CreateInstance(elementType);
                }
                else
                {
                    result = valueList[index];
                }

                return result;
            }

            throw new ArgumentException($"Can't parse {arrayOrListObj.GetType()} as Array or List");
        }

        public static object GetParentObject(this SerializedProperty property)
        {
            if (property == null) return null;

            var path = property.propertyPath.Replace(".Array.data[", "[");
            object obj = property.serializedObject.targetObject;
            var elements = path.Split('.');
            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element[..element.IndexOf("[")];
                    var index = Convert.ToInt32(element[element.IndexOf("[")..].Replace("[", "").Replace("]", ""));
                    obj = ReflectionHelper.GetValue(obj, elementName, index);
                }
                else
                {
                    obj = ReflectionHelper.GetValue(obj, element);
                }
            }
            return obj;
        }


        public static object GetDeclaredObject(this SerializedProperty property)
        {
            if (property == null) return null;

            var path = property.propertyPath.Replace(".Array.data[", "[");
            object obj = property.serializedObject.targetObject;
            var elements = path.Split('.');
            for (int i = 0; i < elements.Length - 1; i++)
            {
                var element = elements[i];
                if (element.Contains("["))
                {
                    var elementName = element[..element.IndexOf("[")];
                    var index = Convert.ToInt32(element[element.IndexOf("[")..].Replace("[", "").Replace("]", ""));
                    obj = ReflectionHelper.GetValue(obj, elementName, index);
                }
                else
                {
                    obj = ReflectionHelper.GetValue(obj, element);
                }
            }
            return obj;
        }

        static T GetFieldOrPropertyValue<T>(string fieldName, object obj, bool includeAllBases = false, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        {
            var field = obj.GetType().GetField(fieldName, bindings);
            if (field != null) return (T)field.GetValue(obj);

            var property = obj.GetType().GetProperty(fieldName, bindings);
            if (property != null) return (T)property.GetValue(obj, null);

            if (includeAllBases)
            {
                foreach (var type in GetBaseClassesAndInterfaces(obj.GetType()))
                {
                    field = type.GetField(fieldName, bindings);
                    if (field != null) return (T)field.GetValue(obj);

                    property = type.GetProperty(fieldName, bindings);
                    if (property != null) return (T)property.GetValue(obj, null);
                }
            }

            return default;
        }

        static bool SetFieldOrPropertyValue(string fieldName, object obj, object value, bool includeAllBases = false, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        {
            var field = obj.GetType().GetField(fieldName, bindings);
            if (field != null)
            {
                field.SetValue(obj, value);
                return true;
            }

            var property = obj.GetType().GetProperty(fieldName, bindings);
            if (property != null)
            {
                property.SetValue(obj, value, null);
                return true;
            }

            if (includeAllBases)
            {
                foreach (var type in GetBaseClassesAndInterfaces(obj.GetType()))
                {
                    field = type.GetField(fieldName, bindings);
                    if (field != null)
                    {
                        field.SetValue(obj, value);
                        return true;
                    }

                    property = type.GetProperty(fieldName, bindings);
                    if (property != null)
                    {
                        property.SetValue(obj, value, null);
                        return true;
                    }
                }
            }
            return false;
        }

        static IEnumerable<Type> GetBaseClassesAndInterfaces(Type type, bool includeSelf = false)
        {
            if (includeSelf) yield return type;

            if (type.BaseType == typeof(object))
            {
                foreach (var interfaceType in type.GetInterfaces())
                {
                    yield return interfaceType;
                }
            }
            else
            {
                foreach (var baseType in Enumerable.Repeat(type.BaseType, 1)
                    .Concat(type.GetInterfaces())
                    .Concat(GetBaseClassesAndInterfaces(type.BaseType))
                    .Distinct())
                {
                    yield return baseType;
                }
            }
        }
    }
}