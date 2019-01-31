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
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinPE_Creation_Tool
{
    public partial class Wizard : Form
    {
        public Wizard()
        {
            InitializeComponent();
        }

        string strChoosed;

        private void Wizard_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            this.Hide();

            form1.ShowDialog();
        }

        private void Wizard_Load(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(@"C:\Program Files (x86)\Windows Kits\10\Assessment and Deployment Kit"))
                {
                    rbAMD64.Checked = true;

                    ManagementObjectCollection drives = new ManagementObjectSearcher("SELECT Caption, DeviceID FROM Win32_DiskDrive WHERE InterfaceType='USB'").Get();

                    cbxUSB.ItemHeight = drives.Count;

                    foreach (ManagementObject drive in drives)

                    {

                        // browse all USB WMI physical disks

                        foreach (ManagementObject partition in new ManagementObjectSearcher(

                        "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='"

                        + drive["DeviceID"]

                        + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())

                        {

                            // browse all USB WMI physical disks

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
                    MessageBox.Show(@"You need to install Windows ADK to use WinPE Creation Tool.\n\nhttps://docs.microsoft.com/en-us/windows-hardware/get-started/adk-install");

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
            }
            catch
            {
                if (cbxUSB.Height == 0)
                {
                    MessageBox.Show("No USB devices are connected to your computer.");
                }
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnGo.Enabled = false;

                if (rbAMD64.Checked == true)
                {
                    strChoosed = "amd64";

                    Process procGenererPE = new Process();
                    procGenererPE.StartInfo.FileName = "cmd";
                    procGenererPE.StartInfo.Verb = "runas";
                    //procGenererPE.StartInfo.WorkingDirectory = @"C:\Program Files (x86)\Windows Kits\10\Assessment and Deployment Kit\Windows Preinstallation Environment";
                    procGenererPE.StartInfo.Arguments = "/k " + Directory.GetCurrentDirectory() + "\\copype.cmd amd64" + " C:\\WinPE_amd64 & exit";
                    procGenererPE.StartInfo.ErrorDialog = true;
                    procGenererPE.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    procGenererPE.Start();
                    procGenererPE.WaitForExit();
                }

                if (rbX86.Checked == true)
                {
                    strChoosed = "x86";

                    Process procGenererPE = new Process();
                    procGenererPE.StartInfo.FileName = "cmd";
                    procGenererPE.StartInfo.Verb = "runas";
                    //procGenererPE.StartInfo.WorkingDirectory = @"C:\Program Files (x86)\Windows Kits\10\Assessment and Deployment Kit\Windows Preinstallation Environment";
                    procGenererPE.StartInfo.Arguments = "/k " + Directory.GetCurrentDirectory() + "\\copype.cmd x86" + " C:\\WinPE_x86 & exit";
                    procGenererPE.StartInfo.ErrorDialog = true;
                    procGenererPE.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    procGenererPE.Start();
                    procGenererPE.WaitForExit();
                }

                if (rbARM.Checked == true)
                {
                    strChoosed = "arm";

                    Process procGenererPE = new Process();
                    procGenererPE.StartInfo.FileName = "cmd";
                    procGenererPE.StartInfo.Verb = "runas";
                    //procGenererPE.StartInfo.WorkingDirectory = @"C:\Program Files (x86)\Windows Kits\10\Assessment and Deployment Kit\Windows Preinstallation Environment";
                    procGenererPE.StartInfo.Arguments = "/k " + Directory.GetCurrentDirectory() + "\\copype.cmd arm" + " C:\\WinPE_arm & exit";
                    procGenererPE.StartInfo.ErrorDialog = true;
                    procGenererPE.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    procGenererPE.Start();
                    procGenererPE.WaitForExit();
                }

                if (Directory.Exists("C:\\WinPE_" + strChoosed))
                {
                    if (tbxLCID.Text != "")
                    {
                        Process procOuvrirWIM = new Process();
                        procOuvrirWIM.StartInfo.FileName = "cmd";
                        procOuvrirWIM.StartInfo.Verb = "runas";
                        procOuvrirWIM.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procOuvrirWIM.StartInfo.Arguments = @"/k Dism /Mount-Image /ImageFile:C:\WinPE_" + strChoosed + @"\media\sources\boot.wim /index:1 /MountDir:C:\WinPE_" + strChoosed + @"\mount & exit";
                        procOuvrirWIM.StartInfo.ErrorDialog = true;
                        procOuvrirWIM.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procOuvrirWIM.Start();
                        procOuvrirWIM.WaitForExit();

                        Process procSysLocale = new Process();
                        procSysLocale.StartInfo.FileName = "cmd";
                        procSysLocale.StartInfo.Verb = "runas";
                        procSysLocale.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procSysLocale.StartInfo.Arguments = "/k Dism /image:C:\\WinPE_" + strChoosed + "\\mount /Set-SysLocale:" + tbxLCID.Text + " & exit";
                        procSysLocale.StartInfo.ErrorDialog = true;
                        procSysLocale.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procSysLocale.Start();
                        procSysLocale.WaitForExit();

                        Process procInputLocale = new Process();
                        procInputLocale.StartInfo.FileName = "cmd";
                        procInputLocale.StartInfo.Verb = "runas";
                        procInputLocale.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procInputLocale.StartInfo.Arguments = "/k Dism /image:C:\\WinPE_" + strChoosed + "\\mount /Set-InputLocale:" + tbxLCID.Text + " & exit";
                        procInputLocale.StartInfo.ErrorDialog = true;
                        procInputLocale.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procInputLocale.Start();
                        procInputLocale.WaitForExit();

                        Process procUserLocale = new Process();
                        procUserLocale.StartInfo.FileName = "cmd";
                        procUserLocale.StartInfo.Verb = "runas";
                        procUserLocale.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procUserLocale.StartInfo.Arguments = "/k Dism /image:C:\\WinPE_" + strChoosed + "\\mount /Set-UserLocale:" + tbxLCID.Text + " & exit";
                        procUserLocale.StartInfo.ErrorDialog = true;
                        procUserLocale.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procUserLocale.Start();
                        procUserLocale.WaitForExit();

                        Process procFermerWIM = new Process();
                        procUserLocale.StartInfo.FileName = "cmd";
                        procUserLocale.StartInfo.Verb = "runas";
                        procUserLocale.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procUserLocale.StartInfo.Arguments = "/k Dism /Unmount-Image /MountDir:C:\\WinPE_" + strChoosed + "\\mount /commit & exit";
                        procUserLocale.StartInfo.ErrorDialog = true;
                        procUserLocale.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procUserLocale.Start();
                        procUserLocale.WaitForExit();
                    }
                }

                if (Directory.Exists("C:\\WinPE_" + strChoosed))
                {
                    if (cbxCreateUSB.Checked == true)
                    {
                        if (cbxUSB.Text != "")
                        {
                            Process procDeplacerFichier = new Process();
                            procDeplacerFichier.StartInfo.FileName = "cmd";
                            procDeplacerFichier.StartInfo.Verb = "runas";
                            procDeplacerFichier.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                            procDeplacerFichier.StartInfo.Arguments = "/k copy " + Directory.GetCurrentDirectory() + @"\bootsect.exe C:\Windows\System32\ & exit";
                            procDeplacerFichier.StartInfo.ErrorDialog = true;
                            procDeplacerFichier.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procDeplacerFichier.Start();
                            procDeplacerFichier.WaitForExit();

                            Process procCreerUSB = new Process();
                            procCreerUSB.StartInfo.FileName = "cmd";
                            procCreerUSB.StartInfo.Verb = "runas";
                            procCreerUSB.StartInfo.Arguments = "/k " + Directory.GetCurrentDirectory() + "\\MakeWinPEMedia.cmd /UFD C:\\WinPE_" + strChoosed + " " + cbxUSB.Text + " & exit";
                            procCreerUSB.StartInfo.ErrorDialog = true;
                            procCreerUSB.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procCreerUSB.Start();
                            procCreerUSB.WaitForExit();
                        }
                    }
                }

                if (Directory.Exists("C:\\WinPE_" + strChoosed))
                {
                    if (cbxGenerateISO.Checked == true)
                    {
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
                        procGenereISO.StartInfo.Arguments = "/k " + Directory.GetCurrentDirectory() + "\\MakeWinPEMedia.cmd /iso C:\\WinPE_" + strChoosed + " C:\\WinPe_" + strChoosed + "\\WinPE.iso & exit";
                        procGenereISO.StartInfo.ErrorDialog = true;
                        procGenereISO.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procGenereISO.Start();
                        procGenereISO.WaitForExit();
                    }
                }

                Cursor.Current = Cursors.Default;
                btnGo.Enabled = true;

                MessageBox.Show("All operations were successfully completed.", "Success", MessageBoxButtons.OK);
            }
            catch
            {
                MessageBox.Show("An unexpected error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxUSB_DropDown(object sender, EventArgs e)
        {
            try
            {
                cbxUSB.Items.Clear();

                ManagementObjectCollection drives = new ManagementObjectSearcher("SELECT Caption, DeviceID FROM Win32_DiskDrive WHERE InterfaceType='USB'").Get();

                cbxUSB.ItemHeight = drives.Count;

                foreach (ManagementObject drive in drives)

                {

                    // browse all USB WMI physical disks

                    foreach (ManagementObject partition in new ManagementObjectSearcher(

                    "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='"

                    + drive["DeviceID"]

                    + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())

                    {

                        // browse all USB WMI physical disks

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
    }
}
