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
            MessageBox.Show("Before using this utility, make sure you have the deployment tools and the WinPE pre-installation environment.\n\nNote : It's necessary to have Windows 10 installed on this computer in order for this utility to work.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
                if (Directory.Exists(@"C:\Program Files (x86)\Windows Kits\10\Assessment and Deployment Kit"))
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

                    if (tbxLCID.Text != "" || cbxDrivers.Checked == true)
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
                    }

                    if (Directory.Exists("C:\\WinPE_" + strChoosed))
                    {
                        if (tbxLCID.Text != "")
                        {
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
                        if (cbxScript.Checked == true)
                        {
                            Process procCreerRepertoireScript = new Process();
                            procCreerRepertoireScript.StartInfo.FileName = "cmd";
                            procCreerRepertoireScript.StartInfo.Verb = "runas";
                            procCreerRepertoireScript.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                            procCreerRepertoireScript.StartInfo.Arguments = @"/k mkdir C:\WinPE_" + strChoosed + @"\mount\scripts & exit";
                            procCreerRepertoireScript.StartInfo.ErrorDialog = true;
                            procCreerRepertoireScript.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procCreerRepertoireScript.Start();
                            procCreerRepertoireScript.WaitForExit();

                            Process procDeplacerFichier1 = new Process();
                            procDeplacerFichier1.StartInfo.FileName = "cmd";
                            procDeplacerFichier1.StartInfo.Verb = "runas";
                            procDeplacerFichier1.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                            procDeplacerFichier1.StartInfo.Arguments = @"/k copy diskpart_DISK.txt C:\WinPE_" + strChoosed + @"\mount\scripts\diskpart_DISK.txt & exit";
                            procDeplacerFichier1.StartInfo.ErrorDialog = true;
                            procDeplacerFichier1.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procDeplacerFichier1.Start();
                            procDeplacerFichier1.WaitForExit();

                            Process procDeplacerFichier2 = new Process();
                            procDeplacerFichier2.StartInfo.FileName = "cmd";
                            procDeplacerFichier2.StartInfo.Verb = "runas";
                            procDeplacerFichier2.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                            procDeplacerFichier2.StartInfo.Arguments = @"/k copy diskpart_prep_1.txt C:\WinPE_" + strChoosed + @"\mount\scripts\diskpart_prep_1.txt & exit";
                            procDeplacerFichier2.StartInfo.ErrorDialog = true;
                            procDeplacerFichier2.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procDeplacerFichier2.Start();
                            procDeplacerFichier2.WaitForExit();

                            Process procDeplacerFichier3 = new Process();
                            procDeplacerFichier3.StartInfo.FileName = "cmd";
                            procDeplacerFichier3.StartInfo.Verb = "runas";
                            procDeplacerFichier3.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                            procDeplacerFichier3.StartInfo.Arguments = @"/k copy diskpart_prep_2.txt C:\WinPE_" + strChoosed + @"\mount\scripts\diskpart_prep_2.txt & exit";
                            procDeplacerFichier3.StartInfo.ErrorDialog = true;
                            procDeplacerFichier3.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procDeplacerFichier3.Start();
                            procDeplacerFichier3.WaitForExit();

                            Process procDeplacerFichier4 = new Process();
                            procDeplacerFichier4.StartInfo.FileName = "cmd";
                            procDeplacerFichier4.StartInfo.Verb = "runas";
                            procDeplacerFichier4.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                            procDeplacerFichier4.StartInfo.Arguments = @"/k copy diskpart_prep_3.txt C:\WinPE_" + strChoosed + @"\mount\scripts\diskpart_prep_3.txt & exit";
                            procDeplacerFichier4.StartInfo.ErrorDialog = true;
                            procDeplacerFichier4.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procDeplacerFichier4.Start();
                            procDeplacerFichier4.WaitForExit();

                            Process procDeplacerFichier5 = new Process();
                            procDeplacerFichier5.StartInfo.FileName = "cmd";
                            procDeplacerFichier5.StartInfo.Verb = "runas";
                            procDeplacerFichier5.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                            procDeplacerFichier5.StartInfo.Arguments = @"/k copy diskpart_TWICE.txt C:\WinPE_" + strChoosed + @"\mount\scripts\diskpart_TWICE.txt & exit";
                            procDeplacerFichier5.StartInfo.ErrorDialog = true;
                            procDeplacerFichier5.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procDeplacerFichier5.Start();
                            procDeplacerFichier5.WaitForExit();

                            Process procDeplacerFichier6 = new Process();
                            procDeplacerFichier6.StartInfo.FileName = "cmd";
                            procDeplacerFichier6.StartInfo.Verb = "runas";
                            procDeplacerFichier6.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                            procDeplacerFichier6.StartInfo.Arguments = @"/k copy diskpart_VOL.txt C:\WinPE_" + strChoosed + @"\mount\scripts\diskpart_VOL.txt & exit";
                            procDeplacerFichier6.StartInfo.ErrorDialog = true;
                            procDeplacerFichier6.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procDeplacerFichier6.Start();
                            procDeplacerFichier6.WaitForExit();

                            Process procDeplacerFichier7 = new Process();
                            procDeplacerFichier7.StartInfo.FileName = "cmd";
                            procDeplacerFichier7.StartInfo.Verb = "runas";
                            procDeplacerFichier7.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                            procDeplacerFichier7.StartInfo.Arguments = @"/k copy dism_script.bat C:\WinPE_" + strChoosed + @"\mount\scripts\dism_script.bat & exit";
                            procDeplacerFichier7.StartInfo.ErrorDialog = true;
                            procDeplacerFichier7.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procDeplacerFichier7.Start();
                            procDeplacerFichier7.WaitForExit();

                            Process procDeplacerFichier8 = new Process();
                            procDeplacerFichier8.StartInfo.FileName = "cmd";
                            procDeplacerFichier8.StartInfo.Verb = "runas";
                            procDeplacerFichier8.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                            procDeplacerFichier8.StartInfo.Arguments = @"/k copy Ghost64.exe C:\WinPE_" + strChoosed + @"\mount\scripts\Ghost64.bat & exit";
                            procDeplacerFichier8.StartInfo.ErrorDialog = true;
                            procDeplacerFichier8.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procDeplacerFichier8.Start();
                            procDeplacerFichier8.WaitForExit();

                            Process procDeplacerFichier9 = new Process();
                            procDeplacerFichier9.StartInfo.FileName = "cmd";
                            procDeplacerFichier9.StartInfo.Verb = "runas";
                            procDeplacerFichier9.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                            procDeplacerFichier9.StartInfo.Arguments = @"/k copy /y startnet.cmd C:\WinPE_" + strChoosed + @"\mount\Windows\System32\startnet.cmd & exit";
                            procDeplacerFichier9.StartInfo.ErrorDialog = true;
                            procDeplacerFichier9.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                            procDeplacerFichier9.Start();
                            procDeplacerFichier9.WaitForExit();
                        }
                    }

                    if (Directory.Exists("C:\\WinPE_" + strChoosed))
                    {
                        if (cbxDrivers.Checked == true)
                        {
                            if (tbxDrivers.Text != "")
                            {
                                string[] lines = tbxDrivers.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                                foreach (string l in lines)
                                {
                                    Process procAddDriver = new Process();
                                    procAddDriver.StartInfo.FileName = "cmd";
                                    procAddDriver.StartInfo.Verb = "runas";
                                    procAddDriver.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                                    procAddDriver.StartInfo.Arguments = "/k Dism /Add-Driver /Image:C:\\WinPE_" + strChoosed + @"\mount /Driver:\"" + l + "" & exit";
                                    procAddDriver.StartInfo.ErrorDialog = true;
                                    procAddDriver.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                                    procAddDriver.Start();
                                    procAddDriver.WaitForExit();
                                }
                            }
                        }
                    }

                    if (tbxLCID.Text != "" || cbxDrivers.Checked == true)
                    {
                        Process procFermerWIM = new Process();
                        procFermerWIM.StartInfo.FileName = "cmd";
                        procFermerWIM.StartInfo.Verb = "runas";
                        procFermerWIM.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                        procFermerWIM.StartInfo.Arguments = "/k Dism /Unmount-Image /MountDir:C:\\WinPE_" + strChoosed + "\\mount /commit & exit";
                        procFermerWIM.StartInfo.ErrorDialog = true;
                        procFermerWIM.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                        procFermerWIM.Start();
                        procFermerWIM.WaitForExit();
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
                else
                {
                    System.Diagnostics.Process.Start("https://docs.microsoft.com/en-us/windows-hardware/get-started/adk-install");
                    MessageBox.Show("You need to install Windows ADK to use WinPE Creation Tool.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Windows Language Code Identifier. For example : fr-FR, en-US, de-DE...");
        }

        private void BtnHelpScript_Click(object sender, EventArgs e)
        {
            MessageBox.Show("By checking this box, WinPE Creation Tool will make a pre-made script launch when WinPE starts. This script is used to capture and deploy WIM, FFU and Ghost images. It is also used to find out the index of a WIM image, add an entry to the boot menu, and prepare a partition for deployment.\n\nOf course, you can edit the script at any time. To do this, edit the \"dism_script.bat\" file.");
        }

        private void CbxDrivers_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxDrivers.Checked == true)
            {
                MessageBox.Show("Add ONE driver path on EACH line.");
            }
        }
    }
}
