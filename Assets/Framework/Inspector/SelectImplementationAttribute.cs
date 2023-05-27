using System;
using UnityEngine;

namespace Framework.Inspector {
    public class SelectImplementationAttribute : PropertyAttribute {
        public readonly Type fieldType;

        public SelectImplementationAttribute(Type fieldType) {
            this.fieldType = fieldType;
        }
    }
}