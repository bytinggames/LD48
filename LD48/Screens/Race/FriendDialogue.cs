using JuliHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class FriendDialogue : RaceDialogue
    {
        public enum State
        {
            Hit
        }

        public State state;

        protected override IEnumerable<string> GetDialogue()
        {
            while (true)
            {
                switch (state)
                {
                    case State.Hit:
                        yield return "Ouch";
                        break;
                }
            }
        }
    }
}
