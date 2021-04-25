using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LD48
{
    class PlayerCar : Car
    {
        public override object[] GetConstructorValues() => new object[] { Pos, orientation };

        public PlayerCar(Vector2 pos, float orientation) : base(pos, orientation)
        {
            InitLowered();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var goal in Race.instance.goals)
            {
                if (goal.Mask.ColVector(Pos))
                    Race.instance.Win();
            }
        }
    }
}
