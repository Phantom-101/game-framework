#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Framework.Inspector.Editor {
    [CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
    public class SelectImplementationDrawer : PropertyDrawer {
        private List<Type>? _implementations;
        private int _implementationTypeIndex;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property) + EditorGUIUtility.singleLineHeight * 4;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var height = EditorGUIUtility.singleLineHeight;
            
            var refreshRect = new Rect(position.x, position.y, position.width, height);
            if (_implementations == null || GUI.Button(refreshRect, "Refresh Implementations")) {
                RefreshImplementations();
            }

            var textRect = new Rect(position.x, refreshRect.y + height, position.width, height);
            EditorGUI.LabelField(textRect, $"Found {_implementations!.Count} implementations");
            
            var popupRect = new Rect(position.x, textRect.y + height, position.width, height);
            _implementationTypeIndex = EditorGUI.Popup(popupRect, _implementationTypeIndex, _implementations.Select(impl => impl.FullName).ToArray());

            var buttonRect = new Rect(position.x, popupRect.y + height, position.width, height);
            if (GUI.Button(buttonRect, "Create Instance")) {
                property.managedReferenceValue = Activator.CreateInstance(_implementations[_implementationTypeIndex]);
            }

            var propertyRect = new Rect(position.x, buttonRect.y + height, position.width, height);
            EditorGUI.PropertyField(propertyRect, property, true);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            if (_implementations == null) {
                RefreshImplementations();
            }

            var ret = new VisualElement();

            ret.Add(new Button(RefreshImplementations) {
                text = "Refresh Implementations"
            });

            ret.Add(new Label($"Found {_implementations!.Count} implementations"));

            var popup = new PopupField<Type>(_implementations, 0);
            popup.RegisterCallback<ChangeEvent<Type>>(evt => { popup.value = evt.newValue; });
            ret.Add(popup);

            ret.Add(new Button(() => CreateInstance(property, popup.value)) {
                text = "Create Instance"
            });

            ret.Add(new PropertyField(property));

            return ret;
        }

        private void RefreshImplementations() {
            _implementations = GetImplementations(((SelectImplementationAttribute)attribute).fieldType)
                .Where(impl => !impl.IsSubclassOf(typeof(UnityEngine.Object))).ToList();
        }

        private static IEnumerable<Type> GetImplementations(Type interfaceType) {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
            return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
        }

        private static void CreateInstance(SerializedProperty property, Type type) {
            property.managedReferenceValue = Activator.CreateInstance(type);
        }
    }
}