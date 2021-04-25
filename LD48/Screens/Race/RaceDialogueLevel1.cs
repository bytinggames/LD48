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
            yield return "noooo, the car does not work this tads fsdf sad f dsfgdf gdfgdsfgdsf gdfs gsdf gdfgdfsgdfgdsfgdfgfdgdsfgdsfgdf gsfgdf dsfg dfg ";
            Player();
            yield return "yes...";
        }
    }
}
