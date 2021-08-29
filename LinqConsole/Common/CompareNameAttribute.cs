using System;
using System.Collections.Generic;
using System.Text;

namespace LinqConsole
{
    /// <summary>
    /// 自定义比较属性
    /// 摘要:
    ///     Specifies the compare name for a property, event, or public void method which
    ///     takes no arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
    public class CompareNameAttribute : Attribute
    {
        //
        // 摘要:
        //     Specifies the default value for the System.ComponentModel.CompareNameAttribute.
        //     This field is read-only.
        public static readonly CompareNameAttribute Default;

        //
        // 摘要:
        //     Initializes a new instance of the System.ComponentModel.CompareNameAttribute
        //     class.
        public CompareNameAttribute() { }
        //
        // 摘要:
        //     Initializes a new instance of the System.ComponentModel.CompareNameAttribute
        //     class using the compare name.
        //
        // 参数:
        //   compareName:
        //     The compare name.
        public CompareNameAttribute(string compareName)
        {
            this.CompareName = compareName;
            this.CompareNameValue = compareName;
        }

        //
        // 摘要:
        //     Gets the compare name for a property, event, or public void method that takes
        //     no arguments stored in this attribute.
        //
        // 返回结果:
        //     The compare name.
        public virtual string CompareName { get; }
        //
        // 摘要:
        //     Gets or sets the compare name.
        //
        // 返回结果:
        //     The compare name.
        protected string CompareNameValue { get; set; }

        //
        // 摘要:
        //     Determines whether two System.ComponentModel.CompareNameAttribute instances are
        //     equal.
        //
        // 参数:
        //   obj:
        //     The System.ComponentModel.CompareNameAttribute to test the value equality of.
        //
        // 返回结果:
        //     true if the value of the given object is equal to that of the current object;
        //     otherwise, false.
        public override bool Equals(object obj) {            
            return base.Equals(obj);
        }
        //
        // 摘要:
        //     Returns the hash code for this instance.
        //
        // 返回结果:
        //     A hash code for the current System.ComponentModel.CompareNameAttribute.
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        //
        // 摘要:
        //     Determines if this attribute is the default.
        //
        // 返回结果:
        //     true if the attribute is the default value for this attribute class; otherwise,
        //     false.
        public override bool IsDefaultAttribute() {
            return base.IsDefaultAttribute();
        }
    }
}
