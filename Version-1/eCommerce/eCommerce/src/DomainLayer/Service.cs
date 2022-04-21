using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace eCommerce.src.DomainLayer
{
    public class Service
    {
        public static String GenerateId()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static class ObjectDictionaryMapper<T>
        {
            // function to build an object of type T from dictionary
            public static T GetObject(IDictionary<String, Object> dict)
            {
                PropertyInfo[] props = typeof(T).GetProperties();
                T res = Activator.CreateInstance<T>();
                for (int i = 0; i < props.Length; i++)
                {
                    if (props[i].CanWrite && dict.ContainsKey(props[i].Name))
                    {
                        props[i].SetValue(res, dict[props[i].Name], null);
                    }
                }
                return res;
            }

            // function to dump object of type T to dictionary
            public static IDictionary<String, Object> GetDictionary(T obj)
            {
                IDictionary<String, Object> res = new Dictionary<String, Object>();
                PropertyInfo[] props = typeof(T).GetProperties();
                for (int i = 0; i < props.Length; i++)
                {
                    if (props[i].CanRead)
                    {
                        res.Add(props[i].Name, props[i].GetValue(obj, null));
                    }
                }
                return res;
            }

            //function for updating several properties of an object using dictionary
            public static void SetPropertyValue(Object obj, IDictionary<String, Object> dict)
            {
                PropertyInfo[] properties = obj.GetType().GetProperties();
                IDictionary<String, Object> lowerCaseDict = dict.ToDictionary(k => k.Key.ToLower(), k => k.Value);
                for (int i = 0; i < properties.Length; i++)
                {
                    if (properties[i].CanWrite && lowerCaseDict.ContainsKey(properties[i].Name.ToLower()))
                    {
                        properties[i].SetValue(obj, lowerCaseDict[properties[i].Name.ToLower()], null);
                    }
                }
            }
        }
    }
}
