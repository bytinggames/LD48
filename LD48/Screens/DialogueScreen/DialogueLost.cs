using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class DialogueLost : DialogueScreen
    {
        protected override IEnumerable<string> GetDialogue()
        {
            yield return "Game Over";
        }
    }
}
