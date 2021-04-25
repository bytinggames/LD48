using JuliHelper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class RaceDialogueLevel1 : RaceDialogue
    {
        public RaceDialogueLevel1() : base()
        {
        }

        protected override IEnumerable<string> GetDialogue()
        {
            Friend();
            yield return "WHAT?!";
            yield return "no.. not now...";
            yield return "IT WON'T MOVE";
            yield return "maybe it will start when I push it";
            Swap();
            yield return "seriously?";
            Swap();
            yield return "ANYTHING but the last place!";
        }
    }
}
