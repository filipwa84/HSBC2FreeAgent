using System;
using System.IO;
using Gtk;

namespace HSBC2FreeAgent
{
	public class FileDialogMethods
	{
		public FileDialogMethods ()
		{
		}

		public string OpenFile(Window activeWindow, string fileExtention)
		{
			string filename="";
			FileChooserDialog fileChooserDialogOpen = new FileChooserDialog ("Open file...", activeWindow, FileChooserAction.Open, "Cancel", ResponseType.Cancel, "Open",ResponseType.Accept);
			FileFilter filter = new FileFilter ();
			filter.AddPattern (fileExtention);
			filter.Name = "Comma separated textfile (*.csv)";

			fileChooserDialogOpen.AddFilter(filter);




			int status = fileChooserDialogOpen.Run();
			if (status == (int)ResponseType.Accept) {
				filename = fileChooserDialogOpen.Filename;
			}

			fileChooserDialogOpen.Destroy ();
			return(filename);

		}

		public string SaveFile(Window activeWindow, string fileExtention)
		{
			string filename="";
			FileChooserDialog fileChooserDialogOpen = new FileChooserDialog ("Save file...", activeWindow, FileChooserAction.Save, "Cancel", ResponseType.Cancel, "Save As", ResponseType.Accept);
			FileFilter filter = new FileFilter ();
			filter.AddPattern (fileExtention);
			filter.Name = "Comma separated textfile (*.csv)";

			fileChooserDialogOpen.DoOverwriteConfirmation = true;
			fileChooserDialogOpen.AddFilter (filter);

			int status = fileChooserDialogOpen.Run();

			if (status == (int)ResponseType.Accept) {
				filename = fileChooserDialogOpen.Filename;
			}

			fileChooserDialogOpen.Destroy ();
			return(filename);

		}

	}
}

