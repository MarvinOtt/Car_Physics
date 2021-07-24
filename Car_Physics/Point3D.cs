using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Physics
{
    class Point3d
    {
        public struct Point3D : IEquatable<Point3D>
        {
            public static readonly Point3D Zero = new Point3D();

            public float X;
            public float Y;
            public float Z;

            public Point3D(float x, float y, float z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is Point3D))
                {
                    return false;
                }

                return this.Equals((Point3D)obj);
            }

            public static bool operator ==(Point3D one, Point3D two)
            {
                return one.Equals(two);
            }
            public static bool operator !=(Point3D one, Point3D two)
            {
                return !one.Equals(two);
            }

            public static Point3D operator *(float n, Point3D v)
            {
                return new Point3D(v.X * n, v.Y * n, v.Z * n);
            }
            public static Point3D operator +(Point3D v1, Point3D v2)
            {
                return new Point3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
            }
            public static Point3D operator -(Point3D v1, Point3D v2)
            {
                return new Point3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
            }

            public static float operator *(Point3D v1, Point3D v2)
            {
                return (v1.X * v2.X) + (v1.Y * v2.Y) + (v1.Z * v2.Z);
            }
            public static float Magnitude(Point3D v)
            {
                return (float)Math.Sqrt(v * v);
            }

            public static Point3D Normalize(Point3D v)
            {
                float mag = Magnitude(v);
                float div = (mag == 0) ? float.PositiveInfinity : (1 / mag);
                return div * v;
            }
            public static Point3D Cross(Point3D v1, Point3D v2)
            {
                return new Point3D(((v1.Y * v2.Z) - (v1.Z * v2.Y)),
                                  ((v1.Z * v2.X) - (v1.X * v2.Z)),
                                  ((v1.X * v2.Y) - (v1.Y * v2.X)));
            }

            /// <summary>
            ///  doesnt take square root
            /// </summary>
            public static float FastDistance(Point3D v1, Point3D v2)
            {
                float x = v1.X - v2.X;
                x *= x;


                float y = v1.Y - v2.Y;
                y *= y;


                float z = v1.Z - v2.Z;
                z *= z;

                return x + y + z;
            }
            /// <summary>
            /// Takes square root:
            /// </summary>
            public static float Distance(Point3D v1, Point3D v2)
            {
                return (float)Math.Sqrt(Point3D.FastDistance(v1, v2));
            }

            public override int GetHashCode()
            {
                return this.X.GetHashCode()
                    ^ this.Y.GetHashCode()
                    ^ this.Y.GetHashCode();
            }

            public override string ToString()
            {
                return this.X + ", " + this.Y + ", " + this.Z;
            }

            public bool Equals(Point3D other)
            {
                return this.X == other.X
                    && this.Y == other.Y
                    && this.Z == other.Z;
            }
        }
    }
}
