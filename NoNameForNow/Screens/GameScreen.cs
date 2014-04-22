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

            FlatRedBall.Debugging.Debugger.Write(GoToLevelEntityList.Count);

            CollisionActivity();

            if(InputManager.Keyboard.KeyReleased(Keys.Enter))
            {
                ChangeLevel("Level2");
            }
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

                if(mesh.Name.StartsWith("GoTo"))
                {
                    string[] gotoLevelName = mesh.Name.Split(seperators, StringSplitOptions.None);
                    Entities.GoToLevelEntity go = new Entities.GoToLevelEntity();
                    go.levelName = gotoLevelName[1];
                    go.Position = transforms[mesh.ParentBone.Index].Translation;
                    GoToLevelEntityList.Add(go);
                }

                if(mesh.Name == "VisibleTest")
                {
                    //mesh.Draw();
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

            for (int z = 0; z < GoToLevelEntityList.Count; z++)
            {
                if (PlayerInstance.cube.CollideAgainst(GoToLevelEntityList[z].cube))
                {
                    ChangeLevel(GoToLevelEntityList[z].levelName);
                }
            }

            foreach(AxisAlignedCube cube in Level1Instance.collisionCubes)
            {
                PlayerInstance.cube.CollideAgainstMove(cube, 0, 1);
            }
        }

        public void ChangeLevel(string levelName)
        {
            Level1Instance.SetLevelByName(levelName);
            for (int i = TeleInList.Count - 1; i >= 0; i--)
            {
                TeleInList[i].Destroy();
            }
            for (int x = TeleOutList.Count - 1; x >= 0; x--)
            {
                TeleOutList[x].Destroy();                
            }
            for (int z = GoToLevelEntityList.Count - 1; z >= 0; z--)
            {
                GoToLevelEntityList[z].Destroy();
            }
            
            SetUpObjects(Level1Instance.model);
        }
	}
}
