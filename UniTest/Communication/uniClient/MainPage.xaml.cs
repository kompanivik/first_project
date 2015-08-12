using Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
        Task placeholder;

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
            Logger.log("Updating list");
            tList = new ObservableCollection<Task>(tl);
            TaskList.ItemsSource = tList.OrderBy(x => x.completed).OrderBy(x => x.project);

        }

        private void TaskBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Task task = new Task();
            task.completed = false;

            if(!NewDescription.Equals("") && !NewProject.Equals("")){
                task = prepareTask(task);
                api.Post(task);
            }
        }

        private Task prepareTask(Task task)
        {
            task.description = NewDescription.Text;
            task.project = NewProject.Text;
            string dateTime = NewDate.Date.ToString("yyyy-MM-dd");
            string time = NewTime.Time.ToString();
            task.duedate = dateTime + "T" + time;
            Logger.log(dateTime + "T" + time);
            return task;
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = (int)btn.Tag;
            api.Delete(id);
        }
        /// <summary>
        /// this function prepares the selected task for editing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = (int)btn.Tag;

            placeholder = tList.Where(x => x.id == id).First();

            FinalEdit.Visibility = Windows.UI.Xaml.Visibility.Visible;
            TaskBtn.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            
            NewDate.Date = placeholder.dateTime.Date;
            NewTime.Time = placeholder.dateTime.TimeOfDay;

            NewProject.Text = placeholder.project;
            NewDescription.Text = placeholder.description;

        }
        /// <summary>
        /// here we send the edited task to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinalEdit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            placeholder = prepareTask(placeholder);
            api.Put(placeholder);

            NewProject.Text = "";
            NewDescription.Text = "";

            FinalEdit.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            TaskBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;

        }

        private void FinishBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Task task = tList.Where(x => x.id == (int)btn.Tag).First();
            task.completed = true;
            api.Put(task);
        }

        private void CancelBtn_Tapped(object sender, TappedRoutedEventArgs e)
        {
            NewDescription.Text = "";
            NewProject.Text = "";
            TaskBtn.Visibility = Windows.UI.Xaml.Visibility.Visible;
            FinalEdit.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }


        
    }
}
