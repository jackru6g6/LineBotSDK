using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineBotSDK.Interface
{
    interface IServiceDao
    {
        T Get<T>();
        void Add();
        void Edit();
        void Delete();
    }
}
