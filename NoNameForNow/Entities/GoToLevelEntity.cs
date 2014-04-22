#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework;

#endif
#endregion

namespace NoNameForNow.Entities
{
	public partial class GoToLevelEntity
	{
        public AxisAlignedCube cube = new AxisAlignedCube();

        public string levelName;

		private void CustomInitialize()
		{
            cube.Visible = true;
            cube.Color = Color.Pink;
            cube.ScaleX = 25;
            cube.ScaleY = 25;
            cube.ScaleZ = 25;

		}

		private void CustomActivity()
		{
            cube.Position = this.Position;

		}

		private void CustomDestroy()
		{
            cube.RemoveSelfFromListsBelongingTo();

		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
