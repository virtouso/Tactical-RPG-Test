using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : ILogger
{
    private Channels _channels;

    private void SetAllChannels()
    {
        _channels = (Channels)~0u;
    }

    private void ResetChannels()
    {
        _channels = (Channels)0u;
    }

    private bool IsChannelActive(Channels channelToCheck)
    {
        return (_channels & channelToCheck) == channelToCheck;
    }

    private void AddChannel(Channels channelToAdd)
    {
        _channels |= channelToAdd;
    }

    public Logger(ILoggerChannels loggerChannels)
    {
        switch (loggerChannels.LoggingMode)
        {
            case LoggingMode.All:
                SetAllChannels();
                break;
            case LoggingMode.None:
                ResetChannels();
                break;
            case LoggingMode.Customized:
                foreach (var item in loggerChannels.Channels)
                {
                    AddChannel(item);
                }

                break;
        }
    }


    public void ShowNormalLog(string message, Color color,Channels channel)
    {
        if (!IsChannelActive(channel)) return;
        Debug.Log($"<color=#{ ColorUtility.ToHtmlStringRGB( color)}>{message}</color>");
    }

    public void ShowWarningLog(string message, Color color, Channels channel)
    {
        if (!IsChannelActive(channel)) return;
        Debug.LogWarning($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{message}</color>");
    }

    public void ShowErrorLog(string message, Color color,Channels channel)
    {
        if (!IsChannelActive(channel)) return;
        Debug.LogError($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{message}</color>");
    }
}