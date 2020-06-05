using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LogHelper
{
	private string _Source = "";

	public LogHelper(Type sourceType)
	{
		_Source = sourceType.Name;
	}

	public void WriteInfo(string line)
	{
		Log.WriteInfo($"{_Source} -> {line}");
	}
	public void WriteWarning(string line)
	{
		Log.WriteWarning($"{_Source} -> {line}");
	}
	public void WriteError(string line)
	{
		Log.WriteError($"{_Source} -> {line}");
	}
	public void WriteError(Exception exception)
	{
		if(exception != null)
		{
			Log.WriteError($"{_Source} -> {exception.Message}");
		}
	}
}