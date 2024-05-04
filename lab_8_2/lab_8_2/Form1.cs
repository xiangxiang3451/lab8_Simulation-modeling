using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_8_2
{
    public partial class Form1 : Form
    {

        private List<string> answers;


        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            // Probability of events
            double[] probabilities = { 0.1, 0.2, 0.3, 0.4 };

            // Initialize the random number generator
            Random random = new Random();

            // Get event number
            int eventIndex = GetEventIndex(probabilities, random);

            switch (eventIndex)
            {
                case 0:
                    ans.Text = ("Да");
                    break;
                case 1:
                    ans.Text = ("Нет");
                    break;
                case 2:
                    ans.Text = ("Возможно");
                    break;
                case 3:
                    ans.Text = ("Не могу сейчас предсказать");
                    break;
                default:
                    ans.Text = ("Сосредоточьтесь и спросите снова");
                    break;
            }
        }
        private int GetEventIndex(double[] probabilities, Random random)
        {
            // Calculate the total probability,the sum of the probabilities of all events
            double totalProbability = 0;
            foreach (var probability in probabilities)
            {
                totalProbability += probability;
            }

            // Randomly generate a probability value
            double randomValue = random.NextDouble() * totalProbability;

            // Calculate event number
            int eventIndex = 0;
            double cumulativeProbability = 0;
            foreach (var probability in probabilities)
            {
                cumulativeProbability += probability;
                if (randomValue <= cumulativeProbability)
                {
                    break;
                }
                eventIndex++;
            }

            return eventIndex;
        }
    }
}
