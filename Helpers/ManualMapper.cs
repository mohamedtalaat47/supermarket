using System.Collections;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace supermarket.Helpers
{
    public class ManualMapper
    {
        public T MapManually<C, T>(C obj) where T : new()
        {
            var result = new T();
            foreach (var prop in obj.GetType().GetProperties())
            {
                result.GetType().GetProperties().Where(p => p.Name.Equals(prop.Name)).ToList().ForEach(res => res.SetValue(result, prop.GetValue(obj, null)));
            }
            return result;
        }

        public IList<TDest> MapManuallyList<TSource, TDest>(IEnumerable<TSource> sourceList) where TDest : new()
        {
            var resultList = new List<TDest>();
            var destProperties = typeof(TDest).GetProperties();
            foreach (var sourceObj in sourceList)
            {
                var result = new TDest();
                var sourceProperties = sourceObj.GetType().GetProperties();
                foreach (var sourceProp in sourceProperties)
                {
                    var destProp = destProperties.FirstOrDefault(p => p.Name == sourceProp.Name);
                    if (destProp != null)
                    {
                        destProp.SetValue(result, sourceProp.GetValue(sourceObj));
                    }
                }
                resultList.Add(result);
            }

            return resultList;
        }


        // public IList<TDest> MapManuallyCustomNames<TSource, TDest>(IEnumerable<TSource> sourceList, Dictionary<string, string> propertyMap) where TDest : new()
        // {
        //     var resultList = new List<TDest>();
        //     var destProperties = typeof(TDest).GetProperties();

        //     foreach (var sourceObj in sourceList)
        //     {
        //         var result = new TDest();
        //         var sourceProperties = sourceObj.GetType().GetProperties();

        //         foreach (var sourceProp in sourceProperties)
        //         {
        //             if (propertyMap.TryGetValue(sourceProp.Name, out string destinationPropName))
        //             {
        //                 var destProp = destProperties.FirstOrDefault(p => p.Name == destinationPropName);
        //                 if (destProp != null && destProp.CanWrite && sourceProp.CanRead)
        //                 {
        //                     var valueToSet = sourceProp.GetValue(sourceObj);
        //                     if (valueToSet != null && destProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
        //                     {
        //                         destProp.SetValue(result, valueToSet);
        //                     }
        //                 }
        //             }
        //         }

        //         resultList.Add(result);
        //     }

        //     return resultList;
        // }

        public IList<TDest> MapManuallyListCustomNames<TSource, TDest>(IEnumerable<TSource> sourceList, Dictionary<string, string> propertyMap) where TDest : new()
        {
            var resultList = new List<TDest>();
            var destProperties = typeof(TDest).GetProperties();
            foreach (var sourceObj in sourceList)
            {
                var result = new TDest();
                var sourceProperties = sourceObj.GetType().GetProperties();
                foreach (var sourceProp in sourceProperties)
                {
                    var destProp = destProperties.FirstOrDefault(p => p.Name == sourceProp.Name || (propertyMap.TryGetValue(sourceProp.Name, out string destinationPropName ) && p.Name == destinationPropName));
                    if (destProp != null)
                    {
                        destProp.SetValue(result, sourceProp.GetValue(sourceObj));
                    }
                }
                resultList.Add(result);
            }
            return resultList;
        }
    }
}