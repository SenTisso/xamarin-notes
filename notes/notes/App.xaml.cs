using System;
using System.IO;
using Xamarin.Forms;
using notes.Data;
using Xamarin.Essentials;

namespace notes
{
    public partial class App : Application
    {
        static NoteDatabase database;
        public static double notesListWidth { get; set; }

        public static NoteDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Notes.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // (sirka displeje) - (padding na jedne strane * 2)
            App.notesListWidth = (mainDisplayInfo.Width / mainDisplayInfo.Density) - (16);

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#000"),
                BarTextColor = Color.FromHex("#e9ab0b")
            };

        }
    }
}
