using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Threading;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace Car_Physics
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        int cameramode = 1, camerastopp = 0;
        int nantest = 0, lber;
        float radspeedtest;
        float w1, w2, fnd1, fnd2;
        float zoom = 1, altescrollmenge;
        Vector3 fn1, fn2, fnu, Sp, Sptest;
        int punktposupdate = 0;
        int physicabbruch = 0;
        int slowmotionstop1 = 0, slowmotionstop2 = 0;
        int slowmotion = 16;
        int tttest = 0;
        int physicsstart = 0;
        float gt2;
        int punktmenge;
        float mapmodellvertnum;
        float power = 1f;
        float b, j;
        int yoffset = 500;
        float xoffset = 10000;
        float dist;
        int aaaaa = 0;
        int count42 = 0;
        int FUCK = 0;
        int speedacount;
        float grenze = 0;
        float tw, tg;
        float aba = 0;
        bool ergebniss;
        triangle tr1;
        Viewport ss2 = new Viewport();
        Vector3 verschiebung, verschiebungupdate;
        triangle[] maptriangles = new triangle[8400000];
        int[] maptrianglesbesetzt = new int[8400000];
        world map;
        Texture2D mapheighttex;
        Vector3[,] mapvertexheight;
        Color[,] mapvertexcolor;
        rechteck[,] maprechtecke;
        int terrainWidth;
        int terrainHeight;
        VertexPositionNormalTexture[] maptex;
        VertexBuffer terrainVertexBuffer;
        IndexBuffer terrainIndexBuffer;
        VertexDeclaration terrainVertexDeclaration;
        Vector3 tctest;
        Vector3 tcposvec;
        Vector3 endspeed;
        floor Floor;
        Plane rampe;
        Vector3 testspeed;
        Vector3 cabrot;
        BasicEffect effect;
        Vector2 mouseposition3d;
        float stangenweichheit = 1f;
        GraphicsDeviceManager graphics;
        Matrix[] originalbone = new Matrix[6];
        double[] radrotation = new double[4];
        double[] radrotation2 = new double[4];
        Vector3[] alteradpos = new Vector3[4];
        Model wheelmodel, mapmodell;
        Matrix originalmap;
        Matrix[] bonetransformations;
        int st1, st2;
        int stop2, wind = 1;
        int beidepunktegezogen;
        int[,] punktnum = new int[50, 50];
        int gezogenermuskel, gezogenermuskelp1, gezogenermuskelp2;
        int geklickterpunkt, stopp, platzieren = 1, stopp2;
        Vector2 unterschied1, unterschied2;
        Vector2[] unterschied3 = new Vector2[100];
        Vector3 camera = Vector3.Zero;
        Vector3 transformedReference;
        Vector3 cameraLookat;
        SpriteBatch spriteBatch;
        Texture2D kreis;
        Random r = new Random();
        const int ballmenge = 300;
        int[] punktbesetzt = new int[ballmenge];
        int[] punktbodenkontakt = new int[ballmenge];
        Vector3[] punktpos = new Vector3[ballmenge];
        Vector3[] punktpos2 = new Vector3[ballmenge];
        Vector3[] altepunktpos = new Vector3[ballmenge];
        Vector3[] punktspeed = new Vector3[ballmenge];
        Vector3[] punkta = new Vector3[ballmenge];
        float[] grippunktspeed = new float[ballmenge];
        float[] planecarwinkel = new float[ballmenge];
        float[] punktmasse = new float[ballmenge];
        float[] punktgrip = new float[ballmenge];
        Vector3[] punkttrinormale = new Vector3[ballmenge];
        const int muskelmenge = 12000;
        int[] muskelbesetzt = new int[muskelmenge];
        int[] muskelpunkt1 = new int[muskelmenge];
        int[] muskelpunkt2 = new int[muskelmenge];
        float[] muskelstärke = new float[muskelmenge];
        float[] muskelabdämpfung = new float[muskelmenge];
        float[] muskelabdämpfung2 = new float[muskelmenge];
        float[] idealemuskellänge = new float[muskelmenge];
        int[] muskelposänderung = new int[muskelmenge];
        Vector3 windspeed;
        SpriteFont a;

        GraphicsDevice device;
        Texture2D grassTexture;
        float yrotcar, yrotpoint;
        float steeringrot = 0;
        Vector3 speedabzugvec;
        Vector2 radrichtung;
        Vector3 newspeed;
        float rotu;
        float faktor;
        float speedlänge;
        float speedabzug;
        Vector3 movevector;
        Vector3 cameralookatges;
        private BasicEffect basicEffect;
        private Vector3 startPoint = new Vector3(0, 0, 0);
        private Vector3 endPoint = new Vector3(1, 0, 50);
        Vector3 cameraReference = new Vector3(0, 0, 1);
        Vector3 rotation;
        Matrix rotationMatrix;
        Vector3 mouserotationbuffer;
        double gametime2;
        float pos = 0;
        Vector3 cameraverschiebung;
        public void MoveTo(Vector3 pos, Vector3 rot)
        {
            rotation = rot;
            camera = pos;
        }
        public Vector3 PreviewMove(Vector3 amount)
        {
            Matrix rotate = Matrix.CreateRotationY(rotation.Y);
            Vector3 movement = new Vector3(amount.X, amount.Y, amount.Z);
            movement = Vector3.Transform(movement, rotate);
            return camera + movement;
        }
        public void Move(Vector3 scale)
        {
            MoveTo(PreviewMove(scale), rotation);
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                GraphicsProfile = GraphicsProfile.HiDef,
                PreferredBackBufferWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width,
                PreferredBackBufferHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height,
                IsFullScreen = false,
                SynchronizeWithVerticalRetrace = false

            };
            IsFixedTimeStep = false;
            Window.IsBorderless = true;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }
        //private static readonly Dictionary<String, List<Vector2>> arcCache = new Dictionary<string, List<Vector2>>();
        private VertexPositionNormalTexture[] SetUpTerrainVertices()
        {
            VertexPositionNormalTexture[] terrainVertices = new VertexPositionNormalTexture[512 * 512];

            for (int x = 0; x < 512; x++)
            {
                for (int y = 0; y < 512; y++)
                {
                    terrainVertices[x + y * 512].Position = new Vector3(x, 10, -y);
                    terrainVertices[x + y * 512].TextureCoordinate.X = (float)x / 30.0f;
                    terrainVertices[x + y * 512].TextureCoordinate.Y = (float)y / 30.0f;

                }
            }

            return terrainVertices;
        }

        private static Texture2D pixel;
        private static void CreateThePixel(SpriteBatch spriteBatch)
        {
            pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
        }
        public static void DrawLine(SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, 1.0f);
        }
        public static void DrawLine(SpriteBatch spriteBatch, float x1, float y1, float x2, float y2, Color color, float thickness)
        {
            DrawLine(spriteBatch, new Vector2(x1, y1), new Vector2(x2, y2), color, thickness);
        }
        public static void DrawLine(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color)
        {
            DrawLine(spriteBatch, point1, point2, color, 1.0f);
        }
        public static void DrawLine(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness)
        {
            // calculate the distance between the two vectors
            float distance = Vector2.Distance(point1, point2);

            // calculate the angle between the two vectors
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }
        public static void DrawLine(SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color)
        {
            DrawLine(spriteBatch, point, length, angle, color, 1.0f);
        }
        public static void DrawLine(SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness)
        {
            if (pixel == null)
            {
                CreateThePixel(spriteBatch);
            }
            spriteBatch.Draw(pixel, point, null, color, angle, Vector2.Zero, new Vector2(length, thickness), SpriteEffects.None, 0);
        }
        public Vector3 TriangleCollision(Vector3 tp1, Vector3 tp2, Vector3 tp3, Vector3 lp1, Vector3 lp2)
        {
            Vector3 returnvector;
            Vector3 lv = lp2 - lp1;
            Ray line = new Ray(lp1, lv);
            Plane plane = new Plane(tp1, tp2, tp3);
            float erg;
            //Ray line2 = new Ray(new Vector3(0, 0, 0), new Vector3(0, 100, 0));
            //plane = new Plane(new Vector3(-50, 50, -50), new Vector3(50, 50, -50), new Vector3(0, 50, 50));
            var t = line.Intersects(plane);
            if (t.HasValue)
            {
                double l1, l2, winkel;
                int abbruch = 0;
                float x = lp1.X + (float)t * lv.X;
                float y = lp1.Y + (float)t * lv.Y;
                float z = lp1.Z + (float)t * lv.Z;
                Vector3 colp = new Vector3(x, y, z);
                tcposvec = tp1;
                double a = Vector3.Distance(tp2, tp3);
                double b = Vector3.Distance(tp3, tp1);
                double c = Vector3.Distance(tp1, tp2);
                double alpha = Math.Acos((b * b + c * c - a * a) / (2 * b * c));
                double beta = (float)Math.Acos((a * a + c * c - b * b) / (2 * a * c));
                double gamma = (float)Math.Acos((a * a + b * b - c * c) / (2 * a * b));

                // FALL 1: alpha

                l1 = Vector3.Distance(tp1, colp);
                l2 = Vector3.Distance(tp2, colp);
                winkel = Math.Acos((l1 * l1 + c * c - l2 * l2) / (2 * l1 * c));
                if (winkel > alpha)
                {
                    return Vector3.Zero;
                }
                l1 = Vector3.Distance(tp1, colp);
                l2 = Vector3.Distance(tp3, colp);
                winkel = Math.Acos((l1 * l1 + b * b - l2 * l2) / (2 * l1 * b));
                if (winkel > alpha)
                {
                    return Vector3.Zero;
                }
                //tw = (l1 * l1 + c * c - l2 * l2) / (2 * l1 * c);
                //tcposvec = new Vector3(l2, l1, c);
                // FALL 2: beta

                l1 = Vector3.Distance(tp2, colp);
                l2 = Vector3.Distance(tp3, colp);
                winkel = Math.Acos((l1 * l1 + a * a - l2 * l2) / (2 * l1 * a));
                if (winkel > beta)
                {
                    return Vector3.Zero;
                }
                l1 = Vector3.Distance(tp2, colp);
                l2 = Vector3.Distance(tp1, colp);
                winkel = Math.Acos((l1 * l1 + c * c - l2 * l2) / (2 * l1 * c));
                if (winkel > beta)
                {
                    return Vector3.Zero;
                }
                // FALL 3: gamma

                l1 = Vector3.Distance(tp3, colp);
                l2 = Vector3.Distance(tp1, colp);
                winkel = Math.Acos((b * b + l1 * l1 - l2 * l2) / (2 * b * l1));
                if (winkel > gamma)
                {
                    return Vector3.Zero;
                }
                l1 = Vector3.Distance(tp3, colp);
                l2 = Vector3.Distance(tp2, colp);
                winkel = Math.Acos((a * a + l1 * l1 - l2 * l2) / (2 * a * l1));
                if (winkel > gamma)
                {
                    return Vector3.Zero;
                }
                /*if (lp1.Y + 1000 < colp.Y)
                {
                    return Vector3.Zero;
                }*/
                return colp;
            }
            else
            {
                return Vector3.Zero;
            }
            //ergebniss = t.HasValue;
            //var t2 = line2.Intersects(plane);
        }



        protected override void Initialize()
        {
            var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(this.Window.Handle);
            form.Location = new System.Drawing.Point(0, 0);
            RasterizerState raststate = new RasterizerState();
            raststate.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = raststate;
            Floor = new floor(GraphicsDevice, 200, 200, -770, 1000);
            effect = new BasicEffect(GraphicsDevice);
            RasterizerState rasterizerState1 = new RasterizerState();
            rasterizerState1.CullMode = CullMode.None;
            graphics.GraphicsDevice.RasterizerState = rasterizerState1;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90), GraphicsDevice.Viewport.AspectRatio, 10f, 1000000f);
            basicEffect.View = Matrix.CreateLookAt(new Vector3(50, 50, 50), new Vector3(0, 0, 0), Vector3.Up);
            //graphics.PreferredBackBufferFormat = SurfaceFormat.HdrBlendable;
            tr1 = new triangle(GraphicsDevice, new Vector3(-300, -700, -5000), new Vector3(300, -700, -5000), new Vector3(0, -500, -5300), Color.Red, Color.Green, Color.Blue);
            rampe = new Plane(new Vector3(-300, -700, -5000), new Vector3(300, -700, -5000), new Vector3(0, -500, -5300));
            windspeed = new Vector3(0.01f, 0, 0);
            kreis = Content.Load<Texture2D>("10p kreis");
            a = Content.Load<SpriteFont>("SpriteFont1");
            wheelmodel = Content.Load<Model>("buggy2");
            mapmodell = Content.Load<Model>("heightmap1model");
            grassTexture = Content.Load<Texture2D>("grass");
            mapheighttex = Content.Load<Texture2D>("maphoch");
            maptex = SetUpTerrainVertices();
            for (int i = 0; i < 6; i++)
            {
                originalbone[i] = wheelmodel.Bones[i].Transform;
            }
            originalmap = mapmodell.Bones[0].Transform;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = GraphicsDevice;
            RasterizerState rasterizerState1 = new RasterizerState();
            rasterizerState1.CullMode = CullMode.None;
            graphics.GraphicsDevice.RasterizerState = rasterizerState1;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            punktbesetzt[0] = 1;
            punktbesetzt[1] = 1;
            punktbesetzt[2] = 1;
            punktbesetzt[3] = 1;
            punktbesetzt[4] = 1;
            punktbesetzt[5] = 1;
            punktbesetzt[6] = 1;
            punktbesetzt[7] = 1;
            punktbesetzt[8] = 1;
            punktbesetzt[9] = 1;
            punktbesetzt[10] = 1;
            punktbesetzt[11] = 1;
            punktbesetzt[12] = 1;
            punktbesetzt[13] = 1;
            punktbesetzt[14] = 1;
            punktbesetzt[15] = 1;
            punktbesetzt[16] = 1;
            punktbesetzt[17] = 1;
            punktbesetzt[18] = 1;
            punktbesetzt[19] = 1;
            punktbesetzt[20] = 1;
            punktbesetzt[21] = 1;
            punktmenge = 30;
            for (int i = 0; i < punktmenge; i++)
            {
                punktmasse[i] = 1;
                punktbesetzt[i] = 1;
            }
            //punktbesetzt[9] = 1;
            punktpos[0] = new Vector3(0, 0 + yoffset, 0);
            punktpos[1] = new Vector3(180f, -30f + yoffset, 300);
            punktpos[2] = new Vector3(-180f, -30f + yoffset, 300);
            punktpos[3] = new Vector3(-180f, -30f + yoffset, -330);
            punktpos[4] = new Vector3(180f, -30f + yoffset, -330);
            punktpos[5] = new Vector3(0, 75f + yoffset, 0);
            punktpos[6] = new Vector3(0, 0 + yoffset, -330);
            punktpos[7] = new Vector3(-180, 0 + yoffset, 0);
            punktpos[8] = new Vector3(180, 0 + yoffset, 0);
            punktpos[9] = new Vector3(60f, 270 + yoffset, 250f);
            punktpos[10] = new Vector3(-60f, 270 + yoffset, 250f);
            punktpos[11] = new Vector3(-60, 270 + yoffset, -30);
            punktpos[12] = new Vector3(60, 270 + yoffset, -30);
            punktpos[13] = new Vector3(180 - 100, 0 + yoffset, 300);
            punktpos[14] = new Vector3(-180 + 100, 0 + yoffset, 300);
            punktpos[15] = new Vector3(-180 + 100, 0 + yoffset, -330);
            punktpos[16] = new Vector3(180 - 100, 0 + yoffset, -330);
            punktpos[17] = new Vector3(0, 270 + yoffset, 0);

            punktpos[18] = new Vector3(180 - 100, 150 + yoffset, 300);
            punktpos[19] = new Vector3(-180 + 100, 150 + yoffset, 300);
            punktpos[20] = new Vector3(-180 + 100, 150 + yoffset, -330);
            punktpos[21] = new Vector3(180 - 100, 150 + yoffset, -330);

            for (int i = 0; i < punktmenge; i++)
            {
                if (punktbesetzt[i] == 1)
                {
                    punktpos[i].X += xoffset;
                }
            }

            /*punktpos[0] = new Vector3(0, 0 + yoffset, 0);
            punktpos[1] = new Vector3(185.6f, -105.47f + yoffset, 389.2f);
            punktpos[2] = new Vector3(-185.6f, -105.47f + yoffset, 389.2f);
            punktpos[3] = new Vector3(-185.6f, -105.47f + yoffset, -257.6f);
            punktpos[4] = new Vector3(185.6f, -105.47f + yoffset, -257.6f);
            punktpos[5] = new Vector3(0, -75f + yoffset, 0);
            punktpos[6] = new Vector3(0, 0 + yoffset, -257.6f);
            punktpos[7] = new Vector3(-185.6f, 0 + yoffset, 0);
            punktpos[8] = new Vector3(185.6f, 0 + yoffset, 0);
            punktpos[9] = new Vector3(-150f, 310 + yoffset, -50);
            punktpos[10] = new Vector3(150f, 310 + yoffset, -50);
            punktpos[11] = new Vector3(-150f, 310 + yoffset, 350f);
            punktpos[12] = new Vector3(150f, 310 + yoffset, 350f);
            punktpos[13] = new Vector3(185.6f, 0 + yoffset, 389.2f);
            punktpos[14] = new Vector3(-185.6f, 0 + yoffset, 389.2f);
            punktpos[15] = new Vector3(-185.6f, 0 + yoffset, -257.6f);
            punktpos[16] = new Vector3(185.6f, 0 + yoffset, -257.6f);
            punktpos[17] = new Vector3(0, 310 + yoffset, 0);*/

            punktmasse[9] = 1f;
            punktmasse[10] = 1f;
            punktmasse[11] = 1f;
            punktmasse[12] = 1f;
            punktmasse[2] = 1f;
            punktmasse[3] = 1f;
            punktmasse[4] = 1f;
            punktmasse[1] = 1f;
            //punktpos[9] = new Vector3(0, -105.47f, -257.6f);
            //punktspeed[2].X = 1f;
            //punktspeed[1].X = 1f;
            //punktspeed[3].X = 1f;
            //punktspeed[4].X = 1f;

            //punktspeed[1].Y = 2f;




            muskelstärke[0] = stangenweichheit;
            muskelstärke[1] = stangenweichheit;
            muskelstärke[2] = stangenweichheit;
            muskelstärke[3] = stangenweichheit;
            muskelstärke[4] = stangenweichheit;
            muskelstärke[5] = stangenweichheit;
            muskelstärke[6] = stangenweichheit;
            muskelstärke[7] = stangenweichheit;
            idealemuskellänge[0] = Vector3.Distance(punktpos[muskelpunkt1[0]], punktpos[muskelpunkt2[0]]);
            idealemuskellänge[1] = Vector3.Distance(punktpos[muskelpunkt1[1]], punktpos[muskelpunkt2[1]]);
            idealemuskellänge[2] = Vector3.Distance(punktpos[muskelpunkt1[2]], punktpos[muskelpunkt2[2]]);
            idealemuskellänge[3] = Vector3.Distance(punktpos[muskelpunkt1[3]], punktpos[muskelpunkt2[3]]);
            idealemuskellänge[4] = Vector3.Distance(punktpos[muskelpunkt1[4]], punktpos[muskelpunkt2[4]]);
            idealemuskellänge[5] = Vector3.Distance(punktpos[muskelpunkt1[5]], punktpos[muskelpunkt2[5]]);
            idealemuskellänge[6] = Vector3.Distance(punktpos[muskelpunkt1[6]], punktpos[muskelpunkt2[6]]);
            idealemuskellänge[7] = Vector3.Distance(punktpos[muskelpunkt1[7]], punktpos[muskelpunkt2[7]]);
            idealemuskellänge[8] = Vector3.Distance(punktpos[muskelpunkt1[8]], punktpos[muskelpunkt2[8]]);
            idealemuskellänge[9] = Vector3.Distance(punktpos[muskelpunkt1[9]], punktpos[muskelpunkt2[9]]);
            idealemuskellänge[10] = Vector3.Distance(punktpos[muskelpunkt1[10]], punktpos[muskelpunkt2[10]]);
            idealemuskellänge[11] = Vector3.Distance(punktpos[muskelpunkt1[11]], punktpos[muskelpunkt2[11]]);
            idealemuskellänge[12] = Vector3.Distance(punktpos[muskelpunkt1[12]], punktpos[muskelpunkt2[12]]);
            idealemuskellänge[13] = Vector3.Distance(punktpos[muskelpunkt1[13]], punktpos[muskelpunkt2[13]]);
            idealemuskellänge[14] = Vector3.Distance(punktpos[muskelpunkt1[14]], punktpos[muskelpunkt2[14]]);
            idealemuskellänge[15] = Vector3.Distance(punktpos[muskelpunkt1[15]], punktpos[muskelpunkt2[15]]);
            idealemuskellänge[16] = Vector3.Distance(punktpos[muskelpunkt1[16]], punktpos[muskelpunkt2[16]]);
            idealemuskellänge[17] = Vector3.Distance(punktpos[muskelpunkt1[17]], punktpos[muskelpunkt2[17]]);
            idealemuskellänge[18] = Vector3.Distance(punktpos[muskelpunkt1[18]], punktpos[muskelpunkt2[18]]);
            idealemuskellänge[19] = Vector3.Distance(punktpos[muskelpunkt1[19]], punktpos[muskelpunkt2[19]]);
            idealemuskellänge[20] = Vector3.Distance(punktpos[muskelpunkt1[20]], punktpos[muskelpunkt2[20]]);
            idealemuskellänge[21] = Vector3.Distance(punktpos[muskelpunkt1[21]], punktpos[muskelpunkt2[21]]);
            int muskelnummer = 0;

            muskelbesetzt[muskelnummer] = 1;
            muskelpunkt1[muskelnummer] = 2;
            muskelpunkt2[muskelnummer] = 7;
            muskelposänderung[muskelnummer] = 1;
            muskelstärke[muskelnummer] = 25;
            muskelabdämpfung[muskelnummer] = 0;
            muskelabdämpfung2[muskelnummer] = 1;
            idealemuskellänge[muskelnummer] = Vector3.Distance(punktpos[muskelpunkt1[muskelnummer]], punktpos[muskelpunkt2[muskelnummer]]);
            muskelnummer++;
            muskelbesetzt[muskelnummer] = 1;
            muskelpunkt1[muskelnummer] = 3;
            muskelpunkt2[muskelnummer] = 7;
            muskelposänderung[muskelnummer] = 1;
            muskelstärke[muskelnummer] = 25;
            muskelabdämpfung[muskelnummer] = 0;
            muskelabdämpfung2[muskelnummer] = 1;
            idealemuskellänge[muskelnummer] = Vector3.Distance(punktpos[muskelpunkt1[muskelnummer]], punktpos[muskelpunkt2[muskelnummer]]);
            muskelnummer++;
            muskelbesetzt[muskelnummer] = 1;
            muskelpunkt1[muskelnummer] = 1;
            muskelpunkt2[muskelnummer] = 8;
            muskelposänderung[muskelnummer] = 1;
            muskelstärke[muskelnummer] = 25;
            muskelabdämpfung[muskelnummer] = 0;
            muskelabdämpfung2[muskelnummer] = 1;
            idealemuskellänge[muskelnummer] = Vector3.Distance(punktpos[muskelpunkt1[muskelnummer]], punktpos[muskelpunkt2[muskelnummer]]);
            muskelnummer++;
            muskelbesetzt[muskelnummer] = 1;
            muskelpunkt1[muskelnummer] = 4;
            muskelpunkt2[muskelnummer] = 8;
            muskelposänderung[muskelnummer] = 1;
            muskelstärke[muskelnummer] = 25;
            muskelabdämpfung[muskelnummer] = 0;
            muskelabdämpfung2[muskelnummer] = 1;
            idealemuskellänge[muskelnummer] = Vector3.Distance(punktpos[muskelpunkt1[muskelnummer]], punktpos[muskelpunkt2[muskelnummer]]);
            muskelnummer++;

            muskelbesetzt[muskelnummer] = 1;
            muskelpunkt1[muskelnummer] = 1;
            muskelpunkt2[muskelnummer] = 2;
            muskelposänderung[muskelnummer] = 1;
            muskelstärke[muskelnummer] = 0f;
            muskelabdämpfung[muskelnummer] = 0.25f;
            muskelabdämpfung2[muskelnummer] = 50;
            idealemuskellänge[muskelnummer] = Vector3.Distance(punktpos[muskelpunkt1[muskelnummer]], punktpos[muskelpunkt2[muskelnummer]]);
            muskelnummer++;
            muskelbesetzt[muskelnummer] = 1;
            muskelpunkt1[muskelnummer] = 3;
            muskelpunkt2[muskelnummer] = 4;
            muskelposänderung[muskelnummer] = 1;
            muskelstärke[muskelnummer] = 0.25f;
            muskelabdämpfung[muskelnummer] = 0f;
            muskelabdämpfung2[muskelnummer] = 50;
            idealemuskellänge[muskelnummer] = Vector3.Distance(punktpos[muskelpunkt1[muskelnummer]], punktpos[muskelpunkt2[muskelnummer]]);
            muskelnummer++;

            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    if (i != 1 && i != 2 && i != 3 && i != 4 && j != 1 && j != 2 && j != 3 && j != 4)
                    {
                        if (i != j)
                        {
                            muskelbesetzt[muskelnummer] = 1;
                            muskelpunkt1[muskelnummer] = i;
                            muskelpunkt2[muskelnummer] = j;
                            muskelposänderung[muskelnummer] = 1;
                            muskelstärke[muskelnummer] = 25;
                            muskelabdämpfung[muskelnummer] = 0;
                            muskelabdämpfung2[muskelnummer] = 1;
                            idealemuskellänge[muskelnummer] = Vector3.Distance(punktpos[muskelpunkt1[muskelnummer]], punktpos[muskelpunkt2[muskelnummer]]);
                            muskelnummer++;
                        }
                    }
                }
            }

            for (int i = 0; i < 18; i++)
            {
                if (i == 1 || i == 2 || i == 3 || i == 4)
                {
                    muskelbesetzt[muskelnummer] = 1;
                    muskelpunkt1[muskelnummer] = i;
                    muskelpunkt2[muskelnummer] = i + 17;
                    muskelposänderung[muskelnummer] = 2;
                    muskelstärke[muskelnummer] = 0.75f;
                    muskelabdämpfung[muskelnummer] = 0.1f;
                    muskelabdämpfung2[muskelnummer] = 1f;
                    idealemuskellänge[muskelnummer] = Vector3.Distance(punktpos[muskelpunkt1[muskelnummer]], punktpos[muskelpunkt2[muskelnummer]]);
                    muskelnummer++;
                }
            }


            for (int i = 0; i < punktmenge; i++)
            {
                for (int j = 0; j < punktmenge; j++)
                {
                    if (i == 18 || i == 19 || i == 20 || i == 21 || i == 13 || i == 14 || i == 15 || i == 16)
                    {
                        if (j == 18 || j == 19 || j == 20 || j == 21 || j == 13 || j == 14 || j == 15 || j == 16)
                        {
                            if (i != j)
                            {
                                muskelbesetzt[muskelnummer] = 1;
                                muskelpunkt1[muskelnummer] = i;
                                muskelpunkt2[muskelnummer] = j;
                                muskelposänderung[muskelnummer] = 1;
                                muskelstärke[muskelnummer] = 60;
                                muskelabdämpfung[muskelnummer] = 0;
                                muskelabdämpfung2[muskelnummer] = 1;
                                idealemuskellänge[muskelnummer] = Vector3.Distance(punktpos[muskelpunkt1[muskelnummer]], punktpos[muskelpunkt2[muskelnummer]]);
                                muskelnummer++;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < punktmenge; i++)
            {
                for (int j = 0; j < punktmenge; j++)
                {
                    if (i == 18 || i == 19 || i == 20 || i == 21 || i == 9 || i == 19 || i == 11 || i == 12)
                    {
                        if (j == 18 || j == 19 || j == 20 || j == 21 || j == 9 || j == 10 || j == 11 || j == 12)
                        {
                            if (i != j)
                            {
                                muskelbesetzt[muskelnummer] = 1;
                                muskelpunkt1[muskelnummer] = i;
                                muskelpunkt2[muskelnummer] = j;
                                muskelposänderung[muskelnummer] = 1;
                                muskelstärke[muskelnummer] = 60;
                                muskelabdämpfung[muskelnummer] = 0;
                                muskelabdämpfung2[muskelnummer] = 1;
                                idealemuskellänge[muskelnummer] = Vector3.Distance(punktpos[muskelpunkt1[muskelnummer]], punktpos[muskelpunkt2[muskelnummer]]);
                                muskelnummer++;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < punktmenge; i++)
            {
                if (i == 13 || i == 14 || i == 15 || i == 16)
                {

                    muskelbesetzt[muskelnummer] = 1;
                    muskelpunkt1[muskelnummer] = i;
                    muskelpunkt2[muskelnummer] = i - 12;
                    muskelposänderung[muskelnummer] = 1;
                    muskelstärke[muskelnummer] = 50f;
                    muskelabdämpfung[muskelnummer] = 0;
                    muskelabdämpfung2[muskelnummer] = 1;
                    idealemuskellänge[muskelnummer] = Vector3.Distance(punktpos[muskelpunkt1[muskelnummer]], punktpos[muskelpunkt2[muskelnummer]]);
                    muskelnummer++;
                }

            }

            muskelnummer++;

            mapvertexheight = new Vector3[mapheighttex.Width, mapheighttex.Height];
            mapvertexcolor = new Color[mapheighttex.Width, mapheighttex.Height];
            maprechtecke = new rechteck[mapheighttex.Width - 1, mapheighttex.Height - 1];
            terrainWidth = mapheighttex.Width;
            terrainHeight = mapheighttex.Height;

            //TextureCreationParameters tparams = new TextureCreationParameters(1024, 1024, 32, 1, SurfaceFormat.Single, ResourceUsage.None, ResourcePool.Managed, Color.AliceBlue, FilterOptions.None, FilterOptions.None);

            //Bitmap map2;// = Bitmap.FromFile("C:/Users/marvi/Documents/maphoch2.png") as Bitmap;
            //map2 = Bitmap.FromFile("C:/Users/marvi/Documents/maphoch2.tif") as Bitmap;
            //var heightMapColors = new Color[terrainWidth * terrainHeight];
            //mapheighttex.GetData(heightMapColors, 0, 12000);
            //BitmapData bd = map2.LockBits(new System.Drawing.Rectangle(0, 0, 1024, 1024), ImageLockMode.ReadWrite, PixelFormat.Format48bppRgb);
            //int strip = bd.Stride;
            unsafe
            {
                int kl = 0;
                Vector3[] vertexpos = new Vector3[terrainWidth * terrainHeight];
                //VertexPositionNormalTexture[] vert = new VertexPositionNormalTexture[terrainWidth * terrainHeight];
                float[] vertices;
                VertexPositionNormalTexture[] uvert = new VertexPositionNormalTexture[terrainHeight * terrainWidth];


                string[] lines = new string[terrainHeight];
                OpenFileDialog worldload = new OpenFileDialog();
                worldload.FileName = "maptest";
                lines = File.ReadAllLines(worldload.FileName);
                string[] linienteile = new string[3];
                for (int i = 0; i < terrainHeight * terrainWidth; i++)
                {
                    linienteile = lines[i].Split('|');
                    vertexpos[i].X = (float)Convert.ToDouble(linienteile[0]);
                    vertexpos[i].Y = (float)Convert.ToDouble(linienteile[1]);
                    vertexpos[i].Z = (float)Convert.ToDouble(linienteile[2]);
                }

                //Int16* ptr = (Int16*)bd.Scan0;
                for (int x = 0; x < terrainWidth; x++)
                {
                    for (int y = 0; y < terrainHeight; y++)
                    {
                        //Bgra4444 ass = new Bgra4444(heightMapColors[x + y * terrainWidth].ToVector4());
                        //mapvertexheight[x, y].Y = (ptr[(x * 3) + y * (strip/2)])*5;
                        mapvertexheight[x, y].Y = vertexpos[x + (terrainHeight - y - 1) * terrainWidth].Y * 8;
                        mapvertexheight[x, y].X = x * 750 - (terrainWidth * 750 / 2);
                        mapvertexheight[x, y].Z = y * 750 - (terrainHeight * 750 / 2);
                        //mapvertexcolor[x, y] = new Color(((200 - heightMapColors[x + y * terrainWidth].R) * 100) / 255, 40, 0);
                        mapvertexcolor[x, y] = Color.White;
                    }
                }


            }
            //map2.UnlockBits(bd);


            int t = 0;
            for (int y = 0; y < terrainHeight - 1; y++)
            {
                for (int x = 0; x < terrainWidth - 1; x++)
                {

                    maptriangles[t] = new triangle(GraphicsDevice, mapvertexheight[x, y], mapvertexheight[x + 1, y], mapvertexheight[x + 1, y + 1], mapvertexcolor[x, y], mapvertexcolor[x + 1, y], mapvertexcolor[x + 1, y + 1]);
                    maptrianglesbesetzt[t] = 1;
                    t++;
                    maptriangles[t] = new triangle(GraphicsDevice, mapvertexheight[x + 1, y + 1], mapvertexheight[x, y + 1], mapvertexheight[x, y], mapvertexcolor[x + 1, y + 1], mapvertexcolor[x, y + 1], mapvertexcolor[x, y]);
                    maptrianglesbesetzt[t] = 1;
                    t++;
                    maprechtecke[x, y] = new rechteck(t - 2, t - 1);
                    count42++;
                }
            }
            for (int i = 0; i < maptriangles.Length; i++)
            {
                if (maptrianglesbesetzt[i] == 1)
                {
                    Plane test = new Plane(maptriangles[i].P1, maptriangles[i].P2, maptriangles[i].P3);
                    Vector3 pn = test.Normal;
                    Vector3 pn2 = new Vector3(pn.X, 0, pn.Z);
                    float dist = Vector3.Distance(pn, pn2);
                    maptriangles[i].C1 = new Color((int)(255 - (dist * 255)) + r.Next(-5, 6), 40 + r.Next(-5, 6), 0 + r.Next(-5, 6));
                    maptriangles[i].C2 = new Color((int)(255 - (dist * 255)) + r.Next(-5, 6), 40 + r.Next(-5, 6), 0 + r.Next(-5, 6));
                    maptriangles[i].C3 = new Color((int)(255 - (dist * 255)) + r.Next(-5, 6), 40 + r.Next(-5, 6), 0 + r.Next(-5, 6));
                }
            }


            rasterizerState1 = new RasterizerState();
            rasterizerState1.CullMode = CullMode.None;
            graphics.GraphicsDevice.RasterizerState = rasterizerState1;
            map = new world(graphics.GraphicsDevice, maptriangles, maptrianglesbesetzt);

            for (int i = 0; i < ballmenge; i++)
            {
                punktpos2[i] = punktpos[i];
            }

        }

        protected Vector2 rechtecksucher(Vector3 pos)
        {
            return new Vector2((int)(((pos.X - verschiebung.X) + (terrainWidth * 750 / 2)) / 750), (int)(((pos.Z - verschiebung.Z) + (terrainWidth * 750 / 2)) / 750));
        }
        protected Vector3 Vectorpg(Vector3 punkt, Vector3 gp1, Vector3 gp2)
        {
            float a2, b2, c2;
            a2 = Vector3.Distance(gp1, punkt);
            b2 = Vector3.Distance(gp2, punkt);
            c2 = Vector3.Distance(gp1, gp2);
            w1 = (float)Math.Acos(((a2 * a2) + (c2 * c2) - (b2 * b2)) / (2 * a2 * c2));
            fnd1 = (float)Math.Cos(w1) * a2;
            return (((gp2 - gp1) / (gp2 - gp1).Length()) * fnd1);
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }



        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        void udelay(long us)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            long v = (us * System.Diagnostics.Stopwatch.Frequency) / 1000000;
            while (sw.ElapsedTicks < v)
            {
            }
        }
        public void physics()
        {
            long zeitu, physicstime;
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var st2 = System.Diagnostics.Stopwatch.StartNew();

            // Game variables

            float grip = 0.2f;

            for (; ; )
            {
                if (platzieren == 0)
                {
                    Vector2 p1, p2;
                    p1 = new Vector2(punktpos2[0].X, punktpos2[0].Z);
                    p2 = new Vector2(punktpos2[6].X, punktpos2[6].Z);
                    float r1 = Vector2.Distance(p1, p2);
                    yrotcar = -(float)Math.Asin((p2.X - p1.X) / r1);
                    if (p2.Y - p1.Y > 0)
                    {
                        yrotcar = (float)(Math.PI / 2 + (Math.PI / 2 - yrotcar));
                    }
                    for (int i = 0; i < ballmenge; i++)
                    {
                        punktbodenkontakt[i] = 0;
                    }
                    for (int i = 0; i < muskelmenge; i++)
                    {
                        if (muskelbesetzt[i] == 1)
                        {
                            float xAbs = Vector3.Distance(punktpos2[muskelpunkt1[i]], punktpos2[muskelpunkt2[i]]);
                            float k = muskelstärke[i];
                            float d = idealemuskellänge[i];
                            float b = muskelabdämpfung[i];
                            Vector3 F1 = k * (xAbs - d) * (Vector3.Normalize(punktpos2[muskelpunkt2[i]] - punktpos2[muskelpunkt1[i]]) / xAbs);
                            Vector3 F2 = k * (xAbs - d) * (Vector3.Normalize(punktpos2[muskelpunkt1[i]] - punktpos2[muskelpunkt2[i]]) / xAbs);
                            if (b > 0)
                            {
                                float a2, b2, c2;
                                a2 = Vector3.Distance(punktpos2[muskelpunkt1[i]] + punktspeed[muskelpunkt1[i]], punktpos2[muskelpunkt2[i]]);
                                b2 = Vector3.Distance(punktpos2[muskelpunkt1[i]], punktpos2[muskelpunkt1[i]] + punktspeed[muskelpunkt1[i]]);
                                c2 = Vector3.Distance(punktpos2[muskelpunkt1[i]], punktpos2[muskelpunkt2[i]]);
                                w1 = (float)Math.Acos(((b2 * b2) + (c2 * c2) - (a2 * a2)) / (2 * b2 * c2));
                                a2 = Vector3.Distance(punktpos2[muskelpunkt2[i]] + punktspeed[muskelpunkt2[i]], punktpos2[muskelpunkt2[i]]);
                                b2 = Vector3.Distance(punktpos2[muskelpunkt1[i]], punktpos2[muskelpunkt2[i]] + punktspeed[muskelpunkt2[i]]);
                                c2 = Vector3.Distance(punktpos2[muskelpunkt1[i]], punktpos2[muskelpunkt2[i]]);
                                w2 = (float)Math.Acos(((a2 * a2) + (c2 * c2) - (b2 * b2)) / (2 * a2 * c2));
                                fnd1 = (float)Math.Cos(w1) * Vector3.Distance(punktpos2[muskelpunkt1[i]], punktpos2[muskelpunkt1[i]] + punktspeed[muskelpunkt1[i]]);
                                fnd2 = (float)Math.Cos(w2) * Vector3.Distance(punktpos2[muskelpunkt2[i]], punktpos2[muskelpunkt2[i]] + punktspeed[muskelpunkt2[i]]);
                                fn1 = (punktpos2[muskelpunkt2[i]] - punktpos2[muskelpunkt1[i]]);
                                fn1 = (fn1 / fn1.Length()) * fnd1;
                                fn2 = (punktpos2[muskelpunkt1[i]] - punktpos2[muskelpunkt2[i]]);
                                fn2 = (fn2 / fn2.Length()) * fnd2;
                                fnu = fn2 - fn1;
                                if (fnu.X.ToString() != "NaN" || fnu.X.ToString() != "n. def.")
                                {
                                    F1 += b * (fnu / 2);
                                    F2 -= b * (fnu / 2);
                                }
                            }

                            punkta[muskelpunkt1[i]] = F1 / punktmasse[muskelpunkt1[i]];
                            punkta[muskelpunkt2[i]] = F2 / punktmasse[muskelpunkt2[i]];

                            punktspeed[muskelpunkt1[i]] += punkta[muskelpunkt1[i]];
                            punktspeed[muskelpunkt2[i]] += punkta[muskelpunkt2[i]];
                            //punktpos[muskelpunkt1[i]] += punktspeed[muskelpunkt1[i]];
                            //punktpos[muskelpunkt2[i]] += punktspeed[muskelpunkt2[i]];
                            //punktpos[i].X += punktspeed[i].X;
                            punkta[muskelpunkt1[i]] = Vector3.Zero;
                            punkta[muskelpunkt2[i]] = Vector3.Zero;
                            if (muskelposänderung[i] >= 1)
                            {
                                float x, y, z, x2, y2, z2, xu, yu, zu;
                                x = (punktpos2[muskelpunkt2[i]].X - punktpos2[muskelpunkt1[i]].X) / xAbs;
                                y = (punktpos2[muskelpunkt2[i]].Y - punktpos2[muskelpunkt1[i]].Y) / xAbs;
                                z = (punktpos2[muskelpunkt2[i]].Z - punktpos2[muskelpunkt1[i]].Z) / xAbs;

                                x2 = x * idealemuskellänge[i];
                                y2 = y * idealemuskellänge[i];
                                z2 = z * idealemuskellänge[i];

                                xu = (x * xAbs) - x2;
                                yu = (y * xAbs) - y2;
                                zu = (z * xAbs) - z2;
                                if ((muskelpunkt1[i] == 1 && muskelpunkt2[i] == 2) || (muskelpunkt1[i] == 3 && muskelpunkt2[i] == 4))
                                {
                                    if (xAbs < idealemuskellänge[i] - idealemuskellänge[i] * 0.05f)
                                    {
                                        punktpos2[muskelpunkt1[i]].X += (xu / 2) / 1.75f;
                                        punktpos2[muskelpunkt1[i]].Y += (yu / 2) / 1.75f;
                                        punktpos2[muskelpunkt1[i]].Z += (zu / 2) / 1.75f;
                                        punktpos2[muskelpunkt2[i]].X -= (xu / 2) / 1.75f;
                                        punktpos2[muskelpunkt2[i]].Y -= (yu / 2) / 1.75f;
                                        punktpos2[muskelpunkt2[i]].Z -= (zu / 2) / 1.75f;
                                    }

                                }
                                else if (muskelposänderung[i] == 2)
                                {
                                    if (xAbs > idealemuskellänge[i] + idealemuskellänge[i] * 0.025f)
                                    {
                                        punktpos2[muskelpunkt1[i]].X += (xu / 2) / 1.5f;
                                        punktpos2[muskelpunkt1[i]].Y += (yu / 2) / 1.5f;
                                        punktpos2[muskelpunkt1[i]].Z += (zu / 2) / 1.5f;
                                        punktpos2[muskelpunkt2[i]].X -= (xu / 2) / 1.5f;
                                        punktpos2[muskelpunkt2[i]].Y -= (yu / 2) / 1.5f;
                                        punktpos2[muskelpunkt2[i]].Z -= (zu / 2) / 1.5f;
                                    }
                                }
                                else
                                {
                                    punktpos2[muskelpunkt1[i]].X += (xu / 2) / muskelabdämpfung2[i];
                                    punktpos2[muskelpunkt1[i]].Y += (yu / 2) / muskelabdämpfung2[i];
                                    punktpos2[muskelpunkt1[i]].Z += (zu / 2) / muskelabdämpfung2[i];
                                    punktpos2[muskelpunkt2[i]].X -= (xu / 2) / muskelabdämpfung2[i];
                                    punktpos2[muskelpunkt2[i]].Y -= (yu / 2) / muskelabdämpfung2[i];
                                    punktpos2[muskelpunkt2[i]].Z -= (zu / 2) / muskelabdämpfung2[i];
                                }

                            }

                        }

                    }
                    for (int i = 0; i < ballmenge; i++)
                    {
                        if (punktbesetzt[i] == 1)
                        {
                            /*if (i != 3 && i != 4 && i != 2 && i != 1)
                            {
                                if (punktpos[i].Y < -700 + verschiebung.Y)
                                {
                                    punktpos[i].Y = -700;
                                    punktspeed[i].Y *= 0.1f;
                                    punktspeed[i].X *= 0.97f;
                                    punktspeed[i].Z *= 0.97f;
                                    punktbodenkontakt[i] = 1;
                                }

                            }
                            else
                            {
                                if ((punktpos[0].Y - punktpos[5].Y) / (Vector3.Distance(punktpos[0], punktpos[5])) < 0.3f && punktpos[i].Y < -670 + verschiebung.Y)
                                {
                                    punktspeed[i].X *= 0.96f;
                                    punktspeed[i].Z *= 0.96f;

                                }
                                if (punktpos[i].Y < -700 + verschiebung.Y)
                                {
                                    punktpos[i].Y = -700;
                                }
                                if (punktpos[i].Y < -690 + verschiebung.Y)
                                {

                                    punktspeed[i].Y *= 0.2f;

                                    punktbodenkontakt[i] = 1;
                                }
                            }*/
                            /*punktspeed[i].X += windspeed.X;
                            punktspeed[i].Y += windspeed.Y;
                            punktspeed[i].Z += windspeed.Z;*/
                            punktspeed[i].Y -= 0.015f;
                            punktspeed[i].X = punktspeed[i].X * 0.99990f;
                            punktspeed[i].Y = punktspeed[i].Y * 0.99995f;
                            punktspeed[i].Z = punktspeed[i].Z * 0.99990f;

                            //punktpos[i].X += punktspeed[i].X;

                        }
                    }
                    float raddurchmesser = 75;
                    float rminus = 5;
                    lber = 0;
                    for (int i = 0; i < ballmenge; i++)
                    {
                        if (punktbesetzt[i] == 1)
                        {
                            if (i == 1 || i == 2 || i == 3 || i == 4)
                            {
                                raddurchmesser = 75;
                            }
                            else
                            {
                                raddurchmesser = 15;
                            }
                            Vector2 xy = rechtecksucher(punktpos2[i]);
                            for (int x = -1; x < 2; x++)
                            {
                                for (int y = -1; y < 2; y++)
                                {
                                    if (xy.X + x >= 0 && xy.X + x < terrainWidth - 1 && xy.Y + y >= 0 && xy.Y + y < terrainHeight - 1)
                                    {
                                        Vector3 tp1, tp2, tp3, pn;
                                        Vector3 rk1, rk2, lp1, lp2;
                                        for (int tn = 0; tn < 2; tn++)
                                        {
                                            if (tn == 0)
                                            {
                                                tp1 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum1].P1;
                                                tp2 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum1].P2;
                                                tp3 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum1].P3;
                                            }
                                            else
                                            {
                                                tp1 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum2].P1;
                                                tp2 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum2].P2;
                                                tp3 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum2].P3;
                                            }
                                            tp1 += verschiebung;
                                            tp2 += verschiebung;
                                            tp3 += verschiebung;
                                            Plane plane = new Plane(tp1, tp2, tp3);
                                            pn = plane.Normal;
                                            lp1 = new Vector3(pn.X * raddurchmesser + punktpos2[i].X, pn.Y * raddurchmesser + punktpos2[i].Y, pn.Z * raddurchmesser + punktpos2[i].Z);
                                            lp2 = new Vector3(pn.X * (raddurchmesser - rminus) + punktpos2[i].X, pn.Y * (raddurchmesser - rminus) + punktpos2[i].Y, pn.Z * (raddurchmesser - rminus) + punktpos2[i].Z);
                                            rk1 = TriangleCollision(tp1, tp2, tp3, lp1, punktpos2[i]);
                                            if (rk1 != Vector3.Zero)
                                            {
                                                rk2 = TriangleCollision(tp1, tp2, tp3, lp2, punktpos2[i]);
                                                if (rk2 != Vector3.Zero)
                                                {
                                                    punktpos2[i] = rk2 + new Vector3(pn.X * -1 * (raddurchmesser - rminus), pn.Y * -1 * (raddurchmesser - rminus), pn.Z * -1 * (raddurchmesser - rminus));
                                                }
                                                Vector3 r2p, sk, nsks;
                                                r2p = punktpos2[i] + (((rk1 - punktpos2[i]) / Vector3.Distance(rk1, punktpos2[i])) * (raddurchmesser - rminus));

                                                punktbodenkontakt[i] = 1;
                                                punkttrinormale[i] = pn;
                                                float b = (punktpos[5] - punktpos[0]).Length();
                                                float a = ((punktpos[5] - punktpos[0]) - pn).Length();
                                                float c = pn.Length();
                                                planecarwinkel[i] = (float)Math.Acos(((b * b) + (c * c) - (a * a)) / (2 * b * c));

                                                c = punktspeed[i].Length();
                                                b = 1;
                                                a = Vector3.Distance(punktspeed[i], pn);
                                                float w = (float)Math.Acos(((b * b) + (c * c) - (a * a)) / (2 * b * c));
                                                //grippunktspeed[i] = ((float)Math.Cos(w) * punktspeed[i].Length());
                                                sk = TriangleCollision(tp1, tp2, tp3, r2p + punktspeed[i], r2p);
                                                if (sk != Vector3.Zero)
                                                {

                                                    Vector3 skspeed = (r2p + punktspeed[i]) - sk;
                                                    nsks = TriangleCollision(tp1, tp2, tp3, sk + skspeed + (pn * 100 * -1), sk + skspeed);
                                                    if (nsks != Vector3.Zero)
                                                    {
                                                        punktspeed[i] = nsks - r2p;
                                                        grippunktspeed[i] = ((sk + skspeed) - (nsks)).Length();
                                                    }
                                                }
                                            }
                                        }
                                        if (x == 0 && y == 0)
                                        {
                                            for (int tn = 0; tn < 2; tn++)
                                            {
                                                if (tn == 0)
                                                {
                                                    tp1 = maptriangles[maprechtecke[(int)xy.X, (int)xy.Y].Tnum1].P1;
                                                    tp2 = maptriangles[maprechtecke[(int)xy.X, (int)xy.Y].Tnum1].P2;
                                                    tp3 = maptriangles[maprechtecke[(int)xy.X, (int)xy.Y].Tnum1].P3;
                                                }
                                                else
                                                {
                                                    tp1 = maptriangles[maprechtecke[(int)xy.X, (int)xy.Y].Tnum2].P1;
                                                    tp2 = maptriangles[maprechtecke[(int)xy.X, (int)xy.Y].Tnum2].P2;
                                                    tp3 = maptriangles[maprechtecke[(int)xy.X, (int)xy.Y].Tnum2].P3;
                                                }
                                                tp1 += verschiebung;
                                                tp2 += verschiebung;
                                                tp3 += verschiebung;
                                                Vector3 linie, linie2, Olp1S, zs, lp1S;
                                                lp1 = tp1;
                                                lp2 = tp2;
                                                linie = lp2 - lp1;
                                                linie2 = punktpos2[i] - lp1;
                                                zs = (linie / linie.Length());
                                                lp1S = Vectorpg(linie2, linie, Vector3.Zero);
                                                Sp = punktpos2[i] - lp2 - lp1S;
                                                if (Sp.Length() < raddurchmesser - rminus)
                                                {
                                                    Olp1S = punktpos2[i] - Sp;
                                                    punktpos2[i] = Olp1S + ((Sp / Sp.Length()) * (raddurchmesser - rminus));
                                                }
                                                lp1 = tp2;
                                                lp2 = tp3;
                                                linie = lp2 - lp1;
                                                linie2 = punktpos2[i] - lp1;
                                                zs = (linie / linie.Length());
                                                lp1S = Vectorpg(linie2, linie, Vector3.Zero);
                                                Sp = punktpos2[i] - lp2 - lp1S;
                                                if (Sp.Length() < raddurchmesser - rminus)
                                                {
                                                    Olp1S = punktpos2[i] - Sp;
                                                    punktpos2[i] = Olp1S + ((Sp / Sp.Length()) * (raddurchmesser - rminus));
                                                }
                                                lp1 = tp3;
                                                lp2 = tp1;
                                                linie = lp2 - lp1;
                                                linie2 = punktpos2[i] - lp1;
                                                zs = (linie / linie.Length());
                                                lp1S = Vectorpg(linie2, linie, Vector3.Zero);
                                                Sp = punktpos2[i] - lp2 - lp1S;
                                                if (Sp.Length() < raddurchmesser - rminus)
                                                {
                                                    Olp1S = punktpos2[i] - Sp;
                                                    punktpos2[i] = Olp1S + ((Sp / Sp.Length()) * (raddurchmesser - rminus));
                                                }
                                                if (i == 1)
                                                {
                                                    //Sptest = Sp;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            /*float raddurchmesser = 75;
                            Vector3 tp1, tp2, tp3, pn;
                            tp1 = new Vector3(-3000, -800, -5000);
                            tp2 = new Vector3(0, 5000, -30000);
                            tp3 = new Vector3(5000, -800, -5000);
                            Plane plane = new Plane(tp1, tp2, tp3);
                            pn = plane.Normal;
                            Vector3 lp1 = new Vector3(pn.X * raddurchmesser + punktpos[i].X, pn.Y * raddurchmesser + punktpos[i].Y, pn.Z * raddurchmesser + punktpos[i].Z);
                            pn = pn * -1;
                            Vector3 speedtest;
                            tctest = TriangleCollision(tp1, tp2, tp3, lp1, punktpos[i]);
                            if(tctest != Vector3.Zero && i != 5)
                            {
                                dist = Vector3.Distance(tctest, punktpos[i]);
                                if (dist < 75)
                                {
                                    punktpos[i] = tctest + new Vector3(pn.X * (raddurchmesser), pn.Y * (raddurchmesser), pn.Z * (raddurchmesser));
                                }
                                if(i == 1)
                                {
                                    testspeed.Y = dist;
                                }
                                punktbodenkontakt[i] = 1;
                                if (1 == 1)
                                {
                                    speedtest = TriangleCollision(tp1, tp2, tp3, tctest, tctest - (punktspeed[i] * 100));
                                    if(speedtest != Vector3.Zero)
                                    {
                                            punktspeed[i].Y = (punktpos[i].Y - altepunktpos[i].Y) - (punktpos[i].Y - altepunktpos[i].Y)/1.9f;
                                            testspeed = punktspeed[i];
                                    }
                                    else
                                    {
                                        speedacount++;
                                    }
                                }
                                if(i != 1 && i != 2 && i != 3 && i != 4)
                                {
                                    punktspeed[i].X *= 0.98f;
                                    punktspeed[i].Z *= 0.98f;
                                }
                            }*/
                        }

                    }
                    Vector2 xy2 = rechtecksucher(punktpos2[1]);
                    Sptest.X = xy2.X;
                    Sptest.Y = xy2.Y;
                    //Vector2 xy2 = rechtecksucher(punktpos2[1]);
                    //Sptest.X = xy2.X;
                    //Sptest.Y = xy2.Y;
                    for (int i = 0; i < ballmenge; i++)
                    {
                        if (punktbesetzt[i] == 1)
                        {
                            if (i != 1 && i != 2 && i != 3 && i != 4)
                            {
                                if (punktbodenkontakt[i] == 1)
                                {
                                    if (punktspeed[i].Length() > grippunktspeed[i] / 3)
                                    {
                                        punktspeed[i] -= (punktspeed[i] / punktspeed[i].Length()) * grippunktspeed[i] / 1000;
                                    }
                                    else
                                    {
                                        punktspeed[i] = Vector3.Zero;
                                    }
                                }
                            }
                        }
                    }


                    for (int i = 1; i < 3; i++)
                    {
                        float winkelgrip = (planecarwinkel[i] - (float)Math.PI / 2);
                        if (winkelgrip < 0)
                        {
                            winkelgrip *= -1;
                        }
                        grip = 3.5f * (0.7f + grippunktspeed[i] * 5);
                        if (grip > 0.006f)
                        {
                            radspeedtest = grip;
                        }
                        if (punktbodenkontakt[i] == 1)
                        {
                            float radspeed = (float)(Math.PI * 70 * (radrotation[i + 1] / Math.PI));
                            float radspeedminus;
                            Vector3 speedadd;
                            Vector3 radrichtung2 = new Vector3(punktpos2[6].X - punktpos2[0].X, 0, punktpos2[6].Z - punktpos2[0].Z);
                            radrichtung2.Y = (-punkttrinormale[i].X * radrichtung2.X - punkttrinormale[i].Z * radrichtung2.Z) / punkttrinormale[i].Y;
                            Vector3 radspeed2 = radrichtung2 / radrichtung2.Length();
                            radspeed2 *= radspeed;
                            radspeed2 -= punktspeed[i];
                            speedadd = radspeed2;
                            if (Vector3.Distance(Vector3.Zero, speedadd) > grip)
                            {
                                speedadd = speedadd / speedadd.Length();
                                speedadd *= grip;
                            }
                            punktspeed[i] += speedadd;
                            Vector3 radrn = radrichtung2 / radrichtung2.Length();
                            float speedadd2 = radrn.X * speedadd.X + radrn.Y * speedadd.Y + radrn.Z * speedadd.Z;
                            radspeedminus = (float)((speedadd2 * Math.PI) / (Math.PI * 70));
                            radrotation[i + 1] -= radspeedminus;

                            p1 = new Vector2(punktpos2[0].X, punktpos2[0].Z);
                            p2 = new Vector2(punktpos2[6].X, punktpos2[6].Z);
                            float r = Vector2.Distance(p1, p2);
                            yrotcar = -(float)Math.Asin((p2.X - p1.X) / r);
                            if (p2.Y - p1.Y > 0)
                            {
                                yrotcar = (float)(Math.PI / 2 + (Math.PI / 2 - yrotcar));
                            }

                            p1 = new Vector2(0, 0);
                            p2 = new Vector2(punktspeed[i].X, punktspeed[i].Z);
                            r = Vector2.Distance(p1, p2);
                            yrotpoint = -(float)Math.Asin((p2.X - p1.X) / r);
                            if (p2.Y - p1.Y > 0)
                            {
                                yrotpoint = (float)(Math.PI / 2 + (Math.PI / 2 - yrotpoint));
                            }

                            yrotpoint = yrotpoint - yrotcar;
                            if (yrotpoint < 0)
                            {
                                yrotpoint = yrotpoint + (float)Math.PI * 2;
                            }
                            yrotcar = 0;
                            if (yrotpoint > Math.PI)
                            {
                                yrotpoint = (float)(Math.PI * 2) - yrotpoint;
                            }
                            rotu = yrotpoint - yrotcar;
                            rotu = -(float)(rotu - Math.PI / 2);
                            speedlänge = (float)Math.Sin(rotu);
                            float r06 = Vector2.Distance(new Vector2(punktpos2[0].X, punktpos2[0].Z), new Vector2(punktpos2[6].X, punktpos2[6].Z));
                            float x06 = punktpos2[0].X - punktpos2[6].X;
                            float z06 = punktpos2[0].Z - punktpos2[6].Z;
                            float faktor = r / r06;
                            newspeed = new Vector3(-x06 * faktor * speedlänge, 0, -z06 * faktor * speedlänge);
                            Vector3 unterschied;
                            unterschied = new Vector3(newspeed.X - punktspeed[i].X, 0, newspeed.Z - punktspeed[i].Z);
                            endspeed = unterschied;
                            if (Vector3.Distance(Vector3.Zero, unterschied) > grip)
                            {

                                float f2 = grip / Vector3.Distance(Vector3.Zero, unterschied);
                                endspeed = new Vector3(unterschied.X * f2, 0, unterschied.Z * f2);
                            }
                            punktspeed[i] += endspeed;

                        }
                    }
                    //Vorderräder
                    for (int i = 3; i < 5; i++)
                    {
                        float winkelgrip = (planecarwinkel[i] - (float)Math.PI / 2);
                        if (winkelgrip < 0)
                        {
                            winkelgrip *= -1;
                        }
                        grip = 0.035f * (0.7f + grippunktspeed[i] * 5);
                        if (punktbodenkontakt[i] == 1)
                        {
                            p1 = new Vector2(punktpos2[0].X, punktpos2[0].Z);
                            p2 = new Vector2(punktpos2[6].X, punktpos2[6].Z);
                            float r = Vector2.Distance(p1, p2);
                            yrotcar = -(float)Math.Asin((p2.X - p1.X) / r);
                            if (p2.Y - p1.Y > 0)
                            {
                                yrotcar = (float)(Math.PI / 2 + (Math.PI / 2 - yrotcar));
                            }
                            yrotcar += steeringrot;
                            p1 = new Vector2(0, 0);
                            p2 = new Vector2(punktspeed[i].X, punktspeed[i].Z);
                            r = Vector2.Distance(p1, p2);
                            yrotpoint = -(float)Math.Asin((p2.X - p1.X) / r);
                            if (p2.Y - p1.Y > 0)
                            {
                                yrotpoint = (float)(Math.PI / 2 + (Math.PI / 2 - yrotpoint));
                            }

                            yrotpoint = yrotpoint - yrotcar;
                            if (yrotpoint < 0)
                            {
                                yrotpoint = yrotpoint + (float)Math.PI * 2;
                            }

                            if (yrotpoint > Math.PI)
                            {
                                yrotpoint = (float)(Math.PI * 2) - yrotpoint;
                            }
                            rotu = yrotpoint - 0;
                            rotu = -(float)(rotu - Math.PI / 2);
                            speedlänge = (float)Math.Sin(rotu);
                            radrichtung = new Vector2((float)Math.Sin(yrotcar), (float)Math.Cos(yrotcar));

                            float radspeed = (float)(Math.PI * 70 * (radrotation[i - 3] / Math.PI));
                            float radspeedminus;
                            Vector3 speedadd;
                            Vector3 radrichtung2 = new Vector3(radrichtung.X, 0, radrichtung.Y);
                            radrichtung2.Y = (-punkttrinormale[i].X * radrichtung2.X - punkttrinormale[i].Z * radrichtung2.Z) / punkttrinormale[i].Y;
                            Vector3 radspeed2 = radrichtung2 / radrichtung2.Length();
                            radspeed2 *= radspeed;
                            radspeed2 -= punktspeed[i];
                            speedadd = radspeed2;
                            if (Vector3.Distance(Vector3.Zero, speedadd) > grip)
                            {
                                speedadd = speedadd / speedadd.Length();
                                speedadd *= grip;
                            }
                            punktspeed[i] += speedadd;
                            Vector3 radrn = radrichtung2 / radrichtung2.Length();
                            float speedadd2 = radrn.X * speedadd.X + radrn.Y * speedadd.Y + radrn.Z * speedadd.Z;
                            radspeedminus = (float)((speedadd2 * Math.PI) / (Math.PI * 70));
                            radrotation[i - 3] -= radspeedminus;

                            float r06 = Vector2.Distance(new Vector2(0, 0), radrichtung);
                            float x06 = 0 - radrichtung.X;
                            float z06 = 0 - radrichtung.Y;
                            faktor = r / r06;
                            newspeed = new Vector3(-x06 * faktor * (-speedlänge), 0, -z06 * faktor * (-speedlänge));
                            //punktspeed[3] = newspeed;
                            Vector3 unterschied;
                            unterschied = new Vector3(newspeed.X - punktspeed[i].X, 0, newspeed.Z - punktspeed[i].Z);
                            endspeed = unterschied;
                            if (Vector3.Distance(Vector3.Zero, unterschied) > grip)
                            {

                                float f2 = grip / Vector3.Distance(Vector3.Zero, unterschied);
                                endspeed = new Vector3(unterschied.X * f2, 0, unterschied.Z * f2);
                            }
                            punktspeed[i] += endspeed;
                        }
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        /*Vector3 unt = alteradpos[i] - punktpos[i + 1];
                        r = Vector3.Distance(Vector3.Zero, unt);
                        radrotation[i] -= r / 70;
                        if (radrotation[i] > Math.PI)
                        {
                            radrotation[i] -= (float)Math.PI;
                        }*/
                        if (radrotation[i].ToString() == "NaN" || radrotation[i].ToString() == "n. def.")
                        {
                            radrotation[i] = 0;
                        }
                        radrotation2[i] += radrotation[i];
                        if (radrotation2[i] > Math.PI)
                        {
                            radrotation2[i] -= (float)Math.PI;
                        }

                    }
                    for (int i = 2; i < 4; i++)
                    {
                        /*Vector3 unt = alteradpos[i] - punktpos[i + 1];
                        r = Vector3.Distance(Vector3.Zero, unt);
                        radrotation[i] -= r / 70;
                        if (radrotation[i] > Math.PI)
                        {
                            radrotation[i] -= (float)Math.PI;
                        }*/
                        if (radrotation[i].ToString() == "NaN" || radrotation[i].ToString() == "n. def.")
                        {
                            radrotation[i] = 0;
                        }
                        radrotation2[i] -= radrotation[i];
                        if (radrotation2[i] > Math.PI)
                        {
                            radrotation2[i] -= (float)Math.PI;
                        }

                    }
                    if (punktpos2[0].X > 1000)
                    {
                        verschiebung.X -= punktpos2
                            [0].X;
                        for (int i = punktmenge - 1; i >= 0; i--)
                        {
                            punktpos2[i].X -= punktpos2[0].X;
                        }
                    }
                    if (punktpos2[0].X < -1000)
                    {
                        verschiebung.X -= punktpos2[0].X;
                        for (int i = punktmenge - 1; i >= 0; i--)
                        {
                            punktpos2[i].X -= punktpos2[0].X;
                        }
                    }
                    if (punktpos2[0].Z > 1000)
                    {
                        verschiebung.Z -= punktpos2[0].Z;
                        for (int i = punktmenge - 1; i >= 0; i--)
                        {
                            punktpos2[i].Z -= punktpos2[0].Z;
                        }
                    }
                    if (punktpos2[0].Z < -1000)
                    {
                        verschiebung.Z -= punktpos2[0].Z;
                        for (int i = punktmenge - 1; i >= 0; i--)
                        {
                            punktpos2[i].Z -= punktpos2[0].Z;
                        }
                    }
                    if (punktpos2[0].Y > 1000)
                    {
                        verschiebung.Y -= punktpos2[0].Y;
                        for (int i = punktmenge - 1; i >= 0; i--)
                        {
                            punktpos2[i].Y -= punktpos2[0].Y;
                        }
                    }
                    if (punktpos2[0].Y < -1000)
                    {
                        verschiebung.Y -= punktpos2[0].Y;
                        for (int i = punktmenge - 1; i >= 0; i--)
                        {
                            punktpos2[i].Y -= punktpos2[0].Y;
                        }
                    }
                    for (int i = 0; i < ballmenge; i++)
                    {
                        if (punktbesetzt[i] == 1)
                        {
                            altepunktpos[i] = punktpos2[i];
                        }
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.T))
                    {
                        /*if (punktbodenkontakt[2] == 1)
                        {
                            punktspeed[2] += ((punktpos[6] - punktpos[0]) / Vector3.Distance(punktpos[6], punktpos[0])) * power;
                        }
                        if (punktbodenkontakt[1] == 1)
                        {
                            punktspeed[1] += ((punktpos[6] - punktpos[0]) / Vector3.Distance(punktpos[6], punktpos[0])) * power;
                        }*/
                        radrotation[2] += 0.00055;
                        radrotation[3] += 0.00055;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.G))
                    {
                        /*if (punktbodenkontakt[2] == 1)
                        {
                            punktspeed[2] -= ((punktpos[6] - punktpos[0]) / Vector3.Distance(punktpos[6], punktpos[0])) * power;
                        }
                        if (punktbodenkontakt[1] == 1)
                        {
                            punktspeed[1] -= ((punktpos[6] - punktpos[0]) / Vector3.Distance(punktpos[6], punktpos[0])) * power;
                        }*/
                        radrotation[2] -= 0.00055;
                        radrotation[3] -= 0.00055;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.F))
                    {
                        if (steeringrot < 0.7f)
                            steeringrot += 0.0015f;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.H))
                    {
                        if (steeringrot > -0.7f)
                            steeringrot -= 0.0015f;
                    }
                    if (steeringrot < 0)
                    {
                        if (steeringrot + 0.0006f > 0)
                        {
                            steeringrot = 0;
                        }
                        else
                        {
                            steeringrot += 0.0006f;
                        }
                    }
                    if (steeringrot > 0)
                    {
                        if (steeringrot - 0.0006f < 0)
                        {
                            steeringrot = 0;
                        }
                        else
                        {
                            steeringrot -= 0.0006f;
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.C) && camerastopp == 0)
                    {
                        if (cameramode == 1)
                        {
                            cameramode = 2;
                        }
                        else if (cameramode == 2)
                        {
                            cameramode = 1;
                        }
                        camerastopp = 1;
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.C) && camerastopp != 0)
                    {
                        camerastopp = 0;
                    }
                    for (int i = 0; i < ballmenge; i++)
                    {
                        if (punktbesetzt[i] == 1)
                        {
                            if (punktspeed[i].X.ToString() == "NaN" || punktspeed[i].X.ToString() == "n. def.")
                            {
                                punktspeed[i] = new Vector3(0.00001f, 0.00001f, 0.00001f);
                                nantest++;
                            }
                            punktpos2[i].Y += punktspeed[i].Y;
                            punktpos2[i].X += punktspeed[i].X;
                            punktpos2[i].Z += punktspeed[i].Z;
                            if (punktpos2[i].X.ToString() == "NaN" || punktpos2[i].X.ToString() == "n. def.")
                            {
                                punktpos2[i] = Vector3.Zero;
                            }
                        }
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                    {
                        punktpos2[0] = new Vector3(0, 0 + yoffset, 0);
                        punktpos2[1] = new Vector3(180f, -30f + yoffset, 300);
                        punktpos2[2] = new Vector3(-180f, -30f + yoffset, 300);
                        punktpos2[3] = new Vector3(-180f, -30f + yoffset, -330);
                        punktpos2[4] = new Vector3(180f, -30f + yoffset, -330);
                        punktpos2[5] = new Vector3(0, 75f + yoffset, 0);
                        punktpos2[6] = new Vector3(0, 0 + yoffset, -330);
                        punktpos2[7] = new Vector3(-180, 0 + yoffset, 0);
                        punktpos2[8] = new Vector3(180, 0 + yoffset, 0);
                        punktpos2[9] = new Vector3(60f, 270 + yoffset, 250f);
                        punktpos2[10] = new Vector3(-60f, 270 + yoffset, 250f);
                        punktpos2[11] = new Vector3(-60, 270 + yoffset, -30);
                        punktpos2[12] = new Vector3(60, 270 + yoffset, -30);
                        punktpos2[13] = new Vector3(180 - 100, 0 + yoffset, 300);
                        punktpos2[14] = new Vector3(-180 + 100, 0 + yoffset, 300);
                        punktpos2[15] = new Vector3(-180 + 100, 0 + yoffset, -330);
                        punktpos2[16] = new Vector3(180 - 100, 0 + yoffset, -330);
                        punktpos2[17] = new Vector3(0, 270 + yoffset, 0);

                        punktpos2[18] = new Vector3(180 - 100, 150 + yoffset, 300);
                        punktpos2[19] = new Vector3(-180 + 100, 150 + yoffset, 300);
                        punktpos2[20] = new Vector3(-180 + 100, 150 + yoffset, -330);
                        punktpos2[21] = new Vector3(180 - 100, 150 + yoffset, -330);

                        for (int i = 0; i < punktmenge; i++)
                        {
                            if (punktbesetzt[i] == 1)
                            {
                                punktpos2[i].X += xoffset;
                            }
                        }
                        verschiebung = Vector3.Zero;
                        verschiebungupdate = Vector3.Zero;
                    }
                    if (punktposupdate == 1)
                    {
                        for (int i = 0; i < ballmenge; i++)
                        {
                            if (punktbesetzt[i] == 1)
                            {
                                punktpos[i] = punktpos2[i];
                            }
                        }
                        verschiebungupdate = verschiebung;
                        punktposupdate = 0;
                    }
                }
                if (physicabbruch > 1500 / slowmotion)
                {
                    physicsstart = 0;
                    break;
                }
                physicabbruch++;
                //long v = (us * System.Diagnostics.Stopwatch.Frequency) / 1000000;
                zeitu = (int)((stopwatch.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency);
                //zeitu = (int)((((double)stopwatch.ElapsedTicks / (double)10) / (double)System.Diagnostics.Stopwatch.Frequency) * (double)1000000);
                stopwatch.Restart();
                gt2 = zeitu;
                physicstime = (int)((st2.ElapsedTicks * 1000000) / System.Diagnostics.Stopwatch.Frequency);
                //gt2 = physicstime;
                if ((1000 * slowmotion) - physicstime < 1)
                { /* Do nothing */ }
                else
                {
                    // Try to delay the physics by 1000 usec
                    udelay((1000 * slowmotion) - physicstime);
                }
                st2.Restart();
            }
        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            gametime2 = (float)gameTime.ElapsedGameTime.Milliseconds;



            if (platzieren == 0)
            {
                float deltax, deltay;

                deltax = mouseState.X - (GraphicsDevice.Viewport.Width / 2);
                deltay = mouseState.Y - (GraphicsDevice.Viewport.Height / 2);
                mouserotationbuffer.X += 0.004f * deltax;
                mouserotationbuffer.Y += 0.004f * deltay;
                if (mouserotationbuffer.Y < MathHelper.ToRadians(-88))
                {
                    mouserotationbuffer.Y = mouserotationbuffer.Y - (mouserotationbuffer.Y - MathHelper.ToRadians(-88));
                }
                if (mouserotationbuffer.Y > MathHelper.ToRadians(88))
                {
                    mouserotationbuffer.Y = mouserotationbuffer.Y - (mouserotationbuffer.Y - MathHelper.ToRadians(88));
                }
                rotation = new Vector3(-mouserotationbuffer.X, -mouserotationbuffer.Y, 0);
                deltax = 0;
                deltay = 0;
                mouseposition3d.X = mouseState.X;
                mouseposition3d.Y = mouseState.Y;

                Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            }
            if (FUCK == 100)
            {
                FUCK = 0;
                //Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                mouseposition3d.X = GraphicsDevice.Viewport.Width / 2;
                mouseposition3d.Y = GraphicsDevice.Viewport.Height / 2;
            }
            /*if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                rotation.X -= 0.02f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                rotation.X += 0.02f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                //camera.Y += 1;
                rotation.Y += 0.02f;
                if(platzieren == 0)
                {
                    //camera.Y += 1;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                //camera.Y -= 1;
                rotation.Y -= 0.02f;
                if (platzieren == 0)
                {
                    //camera.Y -= 1;
                }
            }*/

            Vector2 p1, p2;
            p1 = new Vector2(punktpos[0].X, punktpos[0].Z);
            p2 = new Vector2(punktpos[6].X, punktpos[6].Z);
            float r1 = Vector2.Distance(p1, p2);
            yrotcar = -(float)Math.Asin((p2.X - p1.X) / r1);
            if (p2.Y - p1.Y > 0)
            {
                yrotcar = (float)(Math.PI / 2 + (Math.PI / 2 - yrotcar));
            }
            for (int i = 0; i < ballmenge; i++)
            {
                punktbodenkontakt[i] = 0;
            }


            if (movevector != Vector3.Zero)
            {
                movevector.Normalize();
                movevector *= 0.1f;
                Move(movevector);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) && slowmotionstop1 == 0)
            {
                slowmotionstop1 = 1;
                if (slowmotion < 64)
                {
                    slowmotion *= 2;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && slowmotionstop1 == 0)
            {
                slowmotionstop1 = 1;
                if (slowmotion > 1)
                {
                    slowmotion /= 2;
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                slowmotionstop1 = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                power += 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                power -= 0.01f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //System.Threading.Thread.Sleep(100);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                camera.Z -= (float)Math.Sin(rotation.X) * 10;
                camera.X += (float)Math.Cos(rotation.X) * 10;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                camera.Z += (float)Math.Sin(rotation.X) * 10;
                camera.X -= (float)Math.Cos(rotation.X) * 10;
            }

            if (platzieren == 0)
            {
                /*for (int i = muskelmenge-1; i >-1; i--)
                {
                    if (muskelbesetzt[i] == 1)
                    {
                        float xAbs = Vector3.Distance(punktpos[muskelpunkt1[i]], punktpos[muskelpunkt2[i]]);
                        float k = muskelstärke[i];
                        float d = idealemuskellänge[i];
                        float b = muskelabdämpfung[i];
                        Vector3 F1 = k * (xAbs - d) * (Vector3.Normalize(punktpos[muskelpunkt2[i]] - punktpos[muskelpunkt1[i]]) / xAbs) - b * (punktspeed[muskelpunkt1[i]] - punktspeed[muskelpunkt2[i]]);
                        Vector3 F2 = k * (xAbs - d) * (Vector3.Normalize(punktpos[muskelpunkt1[i]] - punktpos[muskelpunkt2[i]]) / xAbs) - b * (punktspeed[muskelpunkt2[i]] - punktspeed[muskelpunkt1[i]]);
                        punkta[muskelpunkt1[i]] += F1 / punktmasse[muskelpunkt1[i]];
                        punkta[muskelpunkt2[i]] += F2 / punktmasse[muskelpunkt2[i]];

                        punktspeed[muskelpunkt1[i]] += punkta[muskelpunkt1[i]];
                        punktspeed[muskelpunkt2[i]] += punkta[muskelpunkt2[i]];
                        //punktpos[muskelpunkt1[i]] += punktspeed[muskelpunkt1[i]];
                        //punktpos[muskelpunkt2[i]] += punktspeed[muskelpunkt2[i]];
                        //punktpos[i].X += punktspeed[i].X;
                        punkta[muskelpunkt1[i]] = Vector3.Zero;
                        punkta[muskelpunkt2[i]] = Vector3.Zero;
                        if(muskelposänderung[i] == 1)
                        {
                            float x, y, z, x2, y2, z2, xu, yu, zu;
                            x = (punktpos[muskelpunkt2[i]].X - punktpos[muskelpunkt1[i]].X) / xAbs;
                            y = (punktpos[muskelpunkt2[i]].Y - punktpos[muskelpunkt1[i]].Y) / xAbs;
                            z = (punktpos[muskelpunkt2[i]].Z - punktpos[muskelpunkt1[i]].Z) / xAbs;

                            x2 = x * idealemuskellänge[i];
                            y2 = y * idealemuskellänge[i];
                            z2 = z * idealemuskellänge[i];

                            xu = (x * xAbs) - x2;
                            yu = (y * xAbs) - y2;
                            zu = (z * xAbs) - z2;
                            //testspeed = new Vector3(xu, yu, zu);
                            if(muskelpunkt1[i] > 0 && muskelpunkt1[i] < 1)
                            {

                            }
                            punktpos[muskelpunkt1[i]].X += (xu / 2) / 1;
                            punktpos[muskelpunkt1[i]].Y += (yu / 2) / 1;
                            punktpos[muskelpunkt1[i]].Z += (zu / 2) / 1;
                            punktpos[muskelpunkt2[i]].X -= (xu / 2) / 1;
                            punktpos[muskelpunkt2[i]].Y -= (yu / 2) / 1;
                            punktpos[muskelpunkt2[i]].Z -= (zu / 2) / 1;

                        }
                        
                    }
                    
                }
                for (int i = 0; i < ballmenge; i++)
                {
                    if (punktbesetzt[i] == 1)
                    {
                        /*if (i != 3 && i != 4 && i != 2 && i != 1)
                        {
                            if (punktpos[i].Y < -700 + verschiebung.Y)
                            {
                                punktpos[i].Y = -700;
                                punktspeed[i].Y *= 0.1f;
                                punktspeed[i].X *= 0.97f;
                                punktspeed[i].Z *= 0.97f;
                                punktbodenkontakt[i] = 1;
                            }

                        }
                        else
                        {
                            if ((punktpos[0].Y - punktpos[5].Y) / (Vector3.Distance(punktpos[0], punktpos[5])) < 0.3f && punktpos[i].Y < -670 + verschiebung.Y)
                            {
                                punktspeed[i].X *= 0.96f;
                                punktspeed[i].Z *= 0.96f;

                            }
                            if (punktpos[i].Y < -700 + verschiebung.Y)
                            {
                                punktpos[i].Y = -700;
                            }
                            if (punktpos[i].Y < -690 + verschiebung.Y)
                            {

                                punktspeed[i].Y *= 0.2f;
                                
                                punktbodenkontakt[i] = 1;
                            }
                        }*/
                /*punktspeed[i].X += windspeed.X;
                punktspeed[i].Y += windspeed.Y;
                punktspeed[i].Z += windspeed.Z;
                punktspeed[i].Y -= 0.3f;
                punktspeed[i].X = punktspeed[i].X * 0.9985f;
                punktspeed[i].Y = punktspeed[i].Y * 0.9996f;
                punktspeed[i].Z = punktspeed[i].Z * 0.9985f;

                //punktpos[i].X += punktspeed[i].X;

            }
        }
        for (int i = 0; i < ballmenge; i++)
        {
            if (punktbesetzt[i] == 1)
            {
                Vector2 xy = rechtecksucher(punktpos[i]);
                for(int x = -1; x < 2; x++)
                {
                    for(int y = -1; y < 2; y++)
                    {
                        if(xy.X + x >= 0 && xy.X + x <  terrainWidth-1 && xy.Y + y >= 0 && xy.Y + y < terrainHeight - 1)
                        {
                            float raddurchmesser = 75;
                            Vector3 tp1, tp2, tp3, pn;
                            tp1 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum1].P1;
                            tp2 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum1].P2;
                            tp3 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum1].P3;
                            tp1.X += verschiebung.X;
                            tp2.X += verschiebung.X;
                            tp3.X += verschiebung.X;
                            tp1.Z += verschiebung.Z;
                            tp2.Z += verschiebung.Z;
                            tp3.Z += verschiebung.Z;
                            tp1.Y += verschiebung.Y;
                            tp2.Y += verschiebung.Y;
                            tp3.Y += verschiebung.Y;
                            Plane plane = new Plane(tp1, tp2, tp3);
                            pn = plane.Normal;
                            Vector3 lp1 = new Vector3(pn.X * raddurchmesser + punktpos[i].X, pn.Y * raddurchmesser + punktpos[i].Y, pn.Z * raddurchmesser + punktpos[i].Z);
                            pn = pn * -1;
                            Vector3 speedtest;
                            tctest = TriangleCollision(tp1, tp2, tp3, lp1, punktpos[i]);
                            if (tctest != Vector3.Zero && i != 5)
                            {
                                dist = Vector3.Distance(tctest, punktpos[i]);
                                if (dist < 75)
                                {
                                    punktpos[i] = tctest + new Vector3(pn.X * (raddurchmesser), pn.Y * (raddurchmesser), pn.Z * (raddurchmesser));
                                }
                                if (i == 1)
                                {
                                    testspeed.Y = dist;
                                }
                                punktbodenkontakt[i] = 1;
                                if (1 == 1)
                                {
                                    speedtest = TriangleCollision(tp1, tp2, tp3, tctest, tctest - (punktspeed[i] * 100));
                                    if (speedtest != Vector3.Zero)
                                    {
                                        punktspeed[i].Y = (punktpos[i].Y - altepunktpos[i].Y) - (punktpos[i].Y - altepunktpos[i].Y) / 1.75f;
                                        if((punktpos[i].Y - altepunktpos[i].Y) < 0)
                                        {
                                            punktspeed[i].Y = (punktpos[i].Y - altepunktpos[i].Y) - (punktpos[i].Y - altepunktpos[i].Y) / 5f;
                                        }
                                        testspeed = punktspeed[i];
                                    }
                                    else
                                    {
                                        speedacount++;
                                    }
                                }
                                if (i != 1 && i != 2 && i != 3 && i != 4)
                                {
                                    punktspeed[i].X *= 0.98f;
                                    punktspeed[i].Z *= 0.98f;
                                }
                            }
                            if(i == 0 && punktspeed[i].Y < -400)
                            {
                                tttest++;
                            }
                            int xxx = maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum2;
                            tp1 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum2].P1;
                            tp2 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum2].P2;
                            tp3 = maptriangles[maprechtecke[(int)xy.X + x, (int)xy.Y + y].Tnum2].P3;
                            tp1.X += verschiebung.X;
                            tp2.X += verschiebung.X;
                            tp3.X += verschiebung.X;
                            tp1.Z += verschiebung.Z;
                            tp2.Z += verschiebung.Z;
                            tp3.Z += verschiebung.Z;
                            tp1.Y += verschiebung.Y;
                            tp2.Y += verschiebung.Y;
                            tp3.Y += verschiebung.Y;
                            plane = new Plane(tp1, tp2, tp3);
                            pn = plane.Normal;
                            lp1 = new Vector3(pn.X * raddurchmesser + punktpos[i].X, pn.Y * raddurchmesser + punktpos[i].Y, pn.Z * raddurchmesser + punktpos[i].Z);
                            pn = pn * -1;
                            tctest = TriangleCollision(tp1, tp2, tp3, lp1, punktpos[i]);
                            if (tctest != Vector3.Zero && i != 5)
                            {
                                dist = Vector3.Distance(tctest, punktpos[i]);
                                if (dist < 75)
                                {
                                    punktpos[i] = tctest + new Vector3(pn.X * (raddurchmesser), pn.Y * (raddurchmesser), pn.Z * (raddurchmesser));
                                }
                                if (i == 1)
                                {
                                    testspeed.Y = dist;
                                }
                                punktbodenkontakt[i] = 1;
                                if (1 == 1)
                                {
                                    speedtest = TriangleCollision(tp1, tp2, tp3, tctest, tctest - (punktspeed[i] * 100));
                                    if (speedtest != Vector3.Zero)
                                    {
                                        punktspeed[i].Y = (punktpos[i].Y - altepunktpos[i].Y) - (punktpos[i].Y - altepunktpos[i].Y) / 1.75f;
                                        if ((punktpos[i].Y - altepunktpos[i].Y) < 0)
                                        {
                                            punktspeed[i].Y = (punktpos[i].Y - altepunktpos[i].Y) - (punktpos[i].Y - altepunktpos[i].Y) / 5;
                                        }
                                    }
                                    else
                                    {
                                        speedacount++;
                                    }
                                }
                                if (i != 1 && i != 2 && i != 3 && i != 4)
                                {
                                    punktspeed[i].X *= 0.98f;
                                    punktspeed[i].Z *= 0.98f;
                                }
                            }
                        }
                    }
                }
                /*float raddurchmesser = 75;
                Vector3 tp1, tp2, tp3, pn;
                tp1 = new Vector3(-3000, -800, -5000);
                tp2 = new Vector3(0, 5000, -30000);
                tp3 = new Vector3(5000, -800, -5000);
                Plane plane = new Plane(tp1, tp2, tp3);
                pn = plane.Normal;
                Vector3 lp1 = new Vector3(pn.X * raddurchmesser + punktpos[i].X, pn.Y * raddurchmesser + punktpos[i].Y, pn.Z * raddurchmesser + punktpos[i].Z);
                pn = pn * -1;
                Vector3 speedtest;
                tctest = TriangleCollision(tp1, tp2, tp3, lp1, punktpos[i]);
                if(tctest != Vector3.Zero && i != 5)
                {
                    dist = Vector3.Distance(tctest, punktpos[i]);
                    if (dist < 75)
                    {
                        punktpos[i] = tctest + new Vector3(pn.X * (raddurchmesser), pn.Y * (raddurchmesser), pn.Z * (raddurchmesser));
                    }
                    if(i == 1)
                    {
                        testspeed.Y = dist;
                    }
                    punktbodenkontakt[i] = 1;
                    if (1 == 1)
                    {
                        speedtest = TriangleCollision(tp1, tp2, tp3, tctest, tctest - (punktspeed[i] * 100));
                        if(speedtest != Vector3.Zero)
                        {
                                punktspeed[i].Y = (punktpos[i].Y - altepunktpos[i].Y) - (punktpos[i].Y - altepunktpos[i].Y)/1.9f;
                                testspeed = punktspeed[i];
                        }
                        else
                        {
                            speedacount++;
                        }
                    }
                    if(i != 1 && i != 2 && i != 3 && i != 4)
                    {
                        punktspeed[i].X *= 0.98f;
                        punktspeed[i].Z *= 0.98f;
                    }
                }
            }
        }
        if (punktpos[0].X > 1000)
        {
            verschiebung.X -= punktpos
                [0].X;
            for (int i = punktmenge - 1; i >= 0; i--)
            {
                punktpos[i].X -= punktpos[0].X;
            }
        }
        if (punktpos[0].X < -1000)
        {
            verschiebung.X -= punktpos[0].X;
            for (int i = punktmenge - 1; i >= 0; i--)
            {
                punktpos[i].X -= punktpos[0].X;
            }
        }
        if (punktpos[0].Z > 1000)
        {
            verschiebung.Z -= punktpos[0].Z;
            for (int i = punktmenge - 1; i >= 0; i--)
            {
                punktpos[i].Z -= punktpos[0].Z;
            }
        }
        if (punktpos[0].Z < -1000)
        {
            verschiebung.Z -= punktpos[0].Z;
            for (int i = punktmenge - 1; i >= 0; i--)
            {
                punktpos[i].Z -= punktpos[0].Z;
            }
        }
        if (punktpos[0].Y > 1000)
        {
            verschiebung.Y -= punktpos[0].Y;
            for (int i = punktmenge - 1; i >= 0; i--)
            {
                punktpos[i].Y -= punktpos[0].Y;
            }
        }
        if (punktpos[0].Y < -1000)
        {
            verschiebung.Y -= punktpos[0].Y;
            for (int i = punktmenge - 1; i >= 0; i--)
            {
                punktpos[i].Y -= punktpos[0].Y;
            }
        }
        for (int i = 0; i < ballmenge; i++)
        {
            if(punktbesetzt[i] == 1)
            {
                altepunktpos[i] = punktpos[i];
            }
        }
        //testspeed = punktspeed[1];
        punktbodenkontakt[5] = 0;
        float grip;
        grip = 1.8f * (punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[5], punktpos[0]));
        if (punktbodenkontakt[2] == 1)
        {
            p1 = new Vector2(punktpos[0].X, punktpos[0].Z);
            p2 = new Vector2(punktpos[6].X, punktpos[6].Z);
            float r = Vector2.Distance(p1, p2);
            yrotcar = -(float)Math.Asin((p2.X - p1.X) / r);
            if (p2.Y - p1.Y > 0)
            {
                yrotcar = (float)(Math.PI / 2 + (Math.PI / 2 - yrotcar));
            }

            p1 = new Vector2(0, 0);
            p2 = new Vector2(punktspeed[2].X, punktspeed[2].Z);
            r = Vector2.Distance(p1, p2);
            yrotpoint = -(float)Math.Asin((p2.X - p1.X) / r);
            if (p2.Y - p1.Y > 0)
            {
                yrotpoint = (float)(Math.PI / 2 + (Math.PI / 2 - yrotpoint));
            }

            yrotpoint = yrotpoint - yrotcar;
            if (yrotpoint < 0)
            {
                yrotpoint = yrotpoint + (float)Math.PI * 2;
            }
            yrotcar = 0;
            if (yrotpoint > Math.PI)
            {
                yrotpoint = (float)(Math.PI * 2) - yrotpoint;
            }
            rotu = yrotpoint - yrotcar;
            rotu = -(float)(rotu - Math.PI / 2);
            speedlänge = (float)Math.Sin(rotu);
            float r06 = Vector2.Distance(new Vector2(punktpos[0].X, punktpos[0].Z), new Vector2(punktpos[6].X, punktpos[6].Z));
            float x06 = punktpos[0].X - punktpos[6].X;
            float z06 = punktpos[0].Z - punktpos[6].Z;
            float faktor = r / r06;
            newspeed = new Vector3(-x06 * faktor * speedlänge, 0, -z06 * faktor * speedlänge);
            Vector3 unterschied;
            unterschied = new Vector3(newspeed.X - punktspeed[2].X, punktspeed[2].Y, newspeed.Z - punktspeed[2].Z);
            Vector3 endspeed = unterschied;
            if (Vector3.Distance(Vector3.Zero, unterschied) > grip)
            {

                float f2 = grip / Vector3.Distance(Vector3.Zero, unterschied);
                endspeed = new Vector3(unterschied.X * f2, punktspeed[2].Y, unterschied.Z * f2);
            }
            punktspeed[2] += endspeed;

        }
        grip = 1.8f * (punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5]));
        if (punktbodenkontakt[1] == 1)
        {
            p1 = new Vector2(punktpos[0].X, punktpos[0].Z);
            p2 = new Vector2(punktpos[6].X, punktpos[6].Z);
            float r = Vector2.Distance(p1, p2);
            yrotcar = -(float)Math.Asin((p2.X - p1.X) / r);
            if (p2.Y - p1.Y > 0)
            {
                yrotcar = (float)(Math.PI / 2 + (Math.PI / 2 - yrotcar));
            }

            p1 = new Vector2(0, 0);
            p2 = new Vector2(punktspeed[1].X, punktspeed[1].Z);
            r = Vector2.Distance(p1, p2);
            yrotpoint = -(float)Math.Asin((p2.X - p1.X) / r);
            if (p2.Y - p1.Y > 0)
            {
                yrotpoint = (float)(Math.PI / 2 + (Math.PI / 2 - yrotpoint));
            }

            yrotpoint = yrotpoint - yrotcar;
            if (yrotpoint < 0)
            {
                yrotpoint = yrotpoint + (float)Math.PI * 2;
            }
            yrotcar = 0;
            if (yrotpoint > Math.PI)
            {
                yrotpoint = (float)(Math.PI * 2) - yrotpoint;
            }
            rotu = yrotpoint - yrotcar;
            rotu = -(float)(rotu - Math.PI / 2);
            speedlänge = (float)Math.Sin(rotu);
            float r06 = Vector2.Distance(new Vector2(punktpos[0].X, punktpos[0].Z), new Vector2(punktpos[6].X, punktpos[6].Z));
            float x06 = punktpos[0].X - punktpos[6].X;
            float z06 = punktpos[0].Z - punktpos[6].Z;
            float faktor = r / r06;
            newspeed = new Vector3(-x06 * faktor * speedlänge, 0, -z06 * faktor * speedlänge);
            Vector3 unterschied;
            unterschied = new Vector3(newspeed.X - punktspeed[1].X, punktspeed[1].Y, newspeed.Z - punktspeed[1].Z);
            Vector3 endspeed = unterschied;
            if (Vector3.Distance(Vector3.Zero, unterschied) > grip)
            {

                float f2 = grip / Vector3.Distance(Vector3.Zero, unterschied);
                endspeed = new Vector3(unterschied.X * f2, punktspeed[1].Y, unterschied.Z * f2);
            }
            punktspeed[1] += endspeed;

        }
        grip = 1.9f * (punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5]));
        if (punktbodenkontakt[3] == 1)
        {
            p1 = new Vector2(punktpos[0].X, punktpos[0].Z);
            p2 = new Vector2(punktpos[6].X, punktpos[6].Z);
            float r = Vector2.Distance(p1, p2);
            yrotcar = -(float)Math.Asin((p2.X - p1.X) / r);
            if (p2.Y - p1.Y > 0)
            {
                yrotcar = (float)(Math.PI / 2 + (Math.PI / 2 - yrotcar));
            }
            yrotcar += steeringrot;
            p1 = new Vector2(0, 0);
            p2 = new Vector2(punktspeed[3].X, punktspeed[3].Z);
            r = Vector2.Distance(p1, p2);
            yrotpoint = -(float)Math.Asin((p2.X - p1.X) / r);
            if (p2.Y - p1.Y > 0)
            {
                yrotpoint = (float)(Math.PI / 2 + (Math.PI / 2 - yrotpoint));
            }

            yrotpoint = yrotpoint - yrotcar;
            if (yrotpoint < 0)
            {
                yrotpoint = yrotpoint + (float)Math.PI * 2;
            }

            if (yrotpoint > Math.PI)
            {
                yrotpoint = (float)(Math.PI * 2) - yrotpoint;
            }
            rotu = yrotpoint - 0;
            rotu = -(float)(rotu - Math.PI / 2);
            speedlänge = (float)Math.Sin(rotu);
            radrichtung = new Vector2((float)Math.Sin(yrotcar), (float)Math.Cos(yrotcar));
            float r06 = Vector2.Distance(new Vector2(0, 0), radrichtung);
            float x06 = 0 - radrichtung.X;
            float z06 = 0 - radrichtung.Y;
            faktor = r / r06;
            newspeed = new Vector3(-x06 * faktor * (-speedlänge), 0, -z06 * faktor * (-speedlänge));
            //punktspeed[3] = newspeed;
            Vector3 unterschied;
            unterschied = new Vector3(newspeed.X - punktspeed[3].X, punktspeed[3].Y, newspeed.Z - punktspeed[3].Z);
            Vector3 endspeed = unterschied;
            if (Vector3.Distance(Vector3.Zero, unterschied) > grip)
            {

                float f2 = grip / Vector3.Distance(Vector3.Zero, unterschied);
                endspeed = new Vector3(unterschied.X * f2, punktspeed[3].Y, unterschied.Z * f2);
            }
            punktspeed[3] += endspeed;
        }
        grip = 1.9f * (punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5]));
        if (punktbodenkontakt[4] == 1)
        {
            p1 = new Vector2(punktpos[0].X, punktpos[0].Z);
            p2 = new Vector2(punktpos[6].X, punktpos[6].Z);
            float r = Vector2.Distance(p1, p2);
            yrotcar = -(float)Math.Asin((p2.X - p1.X) / r);
            if (p2.Y - p1.Y > 0)
            {
                yrotcar = (float)(Math.PI / 2 + (Math.PI / 2 - yrotcar));
            }
            yrotcar += steeringrot;
            p1 = new Vector2(0, 0);
            p2 = new Vector2(punktspeed[4].X, punktspeed[4].Z);
            r = Vector2.Distance(p1, p2);
            yrotpoint = -(float)Math.Asin((p2.X - p1.X) / r);
            if (p2.Y - p1.Y > 0)
            {
                yrotpoint = (float)(Math.PI / 2 + (Math.PI / 2 - yrotpoint));
            }

            yrotpoint = yrotpoint - yrotcar;
            if (yrotpoint < 0)
            {
                yrotpoint = yrotpoint + (float)Math.PI * 2;
            }

            if (yrotpoint > Math.PI)
            {
                yrotpoint = (float)(Math.PI * 2) - yrotpoint;
            }
            rotu = yrotpoint - 0;
            rotu = -(float)(rotu - Math.PI / 2);
            speedlänge = (float)Math.Sin(rotu);
            radrichtung = new Vector2((float)Math.Sin(yrotcar), (float)Math.Cos(yrotcar));
            float r06 = Vector2.Distance(new Vector2(0, 0), radrichtung);
            float x06 = 0 - radrichtung.X;
            float z06 = 0 - radrichtung.Y;
            faktor = r / r06;
            newspeed = new Vector3(-x06 * faktor * (-speedlänge), 0, -z06 * faktor * (-speedlänge));
            //punktspeed[3] = newspeed;
            Vector3 unterschied;
            unterschied = new Vector3(newspeed.X - punktspeed[4].X, punktspeed[4].Y, newspeed.Z - punktspeed[4].Z);
            Vector3 endspeed = unterschied;
            if (Vector3.Distance(Vector3.Zero, unterschied) > grip)
            {

                float f2 = grip / Vector3.Distance(Vector3.Zero, unterschied);
                endspeed = new Vector3(unterschied.X * f2, punktspeed[4].Y, unterschied.Z * f2);
            }
            punktspeed[4] += endspeed;

        }


        if (Keyboard.GetState().IsKeyDown(Keys.T))
        {
            if (punktbodenkontakt[2] == 1)
            {
                punktspeed[2].X -= ((float)Math.Sin(yrotcar) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5])));
                punktspeed[2].Z -= ((float)Math.Cos(yrotcar) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5])));
                punktspeed[2].Y += ((float)Math.Sin(cabrot.X) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5]))) - 0.5f;
            }
            if (punktbodenkontakt[1] == 1)
            {
                punktspeed[1].X -= ((float)Math.Sin(yrotcar) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5])));
                punktspeed[1].Z -= ((float)Math.Cos(yrotcar) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5])));
                punktspeed[1].Y += ((float)Math.Sin(cabrot.X) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5]))) - 0.5f;
            }
        }
        if (Keyboard.GetState().IsKeyDown(Keys.G))
        {
            if (punktbodenkontakt[2] == 1)
            {
                punktspeed[2].X += ((float)Math.Sin(yrotcar) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5])));
                punktspeed[2].Z += ((float)Math.Cos(yrotcar) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5])));
                punktspeed[2].Y -= ((float)Math.Sin(cabrot.X) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5]))) + 0.5f;
            }
            if (punktbodenkontakt[1] == 1)
            {
                punktspeed[1].X += ((float)Math.Sin(yrotcar) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5])));
                punktspeed[1].Z += ((float)Math.Cos(yrotcar) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5])));
                punktspeed[1].Y -= ((float)Math.Sin(cabrot.X) / power) * ((punktpos[5].Y - punktpos[0].Y) / (Vector3.Distance(punktpos[0], punktpos[5]))) + 0.5f;
            }
        }
        for (int i = 0; i < ballmenge; i++)
        {
            if (punktbesetzt[i] == 1)
            {
                punktpos[i].Y += punktspeed[i].Y;
                punktpos[i].X += punktspeed[i].X;
                punktpos[i].Z += punktspeed[i].Z;
            }
        }*/

                punktposupdate = 1;
                while (punktposupdate == 1)
                { }

                if (wind == 1)
                {
                    windspeed.X += (float)((float)r.Next(-50, 50)) / (float)10000;
                    windspeed.Y += (float)((float)r.Next(-50, 50)) / (float)50000;
                    windspeed.Z += (float)((float)r.Next(-50, 50)) / (float)50000;
                    if (windspeed.X < 0.01f)
                    {
                        windspeed.X = 0.01f;
                    }

                    if (windspeed.X > 0.03f)
                    {
                        windspeed.X = 0.03f;
                    }
                    if (windspeed.Y < -0.01f)
                    {
                        windspeed.Y = -0.01f;
                    }
                    if (windspeed.Y > 0.01f)
                    {
                        windspeed.Y = 0.01f;
                    }
                    if (windspeed.Z < -0.01f)
                    {
                        windspeed.Z = -0.01f;
                    }
                    if (windspeed.Z > 0.01f)
                    {
                        windspeed.Z = 0.01f;
                    }

                    /*for (int i = 0; i < 50; i++)
                    {
                        punktpos[i] = new Vector3(300, 200 + i * 10, 0);
                        punktspeed[i] = Vector3.Zero;
                    }*/
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    grenze += 5;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    grenze -= 5;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && stop2 == 0)
                {
                    stop2 = 1;
                    if (wind == 1)
                    {
                        wind = 0;
                    }
                    else
                    {
                        wind = 1;
                    }
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Space))
                {
                    stop2 = 0;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && stopp == 0)
                {
                    for (int i = 0; i < ballmenge; i++)
                    {
                        if (punktbesetzt[i] == 1)
                        {
                            if (mousePosition.X > punktpos[i].X - 5 && mousePosition.X < punktpos[i].X + 5 && mousePosition.Y > punktpos[i].Y - 5 && mousePosition.Y < punktpos[i].Y + 5)
                            {
                                geklickterpunkt = i;
                                stopp = 1;
                            }
                        }
                    }
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && stopp == 1)
                {
                    punktpos[geklickterpunkt].X = mousePosition.X;
                    punktpos[geklickterpunkt].Y = mousePosition.Y;
                    punktspeed[geklickterpunkt].X = 0;
                    punktspeed[geklickterpunkt].Y = 0;

                }
                unterschied1.X = mousePosition.X;
                unterschied1.Y = mousePosition.Y;
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    stopp = 0;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Tab) && stopp2 == 0)
            {
                Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                stopp2 = 1;
                if (platzieren == 0)
                {
                    platzieren = 1;
                }
                else if (platzieren == 1)
                {
                    platzieren = 0;
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Tab))
            {
                stopp2 = 0;
            }
            if (platzieren == 1)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && st1 == 0)
                {
                    for (int i = 0; i < ballmenge; i++)
                    {
                        if (punktbesetzt[i] == 0)
                        {
                            st1 = 1;
                            punktbesetzt[i] = 1;
                            punktpos[i].X = mousePosition.X;
                            punktpos[i].Y = mousePosition.Y;
                            punktgrip[i] = 0.1f;
                            break;
                        }
                    }
                }
                else if (Mouse.GetState().LeftButton == ButtonState.Released)
                {
                    st1 = 0;
                }
                if (Mouse.GetState().RightButton == ButtonState.Pressed && beidepunktegezogen == 0)
                {
                    for (int i = 0; i < ballmenge; i++)
                    {
                        if (punktbesetzt[i] == 1)
                        {
                            if (mousePosition.X > punktpos[i].X - 5 && mousePosition.X < punktpos[i].X + 5 && mousePosition.Y > punktpos[i].Y - 5 && mousePosition.Y < punktpos[i].Y + 5)
                            {
                                for (int j = 0; j < muskelmenge; j++)
                                {
                                    if (muskelbesetzt[j] == 0)
                                    {
                                        beidepunktegezogen = 1;
                                        gezogenermuskel = j;
                                        gezogenermuskelp1 = i;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
                else if (Mouse.GetState().RightButton == ButtonState.Released && beidepunktegezogen == 1)
                {
                    for (int i = 0; i < ballmenge; i++)
                    {
                        if (punktbesetzt[i] == 1)
                        {
                            if (mousePosition.X > punktpos[i].X - 5 && mousePosition.X < punktpos[i].X + 5 && mousePosition.Y > punktpos[i].Y - 5 && mousePosition.Y < punktpos[i].Y + 5)
                            {
                                beidepunktegezogen = 0;
                                muskelbesetzt[gezogenermuskel] = 1;
                                muskelpunkt1[gezogenermuskel] = gezogenermuskelp1;
                                muskelpunkt2[gezogenermuskel] = i;
                                idealemuskellänge[gezogenermuskel] = (float)Math.Sqrt((punktpos[muskelpunkt2[gezogenermuskel]].X - punktpos[muskelpunkt1[gezogenermuskel]].X) * (punktpos[muskelpunkt2[gezogenermuskel]].X - punktpos[muskelpunkt1[gezogenermuskel]].X) + (punktpos[muskelpunkt2[gezogenermuskel]].Y - punktpos[muskelpunkt1[gezogenermuskel]].Y) * (punktpos[muskelpunkt2[gezogenermuskel]].Y - punktpos[muskelpunkt1[gezogenermuskel]].Y));
                                muskelstärke[gezogenermuskel] = stangenweichheit;
                                break;
                            }
                        }
                    }
                }
                if (Mouse.GetState().RightButton == ButtonState.Released)
                {
                    beidepunktegezogen = 0;
                }
            }
            if (physicsstart == 0)
            {
                physicsstart = 1;
                Thread physicsthread = new Thread(physics);
                physicsthread.Start();
            }
            physicabbruch = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                System.Threading.Thread.Sleep(250);
            System.Threading.Thread.Sleep(5);
            base.Update(gameTime);
        }
        public void BeginRender3D()
        {
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Draw(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            var Mouse2 = Mouse.GetState();
            float neuescrollmenge = Mouse2.ScrollWheelValue;

            if (neuescrollmenge > altescrollmenge)
            {
                if (zoom < 10)
                {
                    zoom *= 1.2f;
                }
            }
            else if (neuescrollmenge < altescrollmenge)
            {
                if (zoom > 0.1f)
                {
                    zoom *= 0.8f;
                }
            }
            Mouse2 = Mouse.GetState();
            altescrollmenge = Mouse2.ScrollWheelValue;

            camera = punktpos[5];
            rotationMatrix = Matrix.CreateRotationY(rotation.X);// * Matrix.CreateRotationX(rotationY);
            Vector3 transformedReference = Vector3.TransformNormal(new Vector3(0, 0, 1000 / zoom), rotationMatrix);

            cameraLookat = camera + transformedReference;
            cameralookatges.Y = cameraLookat.Y - (float)Math.Sin(rotation.Y) * Vector3.Distance(camera, cameraLookat);
            cameralookatges.X = cameraLookat.X - (cameraLookat.X - camera.X) * (float)(1 - Math.Cos(rotation.Y));
            cameralookatges.Z = cameraLookat.Z - (cameraLookat.Z - camera.Z) * (float)(1 - Math.Cos(rotation.Y));
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Vector3 cameraunterschied;
                cameraunterschied = camera - cameralookatges;
                camera -= cameraunterschied * 100;
                cameralookatges -= cameraunterschied * 100;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Vector3 cameraunterschied;
                cameraunterschied = camera - cameralookatges;
                camera += cameraunterschied * 10;
                cameralookatges += cameraunterschied * 10;

            }
            Vector3 lokl = cameralookatges;
            cameralookatges = camera;
            camera = lokl;
            if (cameramode == 2)
            {
                cameralookatges = (punktpos[5] + (punktpos[5] - punktpos[0]) * 1.25f);
                camera = (punktpos[5] + (punktpos[5] - punktpos[0]) * 1.5f) + (punktpos[0] - punktpos[6]) / 2.2f;
                basicEffect.View = Matrix.CreateLookAt(camera, cameralookatges, (punktpos[5] - punktpos[0]) / (punktpos[5] - punktpos[0]).Length());
            }
            else if (cameramode == 1)
            {
                basicEffect.View = Matrix.CreateLookAt(camera, cameralookatges, Vector3.Up);
            }


            var vertices = new[] { new VertexPositionColor(startPoint, Color.White), new VertexPositionColor(endPoint, Color.White) };

            //GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
            //DrawLine(spriteBatch, punktpos[muskelpunkt1[i]], punktpos[muskelpunkt2[i]], new Color(255, 255, 255), 0.5f);
            /*vertices = new[] { new VertexPositionColor(new Vector3(0, 0, 0), Color.White), new VertexPositionColor(new Vector3(100, 0, 0), Color.White) };
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
            vertices = new[] { new VertexPositionColor(new Vector3(0, 0, 0), Color.White), new VertexPositionColor(new Vector3(0, 100, 0), Color.White) };
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
            vertices = new[] { new VertexPositionColor(new Vector3(0, 100, 0), Color.White), new VertexPositionColor(new Vector3(100, 100, 0), Color.White) };
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
            vertices = new[] { new VertexPositionColor(new Vector3(100, 100, 0), Color.White), new VertexPositionColor(new Vector3(100, 0, 0), Color.White) };
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
            vertices = new[] { new VertexPositionColor(new Vector3(0, 0, 0), Color.White), new VertexPositionColor(new Vector3(0, 0, 100), Color.White) };
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
            vertices = new[] { new VertexPositionColor(new Vector3(100, 0, 0), Color.White), new VertexPositionColor(new Vector3(100, 0, 100), Color.White) };
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
            vertices = new[] { new VertexPositionColor(new Vector3(0, 100, 0), Color.White), new VertexPositionColor(new Vector3(0, 100, 100), Color.White) };
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
            vertices = new[] { new VertexPositionColor(new Vector3(100, 100, 0), Color.White), new VertexPositionColor(new Vector3(100, 100, 100), Color.White) };
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);

            vertices = new[] { new VertexPositionColor(new Vector3(0, 0, 100), Color.White), new VertexPositionColor(new Vector3(100, 0, 100), Color.White) };
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
            vertices = new[] { new VertexPositionColor(new Vector3(0, 0, 100), Color.White), new VertexPositionColor(new Vector3(0, 100, 100), Color.White) };
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
            vertices = new[] { new VertexPositionColor(new Vector3(0, 100, 100), Color.White), new VertexPositionColor(new Vector3(100, 100, 100), Color.White) };
            GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);*/
            //vertices = new[] { new VertexPositionColor(new Vector3(100, 100, 100), Color.White), new VertexPositionColor(new Vector3(100, 0, 100), Color.White), new VertexPositionColor(new Vector3(0, 0, 0), Color.White) };
            //GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertices, 0, 1);
            basicEffect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(kreis, new Vector2(graphics.PreferredBackBufferWidth / 2 - 5, graphics.PreferredBackBufferHeight / 2 - 5), Color.Red);
            BeginRender3D();
            Vector3 oringalTrans;
            Vector3 newTrans;








            //wheelmodel.Bones[2].Transform = Matrix.CreateRotationY(aba) * Matrix.CreateTranslation(wheelmodel.Bones[2].Transform.Translation.X, wheelmodel.Bones[2].Transform.Translation.Y, wheelmodel.Bones[2].Transform.Translation.Z);
            float xzd = Vector2.Distance(new Vector2(punktpos[0].X, punktpos[0].Z), new Vector2(punktpos[5].X, punktpos[5].Z));
            float d = (float)Vector3.Distance(punktpos[0], punktpos[5]);

            // Y Rotation

            Vector2 p1, p2;
            p1 = new Vector2(punktpos[0].X, punktpos[0].Z);
            p2 = new Vector2(punktpos[6].X, punktpos[6].Z);
            float r = Vector2.Distance(p1, p2);
            cabrot.Y = -(float)Math.Asin((p2.X - p1.X) / r);
            if (p2.Y - p1.Y > 0)
            {
                cabrot.Y = (float)(Math.PI / 2 + (Math.PI / 2 - cabrot.Y));
            }
            if (punktpos[0].Y > punktpos[5].Y)
            {
                cabrot.Y += (float)Math.PI;
            }


            // X Rotation

            p1 = new Vector2(punktpos[5].Z, punktpos[5].Y);
            p2 = new Vector2(punktpos[0].Z, punktpos[0].Y);
            r = Vector2.Distance(p1, p2);
            cabrot.X = -(float)Math.Asin((p2.X - p1.X) / r);
            if (p2.Y - p1.Y > 0)
            {
                //this.Exit();
                //cabrot.X = (float)(Math.PI / 2 + (Math.PI / 2 - cabrot.X));
                //cabrot.Y += (float)Math.PI;
                //cabrot.X = 
            }

            // Z Rotation
            Matrix rm, rm2;
            rm = Matrix.CreateLookAt(punktpos[0], punktpos[6], Vector3.Up);
            rm = Matrix.Invert(rm);
            p1 = new Vector2(punktpos[5].X, punktpos[5].Y);
            p2 = new Vector2(punktpos[0].X, punktpos[0].Y);
            r = Vector2.Distance(p1, p2);
            cabrot.Z = (float)-Math.Asin((p2.Y - p1.Y) / r);
            if (punktpos[0].Y > punktpos[5].Y)
            {
                //this.Exit();
                cabrot.Z = -(cabrot.Z - (float)Math.PI);

            }
            //cabrot.X = -(float)Math.Asin((punktpos[0].Y - punktpos[6].Y) / Vector3.Distance(punktpos[0], punktpos[6]));
            //cabrot.X = -(float)Math.Atan((punktpos[6].Y - punktpos[0].Y) / (punktpos[6].Z - punktpos[0].Z));





            //cabrot.Y = (float)Math.Acos(((punktpos[0].Z - punktpos[6].Z) / Vector3.Distance(punktpos[0], punktpos[6])));
            //cabrot.Y = (float)Math.Atan((punktpos[6].X - punktpos[0].X) / (punktpos[6].Z - punktpos[0].Z));
            if (punktpos[0].X - punktpos[6].X < 0)
            {
                //cabrot.Y *= -1;
            }


            cabrot.Z = (float)Math.Asin((punktpos[0].Y - punktpos[7].Y) / Vector3.Distance(punktpos[0], punktpos[7]));
            if (punktpos[0].Y > punktpos[5].Y)
            {
                //this.Exit();
                cabrot.Z = -(cabrot.Z + (float)Math.PI);

            }
            oringalTrans = originalbone[0].Translation;
            wheelmodel.Bones[0].Transform = originalbone[0] * Matrix.CreateScale(1f);
            newTrans = wheelmodel.Bones[0].Transform.Translation;
            wheelmodel.Bones[0].Transform *= Matrix.CreateTranslation(oringalTrans - newTrans);
            for (int i = 0; i < 4; i++)
            {
                alteradpos[i] = punktpos[i + 1];
            }

            for (int i = 2; i < 4; i++)
            {
                oringalTrans = punktpos[i - 1 + 2];
                wheelmodel.Bones[i].Transform = originalbone[i] * Matrix.CreateRotationX((float)radrotation2[i - 2]) * Matrix.CreateRotationY(steeringrot) * Matrix.CreateRotationZ(cabrot.Z) * rm;
                newTrans = wheelmodel.Bones[i].Transform.Translation;
                wheelmodel.Bones[i].Transform *= Matrix.CreateTranslation(oringalTrans - newTrans);
            }
            for (int i = 4; i < 6; i++)
            {
                oringalTrans = punktpos[i - 1 - 2];
                wheelmodel.Bones[i].Transform = originalbone[i] * Matrix.CreateRotationX((float)radrotation2[i - 2]) * Matrix.CreateRotationZ(cabrot.Z) * rm;
                newTrans = wheelmodel.Bones[i].Transform.Translation;
                wheelmodel.Bones[i].Transform *= Matrix.CreateTranslation(oringalTrans - newTrans);
            }
            oringalTrans = punktpos[0];
            wheelmodel.Bones[1].Transform = originalbone[1] * Matrix.CreateRotationZ(cabrot.Z) * rm;
            newTrans = wheelmodel.Bones[1].Transform.Translation;
            wheelmodel.Bones[1].Transform *= Matrix.CreateTranslation(oringalTrans - newTrans);



            for (int i = 0; i < muskelmenge; i++)
            {
                if (muskelbesetzt[i] == 1 && cameramode == 1)
                {
                    vertices = new[] { new VertexPositionColor(punktpos[muskelpunkt1[i]], Color.Red), new VertexPositionColor(punktpos[muskelpunkt2[i]], Color.Red) };
                    graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
                    //new Vector3(-3000, -1000, -5000), new Vector3(0, 2000, -30000), new Vector3(5000, -400, -5000)
                    vertices = new[] { new VertexPositionColor(new Vector3(-3000, -800, -5000), Color.Red), new VertexPositionColor(new Vector3(0, 5000, -30000), Color.Red), new VertexPositionColor(new Vector3(5000, -800, -5000), Color.Red) };
                    //graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1);
                }
            }



            //Floor.Draw(effect, basicEffect);
            //tr1.Draw(effect, basicEffect);
            /*for(int i = 0; i < 115 * 200; i++)
            {
                if(maptrianglesbesetzt[i] == 1)
                {
                    maptriangles[i].Draw(effect, basicEffect);
                }
            }*/
            map.Draw(effect, basicEffect, verschiebungupdate.X, verschiebungupdate.Y, verschiebungupdate.Z);
            bonetransformations = new Matrix[wheelmodel.Bones.Count];
            wheelmodel.CopyAbsoluteBoneTransformsTo(bonetransformations);
            int meshnum = 0;
            foreach (ModelMesh mesh in wheelmodel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    meshnum++;
                    effect.EnableDefaultLighting();

                    effect.World = bonetransformations[mesh.ParentBone.Index];// * Matrix.CreateRotationX(aba);
                    effect.View = basicEffect.View;
                    effect.Projection = basicEffect.Projection;

                }
                mesh.Draw();
            }


            /*mapmodell.Bones[0].Transform = originalmap * Matrix.CreateScale(1);
            bonetransformations = new Matrix[mapmodell.Bones.Count];
            mapmodell.CopyAbsoluteBoneTransformsTo(bonetransformations);
            meshnum = 0;
            foreach (ModelMesh mesh in mapmodell.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    meshnum++;
                    effect.EnableDefaultLighting();

                    effect.World = bonetransformations[mesh.ParentBone.Index];// * Matrix.CreateRotationX(aba);
                    effect.View = basicEffect.View;
                    effect.Projection = basicEffect.Projection;

                }
                mesh.Draw();
            }*/
            //var vertices = new[] { new VertexPositionColor(new Vector3(0, 0, 0), Color.Green), new VertexPositionColor(new Vector3(0, 0, 0), Color.White) };

            for (int i = 0; i < muskelmenge; i++)
            {
                if (muskelbesetzt[i] == 1)
                {
                    //DrawLine(spriteBatch, punktpos[muskelpunkt1[i]], punktpos[muskelpunkt2[i]], new Color(255, 255, 255), 0.5f);
                }
            }
            for (int i = 0; i < ballmenge; i++)
            {

                if (punktbesetzt[i] == 1)
                {
                    //vertices = new[] { new VertexPositionColor(new Vector3(0, 100, 0), Color.White), new VertexPositionColor(new Vector3(100, 100, 100), Color.White), new VertexPositionColor(new Vector3(0, 100, 100), Color.White) };

                }
            }
            if (beidepunktegezogen == 1)
            {
                //DrawLine(spriteBatch, punktpos[gezogenermuskelp1], new Vector2(mousePosition.X, mousePosition.Y), Color.Red, 1f);
            }
            float k = 0;
            float winkel = (float)Math.Atan((50) / (k));
            if (b < -0.5 || b >= -0.001)
            {
                j = b;
            }
            Vector3 P1, P2, P3;
            Vector3 L1, L2;
            P1 = new Vector3(0, 100, 0);
            P2 = new Vector3(100, 50, 0);
            P3 = new Vector3(50, 0, 100);
            L1 = new Vector3(50, 110, 50);
            L2 = new Vector3(50, 120, 50);
            Vector3 lv = L2 - L1;
            Ray ray = new Ray(L1, lv);
            Plane plane = new Plane(P1, P2, P3);

            var t = ray.Intersects(plane); //Distance along line from L1
            ///Result:
            //float x = L1.X + (float)t * lv.X;
            //float y = L1.Y + (float)t * lv.Y;
            //var z = L1.Z + (float)t * lv.Z;
            //float x2 = (float)x;
            Vector3 a1 = new Vector3(0, 0, 0.001f);
            a1.Normalize();
            Vector3 a2 = new Vector3(0, 0, 0.001f);
            a2.Normalize();
            float abc = (float)Math.Acos(Vector3.Dot(a1, a2));
            //DrawLine(spriteBatch, new Vector2(300, 150), new Vector2(300 + windspeed.X * 3000, 150 + windspeed.Y * 3000), Color.Red, 3f);
            //spriteBatch.DrawString(a, camera.ToString(), new Vector2(100, 100), Color.Red);
            spriteBatch.DrawString(a, slowmotion.ToString(), new Vector2(100, 200), Color.Red);
            spriteBatch.DrawString(a, punktspeed[1].ToString(), new Vector2(100, 220), Color.Red);
            spriteBatch.DrawString(a, gt2.ToString(), new Vector2(100, 240), Color.Red);
            spriteBatch.DrawString(a, radrotation[0].ToString(), new Vector2(100, 260), Color.Red);
            spriteBatch.DrawString(a, punktspeed[1].ToString(), new Vector2(100, 280), Color.Blue);
            spriteBatch.DrawString(a, punktpos[2].ToString(), new Vector2(100, 300), Color.Red);
            spriteBatch.DrawString(a, punktspeed[3].ToString(), new Vector2(100, 320), Color.Red);
            spriteBatch.DrawString(a, punktpos[4].ToString(), new Vector2(100, 340), Color.Red);

            //spriteBatch.DrawString(a, cameralookatges.ToString(), new Vector2(100, 300), Color.Green);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

