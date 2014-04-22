using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NoNameForNow.DrawableBatches
{

    public class DrawableBatchControl
    {
        public void LoadModel(ModelDrawableBatch model)
        {
            SpriteManager.AddPositionedObject(model);
            SpriteManager.AddZBufferedDrawableBatch(model);
        }

        public void UnloadModel(ModelDrawableBatch model)
        {
            model.Destroy();
            SpriteManager.RemoveDrawableBatch(model);
            SpriteManager.RemovePositionedObject(model);
        }

        public void UnloadModelOneWay(ModelDrawableBatch model)
        {
            model.Destroy();
            SpriteManager.RemoveDrawableBatchOneWay(model);
            SpriteManager.RemovePositionedObject(model);
        }

        //There probably is a much better way to do this.
        public List<AxisAlignedCube> CreateCube(ModelDrawableBatch model, bool visible, Color color)
        {
            List<AxisAlignedCube> cubeList = new List<AxisAlignedCube>();
            foreach (ModelMesh mesh in model.model.Meshes)
            {
                AxisAlignedCube cube = new AxisAlignedCube();
                BoundingBox bBox = new BoundingBox();
                Matrix[] transforms = new Matrix[model.model.Bones.Count];
                model.model.CopyAbsoluteBoneTransformsTo(transforms);
                bBox = model.BuildBoundingBox(mesh, transforms[mesh.ParentBone.Index] * Matrix.CreateFromQuaternion(model.rotationQuaternion) * Matrix.CreateTranslation(model.Position));
                cube.ScaleX = Math.Abs(bBox.Max.X - bBox.Min.X) / 2;
                cube.ScaleY = Math.Abs(bBox.Max.Y - bBox.Min.Y) / 2;
                cube.ScaleZ = Math.Abs(bBox.Max.Z - bBox.Min.Z) / 2;

                cube.Position.Z = bBox.Max.Z - cube.ScaleZ;
                cube.Position.X = bBox.Max.X - cube.ScaleX;
                cube.Position.Y = bBox.Max.Y - cube.ScaleY;

                cube.Visible = visible;
                cube.Color = color;
                cubeList.Add(cube);
            }
            return cubeList;
        }
    }
}