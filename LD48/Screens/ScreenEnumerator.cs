using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD48
{
    class ScreenEnumerator
    {
        IEnumerator<GameScreen> enumerator;
        public GameScreen Current { get; set; }

        public ScreenEnumerator(IEnumerable<GameScreen> enumerable)
        {
            enumerator = enumerable.GetEnumerator();
            enumerator.MoveNext();
            Current = enumerator.Current;
        }

        public bool Update(GameTime gameTime)
        {
            if (!Current.Update(gameTime))
            {
                Current.Dispose();
                Current = null;
                if (!enumerator.MoveNext())
                    return false;
                Current = enumerator.Current;
            }
            return true;
        }
    }
}
