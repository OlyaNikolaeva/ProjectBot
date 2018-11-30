using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotienBot
{
    interface IDataService<T>
    {
        void Save(T entity);
        void GiveAway(int id, T entity);
        IEnumerable<T> GetAll();
    }
}
