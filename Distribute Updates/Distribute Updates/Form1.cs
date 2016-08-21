// Данный програмный продукт является результатом труда и стараний Jumper'а
// Все права и копирайты закреплены за ним
// (c) 2011-2016 год
//--------------------------------------------------------------------
//----### ------------------------------------------------------------
//----###---###-----##----#-------##----#######---#######---######----
//----###---###-----##----##------##-----##--###---##-------##---##---
//----###---###-----##----###----###-----##---##---##-------##---##---
//----###---###-----##---#--##--# ##-----##--##--- ##-###---##--##----
//----###---###-----##---#--##-##--##----##--------##-------##--##----
//----###----##----##----#---###---##----##--------##-------##---###--
//----###-----######----##----#----##---####------#######---###----###
//----## -------------------------------------------------------------
//---## ------------------------------------------------CODING FOR YOU

using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace Distribute_Updates
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region patchlist

        string patchlistPath, patchPath;

        private void button1_Click(object sender, EventArgs e)
        {
            open_patchList.ShowDialog();
        }

        private void patchListDialog_FileOk(object sender, CancelEventArgs e)
        {
            textBoxPatchFile.Text = textBoxPatchFileMd5.Text = textBoxPatchFileDownloadUrl.Text = textBoxPatchFileSize.Text = "";
            textBoxPatchlist.Text = open_patchList.FileName;
            patchlistPath = open_patchList.FileName;

            dataGridViewPatchlist.Rows.Clear();

            StreamReader updateData = new StreamReader(textBoxPatchlist.Text);
            string line;
            while ((line = updateData.ReadLine()) != null)
            {
                string[] ex = line.Split(char.Parse("#"));
                dataGridViewPatchlist.Rows.Add( new object[] {ex[1], ex[0], ex[2], ex[3]});
            }
            updateData.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            patchDialog.ShowDialog();
            textBoxPatchFileDownloadUrl.Text = "";
        }

        private void patchDialog_FileOk(object sender, CancelEventArgs e)
        {
            textBoxPatchFile.Text = patchDialog.FileName;
            patchPath = patchDialog.FileName;
            progressBar1.Visible = true;
            textBoxPatchFileMd5.Text = "";
            FileInfo fi = new FileInfo(patchPath);
            textBoxPatchFileSize.Text = fi.Length.ToString();
            this.Enabled = false;
            MD5Calculator.RunWorkerAsync();
        }

        private void MD5Calculator_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] buffer;
            int bytesRead;
            long size;
            long totalBytesRead = 0;

            using (Stream file = File.OpenRead(patchPath))
            {
                size = file.Length;

                using (HashAlgorithm hasher = MD5.Create())
                {
                    do
                    {
                        buffer = new byte[4096];
                        bytesRead = file.Read(buffer, 0, buffer.Length);
                        totalBytesRead += bytesRead;
                        hasher.TransformBlock(buffer, 0, bytesRead, null, 0);
                        MD5Calculator.ReportProgress((int)(totalBytesRead * 1D / size * 100));
                    }
                    while (bytesRead != 0);

                    hasher.TransformFinalBlock(buffer, 0, 0);
                    e.Result = MakeHashString(hasher.Hash);
                }
            }
        }

        private static string MakeHashString(byte[] hashBytes)
        {
            StringBuilder hash = new StringBuilder(32);

            foreach (byte b in hashBytes)
                hash.Append(b.ToString("X2").ToUpper());

            return hash.ToString();
        }

        private void MD5Calculator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            calcPerc.Text = e.ProgressPercentage.ToString() + " %";
        }

        private void MD5Calculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            calcPerc.Text = "";
            progressBar1.Visible = false;
            Enabled = true;
            textBoxPatchFileMd5.Text = e.Result.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridViewPatchlist.CurrentRow.Index;
                dataGridViewPatchlist.Rows.RemoveAt(i);
            }
            catch
            {
                MessageBox.Show("Пожалуйста выделите строку для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MD5Calculator.IsBusy)
            {
                MessageBox.Show("Нельзя выполнить, пока вычисляется хэш файла обновления");
                return;
            }
            if (textBoxPatchlist.Text.Trim().Equals("") || textBoxPatchFile.Text.Trim().Equals("") || textBoxPatchFileMd5.Text.Trim().Equals("") || textBoxPatchFileDownloadUrl.Text.Trim().Equals(""))
                MessageBox.Show("Пожалуйста заполните все данные перед добавлением!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                string name = String.Empty;
                string[] parts = textBoxPatchFile.Text.Split('\\');
                foreach (string part in parts)
                {
                    name = part;
                }
                dataGridViewPatchlist.Rows.Add(new object[] { name, textBoxPatchFileDownloadUrl.Text, textBoxPatchFileMd5.Text, textBoxPatchFileSize.Text });
                textBoxPatchFileMd5.Text = textBoxPatchFileDownloadUrl.Text = textBoxPatchFileSize.Text = "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxPatchFile.Text = textBoxPatchFileMd5.Text = textBoxPatchFileDownloadUrl.Text = textBoxPatchFileSize.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridViewPatchlist.Rows.Clear();
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter patchData = new StreamWriter(textBoxPatchlist.Text);
                string line;
                int i = 0;
                for (i = 0; i < dataGridViewPatchlist.RowCount; i++)
                {
                    line = dataGridViewPatchlist.Rows[i].Cells[1].Value.ToString() + "#" + dataGridViewPatchlist.Rows[i].Cells[0].Value.ToString() + "#" + dataGridViewPatchlist.Rows[i].Cells[2].Value.ToString() + "#" + dataGridViewPatchlist.Rows[i].Cells[3].Value.ToString();
                    patchData.WriteLine(line);
                }
                patchData.Flush();
                patchData.Close();
                MessageBox.Show("Данные успешно сохранены в файл " + textBoxPatchlist.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.Message.Contains("Пустое имя пути не допускается"))
                    open_patchList.ShowDialog();
            }
        }
        #endregion

        #region newslist

        private void btn_choose_news_Click(object sender, EventArgs e)
        {
            open_news.ShowDialog();
        }

        private void open_news_FileOk(object sender, CancelEventArgs e)
        {
            textBoxNews.Text = open_news.FileName;
            textBoxNewsHead.Text = textBoxNewsBody.Text = textBoxNewsImage.Text = textBoxNewsUrl.Text = "";
            dataGridViewNews.Rows.Clear();

            StreamReader updateData = new StreamReader(textBoxNews.Text);
            string line;
            while ((line = updateData.ReadLine()) != null)
            {
                string[] ex = line.Split(char.Parse("#"));
                dataGridViewNews.Rows.Add(new object[] { ex[0], ex[1], ex[2], ex[3] });
            }
            updateData.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridViewNews.CurrentRow.Index;
                dataGridViewNews.Rows.RemoveAt(i);
            }
            catch
            {
                MessageBox.Show("Пожалуйста выделите строку для удаления!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            textBoxNewsHead.Text = textBoxNewsBody.Text = textBoxNewsImage.Text = textBoxNewsUrl.Text = "";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            dataGridViewNews.Rows.Clear();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (textBoxNewsHead.Text.Trim().Equals("") || textBoxNewsBody.Text.Trim().Equals("") || textBoxNewsImage.Text.Trim().Equals("") || textBoxNewsUrl.Text.Trim().Equals(""))
                MessageBox.Show("Пожалуйста заполните все данные перед добавлением!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                dataGridViewNews.Rows.Add(new object[] { textBoxNewsHead.Text, textBoxNewsBody.Text, textBoxNewsImage.Text, textBoxNewsUrl.Text });
                textBoxNewsHead.Text = textBoxNewsBody.Text = textBoxNewsImage.Text = textBoxNewsUrl.Text = "";
            }
        }

        private void btn_save_news_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter newsData = new StreamWriter(textBoxNews.Text);
                string line;
                int i = 0;
                for (i = 0; i < dataGridViewNews.RowCount; i++)
                {
                    line = dataGridViewNews.Rows[i].Cells[0].Value.ToString() + "#" + dataGridViewNews.Rows[i].Cells[1].Value.ToString() + "#" + dataGridViewNews.Rows[i].Cells[2].Value.ToString() + "#" + dataGridViewNews.Rows[i].Cells[3].Value.ToString();
                    newsData.WriteLine(line);
                }
                newsData.Flush();
                newsData.Close();
                MessageBox.Show("Данные успешно сохранены в файл " + textBoxNews.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.Message.Contains("Пустое имя пути не допускается"))
                    open_news.ShowDialog();
            }
        }

        #endregion
    }
}
