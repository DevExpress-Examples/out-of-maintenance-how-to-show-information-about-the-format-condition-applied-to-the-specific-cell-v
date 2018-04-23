using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.Utils;
using DevExpress.XtraPivotGrid.Data;

namespace WindowsApplication53
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateTable();
            pivotGridControl1.RefreshData();
            pivotGridControl1.BestFit();
       }
        private void PopulateTable()
        {
            DataTable myTable = dataSet1.Tables["Data"];
            myTable.Rows.Add(new object[] {"Aaa", DateTime.Today, 7});
            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddDays(1), 4 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today, 12 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1), 14 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today, 11 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1), 10 });

            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddYears(1), 4 });
            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddYears(1).AddDays(1), 2 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddYears(1), 3 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1).AddYears(1), 1 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddYears(1), 8 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1).AddYears(1), 22 });
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != pivotGridControl1) return;
            PivotGridHitInfo hitInfo = pivotGridControl1.CalcHitInfo(e.ControlMousePosition);
            if (hitInfo.CellInfo != null)
            {
                PivotGridStyleFormatCondition condition = GetStyleFormatByValue(hitInfo.CellInfo.Item, pivotGridControl1);
                if (condition != null)
                {
                    object o = hitInfo.CellInfo.DataField.ToString() + hitInfo.CellInfo.ColumnIndex + hitInfo.CellInfo.RowIndex;
                    string toolTipString = Convert.ToString(condition.Tag);
                    e.Info = new ToolTipControlInfo(o, toolTipString);
                }
            }

        }

        PivotGridStyleFormatCondition GetStyleFormatByValue(PivotGridCellItem cellItem, PivotGridControl pivot)
        {

            PivotGridCellType cellType = GetCellType(cellItem);
            int cnt = pivot.FormatConditions.Count;
            if (cnt == 0) return null;
            for (int n = 0; n < cnt; n++)
            {
                PivotGridStyleFormatCondition cond = pivot.FormatConditions[n];
                if (cond.CanApplyToCell(cellType) && cond.CheckValue(cellItem.DataField, cellItem.Value, cellItem))
                    return cond;
            }
            return null;
        }

        protected PivotGridCellType GetCellType(PivotGridCellItem cellViewInfo)
        {
            PivotGridCellType cellType = PivotGridCellType.Cell;
            if (cellViewInfo.IsCustomTotalAppearance)
                cellType = PivotGridCellType.CustomTotal;
            else
            {
                if (cellViewInfo.IsGrandTotalAppearance)
                {
                    cellType = PivotGridCellType.GrandTotal;
                }
                if (cellViewInfo.IsTotalAppearance)
                {
                    cellType = PivotGridCellType.Total;
                }
            }
            return cellType;
        }


       
    }
}