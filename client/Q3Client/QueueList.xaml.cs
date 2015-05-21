﻿using System;
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
using System.Windows.Shapes;

namespace Q3Client
{
    /// <summary>
    /// Interaction logic for QueueList.xaml
    /// </summary>
    public partial class QueueList : Window
    {
        public QueueList(Hub hub)
        {
            InitializeComponent();
            Header.Hub = hub;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.Left = SystemParameters.WorkArea.Right - this.Width;
        }

        private void ShowQueuesClicked(object sender, RoutedEventArgs e)
        {
            this.Show();
            this.Activate();
        }

        private void StartQueueClicked(object sender, RoutedEventArgs e)
        {
            Header.NewQueueClicked(sender, e);
        }

        private void QuitClicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
