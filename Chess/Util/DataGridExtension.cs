using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace Chess.Util
{
    static class DataGridExtension
    {
        /// <summary>
        ///     Extension function for getting a cell of a DataGrid by an existing row, since WPF Datagrid does not seem to have one.
        /// </summary>
        public static DataGridCell GetCell(this DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        /// <summary>
        ///     Gets the currently selected row of the datagrid
        /// </summary>
        public static DataGridRow GetSelectedRow(this DataGrid grid)
        {
            return (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem);
        }

        /// <summary>
        ///     Extension function for selecting a visual child, since WPF Datagrid does not seem to have one.
        /// </summary>
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        /// <summary>
        ///     Extension function for getting the value from a cell, since WPF Datagrid does not seem to have one.
        /// </summary>
        public static object GetCellValue(DataGridCellInfo cell)
        {
            Binding binding = ((DataGridTextColumn)cell.Column).Binding as Binding;
            var boundItem = cell.Item;

            var propertyName = binding.Path.Path;
            var propInfo = boundItem.GetType().GetProperty(propertyName);
            return propInfo.GetValue(boundItem, new object[] { });
        }
    }
}
