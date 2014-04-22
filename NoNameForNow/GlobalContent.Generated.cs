using System.Collections.Generic;
using System.Threading;
using FlatRedBall;
using FlatRedBall.Math.Geometry;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Utilities;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using FlatRedBall.Localization;

namespace NoNameForNow
{
	public static partial class GlobalContent
	{
		
		public static Microsoft.Xna.Framework.Graphics.Effect BloomCombine { get; set; }
		public static Microsoft.Xna.Framework.Graphics.Effect BloomExtract { get; set; }
		public static Microsoft.Xna.Framework.Graphics.Effect GaussianBlur { get; set; }
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "BloomCombine":
					return BloomCombine;
				case  "BloomExtract":
					return BloomExtract;
				case  "GaussianBlur":
					return GaussianBlur;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "BloomCombine":
					return BloomCombine;
				case  "BloomExtract":
					return BloomExtract;
				case  "GaussianBlur":
					return GaussianBlur;
			}
			return null;
		}
		public static bool IsInitialized { get; private set; }
		public static bool ShouldStopLoading { get; set; }
		static string ContentManagerName = "Global";
		public static void Initialize ()
		{
			
            //BloomCombine = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Effect>(@"content/globalcontent/other/bloomcombine.fx", ContentManagerName);
            //BloomExtract = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Effect>(@"content/globalcontent/other/bloomextract.fx", ContentManagerName);
            //GaussianBlur = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Effect>(@"content/globalcontent/other/gaussianblur.fx", ContentManagerName);
						IsInitialized = true;
		}
		public static void Reload (object whatToReload)
		{
		}
		
		
	}
}
