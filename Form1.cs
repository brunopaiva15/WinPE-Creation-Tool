/*
 *  Copyright (C) 2019 Bruno Paiva
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along
 * with this program; if not, write to the Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace WinPE_Creation_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Before using this utility, make sure you have the deployment tools and the WinPE pre-installation environment.\n\nNote : It's essential to have Windows 10 installed on this computer in order for this utility to work.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (Directory.Exists(@"C:\Program Files (x86)\Windows Kits\10\Assessment and Deployment Kit")) {
                cbxArchitecture.SelectedItem = "amd64";
                ManagementObjectCollection drives = new ManagementObjectSearcher("SELECT Caption, DeviceID FROM Win32_DiskDrive WHERE InterfaceType='USB'").Get();
                cbxUSB.ItemHeight = drives.Count;

                foreach (ManagementObject drive in drives) {
                    foreach (ManagementObject partition in new ManagementObjectSearcher(
                    "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='"
                    + drive["DeviceID"]
                    + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                    {
                        foreach (ManagementObject disk in new ManagementObjectSearcher(
                        "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
                        + partition["DeviceID"]
                        + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                        {
                            cbxUSB.Items.Add(disk["CAPTION"].ToString());
                        }
                    }
                }
            }
                else
                {
                 MessageBox.Show(@"WARNING. You need to install Windows ADK to use WinPE Creation Tool. https://docs.microsoft.com/en-us/windows-hardware/get-started/adk-install");
            }
        }
            catch
            {
                if (cbxUSB.Height == 0)
                {
                    MessageBox.Show("No USB devices are connected to your computer.");
                }
            }
        }

        private void btnGenererPE_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                btnGenererPE.Text = "Generation in progress";
                btnGenererPE.Enabled = false;
                btnGenererISO.Enabled = false;
                btnCreerCleBootable.Enabled = false;
                btnChangeLanguage.Enabled = false;

                Process procGenererPE = new Process();
                procGenererPE.StartInfo.FileName = "cmd";
                procGenererPE.StartInfo.Verb = "runas";
                //procGenererPE.StartInfo.WorkingDirectory = @"C:\Program Files (x86)\Windows Kits\10\Assessment and Deployment Kit\Windows Preinstallation Environment";
                procGenererPE.StartInfo.Arguments = "/k " + Directory.GetCurrentDirectory() + "\\copype.cmd " + cbxArchitecture.Text + " C:\\WinPE_" + cbxArchitecture.Text + " & exit";
                procGenererPE.StartInfo.ErrorDialog = true;
                procGenererPE.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                procGenererPE.Start();
                procGenererPE.WaitForExit();

                btnGenererPE.Text = "Generate WinPE directory";
                btnGenererPE.Enabled = true;
                btnGenererISO.Enabled = true;
                btnCreerCleBootable.Enabled = true;
                btnChangeLanguage.Enabled = true;

                Cursor.Current = Cursors.Default;

                MessageBox.Show("The WinPE directory was successfully created at the location : \"C:\\WinPE_" + cbxArchitecture.Text + "\".");
            }
            catch
            {
                MessageBox.Show("An unexpected error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DialogResult form1 = MessageBox.Show("Are you sure you want to leave the utility ?", "Exit", MessageBoxButtons.YesNo);

                if (form1 == DialogResult.Yes)
                {
                    System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("cmd");
                    foreach (System.Diagnostics.Process p in process)
                    {
                        if (!string.IsNullOrEmpty(p.ProcessName))
                        {
                            p.Kill();
                        }
                    }

                    System.Diagnostics.Process[] process1 = System.Diagnostics.Process.GetProcessesByName("Dism");
                    foreach (System.Diagnostics.Process p in process1)
                    {
                        if (!string.IsNullOrEmpty(p.ProcessName))
                        {
                            p.Kill();
                        }
                    }

                    Process.GetCurrentProcess().Kill();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch
            {
                MessageBox.Show("Error when closing the program.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenererISO_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists("C:\\WinPE_" + cbxArchitecture.Text))
                {
                    Cursor.Current = Cursors.WaitCursor;

                    btnGenererISO.Text = "Generation in progress";
                    btnGenererPE.Enabled = false;
                    btnGenererISO.Enabled = false;
                    btnCreerCleBootable.Enabled = false;
                    btnChangeLanguage.Enabled = false;

                    Process procDeplacerFichier = new Process();
                    procDeplacerFichier.StartInfo.FileName = "cmd";
                    procDeplacerFichier.StartInfo.Verb = "runas";
                    procDeplacerFichier.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                    procDeplacerFichier.StartInfo.Arguments = "/k copy " + Directory.GetCurrentDirectory() + @"\oscdimg.exe C:\Windows\System32\ & exit";
                    procDeplacerFichier.StartInfo.ErrorDialog = true;
                    procDeplacerFichier.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    procDeplacerFichier.Start();
                    procDeplacerFichier.WaitForExit();

                    Process procGenereISO = new Process();
                    procGenereISO.StartInfo.FileName = "cmd";
                    procGenereISO.StartInfo.Verb = "runas";
                    procGenereISO.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                    procGenereISO.StartInfo.Arguments = "/k " + Directory.GetCurrentDirectory() + "\\MakeWinPEMedia.cmd /iso C:\\WinPE_" + cbxArchitecture.Text + " C:\\WinPe_" + cbxArchitecture.Text + "\\WinPE.iso & exit";
                    procGenereISO.StartInfo.ErrorDialog = true;
                    procGenereISO.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    procGenereISO.Start();
                    procGenereISO.WaitForExit();

                    btnGenererISO.Text = "Generate ISO";
                    btnGenererPE.Enabled = true;
                    btnGenererISO.Enabled = true;
                    btnCreerCleBootable.Enabled = true;
                    btnChangeLanguage.Enabled = true;

                    Cursor.Current = Cursors.Default;

                    MessageBox.Show("The ISO file was successfully created at the location : \"C:\\WinPE_" + cbxArchitecture.Text + "\".");
                }
                else
                {
                    MessageBox.Show("Please create a WinPE directory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("An unexpected error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreerCleBootable_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists("C:\\WinPE_" + cbxArchitecture.Text))
                {
                    if (cbxUSB.Text != "")
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        Process procDeplacerFichier = new Process();
                        procDeplacerFichier.StartInfo.FileName = "cmd";
                        procDeplacerFichier.StartInfo.Verb = "runas";
                        procDeplacerFichier.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procDeplacerFichier.StartInfo.Arguments = "/k copy " + Directory.GetCurrentDirectory() + @"\bootsect.exe C:\Windows\System32\ & exit";
                        procDeplacerFichier.StartInfo.ErrorDialog = true;
                        procDeplacerFichier.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procDeplacerFichier.Start();
                        procDeplacerFichier.WaitForExit();

                        btnCreerCleBootable.Text = "Creation in progress";
                        btnGenererPE.Enabled = false;
                        btnGenererISO.Enabled = false;
                        btnCreerCleBootable.Enabled = false;
                        btnChangeLanguage.Enabled = false;

                        Process procCreerUSB = new Process();
                        procCreerUSB.StartInfo.FileName = "cmd";
                        procCreerUSB.StartInfo.Verb = "runas";
                        procCreerUSB.StartInfo.Arguments = "/k " + Directory.GetCurrentDirectory() + "\\MakeWinPEMedia.cmd /UFD C:\\WinPE_" + cbxArchitecture.Text + " " + cbxUSB.Text + " & exit";
                        procCreerUSB.StartInfo.ErrorDialog = true;
                        procCreerUSB.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procCreerUSB.Start();
                        procCreerUSB.WaitForExit();

                        btnCreerCleBootable.Text = "Create bootable USB";
                        btnGenererPE.Enabled = true;
                        btnGenererISO.Enabled = true;
                        btnCreerCleBootable.Enabled = true;
                        btnChangeLanguage.Enabled = true;

                        Cursor.Current = Cursors.Default;

                        MessageBox.Show("The USB stick is now ready for use.");
                    }
                    else
                    {
                        MessageBox.Show("Please select a USB device.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please create a WinPE directory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("An unexpected error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxUSB_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbxUSB_DropDown(object sender, EventArgs e)
        {
            try
            {
                cbxUSB.Items.Clear();
                
                ManagementObjectCollection drives = new ManagementObjectSearcher("SELECT Caption, DeviceID FROM Win32_DiskDrive WHERE InterfaceType='USB'").Get();
                
                cbxUSB.ItemHeight = drives.Count;
                
                foreach (ManagementObject drive in drives) {
                    foreach (ManagementObject partition in new ManagementObjectSearcher(
                    "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='"
                    + drive["DeviceID"]
                    + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                    {
                        foreach (ManagementObject disk in new ManagementObjectSearcher(
                        "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
                        + partition["DeviceID"]
                        + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                        {
                            cbxUSB.Items.Add(disk["CAPTION"].ToString());
                        }
                    }
                }
            }
            catch
            {
                if (cbxUSB.Height == 0)
                {
                    MessageBox.Show("No USB devices are connected to your computer.");
                }
            }
        }

        private void btnChangeLanguage_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists("C:\\WinPE_" + cbxArchitecture.Text))
                {
                    if (tbxLCID.Text != "")
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        btnChangeLanguage.Text = "Change in progress";
                        btnGenererPE.Enabled = false;
                        btnGenererISO.Enabled = false;
                        btnCreerCleBootable.Enabled = false;
                        btnChangeLanguage.Enabled = false;

                        Process procOuvrirWIM = new Process();
                        procOuvrirWIM.StartInfo.FileName = "cmd";
                        procOuvrirWIM.StartInfo.Verb = "runas";
                        procOuvrirWIM.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procOuvrirWIM.StartInfo.Arguments = @"/k Dism /Mount-Image /ImageFile:C:\WinPE_" + cbxArchitecture.Text + @"\media\sources\boot.wim /index:1 /MountDir:C:\WinPE_" + cbxArchitecture.Text + @"\mount & exit";
                        procOuvrirWIM.StartInfo.ErrorDialog = true;
                        procOuvrirWIM.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procOuvrirWIM.Start();
                        procOuvrirWIM.WaitForExit();

                        Process procSysLocale = new Process();
                        procSysLocale.StartInfo.FileName = "cmd";
                        procSysLocale.StartInfo.Verb = "runas";
                        procSysLocale.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procSysLocale.StartInfo.Arguments = "/k Dism /image:C:\\WinPE_" + cbxArchitecture.Text + "\\mount /Set-SysLocale:" + tbxLCID.Text + " & exit";
                        procSysLocale.StartInfo.ErrorDialog = true;
                        procSysLocale.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procSysLocale.Start();
                        procSysLocale.WaitForExit();

                        Process procInputLocale = new Process();
                        procInputLocale.StartInfo.FileName = "cmd";
                        procInputLocale.StartInfo.Verb = "runas";
                        procInputLocale.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procInputLocale.StartInfo.Arguments = "/k Dism /image:C:\\WinPE_" + cbxArchitecture.Text + "\\mount /Set-InputLocale:" + tbxLCID.Text + " & exit";
                        procInputLocale.StartInfo.ErrorDialog = true;
                        procInputLocale.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procInputLocale.Start();
                        procInputLocale.WaitForExit();

                        Process procUserLocale = new Process();
                        procUserLocale.StartInfo.FileName = "cmd";
                        procUserLocale.StartInfo.Verb = "runas";
                        procUserLocale.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procUserLocale.StartInfo.Arguments = "/k Dism /image:C:\\WinPE_" + cbxArchitecture.Text + "\\mount /Set-UserLocale:" + tbxLCID.Text + " & exit";
                        procUserLocale.StartInfo.ErrorDialog = true;
                        procUserLocale.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procUserLocale.Start();
                        procUserLocale.WaitForExit();

                        Process procFermerWIM = new Process();
                        procUserLocale.StartInfo.FileName = "cmd";
                        procUserLocale.StartInfo.Verb = "runas";
                        procUserLocale.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procUserLocale.StartInfo.Arguments = "/k Dism /Unmount-Image /MountDir:C:\\WinPE_" + cbxArchitecture.Text + "\\mount /commit & exit";
                        procUserLocale.StartInfo.ErrorDialog = true;
                        procUserLocale.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procUserLocale.Start();
                        procUserLocale.WaitForExit();

                        btnChangeLanguage.Text = "Change keyboard language";
                        btnGenererPE.Enabled = true;
                        btnGenererISO.Enabled = true;
                        btnCreerCleBootable.Enabled = true;
                        btnChangeLanguage.Enabled = true;

                        Cursor.Current = Cursors.Default;

                        MessageBox.Show("The language has been modified.");
                    }
                    else
                    {
                        MessageBox.Show("Please enter a LCID string.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please create a WinPE directory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("An unexpected error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Windows Language Code Identifier. For example : fr-FR, en-US, de-DE...");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Wizard wizard = new Wizard();

            this.Hide();

            wizard.ShowDialog();
        }
    }
}
