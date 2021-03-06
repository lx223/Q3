﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Q3Client
{
    class DisplayTimer
    {
        private QueueList targetWindow;
        private QueueList.eWindowStateExtended previousState;
        private volatile bool cancel;

        public DisplayTimer(QueueList targetWindow)
        {
            this.targetWindow = targetWindow;
            targetWindow.GotFocus += (s, e) => cancel = true;
        }

        public async void ShowAlert(bool leaveInForeground = false)
        {
            if (!targetWindow.IsActive)
            {
                cancel = false;

                Action reset;
                DateTime startTime = DateTime.Now;

                targetWindow.Topmost = true;

                if (targetWindow.WindowStateExtended != QueueList.eWindowStateExtended.Normal)
                {
                    previousState = targetWindow.WindowStateExtended;
                    targetWindow.WindowStateExtended = QueueList.eWindowStateExtended.Normal;
                    reset = () => targetWindow.WindowStateExtended = previousState;
                }
                else
                {
                    Win32.BringToFront(targetWindow);
                    reset = () => Win32.SendToBack(targetWindow);
                }

                while (startTime > IdleTimer.GetLastInputTime())
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                await Task.Delay(TimeSpan.FromSeconds(4));

                targetWindow.Topmost = false;

                if (!cancel && !targetWindow.IsActive && !leaveInForeground && !DataCache.Load<UserConfig>().PersistentNewQueueNotifications)
                {
                    reset();
                }

            }
        }
    }
}
