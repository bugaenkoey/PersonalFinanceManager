using System;
using System.Collections.Generic;

namespace PersonalFinanceManager.Service
{
    interface IService
    {
        IEnumerable<INote> GetAll();
        IEnumerable<INote> GetId(int id);
        IEnumerable<INote> GetBetween(DateTime dateTimeStart, DateTime dateTimeEnd);

        void Add(INote note);
        void Edit(int id, INote note);
        void DeleteId(int id);
        void DeleteBetween(DateTime dateTimeStart, DateTime dateTimeEnd);

        void Save(IEnumerable<INote> notes);
        //  IEnumerable<INote> Load();
        void Load();
    }
}
