using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NoNameForNow.DrawableBatches
{

    public class ModelDrawableBatch : PositionedObject, IDrawableBatch
    {
        public Model model;
        private Texture2D modelTexture;
        private bool drawModel;
        private bool addTexture;

        public bool defaultLight = false;

        public Quaternion rotationQuaternion;

        public bool UpdateEveryFrame
        {
            get { return false; }
        }

        public ModelDrawableBatch(string _modelLocation, bool _draw)
        {
            model = FlatRedBallServices.Load<Model>(_modelLocation);
            drawModel = _draw;
            addTexture = false;

            GlobalLoad();
        }

        public ModelDrawableBatch(string _modelLocation, string _textureLocation, bool _draw)
        {
            model = FlatRedBallServices.Load<Model>(_modelLocation);
            modelTexture = FlatRedBallServices.Load<Texture2D>(_textureLocation);
            drawModel = _draw;
            addTexture = true;

            GlobalLoad();
        }

        public void Update()
        {
            rotationQuaternion = Quaternion.CreateFromRotationMatrix(this.RotationMatrix);
        }

        public void Destroy()
        {
            FlatRedBallServices.Unload<Model>(model, FlatRedBallServices.GlobalContentManager);
        }

        public void Draw(Camera camera)
        {
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        //effect.EnableDefaultLighting();
                        effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateFromQuaternion(rotationQuaternion) * Matrix.CreateTranslation(this.Position);
                        effect.View = Camera.Main.View;
                        effect.Projection = Camera.Main.Projection;

                        //effect.LightingEnabled = true;
                        //effect.AmbientLightColor = new Vector3(10, 10, 10);

                        RasterizerState rasterizerState = new RasterizerState();
                        rasterizerState.CullMode = CullMode.None;
                        rasterizerState.FillMode = FillMode.Solid;
                        effect.GraphicsDevice.RasterizerState = rasterizerState;

                        effect.GraphicsDevice.SamplerStates[0] = SamplerState.AnisotropicClamp;
                        effect.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                        effect.GraphicsDevice.BlendState = BlendState.Opaque;

                        effect.PreferPerPixelLighting = true;
                        effect.SpecularPower = 1f;

                        if (addTexture)
                        {
                            effect.TextureEnabled = true;
                            effect.Texture = modelTexture;
                        }
                    }
                    if (drawModel)
                    {
                        if(mesh.Name != "VisibleTest")
                        {
                            mesh.Draw();
                        }
                    }
            }
        }
        public BoundingBox BuildBoundingBox(ModelMesh mesh, Matrix meshTransform)
        {
            Vector3 meshMax = new Vector3(float.MinValue);
            Vector3 meshMin = new Vector3(float.MaxValue);

            foreach (ModelMeshPart part in mesh.MeshParts)
            {
                int stride = part.VertexBuffer.VertexDeclaration.VertexStride;

                VertexPositionNormalTexture[] vertexData = new VertexPositionNormalTexture[part.NumVertices];
                part.VertexBuffer.GetData(part.VertexOffset * stride, vertexData, 0, part.NumVertices, stride);

                Vector3 vertPosition = new Vector3();

                for (int i = 0; i < vertexData.Length; i++)
                {
                    vertPosition = vertexData[i].Position;
                    meshMin = Vector3.Min(meshMin, vertPosition);
                    meshMax = Vector3.Max(meshMax, vertPosition);
                }
            }
            meshMin = Vector3.Transform(meshMin, meshTransform);
            meshMax = Vector3.Transform(meshMax, meshTransform);

            BoundingBox box = new BoundingBox(meshMin, meshMax);
            return box;
        }

        private void GlobalLoad()
        {
            
        }
    }
}
