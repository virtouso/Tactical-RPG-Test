using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILogger
{
 void ShowNormalLog(string message, Color color, Channels channel);
 void ShowWarningLog(string message, Color color,Channels channel);
 void ShowErrorLog(string message, Color color,Channels channel);
}
