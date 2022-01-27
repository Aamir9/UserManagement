//using System;
//using System.Collections.Generic;
//using System.Reflection;

//namespace AutoSmartTechAPI.UserComm
//{
//    internal class DynamicMapping
//    {
//        public static T MapSamePropertiesDynamic<T>(T source)
//        {
//            var destination = Activator.CreateInstance<T>();

//            //ExpandoObject implements dictionary
//           // var properties = expando as IDictionary<string, object>;

//            PropertyInfo[] sourceInfos = source.GetType().GetProperties();
//            PropertyInfo[] desinationInfos = destination.GetType().GetProperties();
      

//            // Dictionary<string, string> properties = new Dictionary<string, string>();

//            foreach (PropertyInfo sourceProp in sourceInfos)
//            {
//                foreach (var desProp in desinationInfos)
//                {
//                   if(sourceProp.Name == desProp.Name)
//                    {
//                       //destination.GetType().GetProperty(desProp.Name).GetValue(source, null);
//                       destination.GetType().GetProperty(desProp.Name).SetValue(source, sourceProp.GetValue(destination, null));
                     
//                    }
//                }
//            }
//            return destination;

//                //if(info.GetValue(source, null) != null )
//                //{
//                //    if (info.Name == "AdditionalCharges")
//                //    {
//                //        properties.Add(info.Name, null);
//                //    }

//            //    properties.Add(info.Name, info.GetValue(source, null).ToString());
//            //}
//            //else
//            //{
//            //    properties.Add(info.Name, null);
//            //}



//            //if (properties == null)
//            //    return entity;

//            //foreach (var entry in properties)
//            //{
//            //    var propertyInfo = entity.GetType().GetProperty(entry.Key);
//            //    if (propertyInfo != null)
//            //        propertyInfo.SetValue(entity, entry.Value, null);
//            //}
//            //return entity;
//        }
//    }
//}
