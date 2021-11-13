using System;
using VectorLib;

namespace Mechanics
{
    public class Ball
    {
        public double M { get; set; }
        public double Radius
        {
            get; set;
        }
        public Vector V { get; set; }
        public Vector A { get; set; }
        public Vector R { get; set; }
        public double constRadius { get; set; }
        public double friction { get; set; } = 1;

        public void Move(double dt)
        {
            V += A * dt;
            V *= friction;
            R += V * dt;

        }

        public void Move(double dt, Vector F)
        {
            A = F / M;
            Move(dt);
        }
    }
}
