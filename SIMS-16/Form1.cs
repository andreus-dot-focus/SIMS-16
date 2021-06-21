using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SM._931802.Komarov.Lab16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const double o = 0.1;
        const double u = 0.05;
        const double dt = 0.1;
        public double USD = 0;
        public double RUB = 1000;
        private double normalRV()
        {
            const double var = 1;
            const double mean = 0;
            Random rnd = new Random();
            double a1 = rnd.NextDouble();
            double a2 = rnd.NextDouble();
            double kor = Math.Sqrt(-2 * Math.Log(a1));
            double cos = Math.Cos(2 * Math.PI * a2);
            double x = (double)(kor * cos * Math.Sqrt(var) + mean);
            return x;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            double rate = (double)cur.Value;
            int d = (int)days.Value;
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(0, rate);

            for (int i = 1; i <= d; i++)
            {
                rate *= Math.Exp((u - o * o / 2) * dt + o * normalRV() * Math.Sqrt(dt));
                cur.Value = (decimal)rate;
                chart1.Series[0].Points.AddXY(i, rate);
            }
        }

        private void sell_Click(object sender, EventArgs e)
        {
            double amount = (double)number.Value;
            double rate = (double)cur.Value;
            double A = amount;
            if (A <= USD)
            {
                USD -= amount;
                RUB += amount * rate;
            }
            rubles.Text = "RUB: " + RUB;
            dollars.Text = "USD: " + USD;
        }

        private void buy_Click(object sender, EventArgs e)
        {
            double amount = (double)number.Value;
            double rate = (double)cur.Value;
            double A = rate * amount;
            if (A <= RUB)
            {
                RUB -= A;
                USD += amount;
            }
            rubles.Text = "RUB: " + RUB;
            dollars.Text = "USD: " + USD;
        }
    }
}
