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
    public partial class TimeColumn : UserControl
    {
        int currentRow = -1;
        bool isMouseDown = false;
        List<int> selectedRow = new List<int>();
        List<int> tempRows = new List<int>();

        public ColorData ColorData = new ColorData();
        public TimeColumn()
        {
            InitializeComponent();

            tableLayoutPanel1.CellPaint += tableLayoutPanel1_CellPaint;
            this.Invalidated += (x,y) => tableLayoutPanel1.Invalidate();
        }

        #region MouseEvents
        public void MouseUpOnPanel(Point point)
        {
            isMouseDown = false;
            tempRows.Clear();
        }

        public void MouseDownOnPanel(Point point)
        {
            isMouseDown = true;
            if (selectedRow.Contains(currentRow))
            {
                selectedRow.Remove(currentRow);
            }
            else
            {
                selectedRow.Add(currentRow);
            }
            tempRows.Add(currentRow);
            tableLayoutPanel1.Invalidate();
        }

        public void MouseMoveOnPanel(Point position, bool mouseDown)
        {
            SetCurrentRow(tableLayoutPanel1, tableLayoutPanel1.PointToClient(position));

            if (mouseDown)
            {
                if (tempRows.Contains(currentRow))
                {
                    return;
                }

                tempRows.Add(currentRow);
                if (selectedRow.Contains(currentRow))
                {
                    selectedRow.Remove(currentRow);
                }
                else
                {
                    selectedRow.Add(currentRow);
                }
                tableLayoutPanel1.Invalidate();
            }
        }

        public void MouseLeftPanel(Point position)
        {
            Point tplPoint = tableLayoutPanel1.PointToClient(position);
            if (!tableLayoutPanel1.ClientRectangle.Contains(tplPoint))
                currentRow = -1;
            tableLayoutPanel1.Invalidate();
        }
        #endregion

        public void AddToSelected(int index)
        {
            selectedRow.Add(index);
        }
        private void tableLayoutPanel1_CellPaint(object? sender, TableLayoutCellPaintEventArgs e)
        {
            if (selectedRow.Contains(e.Row))
            {
                if(e.Row == currentRow){
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
                if (e.Row == currentRow)
                    using (SolidBrush brush = new SolidBrush(ColorData.HighLightColor))
                        e.Graphics.FillRectangle(brush, e.CellBounds);
            }
        }

        bool SetCurrentRow(TableLayoutPanel tlp, Point pt)
        {
            var rs = tlp.RowStyles;
            var rh = 0f;
            for (int i = 0; i < rs.Count; i++)
            {
                float height = (rs[i].Height / 100 * tlp.Bounds.Height);
                if (pt.Y >= rh && pt.Y <= rh + height)
                {
                    if (currentRow == i)
                    {
                        return false;
                    }
                    if (currentRow != i)
                    {
                        currentRow = i;
                        tlp.Invalidate();
                        return true;
                    }
                }
                rh += height;
            }

            currentRow = -1;
            return false;
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
