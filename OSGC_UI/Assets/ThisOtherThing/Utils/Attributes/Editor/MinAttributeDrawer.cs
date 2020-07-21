using UnityEngine;
using UnityEditor;

using ThisOtherThing.Utils;

namespace ThisOtherThing
{
	[CustomPropertyDrawer(typeof(UnityEngine.MinAttribute))] 
	public class MinDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			ThisOtherThing.Utils.MinAttribute attribute = (ThisOtherThing.Utils.MinAttribute)base.attribute;

			switch (property.propertyType)
			{
				case SerializedPropertyType.Integer:
					int valueI = EditorGUI.IntField(position, label, property.intValue);
					property.intValue = Mathf.Max(valueI, attribute.minInt);
					break;
				case SerializedPropertyType.Float:
					float valueF = EditorGUI.FloatField(position, label, property.floatValue);
					property.floatValue = Mathf.Max(valueF, attribute.minFloat);
					break;
			}
		}
	}
}
