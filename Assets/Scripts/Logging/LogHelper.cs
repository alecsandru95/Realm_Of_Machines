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

	public void WriteLine(string line)
	{
		Log.WriteLine($"{_Source} -> {line}");
	}
}