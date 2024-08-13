using TestingSystem.Service.Interface;
using Microsoft.VisualBasic;
using System;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Model;


namespace TestingSystem.Service
{

    public sealed class LocalDbService: ILocalDbService
    {
        private const string DB_NAME = "sfGgF.db3";
        private SQLiteConnection _connection;
        private const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create  |
            SQLiteOpenFlags.SharedCache;

        public void Init()
        {
            if (_connection is not null)
                return;
            _connection = new SQLiteConnection(Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, DB_NAME), Flags);

            try
            {
                _ = _connection.CreateTable<TestDisplayer>();
                _ = _connection.CreateTable<AnswerOption>();
                _ = _connection.CreateTable<Test>();
                _ = _connection.CreateTable<QuestionTest>();
                _ = _connection.CreateTable<Category>();
            }
            catch (Exception ex)
            {

                throw;
            }
               
                //_ = _connection.CreateTable<VerificationQRCode>();
                //_ = _connection.CreateTable<MessageText>();
                //_ = _connection.CreateTable<MailAccount>();
                //_ = _connection.CreateTable<MailServer>();  
        }

        public TestDisplayer GetTestDisplayer()
        {
            Init();
            TestDisplayer wholeEvent = null;
            try
            {
                wholeEvent = _connection.GetAllWithChildren<TestDisplayer>(recursive: true).FirstOrDefault();
            }
            catch (Exception ex)
            {

               
            }
            
            if (wholeEvent is null)
            {
                wholeEvent = new TestDisplayer();
                Create(wholeEvent);
            }
            return wholeEvent;
        }

        public void CreateAndUpdate<TCreate, TUpdate>(TCreate valueCreate, TUpdate valueUpdate)
        {
            Create(valueCreate);
            Update(valueUpdate);
        }
        public void DeleteAndUpdate<TDelete, TUpdate>(TDelete valueDelete, TUpdate valueUpdate)
        {
            Delete(valueDelete);
            Update(valueUpdate);
        }

        public void Create<T>(T value)
        {
            try
            {
                Init();
                _connection.InsertWithChildren(value, recursive: true);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void Update<T>(T value)
        {
            Init();
            _connection.UpdateWithChildren(value);
        }
        
        public void DeleteFileData()
        {
            Init();
            try
            {
                _connection.Dispose();
                File.Delete(Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, DB_NAME));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }    
        }

        public void Delete<T>(T value)
        {
            Init();
            try
            {
                _connection.Delete(value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
