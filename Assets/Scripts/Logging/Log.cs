using System;
using System.Threading;
using UnityEngine;
using UnityEngine.XR;

public static class Log
{
	public static readonly string[] _MessageTyes = { "INFO", "WARNING", "ERROR" };

	public static void WriteInfo(string line)
	{
		WriteLineInternal(line, 0);
	}
	public static void WriteWarning(string line)
	{
		WriteLineInternal(line, 1);
	}
	public static void WriteError(string line)
	{
		WriteLineInternal(line, 2);
	}
	public static void WriteError(Exception exception)
	{
		if(exception != null)
		{
			WriteLineInternal(exception.Message, 2);
		}
	}
	private static void WriteLineInternal(string line, int messageType)
	{
		string message = $"[{Thread.CurrentThread.ManagedThreadId,4}] [{_MessageTyes[messageType],-7}] {line}";
		switch (messageType)
		{
			case 0:
				Debug.Log(message);
				break;
			case 1:
				Debug.LogWarning(message);
				break;
			case 2:
				Debug.LogError(message);
				break;
			default:
				Debug.LogWarning($"Unknown message type {messageType}");
				break;
		}
	}
}