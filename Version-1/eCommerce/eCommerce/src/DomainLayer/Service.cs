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
