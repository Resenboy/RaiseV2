using Raise.Model.App;
using SQLite;
using Raise.Utils;
using System.IO;
using System;
using Xamarin.Forms;

namespace Raise.ViewModels
{
    public class AppCustomConfigurationViewModel : BaseViewModel
    {
        readonly SQLiteConnection _database;

        public AppCustomConfigurationViewModel()
#if MONOTOUCH
            : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "RaiseAppDb.db");
#else
            : this(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RaiseAppDb.db3"))
#endif
        {
        }

        public AppCustomConfigurationViewModel(string dbPath)
        {
            var table = new SQLiteConnection(dbPath).GetTableInfo("UserCustomConfigurationApp");
            _database = new SQLiteConnection(dbPath, true);

            if (table.Count == 0)
            {
                _database.CreateTable<UserCustomConfigurationApp>();
            }
        }

        public UserCustomConfigurationApp LoadUserCustomConfigurationApp()
        {
            var currentUserConfiguration = _database.Table<UserCustomConfigurationApp>().FirstOrDefault();
            return currentUserConfiguration ?? new UserCustomConfigurationApp();
        }

        public int SaveUserCustomConfigAsync(UserCustomConfigurationApp userCustomConfig)
        {
            var success = 0;

            try
            {
                if (userCustomConfig.GuidKey == GuidGenerate.USER_ID.ToString())
                {
                    success = _database.Update(userCustomConfig);
                }
                else
                {
                    success = _database.Insert(userCustomConfig);
                }

            }
            catch (Exception exc)
            {
                Shell.Current.DisplayAlert("Erro", "Exc: " + exc.Message, "OK");
            }

            return success;
        }
    }
}
