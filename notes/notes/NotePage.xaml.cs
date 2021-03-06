﻿using notes.Data;
using notes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePage : ContentPage
    {
        public NotePage()
        {
            InitializeComponent();
        }

        /// <summary>kdyz se unfocusne input u poznamky</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnEditorUnfocus(object sender, EventArgs e)
        {
            // tak poznamku ulozit/updatovat

            var note = (Note)BindingContext;

            // kdyz ma ID...
            if (note.ID != null)
            {

                // tak poznamku upravit
                await App.Database.EditNote(note);
            }
            else
            {
                // kdyz nema ID, tak ji vytvorit

                if (!(note.Title == null && note.Text == null)) // pokud poznamka neni uplne prazdna
                {
                    note.ID = await App.Database.CreateNote(note); // tak ji vytvor a nastav nove ID
                }
            }
        }

        async void OnNoteDeleted(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (note.ID != null)
            {
                note.ID = await App.Database.DeleteNote(note);
            }

            await Navigation.PopAsync();
        }
    }
}