#if UNITY_EDITOR

using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class GrpcHelper
{
	private static readonly LogHelper _Log = new LogHelper(typeof(GrpcHelper));



	[MenuItem("GRP/Build proto files")]
	private static void BuildProtoFiles()
	{
		_Log.WriteInfo("BuildProtoFiles");
		try
		{
			var protoExe = Path.GetFullPath("Assets/Plugins/Grpc.Tools/protoc.exe");
			var grpcPluginExe = Path.GetFullPath("Assets/Plugins/Grpc.Tools/grpc_csharp_plugin.exe");
			var protoFileExtension = ".proto";

			var protoFiles = Directory
					.EnumerateFiles("Assets/Scripts/", "*.*", SearchOption.AllDirectories)
					.Where(f => Path.GetExtension(f).ToLowerInvariant().Equals(protoFileExtension)).Select(p => Path.GetFullPath(p));

			foreach (var protoFile in protoFiles)
			{
				var protoFileName = Path.GetFileName(protoFile);
				_Log.WriteInfo($"Compiling protofile : {protoFileName}");

				var directory = Path.GetDirectoryName(protoFile);

				var arguments = $"/K {protoExe} -I {directory} --plugin=protoc-gen-grpc={grpcPluginExe} --csharp_out={directory} --grpc_out={directory} {protoFile}";

				//Process.Start("cmd.exe", arguments);
				
				var processStartInfo = new ProcessStartInfo()
				{
					FileName = "cmd.exe",
					UseShellExecute = false,
					Arguments = arguments,
					RedirectStandardOutput = true,
					CreateNoWindow = false
				};
				var rezultProcess = Process.Start(processStartInfo);

				var rezult = rezultProcess.StandardOutput.ReadToEnd();
				rezultProcess.WaitForExit();
			}
		}
		catch (Exception exception)
		{
			_Log.WriteInfo(exception.Message);
		}
	}
}

#endif