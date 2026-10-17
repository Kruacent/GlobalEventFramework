using System;
using CommandSystem;
using KE.Utils.API.Commands;
using GlobalEventFramework.API.Features;

namespace GlobalEventFramework.Commands
{
    public class ForceMiddleEvent : KECommand
    {
        public override string Command { get; } = "forcemiddle";
        public override string[] Aliases { get; } = new string[] { "fm" };
        public override string Description { get; } = "force a middle event";

        public override string[] Usage => new string[] { "<MiddleEvent>" };

        public override bool ExecuteCommand(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            MiddleEvent middle = null;
            if (arguments.Count > 0)
            {
                if (!KEEvents.TryGet(arguments.At(0), out var @event))
                {
                    middle = @event as MiddleEvent;
                    if(middle == null)
                    {
                        response = "not a middle event";
                        return false;
                    }

                }
                else
                {
                    response = "middle event not found";
                    return false;
                }
            }



            

            bool res = MiddleEvent.Activate(middle);

            response = res ? "activated" : "not activated";
            return res;
        }
    }
}
