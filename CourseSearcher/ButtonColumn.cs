using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseSearcher
{
    public partial class ButtonColumn : UserControl
    {
        int tlpRow = -1;
        bool isMouseDown = false;
        public List<int> selectedRow = new List<int>();
        List<int> tempRows = new List<int>();

        public ColorData ColorData = new ColorData();
        public ButtonColumn()
        {
            InitializeComponent();

            tableLayoutPanel1.CellPaint += tableLayoutPanel1_CellPaint;
            tableLayoutPanel1.MouseMove += tableLayoutPanel1_MouseMove;
            tableLayoutPanel1.MouseLeave += tableLayoutPanel1_MouseLeave;
            tableLayoutPanel1.MouseMove += TableLayoutPanel1_MouseMove;
            tableLayoutPanel1.MouseDown += TableLayoutPanel1_MouseDown1;
            tableLayoutPanel1.MouseUp += TableLayoutPanel1_MouseUp;
            this.Invalidated += ButtonColumn_Invalidated;
        }

        private void ButtonColumn_Invalidated(object? sender, InvalidateEventArgs e)
        {
            tableLayoutPanel1.Invalidate();
        }

        private void TableLayoutPanel1_MouseUp(object? sender, MouseEventArgs e)
        {
            isMouseDown = false;
            tempRows.Clear();
        }

        private void TableLayoutPanel1_MouseDown1(object? sender, EventArgs e)
        {
            isMouseDown = true;
            if (selectedRow.Contains(tlpRow))
            {
                selectedRow.Remove(tlpRow);
            }
            else
            {
                selectedRow.Add(tlpRow);
            }
            tempRows.Add(tlpRow);
            tableLayoutPanel1.Invalidate();
        }

        private void TableLayoutPanel1_MouseMove(object? sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (tempRows.Contains(tlpRow))
                {
                    return;
                }

                tempRows.Add(tlpRow);
                if (selectedRow.Contains(tlpRow))
                {
                    selectedRow.Remove(tlpRow);
                }
                else
                {
                    selectedRow.Add(tlpRow);
                }
                tableLayoutPanel1.Invalidate();
            }
        }

        private void tableLayoutPanel1_CellPaint(object? sender, TableLayoutCellPaintEventArgs e)
        {
            if (selectedRow.Contains(e.Row))
            {
                if(e.Row == tlpRow){
                    using (SolidBrush brush = new SolidBrush(ColorData.HoverColor))
                        e.Graphics.FillRectangle(brush, e.CellBounds);
                }
                else
                {
                    using (SolidBrush brush = new SolidBrush(ColorData.SelectColor))
                        e.Graphics.FillRectangle(brush, e.CellBounds);
                }

            }
            else
            {
                if (e.Row == tlpRow)
                    using (SolidBrush brush = new SolidBrush(ColorData.HighLightColor))
                        e.Graphics.FillRectangle(brush, e.CellBounds);
            }
        }

        bool testTLP(TableLayoutPanel tlp, Point pt)
        {
            var rs = tlp.RowStyles;
            var rh = 0f;
            for (int i = 0; i < rs.Count; i++)
            {
                float height = (rs[i].Height / 100 * tlp.Bounds.Height);
                if (pt.Y >= rh && pt.Y <= rh + height)
                {
                    if (tlpRow == i)
                    {
                        return false;
                    }
                    if (tlpRow != i)
                    {
                        tlpRow = i;
                        tlp.Invalidate();
                        return true;
                    }
                }
                rh += height;
            }

            tlpRow = -1;
            return false;
        }

        private void tableLayoutPanel1_MouseMove(object? sender, MouseEventArgs e)
        {
            testTLP(tableLayoutPanel1, e.Location);
        }

        private void tableLayoutPanel1_MouseLeave(object? sender, EventArgs e)
        {
            Point tplPoint = tableLayoutPanel1.PointToClient(Control.MousePosition);
            if (!tableLayoutPanel1.ClientRectangle.Contains(tplPoint)) tlpRow = -1;
            tableLayoutPanel1.Invalidate();
        }
        public void Clear()
        {
            selectedRow.Clear();
            tableLayoutPanel1.Invalidate();
        }
        public List<bool> GetBlockedSlots()
        {
            return Enumerable.Range(0, tableLayoutPanel1.RowCount).Select(x=> selectedRow.Contains(x)).ToList();
        }
    }
}
