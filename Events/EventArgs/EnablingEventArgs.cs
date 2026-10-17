using Exiled.Events.EventArgs.Interfaces;
using GlobalEventFramework.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalEventFramework.Events.EventArgs
{
    public class EnablingEventArgs : IExiledEvent, IDeniableEvent
    {
        public bool IsAllowed { get; set; }
        
        public KEEvents Event { get; }

        public EnablingEventArgs(KEEvents ev,bool isallowed)
        {
            Event = ev;
            IsAllowed = isallowed;
        }
    }
}
