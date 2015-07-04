//
//  Form1.cs
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace RelativeSearch
{
    public partial class Form1 : Form
    {
		private SearchInfo searchInfo;

        public Form1()
        {
            InitializeComponent();
            comboEnc.SelectedIndex = 0;
			comboCharSize.SelectedIndex = 0;
        }

        private void txtEntryTextChanged(object sender, EventArgs e)
        {
			if (!checkBytes.Checked) {
				var encoding = Encoding.GetEncoding(comboEnc.Text);
				byte[] data  = encoding.GetBytes(txtEntry.Text);
				txtOutput.Text = BitConverter.ToString(data).Replace('-', ' ');
			} else {
				string validData = IsByteArray(txtEntry.Text);
				if (validData == null)
					txtOutput.Text = "Invalid string";
				else {
					string decoded = Decode(validData);
					if (decoded == null)
						txtOutput.Text = "Error decoding";
					else {
						txtOutput.Text = decoded;
						Console.WriteLine(decoded);
					}
				}
            }
        }

		private string IsByteArray(string data)
		{
			StringBuilder builder = new StringBuilder(data);
			builder.Replace(" ", "");
			builder.Replace("-", "");

			if (builder.Length == 0 || builder.Length % 2 != 0)
				return null;

			string dataStr = builder.ToString();
			foreach (char ch in dataStr)
				if (!IsHex(ch))
					return null;

			return dataStr;
		}

		private bool IsHex(char ch)
		{
			return (ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'F') ||
				(ch >= 'a' && ch <= 'f');
		}

		private string Decode(string data)
		{
			byte[] binary = Enumerable.Range(0, data.Length / 2)
				.Select(i => data.Substring(i * 2, 2))
				.Select(hex => Convert.ToByte(hex, 16))
				.ToArray();

			var encoding = Encoding.GetEncoding(comboEnc.Text);
			string decoded = null;
			try {
				decoded = encoding.GetString(binary);
			} catch (FormatException) { }

			return decoded;
		}

        private void btnToClipboardClick(object sender, EventArgs e)
        {
            Clipboard.SetText(txtOutput.Text, TextDataFormat.Text);
        }

        private void btnSearchClick(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.CheckFileExists = true;
            if (o.ShowDialog() != DialogResult.OK)
                return;

			searchInfo = new SearchInfo(
				o.FileName,
				txtEntry.Text,
				Encoding.GetEncoding(comboEnc.Text),
				comboCharSize.Text);
			worker.RunWorkerAsync(searchInfo);
        }

        private void comboEncSelectedIndexChanged(object sender, EventArgs e)
        {
            txtEntryTextChanged(sender, e);
        }

        private void workerDoWork(object sender, DoWorkEventArgs e)
        {
			var info = (SearchInfo)e.Argument;
			int mode = (info.CharSize == "1 Byte") ? 0 : 1;

			byte[] data = File.ReadAllBytes(info.Filename);
			int[] diff = new int[info.TextToSearch.Length - 1];
            for (int i = 0; i < diff.Length; i++) {
				var b1 = info.Encoding.GetBytes(info.TextToSearch[i].ToString());
				ushort c1 = (b1.Length == 1) ? b1[0] : BitConverter.ToUInt16(b1, 0);

				var b2 = info.Encoding.GetBytes(info.TextToSearch[i + 1].ToString());
				ushort c2 = (b2.Length == 1) ? b2[0] : BitConverter.ToUInt16(b2, 0);

                diff[i] = c1 - c2;
            }

            int pos = 0;
            int old_pos = 0;
            int j = 0;
            int k = 0;
            int sub = 0;
            int rango = 0;
            bool found = false;
            while (pos + diff.Length < data.Length) {
                // mode 1 byte
                if (mode == 0) {
                    for (j = 0; j < diff.Length; j++) {
                        sub = data[pos + j] - data[pos + j + 1];
                        if (sub >= diff[j] - rango && sub <= diff[j] + rango)
                            found = true;
                        else {
                            found = false;
                            break;
                        }
                    }
                } else if (mode == 1) {
                    // mode 2 bytes
                    for (j = 0, k = 0; j < diff.Length; j++, k += 2)
                    {
                        sub = BitConverter.ToUInt16(data, pos + k) - BitConverter.ToUInt16(data, pos + k + 2);
                        if (sub >= diff[j] - rango && sub <= diff[j] + rango)
                            found = true;
                        else
                        {
                            found = false;
                            break;
                        }
                    }
                }

                if (found) {
					Console.WriteLine("Found at: " + pos.ToString("X8"));
                    //MessageBox.Show("Found at: 0x" + pos.ToString("x"));
                    found = false;
                }

                pos++;
                if (old_pos + 0x5000 < pos) {
                    old_pos = pos;
                    worker.ReportProgress(pos);
                }
            }

            worker.ReportProgress(pos);
            e.Result = pos;

			Console.WriteLine("------------");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            long perc = e.ProgressPercentage * 100 / searchInfo.FileLength;
			string status = String.Format("{0} of {1} ({2})", e.ProgressPercentage,
				searchInfo.FileLength, perc);
			lblStatus.Text = status;
            this.Update();
        }

		class SearchInfo
		{
			public SearchInfo(string filename, string textToSearch, Encoding encoding,
				string charSize)
			{
				this.Filename = filename;
				this.TextToSearch = textToSearch;
				this.Encoding = encoding;
				this.CharSize = charSize;

				FileLength = (int)new FileInfo(filename).Length;
			}

			public long FileLength      { get; private set; }
			public string Filename     { get; private set; }
			public string TextToSearch { get; private set; }
			public Encoding Encoding   { get; private set; }
			public string CharSize     { get; private set; }
		}
    }
}
