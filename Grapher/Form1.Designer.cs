using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Grapher
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Panel pnl001;
            System.Windows.Forms.Label lbl002;
            System.Windows.Forms.Label lbl003;
            System.Windows.Forms.Label lbl001;
            System.Windows.Forms.Panel pnl002;
            System.Windows.Forms.Label lbl004;
            System.Windows.Forms.Panel pnl003;
            System.Windows.Forms.Label lbl005;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.txtXFrom = new System.Windows.Forms.TextBox();
            this.txtXTo = new System.Windows.Forms.TextBox();
            this.lblRawParsed = new System.Windows.Forms.Label();
            this.lblParsed = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grpVariables = new System.Windows.Forms.GroupBox();
            pnl001 = new System.Windows.Forms.Panel();
            lbl002 = new System.Windows.Forms.Label();
            lbl003 = new System.Windows.Forms.Label();
            lbl001 = new System.Windows.Forms.Label();
            pnl002 = new System.Windows.Forms.Panel();
            lbl004 = new System.Windows.Forms.Label();
            pnl003 = new System.Windows.Forms.Panel();
            lbl005 = new System.Windows.Forms.Label();
            pnl001.SuspendLayout();
            pnl002.SuspendLayout();
            pnl003.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl001
            // 
            pnl001.Controls.Add(this.txtExpression);
            pnl001.Controls.Add(lbl002);
            pnl001.Controls.Add(this.txtXFrom);
            pnl001.Controls.Add(lbl003);
            pnl001.Controls.Add(this.txtXTo);
            pnl001.Controls.Add(lbl001);
            pnl001.Dock = System.Windows.Forms.DockStyle.Top;
            pnl001.Location = new System.Drawing.Point(0, 0);
            pnl001.Name = "pnl001";
            pnl001.Size = new System.Drawing.Size(883, 23);
            pnl001.TabIndex = 0;
            // 
            // txtExpression
            // 
            this.txtExpression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExpression.Location = new System.Drawing.Point(117, 0);
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(562, 20);
            this.txtExpression.TabIndex = 5;
            this.txtExpression.TextChanged += new System.EventHandler(this.txtExpression_TextChanged);
            // 
            // lbl002
            // 
            lbl002.Dock = System.Windows.Forms.DockStyle.Right;
            lbl002.Location = new System.Drawing.Point(679, 0);
            lbl002.Name = "lbl002";
            lbl002.Size = new System.Drawing.Size(43, 23);
            lbl002.TabIndex = 4;
            lbl002.Text = "X from ";
            lbl002.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtXFrom
            // 
            this.txtXFrom.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtXFrom.Location = new System.Drawing.Point(722, 0);
            this.txtXFrom.Name = "txtXFrom";
            this.txtXFrom.Size = new System.Drawing.Size(68, 20);
            this.txtXFrom.TabIndex = 3;
            this.txtXFrom.TextChanged += new System.EventHandler(this.txtXTextChanged);
            // 
            // lbl003
            // 
            lbl003.Dock = System.Windows.Forms.DockStyle.Right;
            lbl003.Location = new System.Drawing.Point(790, 0);
            lbl003.Name = "lbl003";
            lbl003.Size = new System.Drawing.Size(25, 23);
            lbl003.TabIndex = 2;
            lbl003.Text = " to ";
            lbl003.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtXTo
            // 
            this.txtXTo.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtXTo.Location = new System.Drawing.Point(815, 0);
            this.txtXTo.Name = "txtXTo";
            this.txtXTo.Size = new System.Drawing.Size(68, 20);
            this.txtXTo.TabIndex = 1;
            this.txtXTo.TextChanged += new System.EventHandler(this.txtXTextChanged);
            // 
            // lbl001
            // 
            lbl001.Dock = System.Windows.Forms.DockStyle.Left;
            lbl001.Location = new System.Drawing.Point(0, 0);
            lbl001.Name = "lbl001";
            lbl001.Size = new System.Drawing.Size(117, 23);
            lbl001.TabIndex = 0;
            lbl001.Text = "Expression :";
            lbl001.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl002
            // 
            pnl002.Controls.Add(this.lblRawParsed);
            pnl002.Controls.Add(lbl004);
            pnl002.Dock = System.Windows.Forms.DockStyle.Top;
            pnl002.Location = new System.Drawing.Point(0, 23);
            pnl002.Name = "pnl002";
            pnl002.Size = new System.Drawing.Size(883, 23);
            pnl002.TabIndex = 1;
            // 
            // lblRawParsed
            // 
            this.lblRawParsed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRawParsed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRawParsed.Location = new System.Drawing.Point(117, 0);
            this.lblRawParsed.Name = "lblRawParsed";
            this.lblRawParsed.Size = new System.Drawing.Size(766, 23);
            this.lblRawParsed.TabIndex = 1;
            this.lblRawParsed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl004
            // 
            lbl004.Dock = System.Windows.Forms.DockStyle.Left;
            lbl004.Location = new System.Drawing.Point(0, 0);
            lbl004.Name = "lbl004";
            lbl004.Size = new System.Drawing.Size(117, 23);
            lbl004.TabIndex = 0;
            lbl004.Text = "Parsed Expression :";
            lbl004.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl003
            // 
            pnl003.Controls.Add(this.lblParsed);
            pnl003.Controls.Add(lbl005);
            pnl003.Dock = System.Windows.Forms.DockStyle.Top;
            pnl003.Location = new System.Drawing.Point(0, 46);
            pnl003.Name = "pnl003";
            pnl003.Size = new System.Drawing.Size(883, 23);
            pnl003.TabIndex = 2;
            // 
            // lblParsed
            // 
            this.lblParsed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblParsed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblParsed.Location = new System.Drawing.Point(117, 0);
            this.lblParsed.Name = "lblParsed";
            this.lblParsed.Size = new System.Drawing.Size(766, 23);
            this.lblParsed.TabIndex = 1;
            this.lblParsed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl005
            // 
            lbl005.Dock = System.Windows.Forms.DockStyle.Left;
            lbl005.Location = new System.Drawing.Point(0, 0);
            lbl005.Name = "lbl005";
            lbl005.Size = new System.Drawing.Size(117, 23);
            lbl005.TabIndex = 0;
            lbl005.Text = "Pre-Evaluated :";
            lbl005.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chart
            // 
            this.chart.BackColor = System.Drawing.Color.LightSteelBlue;
            this.chart.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.ScaleBreakStyle.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(0, 90);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(883, 386);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            // 
            // grpVariables
            // 
            this.grpVariables.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpVariables.Location = new System.Drawing.Point(0, 69);
            this.grpVariables.Name = "grpVariables";
            this.grpVariables.Size = new System.Drawing.Size(883, 21);
            this.grpVariables.TabIndex = 3;
            this.grpVariables.TabStop = false;
            this.grpVariables.Text = "Variables";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 476);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.grpVariables);
            this.Controls.Add(pnl003);
            this.Controls.Add(pnl002);
            this.Controls.Add(pnl001);
            this.Name = "Form1";
            this.Text = "Grapher";
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            pnl001.ResumeLayout(false);
            pnl001.PerformLayout();
            pnl002.ResumeLayout(false);
            pnl003.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Chart chart;
        private TextBox txtExpression;
        private TextBox txtXFrom;
        private TextBox txtXTo;
        private Label lblRawParsed;
        private Label lblParsed;
        private GroupBox grpVariables;

    }
}

