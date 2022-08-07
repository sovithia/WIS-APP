using System;
using System.Threading.Tasks;

namespace WIS.Interfaces
{        
    public interface IAppHandler
    {
        Task<bool> LaunchApp(string uri,string fallbackuri);
    }
}

