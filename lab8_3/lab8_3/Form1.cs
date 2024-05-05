using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab8_3
{
    public partial class Form1 : Form
    {
        private double[] probabilities = new double[5]; // Array that stores event probabilities
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            InitializeProbabilities();
        }
        private void InitializeProbabilities()
        {
            TextBox[] probabilityTextBoxes = { textBox1, textBox2, textBox3, textBox4, textBox5 };

            // Get the probability from the textbox and initialize it into the array
            for (int i = 0; i < probabilities.Length; i++)
            {
                double probability;
                if (double.TryParse(probabilityTextBoxes[i].Text, out probability))
                {
                    probabilities[i] = probability;
                }
                else
                {
                    // If the content in the text box cannot be parsed, the default probability value is used
                    probabilities[i] = 0.2;
                }
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            InitializeProbabilities();
            if (!ValidateProbabilities())
            {
                MessageBox.Show("Please make sure that the total probability of all events is 1!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(textBox6.Text, out int trials) || trials <= 0)
            {
                MessageBox.Show("Please enter a valid number of trials!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int[] eventCounts = new int[probabilities.Length]; // Store the number of times each event occurs


            for (int i = 0; i < trials; i++)
            {
                double randomNumber = random.NextDouble();

                double cumulativeProbability = 0;
                for (int j = 0; j < probabilities.Length; j++)
                {
                    cumulativeProbability += probabilities[j];
                    if (randomNumber < cumulativeProbability)
                    {
                        eventCounts[j]++;
                        break;
                    }
                }
            }
            UpdateChart(eventCounts, trials);
        }

        private bool ValidateProbabilities()
        {
            double sum = probabilities.Sum();
            return sum >= 0.999 && sum <= 1.001;
        }

        private void UpdateChart(int[] eventCounts, int trials)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Event Frequency");
            // add data
            for (int i = 0; i < eventCounts.Length; i++)
            {
                double frequency = (double)eventCounts[i] / trials;
                chart1.Series["Event Frequency"].Points.AddXY($"Event {i + 1}", frequency);
            }
        }
    }
}
