using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD48
{
    class UpdrawEnumerator
    {
        IEnumerator<Updraw> enumerator;
        public Updraw Current { get; set; }

        public UpdrawEnumerator(IEnumerable<Updraw> enumerable)
        {
            enumerator = enumerable.GetEnumerator();
            enumerator.MoveNext();
            Current = enumerator.Current;
        }

        public bool Update(GameTime gameTime)
        {
            if (Current == null)
                return false;

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
