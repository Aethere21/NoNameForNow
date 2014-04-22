using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4 || WINDOWS_8
using Color = Microsoft.Xna.Framework.Color;
#elif FRB_MDX
using Color = System.Drawing.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using NoNameForNow.Entities;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math;

namespace NoNameForNow.Screens
{
	public partial class GameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private NoNameForNow.Entities.Player PlayerInstance;
		private NoNameForNow.Entities.Level1 Level1Instance;
		private PositionedObjectList<NoNameForNow.Entities.TeleIn> TeleInList;
		private PositionedObjectList<NoNameForNow.Entities.TeleOut> TeleOutList;

		public GameScreen()
			: base("GameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			PlayerInstance = new NoNameForNow.Entities.Player(ContentManagerName, false);
			PlayerInstance.Name = "PlayerInstance";
			Level1Instance = new NoNameForNow.Entities.Level1(ContentManagerName, false);
			Level1Instance.Name = "Level1Instance";
			TeleInList = new PositionedObjectList<NoNameForNow.Entities.TeleIn>();
			TeleInList.Name = "TeleInList";
			TeleOutList = new PositionedObjectList<NoNameForNow.Entities.TeleOut>();
			TeleOutList.Name = "TeleOutList";
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			PlayerInstance.AddToManagers(mLayer);
			Level1Instance.AddToManagers(mLayer);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				PlayerInstance.Activity();
				Level1Instance.Activity();
				for (int i = TeleInList.Count - 1; i > -1; i--)
				{
					if (i < TeleInList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						TeleInList[i].Activity();
					}
				}
				for (int i = TeleOutList.Count - 1; i > -1; i--)
				{
					if (i < TeleOutList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						TeleOutList[i].Activity();
					}
				}
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			
			TeleInList.MakeOneWay();
			TeleOutList.MakeOneWay();
			if (PlayerInstance != null)
			{
				PlayerInstance.Destroy();
				PlayerInstance.Detach();
			}
			if (Level1Instance != null)
			{
				Level1Instance.Destroy();
				Level1Instance.Detach();
			}
			for (int i = TeleInList.Count - 1; i > -1; i--)
			{
				TeleInList[i].Destroy();
			}
			for (int i = TeleOutList.Count - 1; i > -1; i--)
			{
				TeleOutList[i].Destroy();
			}
			TeleInList.MakeTwoWay();
			TeleOutList.MakeTwoWay();

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			CameraSetup.ResetCamera(SpriteManager.Camera);
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			PlayerInstance.RemoveFromManagers();
			Level1Instance.RemoveFromManagers();
			for (int i = TeleInList.Count - 1; i > -1; i--)
			{
				TeleInList[i].Destroy();
			}
			for (int i = TeleOutList.Count - 1; i > -1; i--)
			{
				TeleOutList[i].Destroy();
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				PlayerInstance.AssignCustomVariables(true);
				Level1Instance.AssignCustomVariables(true);
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			PlayerInstance.ConvertToManuallyUpdated();
			Level1Instance.ConvertToManuallyUpdated();
			for (int i = 0; i < TeleInList.Count; i++)
			{
				TeleInList[i].ConvertToManuallyUpdated();
			}
			for (int i = 0; i < TeleOutList.Count; i++)
			{
				TeleOutList[i].ConvertToManuallyUpdated();
			}
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			NoNameForNow.Entities.Player.LoadStaticContent(contentManagerName);
			NoNameForNow.Entities.Level1.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			return null;
		}
		public static object GetFile (string memberName)
		{
			return null;
		}
		object GetMember (string memberName)
		{
			return null;
		}


	}
}
