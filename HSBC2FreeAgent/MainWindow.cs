using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using Gtk;



public partial class MainWindow: Gtk.Window
{

	HSBC2FreeAgent.FileDialogMethods fileDialogMethods = new HSBC2FreeAgent.FileDialogMethods ();
	string openFilename;
	string saveFilename;

	List<HSBC2FreeAgent.CSVData> listCSVData = new List<HSBC2FreeAgent.CSVData> ();


	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();

		this.Title="HSBC2FreeAgent CSV Converter";
		string path = System.Environment.CurrentDirectory;
		path = path + @"\pic\hsbclogo.png";


		SetIconFromFile (path);

		labelOpenFile.SetAlignment (0.0f, 0.5f);
		labelSaveFile.SetAlignment (0.0f, 0.5f);

		tableMain.BorderWidth = 10;

		this.Resizable = false;

		buttonOpenFile.Clicked += OnButtonOpenFileClicked;
		buttonSaveFile.Clicked += OnButtonSaveFileClicked;
		buttonConvert.Clicked += OnButtonConvertClicked;



	}

	void OnButtonOpenFileClicked(object sender, EventArgs e)
	{
		try{
			openFilename = fileDialogMethods.OpenFile (this,"*.csv");
			if(openFilename!="")
				entryOpenFile.Text = openFilename;
			entryOpenFile.Position = openFilename.Length;
		}
		catch(Exception ex) {
		}
	}

	void OnButtonSaveFileClicked(object sender, EventArgs e)
	{
		try{
			saveFilename = fileDialogMethods.SaveFile (this, "*.csv");

			if(saveFilename.Length<4 && saveFilename.Length!=0)
				saveFilename = saveFilename + ".csv";
			else if (saveFilename.Length!=0 && saveFilename.Substring (saveFilename.Length - 4) != ".csv" ){
				saveFilename = saveFilename + ".csv";
			}
			if (saveFilename!="")
				entrySaveFile.Text = saveFilename;
			entrySaveFile.Position = saveFilename.Length;
		}
		catch(Exception ex) {
		}
	}

	void OnButtonConvertClicked(object sender, EventArgs e)
	{
		listCSVData.Clear ();
		try{
			SetCSVData();
			WriteCSVFile();
			AboutDialog abt = new AboutDialog();
			abt.Modal=true;
			abt.SetPosition (WindowPosition.Center);
			abt.ProgramName="Conversion Successful!";
			abt.Run();
			abt.Destroy();

		}
		catch(Exception ex) {
			AboutDialog abt = new AboutDialog();
			abt.Modal=true;
			abt.SetPosition (WindowPosition.Center);
			abt.ProgramName="Conversion Failed!";
			abt.Run();
			abt.Destroy();
		}
	}



	void SetCSVData()
	{

		int counter=1;
		double tmp2;
		double tmp3;

		foreach(var line in File.ReadAllLines(entryOpenFile.Text,Encoding.UTF8))
		{

			if (counter > 2) {
				string[] items = line.Split (',');
				bool isNum2 = double.TryParse (items [2], System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out tmp2);
				bool isNum3 = double.TryParse (items [3], System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out tmp3);

				if (isNum2 == true ) {
					listCSVData.Add (new HSBC2FreeAgent.CSVData {
						date = DateTime.Parse(items [0]).Date.ToString("dd/MM/yyy", CultureInfo.InvariantCulture),
						desctiption = items [1],
						amount = (-tmp2).ToString ()
					});
				} else if(isNum3==true) {
					listCSVData.Add (new HSBC2FreeAgent.CSVData{ date = DateTime.Parse(items[0]).Date.ToString("dd/MM/yyy", CultureInfo.InvariantCulture), desctiption = items [1], amount = tmp3.ToString() });

				}

			}
			counter++;
		}
	}

	void WriteCSVFile()
	{
		StringBuilder csvFile = new StringBuilder ();
		for (int i = 0; i < listCSVData.Count; i++) {
			string newLine = string.Format ("{0},{1},{2}\n", listCSVData [i].date, listCSVData [i].amount, listCSVData [i].desctiption);
			csvFile.Append (newLine);
		}
		File.WriteAllText (entrySaveFile.Text, csvFile.ToString ());
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
