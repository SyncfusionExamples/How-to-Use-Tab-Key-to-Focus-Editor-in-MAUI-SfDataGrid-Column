using Syncfusion.Maui.DataGrid;

namespace SfDataGridSample
{
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
}
