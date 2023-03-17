using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace WpfAppWithThread
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        internal class Bacon { }
        internal class Coffee { }
        internal class Egg { }
        internal class Juice { }
        internal class Toast { }

        public int NumberOfEggs { get; set; }
        public int NumberofBacon { get; set; }
        public int NumberOfBread { get; set; }

        public static List<string> Progress = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            RefreshBinding();
           
        }
        public void MakeBreakFast()
        {
            PourCoffee();
            lstStatus.Items.Add("coffee is ready");

            Egg eggs = FryEggs(2);
            lstStatus.Items.Add("eggs are ready");

            Bacon bacon = FryBacon(3);
            lstStatus.Items.Add("bacon is ready");

            Toast toast = ToastBread(2);
            ApplyButter(toast);
            ApplyJam(toast);
            lstStatus.Items.Add("toast is ready");

            Juice oj = PourOJ();
            lstStatus.Items.Add("oj is ready");
            lstStatus.Items.Add("Breakfast is ready!");


        }

        public void MakeBreakFastAsync()
        {
            PourCoffee();
            lstStatus.Items.Add("coffee is ready");
            var eggTask = FryEggsAsync(3);
            

        }
        private Juice PourOJ()
        {
            lstStatus.Items.Add("Pouring orange juice");
            return new Juice();
        }

        public void RefreshBinding()
        {
            lstStatus.Items.Refresh();
            
        }
        private void ApplyJam(Toast toast) =>
            lstStatus.Items.Add("Putting jam on the toast");

        private void ApplyButter(Toast toast) =>
            lstStatus.Items.Add("Putting butter on the toast");

        private Toast ToastBread(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                lstStatus.Items.Add("Putting a slice of bread in the toaster");
            }
            lstStatus.Items.Add("Start toasting...");
            Task.Delay(3000).Wait();
            lstStatus.Items.Add("Remove toast from toaster");

            return new Toast();
        }

        private Bacon FryBacon(int slices)
        {
            lstStatus.Items.Add($"putting {slices} slices of bacon in the pan");
            lstStatus.Items.Add("cooking first side of bacon...");
            Task.Delay(3000).Wait();
            for (int slice = 0; slice < slices; slice++)
            {
                lstStatus.Items.Add("flipping a slice of bacon");
            }
            lstStatus.Items.Add("cooking the second side of bacon...");
            Task.Delay(3000).Wait();
            lstStatus.Items.Add("Put bacon on plate");

            return new Bacon();
        }

        private Egg FryEggs(int howMany)
        {
            lstStatus.Items.Add("Warming the egg pan...");
            Task.Delay(3000).Wait();
            lstStatus.Items.Add($"cracking {howMany} eggs");
            lstStatus.Items.Add("cooking the eggs ...");
            Task.Delay(3000).Wait();
            lstStatus.Items.Add("Put eggs on plate");

            return new Egg();
        }

        private void PourCoffee()
        {
            lstStatus.Items.Add("Pouring coffee");
           // RefreshBinding();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MakeBreakFastAsync();
        }


        private async Task<Egg> FryEggsAsync(int howMany)
        {
            lstStatus.Items.Add("Warming the egg pan...");
            await Task.Delay(10000);
            for (int i = 0; i < howMany; i++)
            {
                lstStatus.Items.Add($"cracking egg {i+1} ");
            }
           
            lstStatus.Items.Add("cooking the eggs ...");
            await Task.Delay(10000);
            lstStatus.Items.Add("Put eggs on plate");

            lstStatus.Items.Add("eggs are ready");
            return new Egg();
        }

    }
}
