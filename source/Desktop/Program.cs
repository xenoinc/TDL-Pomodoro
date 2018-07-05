﻿/* Copyright Xeno Innovations, Inc. 2017
 * Author:  Damian Suess
 * Date:    2017-5-23
 * File:    Program.cs
 * Description:
 *  Entry point
 *
 * Change Log:
 *  2017-0523 * Initial creation
 */

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Squirrel;

namespace Pomodoro
{
  internal static class Program
  {


    [STAThread]
    private static void Main()
    {
      // Method 1
      Task.Run(async () =>
        {
          //using (var mgr = new UpdateManager(UpdatePath, "Pomodoro"))
          using (var mgr = new UpdateManager(Constants.UpdatePath))
          {
            await mgr.UpdateApp();
          }
        });

      // Method 2.A
      SquirrelAwareApp.HandleEvents(
        onAppUpdate: UpdaterForm.OnAppUpdate,
        onAppUninstall: UpdaterForm.OnAppUninstall,
        onInitialInstall: UpdaterForm.OnInitialInstall);

      // Method 2.B
      SquirrelAwareApp.HandleEvents(ver =>
      {
        var mggr = default(UpdateManager);
        mggr.CreateShortcutsForExecutable("exeName", ShortcutLocation.Desktop, true);
      });

      Application.Run(new PomodoroTimer());
    }
  }
}
