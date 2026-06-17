using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfDataGridSample
{
    public class CustomDataGridTemplateRenderer : DataGridCellTemplateRenderer
    {
        protected override void SetFocus(Microsoft.Maui.Controls.View view, bool needToFocus)
        {
            base.SetFocus(view, needToFocus);
            if (!needToFocus || view == null)
                return;

            var editElement = view is DataGridCell cell ? cell.Content : view;
            if (editElement == null)
                return;
            _ = FocusEditElementAsync(editElement);
        }

        async Task FocusEditElementAsync(Microsoft.Maui.Controls.View editElement)
        {
            try
            {
                await Task.Delay(50);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (editElement is SfMaskedEntry maskedEntry)
                    {
                        if (maskedEntry.Children.Count > 0 && maskedEntry.Children[0] is Entry innerEntry)
                            innerEntry.Focus();
                        else
                            maskedEntry.Focus();
                    }
                    else if (editElement is SfComboBox comboBox)
                    {
                        comboBox.Focus();
                    }
                    else if (editElement is Entry entry)
                    {
                        entry.Focus();
                    }
                });
            }
            catch { }
        }
    }

}
