using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearRegression;

namespace HeadTailBreaks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void HeadPercentage_TextChanged(object sender, EventArgs e)
        {
            if (HeadPercentage.Text != "")
                TailPercentage.Text = (100 - double.Parse(HeadPercentage.Text)).ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TailPercentage.Text = (100 - double.Parse(HeadPercentage.Text)).ToString();

            //BreakResult.Text = "111";
        }

        private void HeadPercentage_MouseLeave(object sender, EventArgs e)
        {
            if (double.Parse(HeadPercentage.Text) > 50 || HeadPercentage.Text == "")
                HeadPercentage.Text = "50";
        }
        private void HeadPercentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键
            //if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数
            //if (e.KeyChar > 0x20)
            //{
            //    try
            //    {
            //        double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
            //    }
            //    catch
            //    {
            //        e.KeyChar = (char)0;   //处理非法字符
            //    }
            //}
            try
            {
                if (!IsNumberic(e.KeyChar.ToString()) && (int)(e.KeyChar) != 8 && e.KeyChar != Convert.ToChar(Keys.Return))
                {
                    e.Handled = true;
                    return;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
        private bool IsNumberic(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"\d|\.");
            return reg1.IsMatch(str);
        }
        private void data_TextChanged(object sender, EventArgs e)
        {
            data.Text = data.Text.TrimEnd();
            if (data.Text != "" && data.Text != "or Paste data here...")
                InputFileAddress.Enabled = false;
            else
                InputFileAddress.Enabled = true;
            //data.Focus();
            if (data.Text != "")
            {
                data.SelectionStart = data.Text.Length - 1;
                data.ScrollToCaret();
            }
        }

        private void data_MouseEnter(object sender, EventArgs e)
        {

        }
        public int getHtindex(List<double> list, ref List<double> meanlist)
        {
            if (list.Count == 0)
                return 0;
            int htIndex = 0;
            double mean = getMean(list);
            double percentInhead = 0.2;
            int cntInhead = 0;
            int percentInTail = 0;
            htIndex = 0;
            while ((int)(percentInhead * 100) <= int.Parse(HeadPercentage.Text))
            {
                htIndex++;
                List<double> numlisttemp = new List<double>();
                mean = getMean(list);
                meanlist.Add(mean);
                foreach (double b in list)
                {
                    if (b >= mean)
                        numlisttemp.Add(b);
                }
                cntInhead = numlisttemp.Count;
                // int cntInTail = numberlist.Count - cntInhead;
                percentInhead = (double)cntInhead / (double)list.Count;
                percentInTail = 100 - (int)(percentInhead * 100);
                list = numlisttemp;
            }
            meanlist.RemoveAt(meanlist.Count - 1);
            return htIndex;

        }
        public double getMean(List<double> list)
        {
            double mean = 0;
            double sum = 0;
            foreach (double b in list)
                sum = sum + b;
            mean = sum / list.Count;
            return mean;

        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (data.Enabled)
            {
                double CRG = 0;
                double htSen = 0;
                int htIn = FromTextBox(ref CRG, ref htSen);
                htIndex.Text = htIn.ToString();
                CRGIndex.Text = Math.Round(CRG, 2).ToString();
                hts.Text = Math.Round(htSen,5).ToString();
                //FromAddress();
                //string copy = BreakResult.Text;
                Copy.Enabled = true;
               
            }
            //else 
            //{
            //    double CRG = 0;
            //    int htIn = FromAddress(ref CRG);
            //    htIndex.Text = htIn.ToString();
            //    CRGIndex.Text = Math.Round(CRG, 2).ToString();
            //    //FromAddress();
            //    //string copy = BreakResult.Text;
            //    string copy = Finaltext1;
            //    Clipboard.SetDataObject(copy);
            //    MessageBox.Show("Result copied in clipboard");


            //}
        }
        public int getHtindex(List<double> list, int headPercent)
        {
            if (list.Count == 0)
                return 0;
            int htIndex = 0;
            double mean = getMean(list);
            double percentInhead = 0.2;
            int cntInhead = 0;
            int percentInTail = 0;
            htIndex = 0;
            while ((int)(percentInhead * 100) <= headPercent)
            {
                htIndex++;
                List<double> numlisttemp = new List<double>();
                mean = getMean(list);

                foreach (double b in list)
                {
                    if (b >= mean)
                        numlisttemp.Add(b);
                }
                cntInhead = numlisttemp.Count;
                // int cntInTail = numberlist.Count - cntInhead;
                percentInhead = (double)cntInhead / (double)list.Count;
                percentInTail = 100 - (int)(percentInhead * 100);
                list = numlisttemp;
            }

            return htIndex;

        }
        public int getHtindex(List<double> list, ref List<double> meanlist, int headPercent)
        {
            if (list.Count == 0)
                return 0;
            int htIndex = 0;
            double mean = getMean(list);
            double percentInhead = 0.2;
            int cntInhead = 0;
            int percentInTail = 0;
            htIndex = 0;
            while ((int)(percentInhead * 100) <= headPercent)
            {
                htIndex++;
                List<double> numlisttemp = new List<double>();
                mean = getMean(list);
                meanlist.Add(mean);
                foreach (double b in list)
                {
                    if (b >= mean)
                        numlisttemp.Add(b);
                }
                cntInhead = numlisttemp.Count;
                // int cntInTail = numberlist.Count - cntInhead;
                percentInhead = (double)cntInhead / (double)list.Count;
                percentInTail = 100 - (int)(percentInhead * 100);
                list = numlisttemp;
            }
            meanlist.RemoveAt(meanlist.Count - 1);
            return htIndex;

        }
        public double[] List2Array(List<double> list)
        {
            double[] array = new double[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }

            return array;
        }
        public double getSensitiveHtindex(List<double> list)
        {
            double htSensitive = 0;
            int headrule = int.Parse(HeadPercentage.Text);
            List<double> meanlist = new List<double>();
            int ht = getHtindex(list, ref meanlist);

            int htBefore = 0; int htafter = 0;
            int indexBefore = 0;
            int indexAfter = 0;
            List<double> listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);

            }
            listtemp = listtemp.OrderByDescending(x => x).ToList();
            htBefore = ht;
            bool loop = false;
            while (!loop)
            {
                listtemp.RemoveAt(listtemp.Count - 1);
                htBefore = getHtindex(listtemp, ref meanlist);
                if (htBefore == ht - 1)
                    loop = true;
            }
            indexBefore = listtemp.Count;

            list = list.OrderByDescending(x => x).ToList();
            List<double> xlist = new List<double>();
            List<double> ylist = new List<double>();
            int cnt = 1;
            foreach (double y in list)
            {
                xlist.Add(cnt);
                ylist.Add(y);
                cnt++;
            }

            List<double> xLoglist = new List<double>();
            List<double> yLoglist = new List<double>();
            foreach (double y in ylist)
                yLoglist.Add(Math.Log10(y));

            foreach (double x in xlist)
                xLoglist.Add(Math.Log10(x));


            double[] xdata = List2Array(xLoglist);
            double[] ydata = List2Array(yLoglist);



            Tuple<double, double> p = Fit.Line(xdata, ydata);

            double a = p.Item1; // == 10; intercept
            double b = p.Item2;

            a = Math.Pow(10, a);
            htafter = ht;
            listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);

            }
            loop = false;
            while (!loop)
            {
                double value = a * (Math.Pow(cnt, b));
                //double value = 0.037037037;
                //if (listtemp.Count > 85)
                //    value = (double)1 / 81;

                listtemp.Add(value);
                htafter = getHtindex(listtemp, headrule);
                if (htafter == ht + 1)
                    loop = true;
                cnt++;
            }


            indexAfter = listtemp.Count;


            //double[] x1 = new[] { 1.0, 2.0, 3.0, 4, 5 }; double[] y1 = new[] { 4.0, 16.0, 64.0, 256, 1024 };
            chooseBestFit(list);
            //double[] re1 = Exponential(x1, y1);

            //int range = listtemp.Count - indexBefore;
            int range = indexAfter - indexBefore; double min = 0;
            if (indexBefore > 0)
                min = listtemp[indexBefore - 1];
            else
                min = listtemp[indexBefore];
            double max = listtemp[indexAfter - 1];
            int baseLog = 2;
            //double difference = Math.Abs(Math.Log(min, baseLog) - Math.Log(max, baseLog));
            double interval = 1 / (double)(range);

            List<double> linearlist = new List<double>();

            double linear = 0;
            for (int i = 0; i < range; i++)
            {
                linear = i * interval;
                linearlist.Add(linear);
            }

            List<double> decimallist = new List<double>();
            foreach (double x in linearlist)
            {
                decimallist.Add(Math.Pow(x, baseLog));
            }


            //int index = list.Count - indexBefore-1;
            int index = list.Count - indexBefore;
            //if (index == -1)
            //    index = 0;
            htSensitive = ht + decimallist[index];
            return htSensitive;

        }

        public double getSensitiveHtindexM(List<double> list, out bool isIntHt)
        {
            double htSensitive = 0;
            int headrule = int.Parse(HeadPercentage.Text);
            List<double> meanlist = new List<double>();
            int ht = getHtindex(list, ref meanlist);

            int htBefore = 0; int htafter = 0;
            int indexBefore = 0;
            int indexAfter = 0;
            List<double> listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);

            }
            listtemp = listtemp.OrderByDescending(x => x).ToList();
            htBefore = ht;
            bool loop = false;
            isIntHt = false;
            while (!loop)
            {
                listtemp.RemoveAt(listtemp.Count - 1);
                htBefore = getHtindex(listtemp, ref meanlist);
                if (htBefore <= ht - 1)
                {
                    if (list.Count - listtemp.Count == 1)
                        isIntHt = true;
                    loop = true;
                }
                if (htBefore > ht)
                {
                    ht = htBefore;
                    loop = true;
                }
            }
            if (isIntHt)
                return ht;
            indexBefore = listtemp.Count;

            list = list.OrderByDescending(x => x).ToList();
            List<double> xlist = new List<double>();
            List<double> ylist = new List<double>();
            int gCnt = 1;
            foreach (double y in list)
            {
                xlist.Add(gCnt);
                ylist.Add(y);
                gCnt++;
            }
            double a;
            double b;
            //powerLawFit(list, out a, out b);
            int mode = 0;
            double[] p = new double[2];
            chooseBestFit(list, out mode, out p);
            a = p[0]; b = p[1];

            htafter = ht;
            listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);
            }
            loop = false;
            while (!loop)
            {
                //double value = a * (Math.Pow(cnt, b));
                ////double value = 0.037037037;
                ////if (listtemp.Count > 85)
                ////    value = (double)1 / 81;

                //listtemp.Add(value);

                double tempValue = 0;
                if (mode == 0)
                    tempValue = a * Math.Exp(b * gCnt);
                else if (mode == 1)
                    tempValue = a * (Math.Pow(gCnt, b));
                else if (mode == 2)
                    tempValue = a + Math.Log(gCnt) * b;
                else if (mode == 3)
                    tempValue = getKochCurveLenByNum(gCnt);
                //double tempValue = listtemp[listtemp.Count-1]-interval;

                tempValue = 1;
                bool isLarge = false;
                while (tempValue > list[list.Count - 1]&&mode!=3)
                {
                    isLarge = true;
                    gCnt++;
                    if (mode == 0)
                        tempValue = a * Math.Exp(b * gCnt);
                    else if (mode == 1)
                        tempValue = a * (Math.Pow(gCnt, b));
                    else if (mode == 2)
                        tempValue = a + Math.Log(gCnt) * b;
                    //tempValue = a * (Math.Pow(gCnt, b));
                }
                if (!isLarge)
                    gCnt++;
                listtemp.Add(tempValue);


                htafter = getHtindex(listtemp, headrule);
                if (listtemp.Count - list.Count >= 10000)
                    htafter = ht + 1;
                if (htafter == ht + 1)
                    loop = true;

            }


            indexAfter = listtemp.Count;


            //double[] x1 = new[] { 1.0, 2.0, 3.0, 4, 5 }; double[] y1 = new[] { 4.0, 16.0, 64.0, 256, 1024 };
            chooseBestFit(list);
            //double[] re1 = Exponential(x1, y1);

            //int range = listtemp.Count - indexBefore;
            int range = indexAfter - indexBefore; double min = 0;
            if (indexBefore > 0)
                min = listtemp[indexBefore - 1];
            else
                min = listtemp[indexBefore];
            double max = listtemp[indexAfter - 1];
            int baseLog = 2;
            //double difference = Math.Abs(Math.Log(min, baseLog) - Math.Log(max, baseLog));
            double interval = 1 / (double)(range);

            List<double> linearlist = new List<double>();

            double linear = 0;
            for (int i = 0; i < range; i++)
            {
                linear = i * interval;
                linearlist.Add(linear);
            }

            List<double> decimallist = new List<double>();
            foreach (double x in linearlist)
            {

                decimallist.Add(Math.Pow(x, baseLog));
            }


            //int index = list.Count - indexBefore-1;
            int index = list.Count - indexBefore;
            //if (index == -1)
            //    index = 0;
            htSensitive = ht + decimallist[index];
            return htSensitive;

        }



        public double getSensitiveHtindexSameValue(List<double> list)
        {
            double htSensitive = 0;
            int headrule = int.Parse(HeadPercentage.Text);
            List<double> meanlist = new List<double>();
            int ht = getHtindex(list, ref meanlist);

            int htBefore = 0; int htafter = 0;
            int indexBefore = 0;
            int indexAfter = 0;
            List<double> listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);

            }
            listtemp = listtemp.OrderByDescending(x => x).ToList();
            htBefore = ht;
            bool loop = false;
            while (!loop)
            {
                listtemp.RemoveAt(listtemp.Count - 1);
                htBefore = getHtindex(listtemp, ref meanlist);
                if (htBefore == ht - 1)
                    loop = true;
            }
            indexBefore = listtemp.Count;

            list = list.OrderByDescending(x => x).ToList();

            htafter = ht;
            listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);

            }
            loop = false;
            while (!loop)
            {

                listtemp.Add(list[list.Count - 1]);
                htafter = getHtindex(listtemp, headrule);
                if (htafter == ht + 1)
                    loop = true;

            }


            indexAfter = listtemp.Count;


            //double[] x1 = new[] { 1.0, 2.0, 3.0, 4, 5 }; double[] y1 = new[] { 4.0, 16.0, 64.0, 256, 1024 };

            //double[] re1 = Exponential(x1, y1);

            //int range = listtemp.Count - indexBefore;
            int range = indexAfter - indexBefore; double min = 0;
            if (indexBefore > 0)
                min = listtemp[indexBefore - 1];
            else
                min = listtemp[indexBefore];
            double max = listtemp[indexAfter - 1];
            int baseLog = 2;
            //double difference = Math.Abs(Math.Log(min, baseLog) - Math.Log(max, baseLog));
            double interval = 1 / (double)(range);

            List<double> linearlist = new List<double>();

            double linear = 0;
            for (int i = 0; i < range; i++)
            {
                linear = i * interval;
                linearlist.Add(linear);
            }

            List<double> decimallist = new List<double>();
            foreach (double x in linearlist)
            {

                decimallist.Add(Math.Pow(x, baseLog));
            }


            //int index = list.Count - indexBefore-1;
            int index = list.Count - indexBefore;
            //if (index == -1)
            //    index = 0;
            htSensitive = ht + decimallist[index];
            return htSensitive;

        }
        public double getSensitiveHtindexN(List<double> list)
        {
            double htSensitive = 0;
            int headrule = int.Parse(HeadPercentage.Text);
            List<double> meanlist = new List<double>();
            int ht = getHtindex(list, ref meanlist);

            int htBefore = 0; int htafter = 0;
            int indexBefore = 0;
            int indexAfter = 0;
            List<double> listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);

            }
            listtemp = listtemp.OrderByDescending(x => x).ToList();
            htBefore = ht;
            bool loop = false;
            while (!loop)
            {
                listtemp.RemoveAt(listtemp.Count - 1);
                htBefore = getHtindex(listtemp, ref meanlist);
                if (htBefore == ht - 1)
                    loop = true;
            }
            indexBefore = listtemp.Count;

            list = list.OrderByDescending(x => x).ToList();
            int cnt = 0;
            htafter = ht;
            listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);

            }
            loop = false;

            List<double> listSample = list.Where(x => x < 1).ToList();
            listSample = listSample.OrderByDescending(x => x).ToList();
            double range2 = listSample[0] - listSample[listSample.Count - 1];
            double interval2 = range2 / listSample.Count;


            while (!loop)
            {
                //double value = a * (Math.Pow(cnt, b));
                double value = listtemp[listtemp.Count - 1] - interval2;
                //double value = 0.037037037;
                //if (listtemp.Count > 85)
                //    value = (double)1 / 81;

                listtemp.Add(value);
                htafter = getHtindex(listtemp, headrule);
                if (htafter == ht + 1)
                    loop = true;
                cnt++;
            }


            indexAfter = listtemp.Count;


            //double[] x1 = new[] { 1.0, 2.0, 3.0, 4, 5 }; double[] y1 = new[] { 4.0, 16.0, 64.0, 256, 1024 };

            //double[] re1 = Exponential(x1, y1);

            //int range = listtemp.Count - indexBefore;
            int range = indexAfter - indexBefore; double min = 0;
            if (indexBefore > 0)
                min = listtemp[indexBefore - 1];
            else
                min = listtemp[indexBefore];
            double max = listtemp[indexAfter - 1];
            int baseLog = 2;
            //double difference = Math.Abs(Math.Log(min, baseLog) - Math.Log(max, baseLog));
            double interval = 1 / (double)(range);

            List<double> linearlist = new List<double>();

            double linear = 0;
            for (int i = 0; i < range; i++)
            {
                linear = i * interval;
                linearlist.Add(linear);
            }

            List<double> decimallist = new List<double>();
            foreach (double x in linearlist)
            {

                decimallist.Add(Math.Pow(x, baseLog));
            }


            //int index = list.Count - indexBefore-1;
            int index = list.Count - indexBefore;
            //if (index == -1)
            //    index = 0;
            htSensitive = ht + decimallist[index];
            return htSensitive;

        }
        public double[] chooseBestFit(List<double> numberlist)
        {
            double[] y = List2Array(numberlist);
            List<double> xlist = new List<double>();
            for (int i = 1; i <= y.Length; i++)
            {
                xlist.Add(i);
            }
            double[] x = List2Array(xlist);

            //exponential
            double[] p1 = Exponential(x, y);
            double[] yh = Generate.Map(x, k => p1[0] * Math.Exp(p1[1] * k));
            double R1 = GoodnessOfFit.RSquared(y, yh);

            //power law
            double[] p2 = powerLawFit(y);
            yh = Generate.Map(x, k => p2[0] * Math.Pow(k, p2[1]));
            double R2 = GoodnessOfFit.RSquared(y, yh);

            //logarithm
            double[] p3 = Logarithmic(x, y);
            yh = Generate.Map(x, k => p3[0] + Math.Log(k) * p3[1]);
            double R3 = GoodnessOfFit.RSquared(y, yh);

            List<double> rlist = new List<double> { R1, R2, R3 };
            List<double[]> paralist = new List<double[]> { p1, p2, p3 };
            int maxIndex = rlist.IndexOf(rlist.Max());

            return paralist[maxIndex];

        }

        public void chooseBestFit(List<double> numberlist, out int mode, out double[] p)
        {
            double size=getKochCurveLenByNum(numberlist.Count-1, numberlist[0]);
            if (Math.Abs(size - numberlist[numberlist.Count - 1]) < 0.01) 
            {
                
                if (numberlist.Count > 2) 
                {
                   var numberTemp= numberlist.FindAll(a => a == numberlist[1]);
                   if (numberTemp.Count > 1) 
                   {
                       mode = 3;
                       p = new double[2];
                       return;
                   
                   }
                }
               
            
            }

            double[] y = List2Array(numberlist);
            List<double> xlist = new List<double>();
            for (int i = 1; i <= y.Length; i++)
            {
                xlist.Add(i);
            }
            double[] x = List2Array(xlist);

            //exponential
            double[] p1 = Exponential(x, y);
            double[] yh = Generate.Map(x, k => p1[0] * Math.Exp(p1[1] * k));
            double R1 = GoodnessOfFit.RSquared(y, yh);

            //power law
            double[] p2 = powerLawFit(y);
            yh = Generate.Map(x, k => p2[0] * Math.Pow(k, p2[1]));
            double R2 = GoodnessOfFit.RSquared(y, yh);

            //logarithm
            double[] p3 = Logarithmic(x, y);
            yh = Generate.Map(x, k => p3[0] + Math.Log(k) * p3[1]);
            double R3 = GoodnessOfFit.RSquared(y, yh);

            List<double> rlist = new List<double> { R1, R2, R3 };
            List<double[]> paralist = new List<double[]> { p1, p2, p3 };
            int maxIndex = rlist.IndexOf(rlist.Max());

            mode = maxIndex;
            p = paralist[maxIndex];


        }

        public double getSensitiveHtindexCity(int Num)
        {
            List<double> list = new List<double>();

            //Num = 10000;
            for (int i = 1; i <= Num; i++)
                list.Add((double)1 / i);

            //string SaveAddress = @"C:\PhD work\Fractal theory\HtIndex\HTs\temp.txt";
            //System.IO.FileStream fs = new System.IO.FileStream(SaveAddress, FileMode.Create);

            //StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            //foreach (double x in list)
            //{
            //    //string text = linecnt + "\t" + mean;
            //    string text = x.ToString();
            //    sw.WriteLine(text);

            //}
            //sw.Close();
            //fs.Close();

            //List<double> meanlist = new List<double>();
            //getHtindex(list, ref meanlist);
            //double CRG = getCRG(meanlist);


            //return 0;
            double htSensitive = 0;
            int headrule = int.Parse(HeadPercentage.Text);
            List<double> meanlist = new List<double>();
            int ht = getHtindex(list, ref meanlist);

            int htBefore = 0; int htafter = 0;
            int indexBefore = 0;
            int indexAfter = 0;
            List<double> listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);

            }
            listtemp = listtemp.OrderByDescending(x => x).ToList();
            htBefore = ht;
            bool loop = false;
            while (!loop)
            {
                listtemp.RemoveAt(listtemp.Count - 1);
                htBefore = getHtindex(listtemp, headrule);
                //if (htBefore == ht - 1)
                if (htBefore != ht)
                    loop = true;
            }
            indexBefore = listtemp.Count;

            list = list.OrderByDescending(x => x).ToList();
            List<double> xlist = new List<double>();
            List<double> ylist = new List<double>();
            int cnt = 1;
            foreach (double y in list)
            {
                xlist.Add(cnt);
                ylist.Add(y);
                cnt++;
            }

            List<double> xLoglist = new List<double>();
            List<double> yLoglist = new List<double>();
            foreach (double y in ylist)
                yLoglist.Add(Math.Log10(y));

            foreach (double x in xlist)
                xLoglist.Add(Math.Log10(x));


            double[] xdata = List2Array(xLoglist);
            double[] ydata = List2Array(yLoglist);



            Tuple<double, double> p = Fit.Line(xdata, ydata);

            double a = p.Item1; // == 10; intercept
            double b = p.Item2;

            a = Math.Pow(10, a);
            htafter = ht;
            listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);

            }
            loop = false;
            while (!loop)
            {
                double value = a * (Math.Pow(cnt, b));
                //double value = 0.037037037;
                //if (listtemp.Count > 85)
                //    value = (double)1 / 81;

                listtemp.Add(value);
                htafter = getHtindex(listtemp, headrule);
                if (htafter == ht + 1)
                    loop = true;
                cnt++;
            }


            indexAfter = listtemp.Count;


            //double[] x1 = new[] { 1.0, 2.0, 3.0, 4, 5 }; double[] y1 = new[] { 4.0, 16.0, 64.0, 256, 1024 };

            //double[] re1 = Exponential(x1, y1);

            //int range = listtemp.Count - indexBefore;
            int range = indexAfter - indexBefore; double min = 0;
            if (indexBefore > 0)
                min = listtemp[indexBefore - 1];
            else
                min = listtemp[indexBefore];
            double max = listtemp[indexAfter - 1];
            int baseLog = 2;
            //double difference = Math.Abs(Math.Log(min, baseLog) - Math.Log(max, baseLog));
            double interval = 1 / (double)(range);

            List<double> linearlist = new List<double>();

            double linear = 0;
            for (int i = 0; i < range; i++)
            {
                linear = i * interval;
                linearlist.Add(linear);
            }

            List<double> decimallist = new List<double>();
            foreach (double x in linearlist)
            {

                decimallist.Add(Math.Pow(x, baseLog));
            }

            int index = list.Count - indexBefore;
            //if (index == -1)
            //    index = 0;
            htSensitive = ht + decimallist[index];
            return htSensitive;

        }

        public double getSensitiveHtindexKoch(int Num)
        {
            List<double> list = new List<double>();

            Num = 5461;

            double sum = 0;
            int iteration = 0;
            double baseLen = (double)1 / 3;
            while (1 == 1)
            {
                double itNum = Math.Pow(4, iteration);
                sum += itNum;
                if (sum <= Num)
                {
                    double size = Math.Pow(baseLen, iteration);
                    for (int i = 0; i < itNum; i++)
                    {
                        list.Add(size);
                    }
                }
                else
                {
                    double size = Math.Pow(baseLen, iteration);
                    int numTemp = Num - Convert.ToInt32((sum - itNum));
                    for (int i = 0; i < numTemp; i++)
                    {
                        list.Add(size);
                    }
                    break;

                }
                iteration++;
            }

            //string SaveAddress = @"C:\PhD work\Fractal theory\HtIndex\HTs\temp.txt";
            //System.IO.FileStream fs = new System.IO.FileStream(SaveAddress, FileMode.Create);

            //StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            //foreach (double x in list)
            //{
            //    //string text = linecnt + "\t" + mean;
            //    string text = x.ToString();
            //    sw.WriteLine(text);

            //}
            //sw.Close();
            //fs.Close();

            //List<double> meanlist = new List<double>();
            //getHtindex(list, ref meanlist);
            //double CRG = getCRG(meanlist);


            //return 0;



            double htSensitive = 0;
            int headrule = int.Parse(HeadPercentage.Text);
            List<double> meanlist = new List<double>();
            int ht = getHtindex(list, ref meanlist);

            int htBefore = 0; int htafter = 0;
            int indexBefore = 0;
            int indexAfter = 0;
            List<double> listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);

            }
            listtemp = listtemp.OrderByDescending(x => x).ToList();
            htBefore = ht;
            bool loop = false;
            while (!loop)
            {
                listtemp.RemoveAt(listtemp.Count - 1);
                htBefore = getHtindex(listtemp, headrule);
                if (htBefore != ht)
                    loop = true;
            }
            indexBefore = listtemp.Count;   // this is correct, don't change

            list = list.OrderByDescending(x => x).ToList();
            listtemp = new List<double>();
            foreach (double x in list)
            {
                listtemp.Add(x);

            }
            loop = false;
            while (!loop)
            {
                double value = getKochCurveLenByNum(listtemp.Count);
                //double value = 0.037037037;
                //if (listtemp.Count > 85)
                //    value = (double)1 / 81;

                listtemp.Add(value);
                htafter = getHtindex(listtemp, headrule);
                if (htafter == ht + 1)
                    loop = true;

            }


            indexAfter = listtemp.Count;


            //double[] x1 = new[] { 1.0, 2.0, 3.0, 4, 5 }; double[] y1 = new[] { 4.0, 16.0, 64.0, 256, 1024 };

            //double[] re1 = Exponential(x1, y1);

            //int range = listtemp.Count - indexBefore;
            int range = indexAfter - indexBefore; double min = 0;
            if (indexBefore > 0)
                min = listtemp[indexBefore - 1];
            else
                min = listtemp[indexBefore];
            double max = listtemp[indexAfter - 1];
            int baseLog = 2;
            //double difference = Math.Abs(Math.Log(min, baseLog) - Math.Log(max, baseLog));
            double interval = 1 / (double)(range);

            List<double> linearlist = new List<double>();

            double linear = 0;
            for (int i = 0; i < range; i++)
            {
                linear = i * interval;
                linearlist.Add(linear);
            }

            List<double> decimallist = new List<double>();
            foreach (double x in linearlist)
            {

                decimallist.Add(Math.Pow(x, baseLog));
            }

            int index = list.Count - indexBefore;
            //if (index == -1)
            //    index = 0;
            htSensitive = ht + decimallist[index];
            return htSensitive;

        }

        double getKochCurveLenByNum(int Num)
        {
            double size = 0;
            double sum = 0;
            int iteration = 0;
            double baseLen = (double)1 / 3;
            while (1 == 1)
            {
                double itNum = Math.Pow(4, iteration);
                sum += itNum;
                size = Math.Pow(baseLen, iteration);
                if (sum > Num)
                {
                    break;
                }
                iteration++;
            }
            return size;
        }

        double getKochCurveLenByNum(int Num, double startingLen)
        {
            double size = 0;
            double sum = 0;
            int iteration = 0;
            double baseLen = (double)1 / 3;
            while (1 == 1)
            {
                double itNum = Math.Pow(4, iteration);
                sum += itNum;
                size = Math.Pow(baseLen, iteration)*startingLen;
                if (sum > Num)
                {
                    break;
                }
                iteration++;
            }
            return size;
        }



        double[] Exponential(double[] x, double[] y,
    DirectRegressionMethod method = DirectRegressionMethod.QR)
        {

            double[] y_hat = Generate.Map(y, Math.Log);
            double[] p_hat = Fit.LinearCombination(x, y_hat, method, t => 1.0, t => t);

            return new[] { Math.Exp(p_hat[0]), p_hat[1] };

        }

        double[] Logarithmic(double[] x1, double[] y,
    DirectRegressionMethod method = DirectRegressionMethod.QR)
        {
            double[] p = Fit.LinearCombination(
             x1,
             y,
             x => 1.0,
             x => Math.Log(x));

            return p;

        }

        public void getSensitiveHtindex2(List<double> list, ref List<double> indexlist)
        {
            List<double> indexlistTemp = new List<double>();
            double htSensitive = 0;
            int headrule = int.Parse(HeadPercentage.Text);
            List<double> meanlist = new List<double>();
            int ht = getHtindex(list, ref meanlist);
            double ht2 = getSensitiveHtindex(list);
            indexlist.Add(ht2);
            int htBefore = 0; int htafter = 0;
            int indexBefore = 0;
            int indexAfter = 0;
            list = list.OrderByDescending(x => x).ToList();
            //list = list.OrderBy(x => x).ToList();
            List<double> listtemp = new List<double>();
            int gCnt = 0;
            int htStart = 1;
            int htValue = ht;
            int gCntTemp = 0;
            double a;
            double b;
            powerLawFit(list, out a, out b);
            while (htStart <= ht)
            {
                if (gCnt < list.Count)
                    listtemp.Add(list[gCnt]);
                else
                {


                    double tempValue = a * (Math.Pow(gCnt, b));
                    while (tempValue > list[list.Count - 1])
                    {
                        powerLawFit(list, out a, out b);
                        gCnt++;
                        tempValue = a * (Math.Pow(gCnt, b));
                    }
                    listtemp.Add(tempValue);

                }

                int htTemp = getHtindex(listtemp, headrule);
                if (htTemp == htStart + 1)
                {
                    List<double> decimallist = new List<double>();
                    if (gCnt < list.Count)
                        decimallist = getDecimalList(listtemp, gCntTemp, gCnt);
                    else
                        decimallist = getDecimalList(listtemp, gCntTemp, listtemp.Count - 1);

                    decimallist = decimallist.OrderByDescending(x => x).ToList();
                    int count = 0;
                    foreach (double d in decimallist)
                    {
                        //if(htStart==1||count!=0)
                        if (htStart == 1)
                        {
                            if (count > 1)
                                indexlistTemp.Add(htValue + d);
                            //indexlistTemp.Add(htStart + d);
                        }
                        else
                        {
                            if (count > 0)
                                indexlistTemp.Add(htValue + d);
                            //indexlistTemp.Add(htStart + d);

                        }
                        count++;
                    }
                    htValue--;
                    if (gCnt < list.Count)
                        gCntTemp = gCnt;
                    else
                        gCntTemp = listtemp.Count - 1;
                    htStart = htTemp;
                }


                gCnt++;
            }
            string SaveAddress = @"C:\PhD work\Fractal theory\HtIndex\HTs\temp.txt";
            System.IO.FileStream fs = new System.IO.FileStream(SaveAddress, FileMode.Create);

            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            foreach (double x in listtemp)
            {
                //string text = linecnt + "\t" + mean;
                string text = x.ToString();
                sw.WriteLine(text);

            }
            sw.Close();
            fs.Close();
            //indexlistTemp = indexlistTemp.OrderByDescending(x => x).ToList();
            if (indexlistTemp.Count > list.Count)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    indexlist.Add(indexlistTemp[i]);


                }
            }
            else
            {
                for (int i = 0; i < indexlistTemp.Count; i++)
                {
                    indexlist.Add(indexlistTemp[i]);


                }

            }




        }




        public void    getSensitiveHtindex3(List<double> list, ref List<double> indexlist, ref List<double> indexWholelist)
        {
            List<double> indexlistTemp = new List<double>();
            double htSensitive = 0;
            int headrule = int.Parse(HeadPercentage.Text);
            List<double> meanlist = new List<double>();
            int ht = getHtindex(list, ref meanlist);
            bool isIntHt = false;
            double ht2 = getSensitiveHtindexM(list, out isIntHt);
           
            //ht = Convert.ToInt32(ht2-0.5);
            ht = (int)Math.Floor(ht2);
            if (isIntHt)
                ht2 = ht;
            
            indexlist.Add(ht2);
            int htBefore = 0; int htafter = 0;
            int indexBefore = 0;
            int indexAfter = 0;
            list = list.OrderByDescending(x => x).ToList();
            //list = list.OrderBy(x => x).ToList();
            //List<double> listSample = list.Where(x => x < 1).ToList();
            //listSample = listSample.OrderByDescending(x => x).ToList();
            //double range = listSample[0] - listSample[listSample.Count - 1];
            //double interval = range / listSample.Count;
            List<double> listtemp = new List<double>();

            int gCnt = 0;
            int htStart = 1;
            int htValue = ht;
            int gCntTemp = 0;
            double a;
            double b;
            //powerLawFit(list, out a, out b);
            int mode = 0;
            double[] p = new double[2];
            chooseBestFit(list, out mode, out p);
            a = p[0]; b = p[1];

            //if(1==1)
            if (!isIntHt)
            {
                //assign hts to each value
                while (htStart <= ht)
                {
                    if (gCnt < list.Count)
                        listtemp.Add(list[gCnt]);
                    else
                    {


                        double tempValue = 0;
                        if (mode == 0)
                            tempValue = a * Math.Exp(b * gCnt);
                        else if (mode == 1)
                            tempValue = a * (Math.Pow(gCnt, b));
                        else if (mode == 2)
                            tempValue = a + Math.Log(gCnt) * b;
                        else if (mode == 3)
                            tempValue = getKochCurveLenByNum(gCnt);
                        //double tempValue = listtemp[listtemp.Count-1]-interval;

                        tempValue = 1;

                        bool isLarge = false;
                        while (tempValue > list[list.Count - 1] && mode != 3)
                        {
                            isLarge = true;
                            gCnt++;
                            if (mode == 0)
                                tempValue = a * Math.Exp(b * gCnt);
                            else if (mode == 1)
                                tempValue = a * (Math.Pow(gCnt, b));
                            else if (mode == 2)
                                tempValue = a + Math.Log(gCnt) * b;
                            //tempValue = a * (Math.Pow(gCnt, b));
                        }
                        if (!isLarge)
                            gCnt++;
                        listtemp.Add(tempValue);

                    }

                    int htTemp = getHtindex(listtemp, headrule);
                    if (listtemp.Count - list.Count >= 10000)
                        htTemp = htStart + 1;
                    if (htTemp == htStart + 1)
                    {
                        List<double> decimallist = new List<double>();
                        if (gCnt < list.Count)
                        {
                            if (htValue == ht)
                            {
                                decimallist = getDecimalList(listtemp, gCntTemp, gCnt, ht, ht2);
                            }
                            else
                                decimallist = getDecimalList(listtemp, gCntTemp, gCnt);
                        }
                        else
                            decimallist = getDecimalList(listtemp, gCntTemp, listtemp.Count - 1);

                        decimallist = decimallist.OrderByDescending(x => x).ToList();
                        int count = 0;
                        foreach (double d in decimallist)
                        {
                            //if(htStart==1||count!=0)
                            if (htStart == 1)
                            {
                                //if (count > 0)
                                    indexlistTemp.Add(htValue + d);
                                //indexlistTemp.Add(htStart + d);
                            }
                            else
                            {
                                //if (count > 0)
                                    indexlistTemp.Add(htValue + d);
                                //indexlistTemp.Add(htStart + d);

                            }
                            count++;
                        }
                        htValue--;
                        if (gCnt < list.Count)
                            gCntTemp = gCnt+1;
                        else
                            gCntTemp = listtemp.Count - 1;
                        htStart = htTemp;
                    }


                    gCnt++;
                }
            }
            else 
            {
                htValue = ht - 1;
                //List<double> decimallist = new List<double>();
                while (gCnt < list.Count)
                //while (htStart <= ht)
                {
                   
                    //if (gCnt < list.Count)
                        listtemp.Add(list[gCnt]);

                    int htTemp = getHtindex(listtemp, headrule);
                    if (htTemp == htStart + 1)
                    {
                        List<double> decimallist = new List<double>();
                       
                        if (htValue == ht-1)
                        {
                            decimallist = getDecimalList(listtemp, gCntTemp, gCnt, ht2-1, ht2);
                        }
                        else
                            decimallist = getDecimalList(listtemp, gCntTemp, gCnt);
                        
                         
                        decimallist = decimallist.OrderByDescending(x => x).ToList();
                        int count = 0;
                        foreach (double d in decimallist)
                        {
                            //if(htStart==1||count!=0)
                            if (htStart == 1)
                            {
                                //if (count > 0)
                                    indexlistTemp.Add(htValue + d);
                                //indexlistTemp.Add(htStart + d);
                            }
                            else
                            {
                                //if (count > 2)
                                    indexlistTemp.Add(htValue + d);
                                //indexlistTemp.Add(htStart + d);

                            }
                            count++;
                        }
                        htValue--;
                        gCntTemp = gCnt+1;
                        htStart = htTemp;
                    }


                    gCnt++;
                

                }
                //while (ht2 > 1)
                //{
                //    htStart =Convert.ToInt32(ht2 - 1);
                //    decimallist = getDecimalList(list, 0, list.Count - 2, htStart, ht2);



                //    decimallist = decimallist.OrderByDescending(x => x).ToList();
                //    int count = 0;
                //    foreach (double d in decimallist)
                //    {
                //        //if(htStart==1||count!=0)


                //        indexlistTemp.Add(htStart + d);
                //        //indexlistTemp.Add(htStart + d);


                //        //count++;
                //    }
                //    ht2--;
                //}
            }
            //string SaveAddress = @"C:\PhD work\Fractal theory\HtIndex\HTs\temp.txt";
            //System.IO.FileStream fs = new System.IO.FileStream(SaveAddress, FileMode.Create);

            //StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            //foreach (double x in listtemp)
            //{
            //    //string text = linecnt + "\t" + mean;
            //    string text = x.ToString();
            //    sw.WriteLine(text);

            //}
            //sw.Close();
            //fs.Close();
            //indexlistTemp = indexlistTemp.OrderByDescending(x => x).ToList();
            if (indexlistTemp.Count > list.Count)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    indexlist.Add(indexlistTemp[i]);


                }
            }
            else
            {
                for (int i = 0; i < indexlistTemp.Count; i++)
                {
                    indexlist.Add(indexlistTemp[i]);


                }

            }

            indexWholelist = indexlistTemp;


        }




        private void powerLawFit(List<double> list, out double a, out double b)
        {
            List<double> xlist = new List<double>();
            List<double> ylist = new List<double>();
            int cnt = 1;
            foreach (double y in list)
            {
                xlist.Add(cnt);
                ylist.Add(y);
                cnt++;
            }

            List<double> xLoglist = new List<double>();
            List<double> yLoglist = new List<double>();
            foreach (double y in ylist)
                yLoglist.Add(Math.Log10(y));

            foreach (double x in xlist)
                xLoglist.Add(Math.Log10(x));


            double[] xdata = List2Array(xLoglist);
            double[] ydata = List2Array(yLoglist);



            Tuple<double, double> p = Fit.Line(xdata, ydata);

            a = p.Item1; // == 10; intercept
            b = p.Item2;

            a = Math.Pow(10, a);
        }
        private double[] powerLawFit(double[] list)
        {
            List<double> xlist = new List<double>();
            List<double> ylist = new List<double>();
            int cnt = 1;
            foreach (double y in list)
            {
                xlist.Add(cnt);
                ylist.Add(y);
                cnt++;
            }

            List<double> xLoglist = new List<double>();
            List<double> yLoglist = new List<double>();
            foreach (double y in ylist)
                yLoglist.Add(Math.Log10(y));

            foreach (double x in xlist)
                xLoglist.Add(Math.Log10(x));


            double[] xdata = List2Array(xLoglist);
            double[] ydata = List2Array(yLoglist);



            Tuple<double, double> p = Fit.Line(xdata, ydata);

            double a = p.Item1; // == 10; intercept
            double b = p.Item2;

            a = Math.Pow(10, a);

            double[] p1 = new double[] { a, b };
            return p1;
        }

        public List<double> getDecimalList(List<double> listtemp, int indexBefore, int indexAfter)
        {
            listtemp = listtemp.OrderBy(x => x).ToList();
            int range = indexAfter - indexBefore + 1; double min = 0;
            if (indexBefore > 0)
                min = listtemp[indexBefore - 1];
            else
                min = listtemp[indexBefore];
            double max = listtemp[indexAfter];
            int baseLog = 2;
            //double difference = Math.Abs(Math.Log(min, baseLog) - Math.Log(max, baseLog));
            double interval = 1 / (double)(range);



            List<double> linearlist = new List<double>();

            double linear = 0;
            for (int i = 0; i < range; i++)
            {
                linear = i * interval;
                linearlist.Add(linear);
            }

            List<double> decimallist = new List<double>();
            foreach (double x in linearlist)
            {

                decimallist.Add(Math.Pow(x, baseLog));
            }

            return decimallist;

        }
        public List<double> getDecimalList(List<double> listtemp, int indexBefore, int indexAfter, double minHt, double maxHt)
        {
            listtemp = listtemp.OrderBy(x => x).ToList();
            int range = indexAfter - indexBefore; double min = 0;
            if (indexBefore > 0)
                min = listtemp[indexBefore - 1];
            else
                min = listtemp[indexBefore];
            double max = listtemp[indexAfter];
            int baseLog = 2;
            //double difference = Math.Abs(Math.Log(min, baseLog) - Math.Log(max, baseLog));
            double interval = (maxHt - minHt) / (double)(range);



            List<double> linearlist = new List<double>();

            double linear = 0;
            for (int i = 0; i < range; i++)
            {
                linear = i * interval;
                linearlist.Add(linear);
            }

            List<double> decimallist = new List<double>();
            foreach (double x in linearlist)
            {

                decimallist.Add(Math.Pow(x, baseLog));
            }

            return decimallist;

        }
        public double getNormalizedValue(double value, List<double> valuelist)
        {
            double normal = 0;

            valuelist = valuelist.OrderBy(x => x).ToList();

            double min = valuelist[0];
            double max = valuelist[valuelist.Count - 1];
            normal = (value - min) / (max - min);

            return normal;


        }

        public double getCRG(List<double> meanlist)
        {
            double CRG = 0;
            int count = meanlist.Count;
            if (count == 1)
                CRG = meanlist[0];
            else if (count > 1)
            {
                for (int i = 1; i < meanlist.Count; i++)
                {
                    double temp = (double)meanlist[i] / meanlist[i - 1];
                    CRG += temp;
                }

            }
            return CRG;
        }

        string text1 = "";
        private int FromTextBox(ref double CRG, ref double htSen)
        {

            List<double> numberlist = new List<double>();
            double number = 0;
            int linecnt = 0;
            string rline = data.Text;

            string[] rlinesplit = rline.Split('\n');
            
            for (int i = 0; i < rlinesplit.Length; i++)
            {
                //if (rlinesplit[i])
                if (rlinesplit[i].Length != 0)
                {
                    double n = 0;
                    if(rlinesplit[i].Contains("/r"))
                         rlinesplit[i] = rlinesplit[i].Remove(rlinesplit[i].Length - 1);
                    bool isNumeric = double.TryParse(rlinesplit[i], out n);
                    if (isNumeric)
                    {
                        number = double.Parse(rlinesplit[i]);
                        numberlist.Add(number);
                    }
                }
            }
            numberlist = numberlist.OrderByDescending(x => x).ToList();
            List<double> meanlist = new List<double>();
            int htIndex = getHtindex(numberlist, ref meanlist);
            //CRG = getCRG(meanlist);
            //htSen= getSensitiveHtindex(numberlist);
            //htSen = getSensitiveHtindexCity(100000);
            //htSen = getSensitiveHtindexKoch(5461);
            List<double> indexlist = new List<double>();
            List<double> indexWholelist = new List<double>();

            //getSensitiveHtindex2(numberlist, ref indexlist);
            //double ht1 = getSensitiveHtindex(numberlist);
            //double ht3 = getSensitiveHtindexN(numberlist);
            getSensitiveHtindex3(numberlist, ref indexlist, ref indexWholelist);
            //double ht4 = getSensitiveHtindexSameValue(numberlist);


            indexlist = indexlist.OrderByDescending(x => x).ToList();
            htSen = indexlist[0];
            //iterative mean calculation    
            List<double> paralist = numberlist;
            List<int> htDlist = new List<int>();
            double percentInhead = 0.2;
            int cntInhead = 0;
            int percentInTail = 0;
            int cntInTail = 0;
            double mean = 0;
            paralist = paralist.OrderByDescending(x => x).ToList();
            foreach (double x in paralist)
            {

                int value = 1;
                foreach (double m in meanlist)
                {
                    if (x >= m)
                    {
                        value++;
                        continue;
                    }
                    else
                        break;
                }
                htDlist.Add(value);
            }

            //string SaveAddress = @"C:\PhD work\OSM\historyWorldOSM\coedit network\option2\1111\HierarchyRule1.txt";
            //System.IO.FileStream fs = new System.IO.FileStream(SaveAddress, FileMode.Append);
            //StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            string text = "";
            text1 = "";
            string Finaltext = "";
            Finaltext1 = "";
            //text = "#Data" + "\t" + "#head" + "\t" + "%head" + "\t" + "#tail" + "\t" + "%tail" + "\t" + "mean";
            text = "Ht-index" + "\t" + "FHt-index";
            Finaltext = text;
            Finaltext1 = "Ht-index" + "\t" + "FHt-index";
            //sw.WriteLine(text);

            for (int i = 0; i < numberlist.Count; i++)
            {
                //text = Math.Round(numberlist[i], 5) + "\t\t" + Math.Round(indexlist[i], 5);
                //text1 = numberlist[i] + "\t" + indexlist[i];
                text = htDlist[i] + "\t\t" + Math.Round(indexlist[i], 5);
                text1 = htDlist[i] + "\t" + indexlist[i];

                //text = text.PadRight(10);
                Finaltext = Finaltext + "\r\n" + text;
                Finaltext = Finaltext.PadRight(10);
                Finaltext1 = Finaltext1 + "\r\n" + text1;
            }
            //while ((int)(percentInhead * 100) <= int.Parse(HeadPercentage.Text))
            //{
            //    htIndex++;
            //    List<double> numlisttemp = new List<double>();
            //    mean = getMean(numberlist);
            //    foreach (double b in numberlist)
            //    {
            //        if (b >= mean)
            //            numlisttemp.Add(b);
            //    }
            //    cntInhead = numlisttemp.Count;
            //    cntInTail = numberlist.Count - cntInhead;
            //    // int cntInTail = numberlist.Count - cntInhead;
            //    percentInhead = (double)cntInhead / (double)numberlist.Count;
            //    percentInTail = 100 - (int)(percentInhead * 100);
            //    text = numberlist.Count + "\t" + cntInhead + "\t" + (int)(percentInhead * 100) + "%" + "\t" + cntInTail + "\t" + percentInTail + "%" + "\t" + Math.Round(mean, 2);
            //    text1 = numberlist.Count + "\t" + cntInhead + "\t" + (int)(percentInhead * 100) + "%" + "\t" + cntInTail + "\t" + percentInTail + "%" + "\t" + mean;
            //    if ((int)(percentInhead * 100) <= int.Parse(HeadPercentage.Text))
            //    {
            //        Finaltext = Finaltext + "\r\n" + text;
            //        Finaltext1 = Finaltext1 + "\r\n" + text1;
            //    }
            //    numberlist = numlisttemp;
            //}

            
            BreakResult.Text = Finaltext;
            ResultTemp.Text = Finaltext1;
            //BreakResult.WordWrap = true;
            //sw.Close();
            //fs.Close();


            //string SaveAddress = @"C:\PhD work\Fractal theory\HtIndex\HTs\hts.txt";
            //System.IO.FileStream fs = new System.IO.FileStream(SaveAddress, FileMode.Create);
            //StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            //foreach (double x in indexlist)
            //{
            //    string text2 = x.ToString();

            //    sw.WriteLine(text2);
            //}

            //sw.Close();
            //fs.Close();

            //SaveAddress = @"C:\PhD work\Fractal theory\HtIndex\HTs\multiHt.txt";
            //fs = new System.IO.FileStream(SaveAddress, FileMode.Create);
            //sw = new StreamWriter(fs, Encoding.Default);
            //if(1==1)
            //{
            //    string text2 = ht1 + "\t" + ht3 + "\t" + ht4;

            //    sw.WriteLine(text2);
            //}
            //sw.Close();
            //fs.Close();
            //return 2;

            //SaveAddress = @"C:\PhD work\Fractal theory\HtIndex\HTs\htfWhole.txt";
            //fs = new System.IO.FileStream(SaveAddress, FileMode.Create);
            //sw = new StreamWriter(fs, Encoding.Default);
            //foreach (double x in indexWholelist)
            //{
            //    string text2 = x.ToString();

            //    sw.WriteLine(text2);
            //}

            //sw.Close();
            //fs.Close();



            //SaveAddress = @"C:\PhD work\Fractal theory\HtIndex\HTs\htD.txt";
            //fs = new System.IO.FileStream(SaveAddress, FileMode.Create);
            //sw = new StreamWriter(fs, Encoding.Default);
            //paralist = paralist.OrderByDescending(x => x).ToList();
            //foreach (double x in paralist)
            //{

            //    int value = 1;
            //    foreach (double m in meanlist)
            //    {
            //        if (x >= m)
            //        {
            //            value++;
            //            continue;
            //        }
            //        else
            //            break;
            //    }
            //    sw.WriteLine(value.ToString());
            //}

            //sw.Close();
            //fs.Close();

            //MessageBox.Show((htIndex).ToString());
            return htIndex;


        }
        struct numCnt
        {
            public double value;
            public double count;

        }
        string Finaltext1 = "";
        private int FromAddress(ref double CRG)
        {
            string OpenAddress = @InputFileAddress.Text;
            StreamReader sr = new StreamReader(OpenAddress);
            List<double> numberlist = new List<double>();

            double number = 0;
            int linecnt = 0;
            while (sr.Peek() > 0)
            {
                //if (linecnt == 0)
                //{
                //    linecnt++;
                //    string rl = sr.ReadLine();
                //    continue;
                //}
                string rline = sr.ReadLine();
                string[] rlinesplit = rline.Split('\t');
                if (rlinesplit.Length == 2)
                {
                    for (int i = 0; i < int.Parse(rlinesplit[1]); i++)
                    {
                        double n = 0;

                        bool isNumeric = double.TryParse(rlinesplit[0], out n);
                        if (isNumeric)
                        {
                            number = double.Parse(rlinesplit[0]);
                            numberlist.Add(number);
                        }


                    }


                }
                else
                {
                    double n = 0;

                    bool isNumeric = double.TryParse(rline, out n);
                    if (isNumeric)
                    {
                        number = double.Parse(rline);
                        numberlist.Add(number);
                    }

                }
            }
            int htIndex = 0;

            List<double> meanlist = new List<double>();
            getHtindex(numberlist, ref meanlist);
            CRG = getCRG(meanlist);
            //iterative mean calculation    

            double percentInhead = 0.2;
            int cntInhead = 0;
            int percentInTail = 0;
            int cntInTail = 0;
            double mean = 0;
            //string SaveAddress = @"C:\PhD work\OSM\historyWorldOSM\coedit network\option2\1111\HierarchyRule1.txt";
            //System.IO.FileStream fs = new System.IO.FileStream(SaveAddress, FileMode.Append);
            //StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            string text = "";
            text1 = "";
            string Finaltext = "";
            Finaltext1 = "";
            text = "#Data" + "\t" + "#head" + "\t" + "%head" + "\t" + "#tail" + "\t" + "%tail" + "\t" + "mean";
            Finaltext = text;
            Finaltext1 = text;
            //sw.WriteLine(text);


            while ((int)(percentInhead * 100) <= int.Parse(HeadPercentage.Text))
            {
                htIndex++;
                List<double> numlisttemp = new List<double>();
                mean = getMean(numberlist);
                foreach (double b in numberlist)
                {
                    if (b >= mean)
                        numlisttemp.Add(b);
                }
                cntInhead = numlisttemp.Count;
                if (cntInhead == 0)
                    break;
                cntInTail = numberlist.Count - cntInhead;
                // int cntInTail = numberlist.Count - cntInhead;
                percentInhead = (double)cntInhead / (double)numberlist.Count;
                percentInTail = 100 - (int)(percentInhead * 100);
                text = numberlist.Count + "\t" + cntInhead + "\t" + (int)(percentInhead * 100) + "%" + "\t" + cntInTail + "\t" + percentInTail + "%" + "\t" + Math.Round(mean, 2);
                text1 = numberlist.Count + "\t" + cntInhead + "\t" + (int)(percentInhead * 100) + "%" + "\t" + cntInTail + "\t" + percentInTail + "%" + "\t" + mean;
                if ((int)(percentInhead * 100) <= int.Parse(HeadPercentage.Text))
                {
                    Finaltext = Finaltext + "\r\n" + text;
                    Finaltext1 = Finaltext1 + "\r\n" + text1;
                }
                numberlist = numlisttemp;
            }
            BreakResult.Text = Finaltext;
            BreakResult.WordWrap = true;
            //sw.Close();
            //fs.Close();


            //MessageBox.Show((htIndex).ToString());


            return htIndex;
        }

        private void BreakResult_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string copy = BreakResult.Text;
            Clipboard.SetDataObject(copy);

        }

        private void data_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符
                }
            }
        }
        #region AllowDrop
        protected void SetAllTextBox()
        {
            foreach (System.Windows.Forms.Control txt in this.Controls)
            {
                if (txt is TextBox)
                {
                    txt.AllowDrop = true;
                    txt.DragDrop += new DragEventHandler(txt_ObjDragDrop);
                    txt.DragEnter += new DragEventHandler(txt_ObjDragEnter);
                }
                else
                {
                    if (txt.Controls.Count > 0)
                    {
                        SetAllTextBox(txt);
                    }
                }
            }


        }

        protected void SetAllTextBox(System.Windows.Forms.Control org)
        {
            foreach (System.Windows.Forms.Control txt in org.Controls)
            {
                if (txt is TextBox)
                {
                    txt.AllowDrop = true;
                    txt.DragDrop += new DragEventHandler(txt_ObjDragDrop);
                    txt.DragEnter += new DragEventHandler(txt_ObjDragEnter);
                }
                else
                {
                    if (txt.Controls.Count > 0)
                    {
                        SetAllTextBox(txt);
                    }
                }
            }
        }

        private void txt_ObjDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void txt_ObjDragDrop(object sender, DragEventArgs e)
        {
            ((TextBox)sender).Text
                = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
        }
        #endregion

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void InputFileAddress_TextChanged(object sender, EventArgs e)
        {
            if (InputFileAddress.Text != "")
                data.Enabled = false;
            else
                data.Enabled = true;
        }

        private void data_MouseClick(object sender, MouseEventArgs e)
        {
            //data.Text = "";
        }

        private void data_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void data_MouseLeave(object sender, EventArgs e)
        {
            //if (data.Text == "")
            //{
            //    data.Text = "or Paste data here...";
            //}
        }

        private void clearBox_Click(object sender, EventArgs e)
        {
            data.Text = "";
            BreakResult.Text = "";
            Copy.Enabled = false;
            htIndex.Text = "";
            hts.Text = "";
        }

        private void BreakResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void Copy_Click(object sender, EventArgs e)
        {
            string copy = Finaltext1;
            Clipboard.SetDataObject(ResultTemp.Text);
            MessageBox.Show("Done! Result copied to clipboard");
        }

    }
}
