using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Chisl3._2_System
{
    public partial class Form_main : Form
    {
        bool ploted = false;

        public Form_main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// отрисовка гафика и решение
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            tb_solution.Clear();
            if ((double)nud_eps.Value <= 0.0)
            {
                MessageBox.Show("Введите положительное eps", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ploted)
                Plot();
            tb_solution.Text = Solve((double)nud_eps.Value);
        }

        /// <summary>
        /// Первая функция
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        double phi1(double y)
        {
            return (Math.Cos(y) / 3 + 0.3);
        }

        /// <summary>
        /// Вторая функция
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        double phi2(double x)
        {
            return (Math.Sin(x - 0.6) - 1.6);
        }

        /// <summary>
        /// Первая функция, продифференцированная по X (для наглядности вычислений)
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        double DiffXPhi1(double y)
        {
            return 0;
        }
        /// <summary>
        /// Первая функция, продифференцированная по Y 
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        double DiffYPhi1(double y)
        {
            return -Math.Sin(y) / 3;
        }

        /// <summary>
        /// Первая функция, продифференцированная по Y 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        double DiffXPhi2(double x)
        {
            return Math.Cos(x-0.6);
        }
        /// <summary>
        /// Первая функция, продифференцированная по Y (для наглядности вычислений)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        double DiffYPhi2(double x)
        {
            return 0;
        }

        /// <summary>
        /// Построение графика
        /// </summary>
        void Plot()
        {
            double start = -10;
            double finish = 10;
            double step = 0.5;

            graph.ChartAreas[0].AxisX.ArrowStyle = AxisArrowStyle.Triangle;
            graph.ChartAreas[0].AxisX.Crossing = 0.0;
            graph.ChartAreas[0].AxisY.Crossing = 0.0;
            graph.ChartAreas[0].AxisX.IsMarksNextToAxis = false;
            graph.ChartAreas[0].AxisY.IsMarksNextToAxis = false;

            graph.ChartAreas[0].AxisX.LineWidth = 2;
            graph.ChartAreas[0].AxisY.LineWidth = 2;
            graph.ChartAreas[0].AxisX.Interval = 0.5;
            graph.ChartAreas[0].AxisY.Interval = 0.5;
            graph.ChartAreas[0].AxisX.Maximum = 3;
            graph.ChartAreas[0].AxisX.Minimum = -3;
            graph.ChartAreas[0].AxisY.Maximum = 3;
            graph.ChartAreas[0].AxisY.Minimum = -3;
            graph.Series[0].ChartType = SeriesChartType.Spline;

            for (double x = start; x < finish; x += step)
                graph.Series[0].Points.AddXY(x, phi2(x));

            graph.Series.Add("Graph2");
            graph.Series[1].ChartType = SeriesChartType.Spline;
            graph.Series[1].Color = Color.Red;

            for (double y = start; y < finish; y += step)
                graph.Series[1].Points.AddXY(phi1(y), y);
            ploted = true;
        }
        /// <summary>
        /// Оценка погрешности
        /// </summary>
        /// <param name="xCurr"></param>
        /// <param name="xPrev"></param>
        /// <param name="yCurr"></param>
        /// <param name="yPrev"></param>
        /// <returns></returns>
        double EvaluateError(double xCurr, double xPrev, double yCurr, double yPrev)
        {
            double q1 = Math.Abs(DiffXPhi1(yCurr)) + Math.Abs(DiffYPhi1(yCurr));
            double q2 = Math.Abs(DiffXPhi2(xCurr)) + Math.Abs(DiffYPhi2(xCurr));
            double M = Math.Min(q1, q2);

            return (M / (1 - M)) * (Math.Abs(xCurr - xPrev) + Math.Abs(yCurr - yPrev));
        }

        /// <summary>
        /// Рассчет корней
        /// </summary>
        /// <param name="eps"></param>
        /// <returns></returns>
        string Solve(double eps)
        {
            double xCurr = 0.15;
            double yCurr = -2;
            double xPrev = 0, yPrev = 0;
            double error = 0;
            int step = 0;

            do
            {
                ++step;
                xPrev = xCurr;
                yPrev = yCurr;
                xCurr = phi1(yCurr);
                yCurr = phi2(xCurr);
                error = EvaluateError(xCurr, xPrev, yCurr, yPrev);
            } while (Math.Abs(error) > eps);

            return ("Шагов проделано: " + step.ToString() + "\r\n" +
                "Полученное значение x: " + xCurr.ToString() + "\r\n" +
                "Полученное значение y: " + yCurr.ToString() + "\r\n");
            
        }
        
        private void Form_main_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void nud_eps_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
