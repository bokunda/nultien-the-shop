using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nultien.TheShop.Common
{
    public static class CollectionExtenders
    {
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue, TValue> updateIfExists,
            Func<TValue> addIfNotExists)
        {
            if (IsDefaultValue(key)) return;

            if (dictionary.ContainsKey(key))
            {
                var oldValue = dictionary[key];
                dictionary[key] = updateIfExists(oldValue);
            }
            else
            {
                dictionary[key] = addIfNotExists();
            }
        }

        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
            TKey key,
            Action<TValue> updateIfExists,
            Func<TValue> addIfNotExists)
        {
            if (IsDefaultValue(key)) return;

            if (dictionary.ContainsKey(key))
            {
                var oldValue = dictionary[key];
                updateIfExists(oldValue);
                dictionary[key] = oldValue;
            }
            else
            {
                dictionary[key] = addIfNotExists();
            }
        }

        private static bool IsDefaultValue<T>(T value)
        {
            if (value is int && Convert.ToInt32(value) == 0) return false;

            return EqualityComparer<T>.Default.Equals(value, default(T));
        }
    }
}
