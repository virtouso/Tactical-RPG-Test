using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Panzers.Utility
{
    public class UtilitySimplePLayerPrefs : IUtilityFile
    {
        public bool KeyExist(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string LoadString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int LoadInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public float LoadFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }
    }
}