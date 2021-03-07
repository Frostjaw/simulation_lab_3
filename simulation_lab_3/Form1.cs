using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Testing;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace simulation_lab_3
{
    public partial class Form1 : Form
    {
        private PoissonRNG _rng;

        public Form1()
        {
            InitializeComponent();

            _rng = new PoissonRNG();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!char.IsDigit(number) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!char.IsDigit(number) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int parameter;
            int experimentsCount;

            var parameterString = textBox1.Text;
            if (string.IsNullOrEmpty(parameterString))
            {
                MessageBox.Show("Enter parameter");

                return;
            }

            var experimentsCountString = textBox2.Text;
            if (string.IsNullOrEmpty(experimentsCountString))
            {
                MessageBox.Show("Enter number of experiments");

                return;
            }

            if (experimentsCountString == "0")
            {
                MessageBox.Show("Enter valid number of experiments");

                return;
            }

            parameter = Convert.ToInt32(textBox1.Text);            
            experimentsCount = Convert.ToInt32(textBox2.Text);

            var sample = _rng.GetRandomNumberList(parameter, experimentsCount);

            var statistics = new PoissonStatistics(sample, parameter);

            var average = statistics.Mean;
            var averageError = statistics.MeanError;
            var variance = statistics.Variance;
            var varianceError = statistics.VarianceError;

            textBox3.Text = average.ToString("N3");
            textBox8.Text = averageError.ToString("N3");
            textBox4.Text = variance.ToString("N3");
            textBox9.Text = varianceError.ToString("N3");

            var chiSquared = statistics.ChiSquared;
            double criticalValue = 0;
            if (experimentsCount > 2)
            {
                criticalValue = ChiSquared.InvCDF(experimentsCount - 2, 0.95);
            }
            else if (experimentsCount == 2)
            {
                criticalValue = ChiSquared.InvCDF(experimentsCount - 1, 0.95);
            }
            else if (experimentsCount == 1)
            {
                criticalValue = ChiSquared.InvCDF(experimentsCount, 0.95);
            }

            textBox5.Text = chiSquared.ToString("N3");
            textBox6.Text = criticalValue.ToString("N3");

            if (chiSquared > criticalValue)
            {
                textBox7.Text = "FALSE";
                textBox7.BackColor = Color.Red;
                textBox7.Show();
            }
            else
            {
                textBox7.Text = "TRUE";
                textBox7.BackColor = Color.Green;
                textBox7.Show();
            }

        }
    }
}
