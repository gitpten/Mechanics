using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using VectorLib;

namespace Mechanics
{
    public static class Collisions
    {
        public static bool IsColliding(Ball b1, Ball b2)
        {
            return (b1.R - b2.R).SqAbs <= (b1.Radius + b2.Radius) * (b1.Radius + b2.Radius);
        }

        public static bool IsColliding(Ball b, Coords p1, Coords p2)
        {
            Vector p1_v = new Vector(p1);
            Vector p1_b_v = b.R - p1_v;
            Vector p1p2_v = new Vector(p1, p2);
            return p1_b_v.Projection(p1p2_v.Normal).SqAbs <= b.Radius * b.Radius;
        }

        public static void Collide(Ball b, Coords[] points)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Collide(b, points[i], points[i + 1]);
            }
            Collide(b, points[points.Length - 1], points[0]);
        }

        public static void Collide(Ball b, Coords p1, Coords p2)
        {
            if(IsColliding(b, p1, p2))
            {
                b.V = b.V.Mirror(new Vector(p1, p2));

                Vector p1_v = new Vector(p1);
                Vector p1_b_v = b.R - p1_v;
                Vector p1p2_v = new Vector(p1, p2);
            }
        }

        public static void Collide(Ball b1, Ball b2)
        {
            if (IsColliding(b1, b2))
            {
                //Этих изменений в мастере нет
                Vector OO = b1.R - b2.R;

                Vector v0 = b1.V.Projection(OO);
                Vector u0 = b2.V.Projection(OO);

                Vector v0y = b1.V - v0;
                Vector u0y = b2.V - u0;

                Vector v = (2 * b2.M * u0 + v0 * (b1.M - b2.M)) / (b1.M + b2.M);
                Vector u = v0 + v - u0;

                b1.V = v + v0y;
                b2.V = u + u0y;
            }
        }
    }
}
