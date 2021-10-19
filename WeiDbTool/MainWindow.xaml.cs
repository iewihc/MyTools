using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeiDbTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // const string a = "dotnet ef dbcontext scaffold \"Server=.;Database=EverRichEC;Trusted_Connection=True;\" Microsoft.EntityFrameworkCore.SqlServer -o Models\\EC -c EverRichECContext --no-pluralize -f -t ";
        private void ImagePanel_Drop(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                var ret = HandleFileOpen(files[0]);
                this.tb1.Text = cmd+string.Join(" -t ", ret);
            }
        }

        private List<string> HandleFileOpen(string v)
        {
            
            var text = System.IO.File.ReadAllText(v);
            var pattern = "(?<=DbSet<).*(?=>)";
            var matches = Regex.Matches(text, pattern);

            
            var ret = new List<string>();
            foreach (var match in matches)
            {
                
                
                var strPrice = match.ToString();
                // strPrice =strPrice.Replace("DbSet<", "");
                // strPrice = strPrice.Replace(">", "");
                ret.Add(strPrice);               
            }
            return ret;
        }


        public string SelectedItem { get; set; }

        public string Path { get; set; }

        public string cmd { get; set; } ="dotnet ef dbcontext scaffold \"Server=.;Database=EverRichEC;Trusted_Connection=True;\" Microsoft.EntityFrameworkCore.SqlServer -o Models\\EC -c EverRichECContext --no-pluralize -f -t";

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            const string text = @"D:\proj\everrich-ec-backend\EverRich.EC\Services\Goods\EverRich.EC.Services.Goods\Models\EC\EverRichECContext.cs";
            try
            {
                this.SelectedItem = ((ComboBoxItem)e.AddedItems[0])?.Content.ToString();
                this.Path = text.Replace("Goods", SelectedItem);
                var ret = HandleFileOpen(this.Path);
                this.tb1.Text = cmd+string.Join(" -t ", ret);

            }
            catch (Exception ex)
            {
             
            }
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (rb1.IsChecked == true)
            {
                cmd = "dotnet ef dbcontext scaffold \"Server=.;Database=EverRichEC;Trusted_Connection=True;\" Microsoft.EntityFrameworkCore.SqlServer -o Models\\EC -c EverRichECContext --no-pluralize -f -t ";
            }  
            if (rb2.IsChecked == true)
            {
                cmd = "dotnet ef dbcontext scaffold \"Server=.;Database=EverRichEC;Trusted_Connection=True;\" Microsoft.EntityFrameworkCore.SqlServer -o Models\\User -c EverRichECUserContext --no-pluralize -f -t ";
            }

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(tb2.Text);
            var g = tb2.Text.Replace("[", "");
            g = g.Replace("]", "");
            g = g.Replace(":", "=");
            g = g.Replace("\"", "");

            tb2.Text = g;

        }
    }
}
