using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace BlueClassCompared
{
    public class ClassCompared
    {

        public static bool AbsoluteCompareResult<T>(T responseOld, T responseNew)
        {
            var resultOld = JsonConvert.SerializeObject(responseOld);
            var result = JsonConvert.SerializeObject(responseNew);

            return result == resultOld;
        }

        protected static bool CompareOneOrMoreNulls<T>(T responseOld, T responseNew)
        {
            if (responseOld == null && responseNew == null)
                return true;
            if (responseOld == null && responseNew.GetType().Name == "String" &&
                responseNew.ToString() == string.Empty)
            {
                return true;
            }
            if (responseNew == null && responseOld.GetType().Name == "String" &&
                responseOld.ToString() == string.Empty)
            {
                return true;
            }

            return false;
        }

        protected static bool CompareOneOrMoreString<T>(T responseOld, T responseNew)
        {
            //Aqui ya sabemos que ninguno es nulo, por lo que si no son ambos de tipo string, ya podremos dar un error
            if (responseOld.GetType().Name == responseNew.GetType().Name)
            {
                return responseOld.ToString().ToLower() == responseNew.ToString().ToLower();
            }
            return false;
        }

        protected static bool CompareList<T>(T responseOld, T responseNew, List<string> excludeProperties = null)
        {
            var count = 0;
            foreach (var responseItemOld in (IList)responseOld)
            {
                if (!ComparePropertiesResult(responseItemOld, ((IList)responseNew)[count], excludeProperties))
                {
                    return false;
                }
                count++;
            }
            return true;
        }

        protected static bool CompareProperties<T>(T responseOld, T responseNew, PropertyInfo[] propertiesOld, PropertyInfo[] propertiesNew, List<string> excludeProperties = null)
        {
            for (int i = 0; i < propertiesNew.Length; i++)
            {
                if (!(excludeProperties != null && excludeProperties.Contains(propertiesOld[i].Name) && propertiesOld[i].Name == propertiesNew[i].Name) &&
                    (!ComparePropertiesResult(propertiesOld[i].GetValue(responseOld),
                        propertiesNew[i].GetValue(responseNew), excludeProperties)))
                    return false;
            }

            return true;
        }

        public static bool ComparePropertiesResult<T>(T responseOld, T responseNew, List<string> excludeProperties = null)
        {
            if (responseOld == null || responseNew == null)
            {
                return CompareOneOrMoreNulls(responseOld, responseNew);
            }
            Type tOld = responseOld.GetType();
            Type tNew = responseNew.GetType();
            if (tOld.Name == "String" || tNew.Name == "String")
            {
                return CompareOneOrMoreString(responseOld, responseNew);
            }
            var propertiesOld = responseOld.GetType().GetProperties();
            var propertiesNew = responseNew.GetType().GetProperties();
            if (propertiesOld.Length != propertiesNew.Length || tOld.Name != tNew.Name)
            {
                return false;
            }

            if (propertiesNew.Length > 0)
            {
                switch (tOld.Name)
                {
                    case "List`1":
                        if (!CompareList(responseOld, responseNew, excludeProperties)) { return false; }
                        break;
                    //Agregar resto de cases si van haciendo falta
                    default:
                        if (!CompareProperties(responseOld, responseNew, propertiesOld, propertiesNew, excludeProperties))
                        { return false; }
                        break;
                }
                return true;
            }

            return (responseOld.GetType().FullName == responseNew.GetType().FullName) &&
                    (string.IsNullOrEmpty(responseOld.ToString()) &&
                    string.IsNullOrEmpty(responseNew.ToString())) ||
                   (responseOld.ToString() ==
                    responseNew.ToString());

        }

    }
}
