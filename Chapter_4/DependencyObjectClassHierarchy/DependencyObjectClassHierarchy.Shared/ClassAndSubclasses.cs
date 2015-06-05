using System;
using System.Collections.Generic;

namespace DependencyObjectClassHierarchy
{
    public class ClassAndSubclasses
    {
        public ClassAndSubclasses(Type parent)
        {
            this.Type = parent;
            this.Subclasses = new List<ClassAndSubclasses>();
        }

        public Type Type { get; protected set; }
        public List<ClassAndSubclasses> Subclasses { get; protected set; }
    }
}
