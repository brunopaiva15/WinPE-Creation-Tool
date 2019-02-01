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

namespace WinPE_Creation_Tool
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenererPE = new System.Windows.Forms.Button();
            this.btnGenererISO = new System.Windows.Forms.Button();
            this.btnCreerCleBootable = new System.Windows.Forms.Button();
            this.cbxUSB = new System.Windows.Forms.ComboBox();
            this.cbxArchitecture = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChangeLanguage = new System.Windows.Forms.Button();
            this.tbxLCID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnWizard = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "WinPE Creation Tool";
            // 
            // btnGenererPE
            // 
            this.btnGenererPE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenererPE.Location = new System.Drawing.Point(43, 121);
            this.btnGenererPE.Name = "btnGenererPE";
            this.btnGenererPE.Size = new System.Drawing.Size(157, 30);
            this.btnGenererPE.TabIndex = 1;
            this.btnGenererPE.Text = "Generate WinPE directory";
            this.btnGenererPE.UseVisualStyleBackColor = true;
            this.btnGenererPE.Click += new System.EventHandler(this.btnGenererPE_Click);
            // 
            // btnGenererISO
            // 
            this.btnGenererISO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenererISO.Location = new System.Drawing.Point(43, 157);
            this.btnGenererISO.Name = "btnGenererISO";
            this.btnGenererISO.Size = new System.Drawing.Size(157, 30);
            this.btnGenererISO.TabIndex = 2;
            this.btnGenererISO.Text = "Generate ISO";
            this.btnGenererISO.UseVisualStyleBackColor = true;
            this.btnGenererISO.Click += new System.EventHandler(this.btnGenererISO_Click);
            // 
            // btnCreerCleBootable
            // 
            this.btnCreerCleBootable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreerCleBootable.Location = new System.Drawing.Point(43, 193);
            this.btnCreerCleBootable.Name = "btnCreerCleBootable";
            this.btnCreerCleBootable.Size = new System.Drawing.Size(157, 30);
            this.btnCreerCleBootable.TabIndex = 3;
            this.btnCreerCleBootable.Text = "Create bootable USB";
            this.btnCreerCleBootable.UseVisualStyleBackColor = true;
            this.btnCreerCleBootable.Click += new System.EventHandler(this.btnCreerCleBootable_Click);
            // 
            // cbxUSB
            // 
            this.cbxUSB.FormattingEnabled = true;
            this.cbxUSB.Location = new System.Drawing.Point(116, 229);
            this.cbxUSB.Name = "cbxUSB";
            this.cbxUSB.Size = new System.Drawing.Size(84, 21);
            this.cbxUSB.TabIndex = 4;
            this.cbxUSB.DropDown += new System.EventHandler(this.cbxUSB_DropDown);
            this.cbxUSB.SelectedIndexChanged += new System.EventHandler(this.cbxUSB_SelectedIndexChanged);
            // 
            // cbxArchitecture
            // 
            this.cbxArchitecture.FormattingEnabled = true;
            this.cbxArchitecture.Items.AddRange(new object[] {
            "amd64",
            "x86",
            "arm"});
            this.cbxArchitecture.Location = new System.Drawing.Point(116, 94);
            this.cbxArchitecture.Name = "cbxArchitecture";
            this.cbxArchitecture.Size = new System.Drawing.Size(84, 21);
            this.cbxArchitecture.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Architecture :";
            // 
            // btnChangeLanguage
            // 
            this.btnChangeLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeLanguage.Location = new System.Drawing.Point(43, 256);
            this.btnChangeLanguage.Name = "btnChangeLanguage";
            this.btnChangeLanguage.Size = new System.Drawing.Size(157, 30);
            this.btnChangeLanguage.TabIndex = 7;
            this.btnChangeLanguage.Text = "Change keyboard language";
            this.btnChangeLanguage.UseVisualStyleBackColor = true;
            this.btnChangeLanguage.Click += new System.EventHandler(this.btnChangeLanguage_Click);
            // 
            // tbxLCID
            // 
            this.tbxLCID.Location = new System.Drawing.Point(118, 292);
            this.tbxLCID.Name = "tbxLCID";
            this.tbxLCID.Size = new System.Drawing.Size(54, 20);
            this.tbxLCID.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "USB :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "LCID string :";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(177, 292);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(23, 20);
            this.btnHelp.TabIndex = 11;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(35, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Free and open-source WinPE utility";
            // 
            // btnWizard
            // 
            this.btnWizard.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWizard.Location = new System.Drawing.Point(64, 333);
            this.btnWizard.Name = "btnWizard";
            this.btnWizard.Size = new System.Drawing.Size(105, 30);
            this.btnWizard.TabIndex = 13;
            this.btnWizard.Text = "WIZARD MODE";
            this.btnWizard.UseVisualStyleBackColor = true;
            this.btnWizard.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(99, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "v1.1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 378);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnWizard);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxLCID);
            this.Controls.Add(this.btnChangeLanguage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxArchitecture);
            this.Controls.Add(this.cbxUSB);
            this.Controls.Add(this.btnCreerCleBootable);
            this.Controls.Add(this.btnGenererISO);
            this.Controls.Add(this.btnGenererPE);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WPECT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenererPE;
        private System.Windows.Forms.Button btnGenererISO;
        private System.Windows.Forms.Button btnCreerCleBootable;
        private System.Windows.Forms.ComboBox cbxUSB;
        private System.Windows.Forms.ComboBox cbxArchitecture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChangeLanguage;
        private System.Windows.Forms.TextBox tbxLCID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnWizard;
        private System.Windows.Forms.Label label6;
    }
}

