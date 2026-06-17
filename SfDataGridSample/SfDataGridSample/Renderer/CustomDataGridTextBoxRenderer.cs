using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Text;

namespace SfDataGridSample
{
    public class CustomDataGridTextBoxRenderer : DataGridTextBoxCellRenderer
    {
        protected override void SetFocus(Microsoft.Maui.Controls.View view, bool needToFocus)
        {
            base.SetFocus(view, needToFocus);
            if (!needToFocus || view == null)
                return;
            _ = FocusEditElementAsync(view);
        }

        async Task FocusEditElementAsync(Microsoft.Maui.Controls.View view)
        {
            try
            {
                await Task.Delay(30);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (!view.IsFocused && view.IsEnabled && view.IsVisible)
                        view.Focus();
                });
            }
            catch { }
        }
    }

}
