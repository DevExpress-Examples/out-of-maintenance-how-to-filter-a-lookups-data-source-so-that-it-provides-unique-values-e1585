using System;
using System.Data;
using DevExpress.XtraGrid;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace WindowsApplication1 {
    public partial class Form1 : Form {
        private DataTable gridDataTable, lookupDataTable;
        private DataView filteredDataView;

        public Form1() {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e) {
            GridControl gridControl = new GridControl();
            gridControl.Parent = this;
            gridControl.Dock = DockStyle.Fill;

            gridDataTable = new DataTable();
            gridDataTable.Columns.Add("Category", typeof(string));
            gridDataTable.Columns.Add("ProductID", typeof(int));
            gridDataTable.Columns.Add("Price", typeof(float));
            gridDataTable.Columns.Add("Quantity", typeof(float));

            gridDataTable.Rows.Add(new object[] { "Beverages", 0, 1.6, 319 });
            gridDataTable.Rows.Add(new object[] { "Beverages", 1, 10034.9, 228 });
            gridDataTable.Rows.Add(new object[] { "Confections", DBNull.Value, 1282.1, 130 });
            gridDataTable.Rows.Add(new object[] { "Confections", DBNull.Value, 3909.0, 380 });

            lookupDataTable = new DataTable();
            lookupDataTable.Columns.Add("ProductID", typeof(int));
            lookupDataTable.Columns.Add("ProductName", typeof(string));
            lookupDataTable.Rows.Add(new object[] { 0, "Chai" });
            lookupDataTable.Rows.Add(new object[] { 1, "Ipoh Coffee" });
            lookupDataTable.Rows.Add(new object[] { 2, "Chocolade" });
            lookupDataTable.Rows.Add(new object[] { 3, "Scottish Longbreads" });

            gridControl.DataSource = gridDataTable.DefaultView;

            RepositoryItemLookUpEdit lookupEditor = new RepositoryItemLookUpEdit();
            lookupEditor.DataSource = lookupDataTable.DefaultView;
            lookupEditor.ValueMember = "ProductID";
            lookupEditor.DisplayMember = "ProductName";

            GridView gridView = (GridView)gridControl.FocusedView;
            gridView.Columns["ProductID"].ColumnEdit = lookupEditor;
            gridView.ShownEditor += gridView_ShownEditor;
            gridView.HiddenEditor += gridView_HiddenEditor;
        }
        private void gridView_ShownEditor(object sender, EventArgs e) {
            GridView gridView = (GridView)sender;
            LookUpEdit lookup = gridView.ActiveEditor as LookUpEdit;
            if (gridView.FocusedColumn.FieldName == "ProductID" && lookup != null) {
                List<string> keys = new List<string>();
                for (int i = 0; i < gridDataTable.DefaultView.Count; i++) {
                    object key = gridDataTable.DefaultView[i].Row["ProductID"];
                    if (key != null && key != DBNull.Value) {
                        keys.Add(key.ToString());
                    }
                }
                filteredDataView = new DataView(lookupDataTable);
                lookup.Properties.DataSource = filteredDataView;
                if (keys.Count > 0) {
                    filteredDataView.RowFilter = string.Format("ProductID NOT IN( {0} )", String.Join(",", keys.ToArray()));
                }
            }
        }
        private void gridView_HiddenEditor(object sender, System.EventArgs e) {
            if (filteredDataView != null) {
                filteredDataView.Dispose();
                filteredDataView = null;
            }
        }
    }
}