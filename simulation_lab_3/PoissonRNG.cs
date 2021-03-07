using System;
using System.Collections.Generic;

namespace simulation_lab_3
{
    public class PoissonRNG
    {
        private readonly Random _rng;

        public PoissonRNG()
        {
            _rng = new Random();
        }

        public int GetRandomNumber(double lambda)
        {
            int m;
            var sum = 0d;

            for (m = 0; ; m++)
            {
                var a = _rng.NextDouble();
                sum += Math.Log(a);

                if (sum < -lambda) break;
            }

            return m;
        }

        public List<int> GetRandomNumberList(double lambda, int length)
        {
            var randomList = new List<int>();

            for (var i = 0; i < length; i++)
            {
                var number = GetRandomNumber(lambda);
                randomList.Add(number);
            }

            return randomList;
        }
    }
}
