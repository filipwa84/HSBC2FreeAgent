using System;
using Gtk;

namespace HSBC2FreeAgent
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.SetPosition (WindowPosition.CenterAlways);
			win.WidthRequest = 400;
			win.Show ();
			Application.Run ();
		}
	}
}
