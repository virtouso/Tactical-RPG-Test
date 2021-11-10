using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LoggerChannels", menuName = "Logger/LoggerChannels")]
public class LoggerChannels :ScriptableObject, ILoggerChannels
{
   [SerializeField] private List<Channels> _channels;
   public List<Channels> Channels => _channels;

   [SerializeField] private LoggingMode _loggingMode;
   public LoggingMode LoggingMode => _loggingMode;

}
