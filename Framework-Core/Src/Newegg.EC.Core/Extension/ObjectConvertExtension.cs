using System;

namespace Newegg.EC.Core
{
    /// <summary>
    /// Object convert extension.
    /// </summary>
    public static class ObjectConvertExtension
    {
        /// <summary>
        /// Converts current object to string. If current object is null or cannot be converted to string, defaultvalue is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>String value.</returns>
        public static string ToNotNullString(this object me, string defaultvalue)
        {
            string result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    result = me as string;
                }
                else
                {
                    result = me.ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to string. If current object is null or cannot be converted to the target type, string.Empty is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>String value.</returns>
        public static string ToNotNullString(this object me)
        {
            return me.ToNotNullString(string.Empty);
        }

        /// <summary>
        /// Converts current object to an char. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Int value.</returns>
        public static char ToChar(this object me, char defaultvalue)
        {
            char result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!char.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToChar(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a char. If current object is null or cannot be converted to the target type, default char is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Int value.</returns>
        public static char ToChar(this object me)
        {
            return me.ToChar(default(char));
        }

        /// <summary>
        /// Converts current object to an byte. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Int value.</returns>
        public static byte ToByte(this object me, byte defaultvalue)
        {
            byte result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!byte.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToByte(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a byte. If current object is null or cannot be converted to the target type, default byte is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Int value.</returns>
        public static byte ToByte(this object me)
        {
            return me.ToByte(default(byte));
        }

        /// <summary>
        /// Converts current object to an sbyte. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Int value.</returns>
        public static sbyte ToSByte(this object me, sbyte defaultvalue)
        {
            sbyte result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!sbyte.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToSByte(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a sbyte. If current object is null or cannot be converted to the target type, default sbyte is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Int value.</returns>
        public static sbyte ToSByte(this object me)
        {
            return me.ToSByte(default(sbyte));
        }

        /// <summary>
        /// Converts current object to an short. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Int value.</returns>
        public static short ToShort(this object me, short defaultvalue)
        {
            short result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!short.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToInt16(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a short. If current object is null or cannot be converted to the target type, default short is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Int value.</returns>
        public static short ToShort(this object me)
        {
            return me.ToShort(default(short));
        }

        /// <summary>
        /// Converts current object to an ushort. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Int value.</returns>
        public static ushort ToUShort(this object me, ushort defaultvalue)
        {
            ushort result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!ushort.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToUInt16(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a ushort. If current object is null or cannot be converted to the target type, default ushort is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Int value.</returns>
        public static ushort ToUShort(this object me)
        {
            return me.ToUShort(default(ushort));
        }

        /// <summary>
        /// Converts current object to an int. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Int value.</returns>
        public static int ToInt(this object me, int defaultvalue)
        {
            int result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!int.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToInt32(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to an int. If current object is null or cannot be converted to the target type, default int is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Int value.</returns>
        public static int ToInt(this object me)
        {
            return me.ToInt(default(int));
        }

        /// <summary>
        /// Converts current object to an uint. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Int value.</returns>
        public static uint ToUInt(this object me, uint defaultvalue)
        {
            uint result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!uint.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToUInt32(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a uint. If current object is null or cannot be converted to the target type, default uint is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Int value.</returns>
        public static uint ToUInt(this object me)
        {
            return me.ToUInt(default(uint));
        }

        /// <summary>
        /// Converts current object to a long. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Long value.</returns>
        public static long ToLong(this object me, long defaultvalue)
        {
            long result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!long.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToInt64(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a long. If current object is null or cannot be converted to the target type, default long is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Long value.</returns>
        public static long ToLong(this object me)
        {
            return me.ToLong(default(long));
        }

        /// <summary>
        /// Converts current object to an uint. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Int value.</returns>
        public static ulong ToULong(this object me, ulong defaultvalue)
        {
            ulong result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!ulong.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToUInt64(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a uint. If current object is null or cannot be converted to the target type, default uint is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Int value.</returns>
        public static ulong ToULong(this object me)
        {
            return me.ToULong(default(ulong));
        }

        /// <summary>
        /// Converts current object to a decimal. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Decimal value.</returns>
        public static decimal ToDecimal(this object me, decimal defaultvalue)
        {
            decimal result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!decimal.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToDecimal(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a decimal. If current object is null or cannot be converted to the target type, default decimal is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Decimal value.</returns>
        public static decimal ToDecimal(this object me)
        {
            return me.ToDecimal(default(decimal));
        }

        /// <summary>
        /// Converts current object to a float. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Long value.</returns>
        public static float ToFloat(this object me, float defaultvalue)
        {
            float result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!float.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToSingle(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a float. If current object is null or cannot be converted to the target type, default float is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Long value.</returns>
        public static float ToFloat(this object me)
        {
            return me.ToFloat(default(float));
        }

        /// <summary>
        /// Converts current object to a double. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Long value.</returns>
        public static double ToDouble(this object me, double defaultvalue)
        {
            double result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is string)
                {
                    if (!double.TryParse((string)me, out result))
                    {
                        result = defaultvalue;
                    }
                }
                else if (me is IConvertible)
                {
                    result = Convert.ToDouble(me);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a double. If current object is null or cannot be converted to the target type, default double is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Long value.</returns>
        public static double ToDouble(this object me)
        {
            return me.ToDouble(default(double));
        }

        /// <summary>
        /// Converts current object to string, then parse string to a DateTime. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>DateTime value.</returns>
        public static DateTime ToDateTime(this object me, DateTime defaultvalue)
        {
            DateTime result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is DateTime)
                {
                    result = (DateTime)me;
                }
                else if (!DateTime.TryParse(me.ToString(), out result))
                {
                    result = defaultvalue;
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to string, then parse string to a DateTime. If current object is null or cannot be converted to the target type, default datetime is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>DateTime value.</returns>
        public static DateTime ToDateTime(this object me)
        {
            return me.ToDateTime(default(DateTime));
        }

        /// <summary>
        /// Converts current object to a bool. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">The default value.</param>
        /// <returns>Bool value.</returns>
        public static bool ToBool(this object me, bool defaultvalue)
        {
            bool result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                if (me is bool)
                {
                    result = (bool)me;
                }
                else if (me.IsNumeric())
                {
                    result = me.ToDecimal() != decimal.Zero ? true : defaultvalue;
                }
                else if (me is string)
                {
                    if (!bool.TryParse(me.ToString(), out result))
                    {
                        result = defaultvalue;
                    }
                }
                else
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to a bool. If current object is null or cannot be converted to the target type, false is returned.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Bool value.</returns>
        public static bool ToBool(this object me)
        {
            return me.ToBool(false);
        }

        /// <summary>
        /// Converts current object to an enum. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <typeparam name="TEnum">Type of enum.</typeparam>
        /// <param name="me">Current object.</param>
        /// <param name="defaultvalue">Default value.</param>
        /// <returns>Enum value.</returns>
        public static TEnum ToEnum<TEnum>(this object me, TEnum defaultvalue) where TEnum : struct
        {
            TEnum result = defaultvalue;

            if (me != null && me != DBNull.Value)
            {
                Type myType = me.GetType();
                Type destinationType = typeof(TEnum);

                if (myType.IsEnum && (myType != destinationType))
                {
                    me = me.ToString();
                }

                if (me is int)
                {
                    if (Enum.IsDefined(typeof(TEnum), me))
                    {
                        result = (TEnum)me;
                    }
                }
                else if (!Enum.TryParse(me.ToString(), out result))
                {
                    result = defaultvalue;
                }
            }

            return result;
        }

        /// <summary>
        /// Converts current object to an enum. If current object is null or cannot be converted to the target type, default value is returned.
        /// </summary>
        /// <typeparam name="TEnum">Type of enum.</typeparam>
        /// <param name="me">Current object.</param>
        /// <returns>Enum value.</returns>
        public static TEnum ToEnum<TEnum>(this object me) where TEnum : struct
        {
            return me.ToEnum(default(TEnum));
        }

        /// <summary>
        /// Converts current object to another type of object.
        /// </summary>
        /// <typeparam name="TDestinationClass">Type of destination class.</typeparam>
        /// <param name="me">Current object.</param>
        /// <param name="converter">Delegate converter.</param>
        /// <returns>Destination class instance.</returns>
        public static TDestinationClass To<TDestinationClass>(this object me, Func<object, TDestinationClass> converter)
        {
            TDestinationClass result = default(TDestinationClass);

            if (converter != null)
            {
                result = converter(me);
            }

            return result;
        }

        /// <summary>
        /// Converts current object to another type of object,If current object is null return target type.
        /// </summary>
        /// <typeparam name="TDestinationClass">Type of destination class.</typeparam>
        /// <param name="me">Current object.</param>
        /// <param name="converter">Delegate converter.</param>
        /// <returns>Destination class instance.</returns>
        public static TDestinationClass To<TDestinationClass>(this object me, Func<TDestinationClass> converter)
        {
            if (me == null)
            {
                return default(TDestinationClass);
            }

            return converter();
        }

        /// <summary>
        /// Check whether current object is numeric.
        /// </summary>
        /// <param name="me">Current object.</param>
        /// <returns>Whether current object is numeric.</returns>
        public static bool IsNumeric(this object me)
        {
            if (!(me is byte ||
                 me is short ||
                 me is int ||
                 me is long ||
                 me is sbyte ||
                 me is ushort ||
                 me is uint ||
                 me is ulong ||
                 me is decimal ||
                 me is double ||
                 me is float))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
