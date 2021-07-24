using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Car_Physics
{
    class world
    {
        triangle[] Triangles;
        int[] Trianglebesetzt;
        private VertexBuffer floorbuffer;
        private GraphicsDevice device;
        public world(GraphicsDevice device, triangle[] triangles, int[] trianglebesetzt)
        {
            Triangles = triangles;
            Trianglebesetzt = trianglebesetzt;
            this.device = device;
            buildworld();
        }
        private void buildworld()
        {
            List<VertexPositionColor> vertexlist = new List<VertexPositionColor>();
            for (int i = 0; i < Triangles.Length; i++)
            {
                if (Trianglebesetzt[i] == 1)
                {
                    vertexlist.Add(new VertexPositionColor(Triangles[i].P1, Triangles[i].C1));
                    vertexlist.Add(new VertexPositionColor(Triangles[i].P2, Triangles[i].C2));
                    vertexlist.Add(new VertexPositionColor(Triangles[i].P3, Triangles[i].C3));
                }
            }
            floorbuffer = new VertexBuffer(device, typeof(VertexPositionColor), vertexlist.Count, BufferUsage.WriteOnly);
            floorbuffer.SetData<VertexPositionColor>(vertexlist.ToArray());
        }
        public void Draw(BasicEffect effect, BasicEffect camera, float x, float y, float z)
        {
            effect.VertexColorEnabled = true;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.World = Matrix.Identity;
            effect.World = Matrix.Identity * Matrix.CreateTranslation(x, y, z);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {

                pass.Apply();
                device.SetVertexBuffer(floorbuffer);
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, floorbuffer.VertexCount / 3);
            }
        }
    }
}
