using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Panzers.Utility
{
    public class UtilityNewtonSoftJson : IUtilitySerializer
    {
        public T Deserialize<T>(string serializedText)
        {

            return JsonConvert.DeserializeObject<T>(serializedText);
        }

        public string Serialize<T>(T dataModel)
        {
            return JsonConvert.SerializeObject(dataModel);
        }
    }
}
