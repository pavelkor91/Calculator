using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Numerics;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private string temproraryString;
        private Calculator calculator;
        private Graphics canvas;
        private bool wasDrawSin;
        private bool wasDrawCos;
        private bool wasDrawDegree;
        private bool wasDrawLn;
        private Factorial factorial;
        private Thread factoralThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            calculator = new Calculator();
            temproraryString = "";
            canvas = this.graphPanel.CreateGraphics();
            this.Lable.TextChanged += this.changeTextAction;
            this.tabControl.Click += this.changeFocusTabControl;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.factorial = new Factorial();
            this.textBoxFactorial.KeyPress += this.textBoxPressKey;
            this.textBoxFactorial.TextChanged += this.textInTBChanged;
        }

        private void changeFocusTabControl(object sender, EventArgs e)
        {
            switch(((TabControl)sender).SelectedIndex)
            {
                case 0:
                    this.ClientSize = new System.Drawing.Size(295, 340);
                    this.tabControl.Size = new System.Drawing.Size(280, 348);

                    break;
                case 1:
                    this.ClientSize = new System.Drawing.Size(890, 465);
                    this.tabControl.Size = new System.Drawing.Size(895, 470);
                    this.redrawLatticeWithScale(this.scaleTrack.Value);
                    break;

                case 2:
                    this.ClientSize = new System.Drawing.Size(295, 340);
                    this.tabControl.Size = new System.Drawing.Size(280, 348);
                    break;
                default:
                    break;
            }

        }

        //Calculator tabControl methods        

         private void addChartersToTemproraryString(string newString)
        {

                if (calculator.counterEqual > 0)
                {
                    calculator.counterEqual = 0;
                    calculator.clear();
                    temproraryString = "";
                }

                if (newString.Equals(",") && temproraryString.Equals("") || newString.Equals(",") && temproraryString.Equals("0"))
                    temproraryString = "0,";
                else
                    if (!(newString.Equals("0") && temproraryString.Equals("0")) && !(newString.Equals("0") && temproraryString.Equals("")))
                    {
                        if (temproraryString.Equals("0"))
                            temproraryString = newString;
                        else
                            temproraryString += newString;
                    }
                    else
                        temproraryString = "0";
                this.Lable.Text = temproraryString;
        }


        //  numbers buttons click
        private void oneBut_Click(object sender, EventArgs e)
        {
            addChartersToTemproraryString(((System.Windows.Forms.Button)sender).Text);
        }
        private void twoBut_Click(object sender, EventArgs e)
        {
            addChartersToTemproraryString(((System.Windows.Forms.Button)sender).Text);
        }
        private void threeBut_Click(object sender, EventArgs e)
        {
            addChartersToTemproraryString(((System.Windows.Forms.Button)sender).Text);
        }
        private void fourBut_Click(object sender, EventArgs e)
        {
            addChartersToTemproraryString(((System.Windows.Forms.Button)sender).Text);
        }
        private void fiveBut_Click(object sender, EventArgs e)
        {
            addChartersToTemproraryString(((System.Windows.Forms.Button)sender).Text);
        }
        private void sixBut_Click(object sender, EventArgs e)
        {
            addChartersToTemproraryString(((System.Windows.Forms.Button)sender).Text);
        }
        private void sevenBut_Click(object sender, EventArgs e)
        {
            addChartersToTemproraryString(((System.Windows.Forms.Button)sender).Text);
        }
        private void eightBut_Click(object sender, EventArgs e)
        {
            addChartersToTemproraryString(((System.Windows.Forms.Button)sender).Text);
        }
        private void nineBut_Click(object sender, EventArgs e)
        {
            addChartersToTemproraryString(((System.Windows.Forms.Button)sender).Text);
        }
        private void zeroBut_Click(object sender, EventArgs e)
        {
            addChartersToTemproraryString(((System.Windows.Forms.Button)sender).Text);
        }
        

        // opreations

        private void plusBut_Click(object sender, EventArgs e)
        {
            this.Lable.Text = calculator.makeOperation(Operator.PLUS, temproraryString);
            temproraryString = "";

        }
        private void minusBut_Click(object sender, EventArgs e)
        {
            this.Lable.Text = calculator.makeOperation(Operator.MINUS, temproraryString);
            temproraryString = "";
        }


        private void multiplyBut_Click(object sender, EventArgs e)
        {
            this.Lable.Text = calculator.makeOperation(Operator.MULTIPLY, temproraryString);
            temproraryString = "";
        }
        private void degreeBut_Click(object sender, EventArgs e)
        {
            this.Lable.Text = calculator.makeOperation(Operator.DIVIDE, temproraryString);
            temproraryString = "";
        }


        private void percentBut_Click(object sender, EventArgs e)
        {
            if (this.Lable.Text.Equals("0") && temproraryString.Equals(""))
                return;
            else if (!this.Lable.Text.Equals("0") && temproraryString.Equals(""))//after operation click
                this.Lable.Text = calculator.percentOperation(temproraryString, "left");
            else if (!this.Lable.Text.Equals("0") && !temproraryString.Equals(""))
            {
                if (this.Lable.Text.Equals(calculator.leftOprand.ToString()))
                    this.Lable.Text = calculator.percentOperation(temproraryString, "left");
                else
                {
                    this.Lable.Text = calculator.percentOperation(temproraryString, "right");
                    temproraryString = this.Lable.Text;
                }
            }
            else
                return; 
        }
        private void pointBut_Click(object sender, EventArgs e)
        {
            addChartersToTemproraryString(((System.Windows.Forms.Button)sender).Text);
        }
        

        private void cBut_Click(object sender, EventArgs e)
        {
            calculator.clear();
            temproraryString = "";
            this.Lable.Text = "0";
        }
        private void changeSignBut_Click(object sender, EventArgs e)
        {
            if (this.Lable.Text.Equals("0") && temproraryString.Equals(""))
                return;
            else if (!this.Lable.Text.Equals("0") && temproraryString.Equals(""))//after operation click
                this.Lable.Text =calculator.changeSign(temproraryString,"left");
            else if (!this.Lable.Text.Equals("0") && !temproraryString.Equals(""))
                {
                if (this.Lable.Text.Equals(temproraryString))
                {
                   this.Lable.Text =calculator.changeSign(temproraryString, "right");
                   temproraryString = this.Lable.Text;
                }
                else
                    this.Lable.Text =calculator.changeSign(temproraryString,"left");
                }
            else
                return;
        }
        private void equalBut_Click(object sender, EventArgs e)
        {
            this.Lable.Text =calculator.equalOperation(temproraryString);
        }

        private void changeTextAction(object sender, EventArgs e)
        {
            if(this.Lable.Text.Length>12)
              this.Lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            if(this.Lable.Text.Length<12)
                this.Lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        // Graph of the function methods

        private void scaleTrack_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = (TrackBar)sender;
            scaleLable.Text = String.Format("{0}",trackBar.Value);
            this.redrawLatticeWithScale(trackBar.Value);
            if (this.wasDrawSin)
                this.drawSin();
            if (this.wasDrawCos)
                this.drawCos();
            if (this.wasDrawDegree)
                this.drawDegreeFunc();
            if (this.wasDrawLn)
                this.drawLnFunc();
        }


        private void redrawLatticeWithScale(int scale)
        {
            this.graphPanel.Refresh();
            Pen latticePen = new Pen(Brushes.Gray, 0.1f);
            for (int i = 0; i <= this.graphPanel.Width / scale; i++)
            {
                canvas.DrawLine(latticePen, new Point(i * scale + this.graphPanel.Width / 2, 0), new Point(i * scale + this.graphPanel.Width / 2, this.graphPanel.Height));
                canvas.DrawLine(latticePen, new Point(this.graphPanel.Width / 2 - i * scale, 0), new Point(this.graphPanel.Width / 2 - i * scale, this.graphPanel.Height));
                canvas.DrawLine(latticePen, new Point(0, this.graphPanel.Height / 2 + i * scale), new Point(this.graphPanel.Width, this.graphPanel.Height / 2 + i * scale));
                canvas.DrawLine(latticePen, new Point(0, this.graphPanel.Height / 2 - i * scale), new Point(this.graphPanel.Width, this.graphPanel.Height / 2 - i * scale));
            }
            this.drawAsix();
        }

        private void drawAsix()
        {
            Pen axisPen = new Pen(Brushes.Black, 2);

            canvas.DrawLine(axisPen, new Point(0, this.graphPanel.Height / 2), new Point(this.graphPanel.Width, this.graphPanel.Height / 2));
            canvas.DrawLine(axisPen, new Point(this.graphPanel.Width / 2, 0), new Point(this.graphPanel.Width / 2, this.graphPanel.Height));
            canvas.DrawLine(axisPen, new Point(this.graphPanel.Width / 2, 0), new Point(this.graphPanel.Width / 2 - 3, 10));
            canvas.DrawLine(axisPen, new Point(this.graphPanel.Width / 2, 0), new Point(this.graphPanel.Width / 2 + 3, 10));
            canvas.DrawLine(axisPen, new Point(this.graphPanel.Width, this.graphPanel.Height / 2), new Point(this.graphPanel.Width - 10, this.graphPanel.Height / 2 + 3));
            canvas.DrawLine(axisPen, new Point(this.graphPanel.Width, this.graphPanel.Height / 2), new Point(this.graphPanel.Width - 10, this.graphPanel.Height / 2 - 3));
        }

       
        private void drawSin()
        {
            int width = this.graphPanel.Width;
            float count = width /this.scaleTrack.Value;
            float firstDegree = 22.5f * count/2 *-1.0f;
            float lastDegree = 22.5f * count / 2;
            float degreeInPoint = 22.5f / this.scaleTrack.Value;
            float[] xCooridnates = new float[this.graphPanel.Width];
            float[] yCooridnates = new float[this.graphPanel.Width];

            for (int i = 0; i< this.graphPanel.Width; i++)
            {
                xCooridnates[i] = i;
                double y = Math.Sin(firstDegree * Math.PI / 180)*-1;
                yCooridnates[i] = (((float)y * 4 * this.scaleTrack.Value)+this.graphPanel.Height/2);
                firstDegree += degreeInPoint;
            }
            Pen latticePen = new Pen(Brushes.Blue, 1.0f);

            for (int i = 0; i < this.graphPanel.Width-1; i++)
                canvas.DrawLine(latticePen, new PointF(xCooridnates[i], yCooridnates[i]), new PointF(xCooridnates[i+1], yCooridnates[i+1]));
        }

        private void drawCos()
        {
            int width = this.graphPanel.Width;
            float count = width / this.scaleTrack.Value;
            float firstDegree = 22.5f * count / 2 * -1.0f;
            float lastDegree = 22.5f * count / 2;
            float degreeInPoint = 22.5f / this.scaleTrack.Value;
            float[] xCooridnates = new float[this.graphPanel.Width];
            float[] yCooridnates = new float[this.graphPanel.Width];

            for (int i = 0; i < this.graphPanel.Width; i++)
            {
                xCooridnates[i] = i;
                double y = Math.Cos(firstDegree * Math.PI / 180) * -1;
                yCooridnates[i] = (((float)y * 4 * this.scaleTrack.Value) + this.graphPanel.Height / 2);
                firstDegree += degreeInPoint;
            }
            Pen latticePen = new Pen(Brushes.Green, 1.0f);

            for (int i = 0; i < this.graphPanel.Width - 1; i++)
                canvas.DrawLine(latticePen, new PointF(xCooridnates[i], yCooridnates[i]), new PointF(xCooridnates[i + 1], yCooridnates[i + 1]));
        }

        private void drawDegreeFunc()
        {
            float x0 = this.graphPanel.Width / 2;
            float x0Reverse = this.graphPanel.Width / 2;
            float y0 = this.graphPanel.Height / 2;
            float step = this.scaleTrack.Value * 0.1f;
            int count = this.graphPanel.Width / this.scaleTrack.Value;
            float[] xCooridnates = new float[count/2 * 10];
            float[] xReverseCooridnates = new float[count / 2 * 10];
            float[] yCooridnates = new float[count/2 * 10];

            for (float i = 0; i < count / 2; i += 0.1f)
            {
                  float localI = i * 10;
                  xCooridnates[(int)localI] = x0;
                  xReverseCooridnates[(int)localI] = x0Reverse;
                  x0 += step;
                  x0Reverse -= step;
                  float yValue = (float)Math.Pow(i,2);
                  yCooridnates[(int)localI] = y0 - yValue * this.scaleTrack.Value;
            }
            Pen latticePen = new Pen(Brushes.Red, 1.0f);
            for (int i = 0; i < count / 2 * 10 - 1; i++)
            {
                canvas.DrawLine(latticePen, new PointF(xCooridnates[i], yCooridnates[i]), new PointF(xCooridnates[i + 1], yCooridnates[i + 1]));
                canvas.DrawLine(latticePen, new PointF(xReverseCooridnates[i], yCooridnates[i]), new PointF(xReverseCooridnates[i + 1], yCooridnates[i + 1]));
            }

        }

        private void drawLnFunc()
        {
            float x0 = this.graphPanel.Width / 2;
            float y0 = this.graphPanel.Height / 2;
            int count = this.graphPanel.Width / this.scaleTrack.Value;
            float step = this.scaleTrack.Value * 0.1f;
            float[] xCooridnates = new float[count / 2 * 10];
            float[] yCooridnates = new float[count / 2 * 10];

            for (int i = 1; i < count / 2 *10; i += 1)
            {
                xCooridnates[i-1] = x0;
                x0 += step;
                float yValue = (float)Math.Log(i/10.0f);
                yCooridnates[i-1] = y0 - yValue * this.scaleTrack.Value*4;
            }
            Pen latticePen = new Pen(Brushes.Orange, 1.0f);

            for (int i = 0; i < count / 2 * 10 - 2; i++)
                canvas.DrawLine(latticePen, new PointF(xCooridnates[i], yCooridnates[i]), new PointF(xCooridnates[i + 1], yCooridnates[i + 1]));
        }

        private void sinButton_Click(object sender, EventArgs e)
        {
            this.drawSin();
            this.wasDrawSin = true;

        }

        private void cosButton_Click(object sender, EventArgs e)
        {
            this.drawCos();
            this.wasDrawCos = true;
        }

        private void degreeButtton_Click(object sender, EventArgs e)
        {
            this.drawDegreeFunc();
            this.wasDrawDegree = true;
        }

        private void lnButton_Click(object sender, EventArgs e)
        {
            this.drawLnFunc();
            this.wasDrawLn = true;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            TrackBar trackBar = this.scaleTrack;
            scaleLable.Text = String.Format("{0}", trackBar.Value);
            this.wasDrawCos = false;
            this.wasDrawSin = false;
            this.wasDrawDegree = false;
            this.wasDrawLn = false;
            this.redrawLatticeWithScale(trackBar.Value);

        }

        //thread methods


        private void factorialButton_Click(object sender, EventArgs e)
        {

            factorial.progressChanged += progressFactoralChanged;
            this.progressBar.Maximum = int.Parse(this.textBoxFactorial.Text);

            if (factoralThread != null)
                factoralThread.Abort();
            
            factoralThread = new Thread(new ParameterizedThreadStart(factorial.calculateFactorial));
            factoralThread.Start(int.Parse(this.textBoxFactorial.Text));

        }

        private void progressFactoralChanged(int value ,BigInteger result)
        {
            Action action = () =>
                {
                    this.progressBar.Value = value;
                    if (value == int.Parse(this.textBoxFactorial.Text))
                    {
                        this.resultLable.Text = result.ToString("E");
                        MessageBox.Show("Operation completed", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        factoralThread.Abort();
                    }
                    if (factorial.isCancel)
                    {
                        MessageBox.Show("Operation canceled", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        factoralThread.Abort();
                    }
                };
            Invoke(action);
        }

  

        private void cancelButton_Click(object sender, EventArgs e)
        {
            factorial.isCancel = true;
        }

        private void textBoxPressKey(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)|| (e.KeyChar ==8))
                return;
            else
                e.Handled = true;
        }
        private void textInTBChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!textBox.Text.Equals(""))
               if (int.Parse(textBox.Text) > 1000)
               {
                   textBox.Text =textBox.Text.Remove(textBox.Text.Length - 1);
                   MessageBox.Show("Enter number [1]-[1000]", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               }

        }

    }
}

