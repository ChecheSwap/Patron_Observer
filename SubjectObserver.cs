using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patron_Observer
{
    public interface SubjectObserver
    {
        void AddObject(Observer x);
        void DeleteObject(Observer x);
        void NotifyObjects();
    }
}
