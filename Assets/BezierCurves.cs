using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class BezierCurves
    {
        public List<Vector3[]> line(float x, float y)
        {
            return new List<Vector3[]>
            {
                new[]
                {
                    new Vector3(x / 3, y / 3, 0),
                    new Vector3(x * 2 / 3, y * 2 / 3, 0),
                    new Vector3(x, y, 0)
                }
            };
        }

        public List<Vector3[]> arc(float x, float y, float right)
        {
            var dis = x * x + y * y;
            // y, -x
            return new List<Vector3[]>
            {
                new[]
                {
                    new Vector3(y / dis * right, -x / dis * right, 0),
                    new Vector3(x + y / dis * right, y + -x / dis * right, 0),
                    new Vector3(x, y, 0)
                }
            };
        }

        public List<Vector3[]> random(int count = 4, float bound = 0.5f)
        {
            var randomVector3 = new RandomVector3();
            var list = new List<Vector3[]>();
            for (var i = 0; i < count; ++i)
                list.Add(new[]
                {
                    randomVector3.Next(bound),
                    randomVector3.Next(bound),
                    randomVector3.Next(bound)
                });

            return list;
        }

        public List<Vector3[]> concat(params List<Vector3[]>[] curves)
        {
            List<Vector3[]> result = new List<Vector3[]>();
            Vector3 last = new Vector3(0, 0, 0);
            foreach (var curve in curves)
            {
                foreach (var vector3se in curve)
                {
                    result.Add(vector3se.ToList().Select(vector => vector + last).ToArray());
                }

                last = result[result.Count - 1][2];
            }

            return result;
        }

        public List<Vector3[]> sing(Vector3 a, Vector3 b, Vector3 c)
        {
            return new List<Vector3[]>
            {
                new[]
                {
                    a,
                    b,
                    c
                }
            };
        }

        private class RandomVector3
        {
            private Vector3 last = new Vector3(0, 0, 0);

            public Vector3 Next(float bound)
            {
                var next = new Vector3(last.x + Random.Range(-bound, bound), last.y + Random.Range(-bound, bound),
                    last.z);
                last = next;
                return next;
            }
        }
    }
}