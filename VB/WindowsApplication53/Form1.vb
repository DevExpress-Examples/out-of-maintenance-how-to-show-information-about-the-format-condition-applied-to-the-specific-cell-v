Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Data.PivotGrid
Imports DevExpress.Utils
Imports DevExpress.XtraPivotGrid.Data

Namespace WindowsApplication53
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			PopulateTable()
			pivotGridControl1.RefreshData()
			pivotGridControl1.BestFit()
		End Sub
		Private Sub PopulateTable()
			Dim myTable As DataTable = dataSet1.Tables("Data")
			myTable.Rows.Add(New Object() {"Aaa", DateTime.Today, 7})
			myTable.Rows.Add(New Object() { "Aaa", DateTime.Today.AddDays(1), 4 })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today, 12 })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today.AddDays(1), 14 })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today, 11 })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today.AddDays(1), 10 })

			myTable.Rows.Add(New Object() { "Aaa", DateTime.Today.AddYears(1), 4 })
			myTable.Rows.Add(New Object() { "Aaa", DateTime.Today.AddYears(1).AddDays(1), 2 })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today.AddYears(1), 3 })
			myTable.Rows.Add(New Object() { "Bbb", DateTime.Today.AddDays(1).AddYears(1), 1 })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today.AddYears(1), 8 })
			myTable.Rows.Add(New Object() { "Ccc", DateTime.Today.AddDays(1).AddYears(1), 22 })
		End Sub

		Private Sub toolTipController1_GetActiveObjectInfo(ByVal sender As Object, ByVal e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles toolTipController1.GetActiveObjectInfo
			If e.SelectedControl IsNot pivotGridControl1 Then
				Return
			End If
			Dim hitInfo As PivotGridHitInfo = pivotGridControl1.CalcHitInfo(e.ControlMousePosition)
			If hitInfo.CellInfo IsNot Nothing Then
				Dim condition As PivotGridStyleFormatCondition = GetStyleFormatByValue(hitInfo.CellInfo.Item, pivotGridControl1)
				If condition IsNot Nothing Then
					Dim o As Object = hitInfo.CellInfo.DataField.ToString() & hitInfo.CellInfo.ColumnIndex + hitInfo.CellInfo.RowIndex
					Dim toolTipString As String = Convert.ToString(condition.Tag)
					e.Info = New ToolTipControlInfo(o, toolTipString)
				End If
			End If

		End Sub

		Private Function GetStyleFormatByValue(ByVal cellItem As PivotGridCellItem, ByVal pivot As PivotGridControl) As PivotGridStyleFormatCondition

			Dim cellType As PivotGridCellType = GetCellType(cellItem)
			Dim cnt As Integer = pivot.FormatConditions.Count
			If cnt = 0 Then
				Return Nothing
			End If
			For n As Integer = 0 To cnt - 1
				Dim cond As PivotGridStyleFormatCondition = pivot.FormatConditions(n)
				If cond.CanApplyToCell(cellType) AndAlso cond.CheckValue(cellItem.DataField, cellItem.Value, cellItem) Then
					Return cond
				End If
			Next n
			Return Nothing
		End Function

		Protected Function GetCellType(ByVal cellViewInfo As PivotGridCellItem) As PivotGridCellType
			Dim cellType As PivotGridCellType = PivotGridCellType.Cell
			If cellViewInfo.IsCustomTotalAppearance Then
				cellType = PivotGridCellType.CustomTotal
			Else
				If cellViewInfo.IsGrandTotalAppearance Then
					cellType = PivotGridCellType.GrandTotal
				End If
				If cellViewInfo.IsTotalAppearance Then
					cellType = PivotGridCellType.Total
				End If
			End If
			Return cellType
		End Function



	End Class
End Namespace