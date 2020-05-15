using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoomrangInc.Classes
{
   public interface IBaseInterfaceGrid
    {
         void SearchGrid();
         void ClearSearch();

    }
    public interface IBaseInterfaceAdd
    {

        void ClearForm();

        void BindData(object data);

        object GetData(int id);

        bool SaveData(object o);

    }
}
