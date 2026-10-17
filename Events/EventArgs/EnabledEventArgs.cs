using Exiled.Events.EventArgs.Interfaces;
using GlobalEventFramework.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalEventFramework.Events.EventArgs
{
    public class EnabledEventArgs : IExiledEvent
    {
        
        public KEEvents Event { get; }

        public EnabledEventArgs(KEEvents ev)
        {
            Event = ev;
        }
    }
}
