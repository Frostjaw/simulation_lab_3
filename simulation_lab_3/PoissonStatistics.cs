using System;
using System.Collections.Generic;
using System.Linq;

namespace simulation_lab_3
{
    public class PoissonStatistics
    {
        private List<int> _sample;
        private int _numberOfTrials;
        private double _lambda;

        public PoissonStatistics(List<int> sample, double lambda)
        {
            _sample = sample;
            _numberOfTrials = sample.Count;
            _lambda = lambda;

            Occurrences = CalculateOccurrences();
            Frequencies = CalculateFrequencies();
            Mean = CalculateMean();
            Variance = CalculateVariance();
            MeanError = Math.Abs(Mean - _lambda);
            VarianceError = Math.Abs(Variance - _lambda);            
            ChiSquared = CalculateChiSquared();
        }

        public double Mean { get; }
        public double Variance { get; }
        public double MeanError { get; }
        public double VarianceError { get; }
        public double ChiSquared { get; }
        public Dictionary<int, int> Occurrences { get; }
        public Dictionary<int, double> Frequencies { get; }

        private Dictionary<int, int> CalculateOccurrences()
        {
            var occurrences = new Dictionary<int, int>();

            for (var i = 0; i <= _sample.Max(); i++)
            {
                occurrences.Add(i, 0);
            }

            foreach (var number in _sample)
            {
                occurrences[number]++;
            }

            return occurrences;
        }

        private Dictionary<int, double> CalculateFrequencies()
        {
            var frequencies = new Dictionary<int, double>();

            foreach (var item in Occurrences)
            {
                var number = item.Key;
                var count = item.Value;
                frequencies[number] = (double) count / _numberOfTrials;
            }

            return frequencies;
        }

        private double CalculateMean()
        {
            var sum = 0d;
            foreach (var pair in Frequencies)
            {
                var number = pair.Key;
                var freq = pair.Value;
                sum += freq * number;
            }

            return sum;
        }

        private double CalculateVariance()
        {
            var sum = 0d;

            foreach (var pair in Frequencies)
            {
                var number = pair.Key;
                var freq = pair.Value;
                sum += freq * number * number;
            }

            return sum - Mean * Mean;
        }

        private double CalculateChiSquared()
        {
            var sum = 0d;

            foreach (var pair in Frequencies)
            {
                var number = pair.Key;
                var expectedFreq = (Math.Pow(_lambda, number) / CalculateFactorial(number)) * Math.Exp(-_lambda);
                var n = Occurrences[number];
                sum += n * n / (_numberOfTrials * expectedFreq);
            }

            return sum - _numberOfTrials;
        }

        private int CalculateFactorial(int x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                return x * CalculateFactorial(x - 1);
            }
        }
    }
}
