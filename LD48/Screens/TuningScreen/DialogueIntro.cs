using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class DialogueIntro : TuningDialogue
    {
        protected override IEnumerable<string> GetDialogue()
        {
            Friend();
            yield return "Hey";
            yield return "you know..";
            yield return "I like you!";
            Player();
            yield return "Why?";
            Friend();
            yield return "I just like you!";
            Player();
            yield return "ok thx";
        }
    }
}
