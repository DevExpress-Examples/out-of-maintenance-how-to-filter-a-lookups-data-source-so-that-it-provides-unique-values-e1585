Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports DevExpress.XtraGrid
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports System.Collections.Generic
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Repository

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form
		Private gridDataTable, lookupDataTable As DataTable
		Private filteredDataView As DataView

		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			Dim gridControl As New GridControl()
			gridControl.Parent = Me
			gridControl.Dock = DockStyle.Fill

			gridDataTable = New DataTable()
			gridDataTable.Columns.Add("Category", GetType(String))
			gridDataTable.Columns.Add("ProductID", GetType(Integer))
			gridDataTable.Columns.Add("Price", GetType(Single))
			gridDataTable.Columns.Add("Quantity", GetType(Single))

			gridDataTable.Rows.Add(New Object() { "Beverages", 0, 1.6, 319 })
			gridDataTable.Rows.Add(New Object() { "Beverages", 1, 10034.9, 228 })
			gridDataTable.Rows.Add(New Object() { "Confections", DBNull.Value, 1282.1, 130 })
			gridDataTable.Rows.Add(New Object() { "Confections", DBNull.Value, 3909.0, 380 })

			lookupDataTable = New DataTable()
			lookupDataTable.Columns.Add("ProductID", GetType(Integer))
			lookupDataTable.Columns.Add("ProductName", GetType(String))
			lookupDataTable.Rows.Add(New Object() { 0, "Chai" })
			lookupDataTable.Rows.Add(New Object() { 1, "Ipoh Coffee" })
			lookupDataTable.Rows.Add(New Object() { 2, "Chocolade" })
			lookupDataTable.Rows.Add(New Object() { 3, "Scottish Longbreads" })

			gridControl.DataSource = gridDataTable.DefaultView

			Dim lookupEditor As New RepositoryItemLookUpEdit()
			lookupEditor.DataSource = lookupDataTable.DefaultView
			lookupEditor.ValueMember = "ProductID"
			lookupEditor.DisplayMember = "ProductName"

			Dim gridView As GridView = CType(gridControl.FocusedView, GridView)
			gridView.Columns("ProductID").ColumnEdit = lookupEditor
			AddHandler gridView.ShownEditor, AddressOf gridView_ShownEditor
			AddHandler gridView.HiddenEditor, AddressOf gridView_HiddenEditor
		End Sub
		Private Sub gridView_ShownEditor(ByVal sender As Object, ByVal e As EventArgs)
			Dim gridView As GridView = CType(sender, GridView)
			Dim lookup As LookUpEdit = TryCast(gridView.ActiveEditor, LookUpEdit)
			If gridView.FocusedColumn.FieldName = "ProductID" AndAlso lookup IsNot Nothing Then
				Dim keys As New List(Of String)()
				For i As Integer = 0 To gridDataTable.DefaultView.Count - 1
					Dim key As Object = gridDataTable.DefaultView(i).Row("ProductID")
					If key IsNot Nothing AndAlso key IsNot DBNull.Value Then
						keys.Add(key.ToString())
					End If
				Next i
				filteredDataView = New DataView(lookupDataTable)
				lookup.Properties.DataSource = filteredDataView
				If keys.Count > 0 Then
					filteredDataView.RowFilter = String.Format("ProductID NOT IN( {0} )", String.Join(",", keys.ToArray()))
				End If
			End If
		End Sub
		Private Sub gridView_HiddenEditor(ByVal sender As Object, ByVal e As System.EventArgs)
			If filteredDataView IsNot Nothing Then
				filteredDataView.Dispose()
				filteredDataView = Nothing
			End If
		End Sub
	End Class
End Namespace