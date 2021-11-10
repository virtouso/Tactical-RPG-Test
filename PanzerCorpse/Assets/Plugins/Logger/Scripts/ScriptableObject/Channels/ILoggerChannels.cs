using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoggerChannels
{
    LoggingMode LoggingMode { get; }
    List<Channels> Channels { get; }
}