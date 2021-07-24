using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Car_Physics
{
    class triangle
    {
        public Vector3 P1, P2, P3;
        public Color C1, C2, C3;
        private VertexBuffer floorbuffer;
        private GraphicsDevice device;
        public triangle(GraphicsDevice device, Vector3 p1, Vector3 p2, Vector3 p3, Color c1, Color c2, Color c3)
        {
            this.device = device;
            P1 = p1;
            P2 = p2;
            P3 = p3;
            C1 = c1;
            C2 = c2;
            C3 = c3;
        }
        private void buildtriangle()
        {
            VertexPositionColor[] vertexlist = new VertexPositionColor[3];
            vertexlist[0].Position = P1;
            vertexlist[0].Color = C1;
            vertexlist[1].Position = P2;
            vertexlist[1].Color = C2;
            vertexlist[2].Position = P3;
            vertexlist[2].Color = C3;

            floorbuffer = new VertexBuffer(device, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            floorbuffer.SetData<VertexPositionColor>(vertexlist);
        }
        public void Draw(BasicEffect effect, BasicEffect camera)
        {
            /*RasterizerState rasterizerState1 = new RasterizerState();
            rasterizerState1.CullMode = CullMode.None;
            device.RasterizerState = rasterizerState1;*/
            effect.VertexColorEnabled = true;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.World = Matrix.Identity;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {

                pass.Apply();
                device.SetVertexBuffer(floorbuffer);
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);
            }
        }
    }
}
