using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class OpenFileDialogWrapper
{
	// Win API method "GetOpenFileName".
	[DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	private static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
	// Win API method "GetSaveFileName".
	[DllImport("Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
	
	public enum OpenFileFlags : int
	{
		READONLY                = 0x00000001,
		OVERWRITEPROMPT         = 0x00000002,
		HIDEREADONLY            = 0x00000004,
		NOCHANGEDIR             = 0x00000008,
		
		SHOWHELP                = 0x00000010,
		ENABLEHOOK              = 0x00000020,
		ENABLETEMPLATE          = 0x00000040,
		ENABLETEMPLATEHANDLE    = 0x00000080,
		
		NOVALIDATE              = 0x00000100,
		ALLOWMULTISELECT        = 0x00000200,
		EXTENSIONDIFFERENT      = 0x00000400,
		PATHMUSTEXIST           = 0x00000800,
		
		FILEMUSTEXIST           = 0x00001000,
		CREATEPROMPT            = 0x00002000,
		SHAREAWARE              = 0x00004000,
		NOREADONLYRETURN        = 0x00008000,
		
		NOTESTFILECREATE        = 0x00010000,
		NONETWORKBUTTON         = 0x00020000,
		NOLONGNAMES             = 0x00040000,
		EXPLORER                = 0x00080000,
		
		NODEREFERENCELINKS      = 0x00100000,
		LONGNAMES               = 0x00200000,
		ENABLEINCLUDENOTIFY     = 0x00400000,
		ENABLESIZING            = 0x00800000,
		
		FORCESHOWHIDDEN         = 0x10000000,
		DONTADDTORECENT         = 0x02000000,
	}
	
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public class OpenFileName
	{
		public int structSize = 0;
		public IntPtr dlgOwner = IntPtr.Zero;
		public IntPtr instance = IntPtr.Zero;
		
		public string filter = null;
		public string customFilter = null;
		public int maxCustFilter = 0;
		public int filterIndex = 0;
		
		public string file = null;
		public int maxFile = 0;
		
		public string fileTitle = null;
		public int maxFileTitle = 0;
		
		public string initialDir = null;
		
		public string title = null;
		
		public OpenFileFlags flags ;
		public short fileOffset = 0;
		public short fileExtension = 0;
		
		public string defExt = null;
		
		public IntPtr custData = IntPtr.Zero;
		public IntPtr hook = IntPtr.Zero;
		
		public string templateName = null;
		
		public IntPtr reservedPtr = IntPtr.Zero;
		public int reservedInt = 0;
		public int flagsEx = 0;
	}
	public static string GetFileName(string aStartDir, string aTitle, string aFilterName, string aExtension)
	{
		var data = new OpenFileName();
		// never forget to set the "structSize" member to the actual size
		data.structSize = Marshal.SizeOf(data);
		
		if (string.IsNullOrEmpty(aFilterName))
			data.filter = "All Files(*.*)\0*.*\0";
		else
			data.filter = aFilterName + "\0" + "*."+aExtension+"\0All Files(*.*)\0*.*\0";
		// An example filter for several extensions would look like:
		// "Images(png/jpg/bmp)\0*.png;*.jpg;*.bmp\0"
		
		data.filterIndex = 0; // use the first filter
		
		// initialize those strings with a large enough buffer
		data.file = new string(new char[256]);
		data.maxFile = data.file.Length;
		
		data.fileTitle = new string(new char[64]);
		data.maxFileTitle = data.fileTitle.Length;
		data.defExt = aExtension;
		
		// "NOCHANGEDIR" is important at least when testing in the editor. If not set the editor
		// will crash and complain that the working directory must not be changed.
		// This still allows the user to select any file in any directory.
		data.flags = OpenFileFlags.NOCHANGEDIR | OpenFileFlags.FILEMUSTEXIST | OpenFileFlags.PATHMUSTEXIST;
		
		data.initialDir = aStartDir;
		data.title = aTitle;
		if (GetOpenFileName(data))
		{
			// "OK" has been clicked, return the filename. It's the absolute filename
			return data.file;
		}
		// we simply return an empty string if the user has clicked "cancel"
		return "";
	}
	
	public static string GetSaveFileName(string aStartDir, string aTitle, string aFilterName, string aExtension)
	{
		var data = new OpenFileName();
		data.structSize = Marshal.SizeOf(data);
		
		if (string.IsNullOrEmpty(aFilterName))
			data.filter = "All Files(*.*)\0*.*\0";
		else
			data.filter = aFilterName + "\0" + "*." + aExtension + "\0All Files(*.*)\0*.*\0";
		data.file = new string(new char[256]);
		data.maxFile = data.file.Length;
		
		data.fileTitle = new string(new char[64]);
		data.maxFileTitle = data.fileTitle.Length;
		data.defExt = aExtension;
		
		data.flags = OpenFileFlags.NOCHANGEDIR | OpenFileFlags.PATHMUSTEXIST;
		
		data.initialDir = aStartDir;
		data.title = aTitle;
		if (GetSaveFileName(data))
		{
			return data.file;
		}
		return "";
	}
}