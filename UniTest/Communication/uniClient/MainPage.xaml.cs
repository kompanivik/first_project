using Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace uniClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<Task> tList;
        APIComm api;
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            api = new APIComm();
            tList = new ObservableCollection<Task>();
            api.updateTaskList += api_updateTaskList;
            TaskList.ItemsSource = tList;
            api.GetAllTasks();
        }

        void api_updateTaskList(List<Task> tl)
        {

            ObservableCollection<Task> list = new ObservableCollection<Task>(tl);
            Logger.log(list[0].description);
            tList = list;
            TaskList.ItemsSource = tList;
            TaskList.UpdateLayout();
        }

        private void TaskBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Task task = new Task();
            task.completed = false;

            if(!NewDescription.Equals("") && !NewProject.Equals("")){
                task.description = NewDescription.Text;
                task.project = NewProject.Text;
                string dateTime = NewDate.Date.ToString("yyyy-MM-dd");
                string time = NewTime.Time.ToString();
                task.duedate = dateTime + "T" + time;
                Logger.log(dateTime + "T" + time);
                api.Post(task);
            }
        }
    }
}
