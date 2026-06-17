# How to Use Tab Key to Focus Editor in MAUI SfDataGrid Column?
This demo shows how to set focus to the editor when pressing the Tab key in a DataGridTemplateColumn in the [.NET MAUI DataGrid]((https://help.syncfusion.com/maui/datagrid/overview)) (SfDataGrid).
 By default, the Tab key only moves the current cell, so this approach demonstrates how to customize the behavior to automatically focus the editor control, enabling smoother and more efficient keyboard-based data entry.

## Xaml.cs
```
 public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        DataGrid.CellRenderers.Remove("Template");
        DataGrid.CellRenderers.Add("Template", new CustomDataGridTemplateRenderer());

        DataGrid.CellRenderers.Remove("Text");
        DataGrid.CellRenderers.Add("Text", new CustomDataGridTextBoxRenderer());

        DataGrid.SelectionController = new CustomSelectionController(DataGrid);
    }

}

```

## CustomSelectionController.cs
```
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
```
## Requirements to run the demo

To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements)

## Troubleshooting:
### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

## License

Syncfusion� has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion� liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion�'s samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion�'s samples.