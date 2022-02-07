using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.Utility
{
     public interface IUtilitySerializer
     {
          T Deserialize<T>(string serializedText);
          string Serialize<T>(T dataModel);

     }
}