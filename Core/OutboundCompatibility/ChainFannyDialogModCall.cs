﻿using CalRemix.UI;
using System;
using System.Collections.Generic;

namespace CalRemix.Core.OutboundCompatibility
{
    public class ChainFannyDialogModCall : ModCallProvider<object>
    {
        public override IEnumerable<string> CallCommands
        {
            get
            {
                yield return "ChainFannyDialog";
            }
        }

        public override string Name => "ChainFannyDialog";

        public override IEnumerable<Type> InputTypes
        {
            get
            {
                yield return typeof(HelperMessage); // The dialog which will chain into the next message 
                yield return typeof(HelperMessage); // The dialog to chained after the previous message
                yield return typeof(float); // The appear delay, in seconds.
            }
        }

        protected override object ProcessGeneric(params object[] args)
        {
            HelperMessage parent = (HelperMessage)args[0];
            HelperMessage message = (HelperMessage)args[1];
            float appearDelay = (float)args[2];

            // Apply activation requirements relative to the parent message.
            message = message.NeedsActivation().AddDelay(appearDelay);
            parent.AddEndEvent(message.ActivateMessage);

            return message;
        }
    }
}
