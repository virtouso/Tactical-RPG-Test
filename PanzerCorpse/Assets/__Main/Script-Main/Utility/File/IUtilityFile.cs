using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUtilityFile
{
    bool KeyExist(string key);
    void SaveString(string key, string value);
    string LoadString(string key);

    void SaveInt(string key, int value);
    int LoadInt(string key);


    void SaveFloat(string key, float value);
    float LoadFloat(string key);

}
