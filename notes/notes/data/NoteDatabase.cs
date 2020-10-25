using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using notes.Models;
using System.Diagnostics;

namespace notes.Data
{
    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public NoteDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotes()
        {
            return _database.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNote(int id)
        {
            return _database.Table<Note>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> CreateNote(Note note)
        {
            _database.InsertAsync(note);
            return _database.ExecuteScalarAsync<int>(@"select MAX(id) from 'Note'"); // vrati posledni ID
        }

        public Task<int> EditNote(Note note)
        {
            return _database.UpdateAsync(note);
        }


        public Task<int> DeleteNote(Note note)
        {
            return _database.DeleteAsync(note);
        }
    }
}