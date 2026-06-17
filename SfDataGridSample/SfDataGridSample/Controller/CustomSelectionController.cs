using Syncfusion.Maui.Core.Internals;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.GridCommon.ScrollAxis;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfDataGridSample
{
    public class CustomSelectionController : DataGridRowSelectionController
    {
        public CustomSelectionController(SfDataGrid dataGrid) : base(dataGrid)
        { }

        bool HasNavigation = false;

        protected override async void ProcessKeyDown(KeyEventArgs args, bool isCtrlKeyPressed, bool isShiftKeyPressed)
        {
            if (DataGrid == null || DataGrid.View == null)
                return;

            if (args.Key == KeyboardKey.Tab)
            {
                if (HasNavigation)
                    return;

                HasNavigation = true;

                var firstColumnIndex = 0;
                var lastColumnIndex = firstColumnIndex + DataGrid.Columns.Count - 1;
                var firstRowIndex = 1;
                var lastRowIndex = firstRowIndex + DataGrid.View.Records.Count - 1;

                var currentRowIndex = this.DataGrid.CurrentCellManager.RowColumnIndex.RowIndex;
                var currentColumnIndex = this.DataGrid.CurrentCellManager.RowColumnIndex.ColumnIndex;

                if (currentColumnIndex == lastColumnIndex && !isShiftKeyPressed)
                {
                    if (currentRowIndex == lastRowIndex)
                    {
                        return;
                    }
                    else
                    {
                        currentColumnIndex = firstColumnIndex;
                        currentRowIndex++;
                    }
                }
                else if (currentColumnIndex == firstColumnIndex && isShiftKeyPressed)
                {
                    if (currentRowIndex == firstRowIndex)
                    {
                        return;
                    }
                    else
                    {
                        currentColumnIndex = lastColumnIndex;
                        currentRowIndex--;
                    }
                }
                else
                {
                    currentColumnIndex += isShiftKeyPressed ? -1 : 1;
                }

                DataGrid.EndEdit();

                await DataGrid.ScrollToRowColumnIndex(currentRowIndex, currentColumnIndex, scrollToRowPosition: ScrollToPosition.End, scrollToColumnPosition: ScrollToPosition.Center, canAnimate: true);

                await Task.Delay(200);

                DataGrid.MoveCurrentCellTo(new RowColumnIndex(currentRowIndex, currentColumnIndex));

                await Task.Delay(50);

                DataGrid.BeginEdit(currentRowIndex, currentColumnIndex);

                HasNavigation = false;
            }
            else
            {
                base.ProcessKeyDown(args, isCtrlKeyPressed, isShiftKeyPressed);
            }
        }
    }

}
