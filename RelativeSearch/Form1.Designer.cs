//
//  Form1.Designer.cs
//
//  Author:
//       Benito Palacios Sánchez <benito356@gmail.com>
//
//  Copyright (c) 2015 Benito Palacios Sánchez
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
namespace RelativeSearch
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEntry = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboEnc = new System.Windows.Forms.ComboBox();
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.comboCharSize = new System.Windows.Forms.ComboBox();
            this.checkBytes = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.txtEntry.Location = new System.Drawing.Point(12, 12);
            this.txtEntry.Multiline = true;
            this.txtEntry.Name = "textBox1";
            this.txtEntry.Size = new System.Drawing.Size(440, 48);
            this.txtEntry.TabIndex = 0;
            this.txtEntry.TextChanged += new System.EventHandler(this.txtEntryTextChanged);
            // 
            // textBox2
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 66);
            this.txtOutput.Name = "textBox2";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtOutput.Size = new System.Drawing.Size(440, 20);
            this.txtOutput.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Copy";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnToClipboardClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 122);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Relative";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnSearchClick);
            // 
            // comboEnc
            // 
            this.comboEnc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEnc.FormattingEnabled = true;
            this.comboEnc.Items.AddRange(new object[] {
            "ASCII",
            "shift_jis",
            "UNICODE"});
            this.comboEnc.Location = new System.Drawing.Point(94, 94);
            this.comboEnc.Name = "comboEnc";
            this.comboEnc.Size = new System.Drawing.Size(121, 21);
            this.comboEnc.TabIndex = 4;
            this.comboEnc.SelectedIndexChanged += new System.EventHandler(this.comboEncSelectedIndexChanged);
            // 
            // backgroundWorker1
            // 
            this.worker.WorkerReportsProgress = true;
            this.worker.WorkerSupportsCancellation = true;
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerDoWork);
            this.worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // label1
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(324, 98);
            this.lblStatus.Name = "label1";
            this.lblStatus.Size = new System.Drawing.Size(56, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Progresss:";
            // 
            // comboCharSize
            // 
            this.comboCharSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCharSize.FormattingEnabled = true;
            this.comboCharSize.Items.AddRange(new object[] {
            "1 Byte",
            "2 Bytes"});
            this.comboCharSize.Location = new System.Drawing.Point(94, 124);
            this.comboCharSize.Name = "comboCharSize";
            this.comboCharSize.Size = new System.Drawing.Size(121, 21);
            this.comboCharSize.TabIndex = 7;
            // 
            // checkBytes
            // 
            this.checkBytes.AutoSize = true;
            this.checkBytes.Location = new System.Drawing.Point(256, 127);
            this.checkBytes.Name = "checkBytes";
            this.checkBytes.Size = new System.Drawing.Size(66, 17);
            this.checkBytes.TabIndex = 8;
            this.checkBytes.Text = "Reverse";
            this.checkBytes.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 151);
            this.Controls.Add(this.checkBytes);
            this.Controls.Add(this.comboCharSize);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.comboEnc);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.txtEntry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Text ASCII";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEntry;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboEnc;
        private System.ComponentModel.BackgroundWorker worker;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox comboCharSize;
        private System.Windows.Forms.CheckBox checkBytes;
    }
}

