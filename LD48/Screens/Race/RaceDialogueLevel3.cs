using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class RaceDialogueLevel3 : RaceDialogue
    {
        public RaceDialogueLevel3() : base()
        {
        }

        protected override IEnumerable<string> GetDialogue()
        {
            Player();
            yield return "Thomas?";
            yield return "you're here AGAIN?";
            Swap();
            yield return "hey dude";
            yield return "yeah";
            yield return "your competitor went missing";
            yield return "so they let me join instead";
            Swap();
            yield return "sure";
            Swap();
            yield return "let's give it your all!";
            yield return "this is the final race";
        }
    }
}
