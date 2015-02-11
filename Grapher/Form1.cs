using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JDunkerley.Parser;

namespace Grapher
{
    /// <summary>
    /// Chart Control Test Class
    /// </summary>
    public partial class Form1 : Form
    {
        // Expression Component
        private IComponent parsedComponent;

        // Variables
        private double currX;
        private double XMin;
        private double XMax;
        private List<string> variables;
        private Dictionary<string, bool> variablesValid;
        private Dictionary<string, bool> variableTypes;
        private Dictionary<string, double[]> variableValues;

        public Form1()
        {
            InitializeComponent();

            this.variables = new List<string>();
            this.variablesValid = new Dictionary<string, bool>();
            this.variableTypes = new Dictionary<string, bool>();
            this.variableValues = new Dictionary<string, double[]>();

            Parser.RegisterMathFunctions();

            this.grpVariables.Height = 16;
            this.grpVariables.Tag = this.grpVariables.Height;
            this.grpVariables.MouseDoubleClick += grpVariables_MouseDoubleClick;

            this.txtXFrom.Text = "-10";
            this.txtXTo.Text = "10";
        }

        private void grpVariables_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Location.Y >= 0 && e.Location.Y < 16)
                this.grpVariables.Height = (this.grpVariables.Height == (int)this.grpVariables.Tag ? 16 : (int)this.grpVariables.Tag);
        }

        private void txtExpression_TextChanged(object sender, EventArgs e)
        {
            IComponent[] rawComponents;
            IComponent parsed = null;
            IComponent rawParsed = null;

            if (Parser.GetComponents(txtExpression.Text, out rawComponents))
            {
                rawParsed = Parser.GetComponentTree(rawComponents, false);
                parsed = Parser.GetComponentTree(rawComponents);                
            }

            if (parsed == null)
            {
                this.lblRawParsed.Text = "[FAIL]";
                this.lblParsed.Text = "[FAIL]";
                this.parsedComponent = null;

                this.grpVariables.Controls.Clear();
                this.grpVariables.Height = 16;
                this.grpVariables.Tag = this.grpVariables.Height;

                this.txtExpression.BackColor = (this.txtExpression.Text == "" ? Color.White : Color.LightSalmon);
            }
            else
            {
                this.lblRawParsed.Text = rawParsed.Text ?? "[NULL]";
                this.lblParsed.Text = parsed.Text;
                this.parsedComponent = parsed;

                this.grpVariables.Controls.Clear();

                var added = new Dictionary<string, object>();
                for (int i = 0; i < parsed.Variables.Length; i++)
                {
                    string name = parsed.Variables[i].ToUpper();
                    if (name == "X") continue;
                    if (added.ContainsKey(name)) continue;

                    int top = 16 + added.Count * 22;
                    this.grpVariables.Controls.Add(new Label() { Text = name, Width = 114, Height = 21, Left = 3, Top = top, TextAlign = ContentAlignment.MiddleRight });

                    ComboBox cBox = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList, Width = 60, Height = 21, Left = 118, Top = top };
                    cBox.Items.Add("Fixed");
                    cBox.Items.Add("Range");
                    cBox.SelectedIndexChanged += varType_SelIdxChanged;
                    this.grpVariables.Controls.Add(cBox);

                    Control[] ctrls = new Control[6];
                    ctrls[0] = new Label() { Text = "Value : ", Width = 60, Height = 21, Left = 179, Top = top, TextAlign = ContentAlignment.MiddleRight, Tag = name };
                    ctrls[1] = new TextBox() { Text = "", Width = 75, Height = 21, Left = 240, Top = top, Tag = name };
                    ctrls[2] = new Label() { Text = " to : ", Width = 40, Height = 21, Left = 316, Top = top, Tag = name, Visible = false };
                    ctrls[3] = new TextBox() { Text = "", Width = 75, Height = 21, Left = 357, Top = top, Tag = name, Visible = false };
                    ctrls[4] = new Label() { Text = " step : ", Width = 40, Height = 21, Left = 433, Top = top, TextAlign = ContentAlignment.MiddleCenter, Tag = name, Visible = false };
                    ctrls[5] = new TextBox() { Text = "", Width = 75, Height = 21, Left = 464, Top = top, Tag = name, Visible = false };
                    this.grpVariables.Controls.AddRange(ctrls);
                    cBox.Tag = new object[] { name, ctrls };

                    cBox.SelectedIndex = 0;

                    added[name] = true;
                }

                this.grpVariables.Height = 21 * (added.Count + 1);
                this.grpVariables.Tag = this.grpVariables.Height;

                this.txtExpression.BackColor = Color.LightGreen;
            }

            this.EvaluateChart();
        }

        private void varType_SelIdxChanged(object sender, EventArgs e)
        {
            ComboBox cBox = sender as ComboBox;
            if (cBox == null) return;

            object[] data = cBox.Tag as object[];
            if (data == null || data.Length != 2) return;

            string name = data[0] as string;
            if (name == null) return;
            if (!(data[1] is Control[])) return;

            Control[] ctrls = (Control[])data[1];
            for (int i = 0; i < ctrls.Length; i++)
            {
                Control ctrl = ctrls[i] as Control;
                if (ctrl == null || ctrl.Tag as string != name) continue;

                if (ctrl.Left == 179)
                {
                    // Value/From Label
                    ctrl.Text = cBox.SelectedIndex == 0 ? "Value : " : "From : ";
                }
                else if (ctrl.Left > 240)
                {
                    // Other Inputs
                    ctrl.Visible = cBox.SelectedIndex == 1;
                }
            }
        }

        private void varValue_TextChanged(object sender, EventArgs e)
        {
            TextBox tBox = sender as TextBox;
            if (tBox == null) return;

            string tag = tBox.Tag as string;
            if (tag == null) return;

            int idx = this.variables.IndexOf(tag);
            if (idx == -1) return;

            double tmp;
            if (tBox.Text != "" && double.TryParse(tBox.Text, out tmp))
            {
                this.variableValues[tag] = new double[] { tmp };
                this.variablesValid[tag] = true;
                tBox.BackColor = Color.LightGreen;
            }
            else
            {
                this.variableValues[tag] = new double[] { double.NaN };
                this.variablesValid[tag] = false;
                tBox.BackColor = Color.LightSalmon;
            }

            this.EvaluateChart();
        }

        private void txtXTextChanged(object sender, EventArgs e)
        {
            double tmp;
            this.XMin = (this.txtXFrom.Text != "" && double.TryParse(this.txtXFrom.Text, out tmp)) ? tmp : double.NaN;
            this.XMax = (this.txtXTo.Text != "" && double.TryParse(this.txtXTo.Text, out tmp)) ? tmp : double.NaN;
            if (this.XMin >= this.XMax) { this.XMax = double.NaN; this.XMin = double.NaN; }

            this.txtXFrom.BackColor = double.IsNaN(this.XMin) ? Color.LightSalmon : Color.LightGreen;
            this.txtXTo.BackColor = double.IsNaN(this.XMax) ? Color.LightSalmon : Color.LightGreen;

            this.EvaluateChart();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            this.EvaluateChart();
        }

        private void EvaluateChart()
        {
            this.chart.Series.Clear();

            if (double.IsNaN(this.XMin) || double.IsNaN(this.XMax) || this.parsedComponent == null || this.variablesValid.Any(kvp=>!kvp.Value))
                return;

            int points = this.chart.Width * 4;
            double step = (this.XMax - this.XMin) / points;
            this.chart.ChartAreas[0].AxisX.Minimum = this.XMin;
            this.chart.ChartAreas[0].AxisX.Maximum = this.XMax;

            vars = new Dictionary<string, object>();
            if (this.variables.Count == 0)
                this.EvaluateOneSeries(step, "Line");
            else
                this.RecurseVarialbe(0, step, "");
        }

        private void RecurseVarialbe(int VariableIdx, double step, string Name)
        {
            string VarName = this.variables[VariableIdx];
            foreach (var item in this.variableValues)
            {
                vars[VarName.ToUpper()] = item;

                if (this.variables.Count == VariableIdx + 1)
                    this.EvaluateOneSeries(step, Name + item.ToString());
                else
                    this.RecurseVarialbe(VariableIdx + 1, step, Name + item.ToString() + ";");
            }
        }
            
        private void EvaluateOneSeries(double step, string Name)
        {
            var series = this.chart.Series.Add(Name);
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series.ChartArea = this.chart.ChartAreas[0].Name;

            this.currX = this.XMin;
            while (this.currX < this.XMax)
            {
                object newVal = this.parsedComponent.Evaluate(getVariable);
                if (newVal is double)
                {
                    double currY = (double)newVal;
                    series.Points.AddXY(this.currX, currY);
                }

                this.currX += step;
            }
        }

        private Dictionary<string, object> vars;

        private object getVariable(string Variable)
        {
            string name = Variable.ToUpper();
            if (name == "X") return this.currX;

            if (vars.ContainsKey(name))
                return vars[name];

            return null;
        }
    }
}