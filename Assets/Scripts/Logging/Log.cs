using System;
using System.Threading;
using UnityEngine;

public static class Log
{
	public static readonly string[] _MessageTyes = { "INFO", "WARNING", "ERROR" };

	public static void WriteLine(string line)
	{
		WriteLineInternal(line);
	}
	private static void WriteLineInternal(string line)
	{
		Debug.Log($"[{Thread.CurrentThread.ManagedThreadId,2}] [{_MessageTyes[0],-7}] {line}");
	}
}