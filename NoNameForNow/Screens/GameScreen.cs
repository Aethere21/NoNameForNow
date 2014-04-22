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

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using NoNameForNow.DrawableBatches;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endif
#endregion

namespace NoNameForNow.Screens
{
	public partial class GameScreen
	{

		void CustomInitialize()
		{
            SetUpCamera(false);
            SetUpObjects(Level1Instance.model);

		}

		void CustomActivity(bool firstTimeCalled)
		{
            CameraActivity();

            FlatRedBall.Debugging.Debugger.Write(TeleInList.Count);

            CollisionActivity();
		}

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }
        public void SetUpObjects(ModelDrawableBatch model)
        {
            string[] seperators = { "_" };
            Matrix[] transforms = new Matrix[model.model.Bones.Count];
            model.model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.model.Meshes)
            {
                if (mesh.Name.StartsWith("TeleIn"))
                {
                    string[] teleNum = mesh.Name.Split(seperators, StringSplitOptions.None);
                    Entities.TeleIn teleI = new Entities.TeleIn();
                    teleI.Position = transforms[mesh.ParentBone.Index].Translation;
                    teleI.teleOut = teleNum[1];
                    TeleInList.Add(teleI);
                }

                if(mesh.Name.StartsWith("TeleOut"))
                {
                    string[] teleNum = mesh.Name.Split(seperators, StringSplitOptions.None);
                    Entities.TeleOut teleO = new Entities.TeleOut();
                    teleO.teleOut = teleNum[1];
                    teleO.Position = transforms[mesh.ParentBone.Index].Translation;
                    TeleOutList.Add(teleO);
                }

                if(mesh.Name == "SpawnPos")
                {
                    PlayerInstance.Position = transforms[mesh.ParentBone.Index].Translation;
                }
            }
        }

        private void CollisionActivity()
        {
            for (int i = 0; i < TeleInList.Count; i++)
            {
                if(PlayerInstance.cube.CollideAgainst(TeleInList[i].cube))
                {
                    for (int x = 0; x < TeleOutList.Count; x++)
                    {
                        if(TeleInList[i].teleOut == TeleOutList[x].teleOut)
                        {
                            PlayerInstance.Position = TeleOutList[x].Position;
                        }
                    }
                }
            }

            foreach(AxisAlignedCube cube in Level1Instance.collisionCubes)
            {
                PlayerInstance.cube.CollideAgainstMove(cube, 0, 1);
            }
        }
	}
}
