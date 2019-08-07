﻿using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using static PicView.lib.Variables;

namespace PicView.lib
{
    internal static class Helper
    {
        /// <summary>
        /// Greatest Common Divisor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal static int GCD(int x, int y)
        {
            return y == 0 ? x : GCD(y, x % y);
        }

        internal static double GetAspectRatio(bool windowstyle, double width, double height, double monitorSizeWidth, double monitorSizeHeight)
        {
            double maxWidth, maxHeight;
            var interfaceHeight = 93; // TopBar + LowerBar height

            if (windowstyle)
            {
                // Get max width and height, based on user's screen
                maxWidth = Math.Min(monitorSizeWidth - ComfySpace, width);
                maxHeight = Math.Min((monitorSizeHeight - interfaceHeight), height);
            }
            else
            {
                // Get max width and height, based on window size
                maxWidth = Math.Min(monitorSizeWidth, width);
                maxHeight = Math.Min(monitorSizeHeight - interfaceHeight, height);
            }

            return Math.Min((maxWidth / width), (maxHeight / height));
        } 

        /// <summary>
        /// Show progress on taskbar
        /// </summary>
        /// <param name="i">index</param>
        /// <param name="ii">size</param>
        internal static void Progress(int i, int ii)
        {
            TaskbarManager prog = TaskbarManager.Instance;
            prog.SetProgressState(TaskbarProgressBarState.Normal);
            prog.SetProgressValue(i, ii);
        }

        /// <summary>
        /// Stop showing taskbar progress, return to default
        /// </summary>
        internal static void NoProgress()
        {
            TaskbarManager prog = TaskbarManager.Instance;
            prog.SetProgressState(TaskbarProgressBarState.NoProgress);
        }

        /// <summary>
        /// Returns a Windows Thumbnail
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns></returns>
        internal static System.Windows.Media.Imaging.BitmapSource GetWindowsThumbnail(string path, bool extralarge = false)
        {
            if (!File.Exists(path))
                return null;

            if (extralarge)
                return Microsoft.WindowsAPICodePack.Shell.ShellFile.FromFilePath(path).Thumbnail.ExtraLargeBitmapSource;

            return Microsoft.WindowsAPICodePack.Shell.ShellFile.FromFilePath(path).Thumbnail.BitmapSource;
        }

        /// <summary>
        /// Sends the file to Windows print system
        /// </summary>
        /// <param name="path">The file path</param>
        internal static void Print(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return;

            var p = new Process();
            p.StartInfo.FileName = path;
            p.StartInfo.Verb = "print";
            p.Start();
        }

        internal static void SetWindowBorderColor()
        {
            if (Properties.Settings.Default.WindowBorderColorEnabled)
            {
                Application.Current.Resources["ChosenColor"] = AnimationHelper.GetPrefferedColorOver();
                var bgBrush = Application.Current.Resources["WindowBackgroundColorBrush"] as System.Windows.Media.SolidColorBrush;
                bgBrush.Color = AnimationHelper.GetPrefferedColorOver();
            }
        }
     

    }
}
