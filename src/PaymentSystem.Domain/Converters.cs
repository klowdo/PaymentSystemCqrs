using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace PaymentSystem.Domain
{
    public class GuidTypeConverter<T> : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(Guid);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is T) return value;
            if (!(value is string stringValue)) return default(T);
            if (Guid.TryParse(stringValue, out var guid))
                return (T) Activator.CreateInstance(typeof(T), guid);
            return default(T);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            return value.ToString();
        }
    }

    public class StringTypeConverter<T> : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is T) return value;
            var stringValue = value as string;
            if (stringValue == "<empty>")
            {
                if (typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                    .Any(c => c.GetParameters().Length == 0))
                    return (T) Activator.CreateInstance(typeof(T), true);
                stringValue = string.Empty;
            }

            return CreateValue(stringValue);
        }

        protected virtual object CreateValue(string value)
        {
            return value != null ? (object) (T) Activator.CreateInstance(typeof(T), value) : null;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            var str = value.ToString();
            return str == string.Empty ? "<empty>" : str;
        }
    }

    public class IntTypeConverter<T> : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || destinationType == typeof(long) ||
                   destinationType == typeof(int);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(long) || sourceType == typeof(int) ||
                   sourceType == typeof(decimal) || sourceType == typeof(double);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null) return null;
            if (value is T) return value;
            if (value is string stringValue) return CreateValue(stringValue);
            return (T) Activator.CreateInstance(typeof(T), Convert.ToInt32(value));
        }

        protected virtual object CreateValue(string value)
        {
            return (T) Activator.CreateInstance(typeof(T), int.Parse(value, CultureInfo.InvariantCulture));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            return value.ToString();
        }
    }

    public class LongTypeConverter<T> : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || destinationType == typeof(long);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(long);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null) return null;
            if (value is T) return value;
            if (value is string stringValue)
                return (T) Activator.CreateInstance(typeof(T), long.Parse(stringValue, CultureInfo.InvariantCulture));
            return (T) Activator.CreateInstance(typeof(T), Convert.ToInt64(value));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            return value.ToString();
        }
    }

    public class DecimalTypeConverter<T> : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || destinationType == typeof(decimal);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(decimal) || sourceType == typeof(int) ||
                   sourceType == typeof(long);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null) return null;
            if (value is T) return value;
            if (value is string stringValue)
                return (T) Activator.CreateInstance(typeof(T),
                    decimal.Parse(stringValue, CultureInfo.InvariantCulture));
            return (T) Activator.CreateInstance(typeof(T), Convert.ToDecimal(value));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            return value.ToString();
        }
    }

    public class DoubleTypeConverter<T> : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || destinationType == typeof(double);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(double) || sourceType == typeof(int) ||
                   sourceType == typeof(long) || sourceType == typeof(decimal);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null) return null;
            if (value is T) return value;
            if (value is string stringValue)
                return (T) Activator.CreateInstance(typeof(T), double.Parse(stringValue, CultureInfo.InvariantCulture));
            return (T) Activator.CreateInstance(typeof(T), Convert.ToDouble(value));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            return value.ToString();
        }
    }
}