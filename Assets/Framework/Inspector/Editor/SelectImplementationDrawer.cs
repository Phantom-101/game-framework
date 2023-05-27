#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework.Inspector.Editor {
    [CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
    public class SelectImplementationDrawer : PropertyDrawer {
        private List<Type>? _implementations;
        private int _implementationTypeIndex;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property) + EditorGUIUtility.singleLineHeight * 3;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var height = EditorGUIUtility.singleLineHeight;
            var curY = position.y;
            
            var type = ((SelectImplementationAttribute)attribute).fieldType;
            
            var refreshRect = new Rect(position.x, curY, position.width, height);
            var refreshed = false;
            if (_implementations == null || GUI.Button(refreshRect, "Refresh Implementations")) {
                RefreshImplementations(type);
                refreshed = true;
            }

            curY += height;
            
            // Make sure index is compatible with the current value in the property
            if (type.IsInstanceOfType(property.managedReferenceValue)) {
                if (!refreshed) {
                    RefreshImplementations(type);
                }
                
                if (!_implementations![_implementationTypeIndex].IsInstanceOfType(property.managedReferenceValue)) {
                    _implementationTypeIndex = _implementations.IndexOf(property.managedReferenceValue.GetType());
                }
            } else {
                property.managedReferenceValue = null;
            }
            
            var popupRect = new Rect(position.x, curY, position.width, height);
            _implementationTypeIndex = EditorGUI.Popup(popupRect, _implementationTypeIndex, _implementations!.Select(impl => impl.FullName).ToArray());

            curY += height;

            // If type has been changed, change property value accordingly
            if (!_implementations![_implementationTypeIndex].IsInstanceOfType(property.managedReferenceValue)) {
                property.managedReferenceValue = null;
            }

            var buttonRect = new Rect(position.x, curY, position.width, height);
            if (GUI.Button(buttonRect, "Create Instance")) {
                property.managedReferenceValue = Activator.CreateInstance(_implementations[_implementationTypeIndex]);
            }

            curY += height;

            var propertyRect = new Rect(position.x, curY, position.width, height);
            EditorGUI.PropertyField(propertyRect, property, true);
        }

        /* outdated
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
        */

        private void RefreshImplementations(Type interfaceType) {
            _implementations = GetImplementations(interfaceType).Where(e => !typeof(Object).IsAssignableFrom(e)).ToList();
        }

        private static IEnumerable<Type> GetImplementations(Type interfaceType) {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
            return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
        }
    }
}