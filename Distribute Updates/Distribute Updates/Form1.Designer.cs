namespace Distribute_Updates
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.open_patchList = new System.Windows.Forms.OpenFileDialog();
            this.patchDialog = new System.Windows.Forms.OpenFileDialog();
            this.MD5Calculator = new System.ComponentModel.BackgroundWorker();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.calcPerc = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.save = new System.Windows.Forms.Button();
            this.textBoxPatchlist = new System.Windows.Forms.TextBox();
            this.textBoxPatchFile = new System.Windows.Forms.TextBox();
            this.textBoxPatchFileSize = new System.Windows.Forms.TextBox();
            this.textBoxPatchFileMd5 = new System.Windows.Forms.TextBox();
            this.textBoxPatchFileDownloadUrl = new System.Windows.Forms.TextBox();
            this.buttonClearPatchlist = new System.Windows.Forms.Button();
            this.buttonResetPatchlistCurrenpProgress = new System.Windows.Forms.Button();
            this.buttonDeleteFromPatchlist = new System.Windows.Forms.Button();
            this.buttonAddToPatchlist = new System.Windows.Forms.Button();
            this.chTXT = new System.Windows.Forms.Button();
            this.dataGridViewPatchlist = new System.Windows.Forms.DataGridView();
            this.patchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patchURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chMPQ = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_save_news = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGridViewNews = new System.Windows.Forms.DataGridView();
            this.news_head = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.news_text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.news_image = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.news_link = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxNewsUrl = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxNewsImage = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxNewsBody = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxNewsHead = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxNews = new System.Windows.Forms.TextBox();
            this.btn_choose_news = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.AddLk = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.open_news = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatchlist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNews)).BeginInit();
            this.SuspendLayout();
            // 
            // open_patchList
            // 
            this.open_patchList.FileName = "patchlist";
            this.open_patchList.Filter = "(*.txt)|*.txt";
            this.open_patchList.Title = "Выберите текстовый файл хранящий информацию об обновлениях";
            this.open_patchList.FileOk += new System.ComponentModel.CancelEventHandler(this.patchListDialog_FileOk);
            // 
            // patchDialog
            // 
            this.patchDialog.Filter = "Патчи|*.MPQ|Файл запуска клиента|*.EXE|Все файлы|*.*";
            this.patchDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.patchDialog_FileOk);
            // 
            // MD5Calculator
            // 
            this.MD5Calculator.WorkerReportsProgress = true;
            this.MD5Calculator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MD5Calculator_DoWork);
            this.MD5Calculator.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.MD5Calculator_ProgressChanged);
            this.MD5Calculator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MD5Calculator_RunWorkerCompleted);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.calcPerc);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.save);
            this.tabPage1.Controls.Add(this.textBoxPatchlist);
            this.tabPage1.Controls.Add(this.textBoxPatchFile);
            this.tabPage1.Controls.Add(this.textBoxPatchFileSize);
            this.tabPage1.Controls.Add(this.textBoxPatchFileMd5);
            this.tabPage1.Controls.Add(this.textBoxPatchFileDownloadUrl);
            this.tabPage1.Controls.Add(this.buttonClearPatchlist);
            this.tabPage1.Controls.Add(this.buttonResetPatchlistCurrenpProgress);
            this.tabPage1.Controls.Add(this.buttonDeleteFromPatchlist);
            this.tabPage1.Controls.Add(this.buttonAddToPatchlist);
            this.tabPage1.Controls.Add(this.chTXT);
            this.tabPage1.Controls.Add(this.dataGridViewPatchlist);
            this.tabPage1.Controls.Add(this.chMPQ);
            this.tabPage1.Controls.Add(this.progressBar1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(636, 435);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Генерация файла Patchlist.txt";
            // 
            // calcPerc
            // 
            this.calcPerc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.calcPerc.AutoSize = true;
            this.calcPerc.Location = new System.Drawing.Point(561, 97);
            this.calcPerc.Name = "calcPerc";
            this.calcPerc.Size = new System.Drawing.Size(0, 13);
            this.calcPerc.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Файл патчей (patchlist.txt):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(81, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Файл обновления:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(146, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Total bytes:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(150, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "MD5 Hash:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(73, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "URL нового файла:";
            // 
            // save
            // 
            this.save.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.save.Location = new System.Drawing.Point(173, 405);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(304, 23);
            this.save.TabIndex = 15;
            this.save.TabStop = false;
            this.save.Text = "Сохранить в текстовый документ";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // textBoxPatchlist
            // 
            this.textBoxPatchlist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPatchlist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxPatchlist.Location = new System.Drawing.Point(254, 16);
            this.textBoxPatchlist.Name = "textBoxPatchlist";
            this.textBoxPatchlist.ReadOnly = true;
            this.textBoxPatchlist.Size = new System.Drawing.Size(223, 20);
            this.textBoxPatchlist.TabIndex = 4;
            this.textBoxPatchlist.TabStop = false;
            // 
            // textBoxPatchFile
            // 
            this.textBoxPatchFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPatchFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxPatchFile.Location = new System.Drawing.Point(254, 42);
            this.textBoxPatchFile.Name = "textBoxPatchFile";
            this.textBoxPatchFile.ReadOnly = true;
            this.textBoxPatchFile.Size = new System.Drawing.Size(223, 20);
            this.textBoxPatchFile.TabIndex = 5;
            this.textBoxPatchFile.TabStop = false;
            // 
            // textBoxPatchFileSize
            // 
            this.textBoxPatchFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPatchFileSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxPatchFileSize.Location = new System.Drawing.Point(254, 120);
            this.textBoxPatchFileSize.Name = "textBoxPatchFileSize";
            this.textBoxPatchFileSize.ReadOnly = true;
            this.textBoxPatchFileSize.Size = new System.Drawing.Size(223, 20);
            this.textBoxPatchFileSize.TabIndex = 6;
            this.textBoxPatchFileSize.TabStop = false;
            // 
            // textBoxPatchFileMd5
            // 
            this.textBoxPatchFileMd5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPatchFileMd5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxPatchFileMd5.Location = new System.Drawing.Point(254, 94);
            this.textBoxPatchFileMd5.Name = "textBoxPatchFileMd5";
            this.textBoxPatchFileMd5.ReadOnly = true;
            this.textBoxPatchFileMd5.Size = new System.Drawing.Size(223, 20);
            this.textBoxPatchFileMd5.TabIndex = 6;
            this.textBoxPatchFileMd5.TabStop = false;
            // 
            // textBoxPatchFileDownloadUrl
            // 
            this.textBoxPatchFileDownloadUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPatchFileDownloadUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxPatchFileDownloadUrl.Location = new System.Drawing.Point(254, 68);
            this.textBoxPatchFileDownloadUrl.Name = "textBoxPatchFileDownloadUrl";
            this.textBoxPatchFileDownloadUrl.Size = new System.Drawing.Size(223, 20);
            this.textBoxPatchFileDownloadUrl.TabIndex = 7;
            this.textBoxPatchFileDownloadUrl.TabStop = false;
            // 
            // buttonClearPatchlist
            // 
            this.buttonClearPatchlist.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonClearPatchlist.Location = new System.Drawing.Point(483, 146);
            this.buttonClearPatchlist.Name = "buttonClearPatchlist";
            this.buttonClearPatchlist.Size = new System.Drawing.Size(145, 23);
            this.buttonClearPatchlist.TabIndex = 14;
            this.buttonClearPatchlist.TabStop = false;
            this.buttonClearPatchlist.Text = "Очистить список патчей";
            this.buttonClearPatchlist.UseVisualStyleBackColor = true;
            this.buttonClearPatchlist.Click += new System.EventHandler(this.button6_Click);
            // 
            // buttonResetPatchlistCurrenpProgress
            // 
            this.buttonResetPatchlistCurrenpProgress.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonResetPatchlistCurrenpProgress.Location = new System.Drawing.Point(324, 146);
            this.buttonResetPatchlistCurrenpProgress.Name = "buttonResetPatchlistCurrenpProgress";
            this.buttonResetPatchlistCurrenpProgress.Size = new System.Drawing.Size(153, 23);
            this.buttonResetPatchlistCurrenpProgress.TabIndex = 13;
            this.buttonResetPatchlistCurrenpProgress.TabStop = false;
            this.buttonResetPatchlistCurrenpProgress.Text = "Сброс текущего прогресса";
            this.buttonResetPatchlistCurrenpProgress.UseVisualStyleBackColor = true;
            this.buttonResetPatchlistCurrenpProgress.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonDeleteFromPatchlist
            // 
            this.buttonDeleteFromPatchlist.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonDeleteFromPatchlist.Location = new System.Drawing.Point(173, 146);
            this.buttonDeleteFromPatchlist.Name = "buttonDeleteFromPatchlist";
            this.buttonDeleteFromPatchlist.Size = new System.Drawing.Size(145, 23);
            this.buttonDeleteFromPatchlist.TabIndex = 12;
            this.buttonDeleteFromPatchlist.TabStop = false;
            this.buttonDeleteFromPatchlist.Text = "Удалить патч из списка";
            this.buttonDeleteFromPatchlist.UseVisualStyleBackColor = true;
            this.buttonDeleteFromPatchlist.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonAddToPatchlist
            // 
            this.buttonAddToPatchlist.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonAddToPatchlist.Location = new System.Drawing.Point(10, 146);
            this.buttonAddToPatchlist.Name = "buttonAddToPatchlist";
            this.buttonAddToPatchlist.Size = new System.Drawing.Size(157, 23);
            this.buttonAddToPatchlist.TabIndex = 11;
            this.buttonAddToPatchlist.TabStop = false;
            this.buttonAddToPatchlist.Text = "Добавить к списку патчей";
            this.buttonAddToPatchlist.UseVisualStyleBackColor = true;
            this.buttonAddToPatchlist.Click += new System.EventHandler(this.button3_Click);
            // 
            // chTXT
            // 
            this.chTXT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chTXT.Location = new System.Drawing.Point(483, 14);
            this.chTXT.Name = "chTXT";
            this.chTXT.Size = new System.Drawing.Size(75, 23);
            this.chTXT.TabIndex = 8;
            this.chTXT.TabStop = false;
            this.chTXT.Text = "Выбрать";
            this.chTXT.UseVisualStyleBackColor = true;
            this.chTXT.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewPatchlist
            // 
            this.dataGridViewPatchlist.AllowUserToAddRows = false;
            this.dataGridViewPatchlist.AllowUserToResizeRows = false;
            this.dataGridViewPatchlist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPatchlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPatchlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewPatchlist.ColumnHeadersHeight = 21;
            this.dataGridViewPatchlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.patchName,
            this.patchURL,
            this.hash,
            this.total_size});
            this.dataGridViewPatchlist.Location = new System.Drawing.Point(8, 175);
            this.dataGridViewPatchlist.Name = "dataGridViewPatchlist";
            this.dataGridViewPatchlist.ReadOnly = true;
            this.dataGridViewPatchlist.RowHeadersVisible = false;
            this.dataGridViewPatchlist.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewPatchlist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewPatchlist.Size = new System.Drawing.Size(620, 219);
            this.dataGridViewPatchlist.TabIndex = 10;
            this.dataGridViewPatchlist.TabStop = false;
            // 
            // patchName
            // 
            this.patchName.HeaderText = "Имя Патча";
            this.patchName.Name = "patchName";
            this.patchName.ReadOnly = true;
            // 
            // patchURL
            // 
            this.patchURL.HeaderText = "URL адресс";
            this.patchURL.Name = "patchURL";
            this.patchURL.ReadOnly = true;
            // 
            // hash
            // 
            this.hash.HeaderText = "MD5 HASH";
            this.hash.Name = "hash";
            this.hash.ReadOnly = true;
            // 
            // total_size
            // 
            this.total_size.HeaderText = "Размер файла (bytes)";
            this.total_size.Name = "total_size";
            this.total_size.ReadOnly = true;
            // 
            // chMPQ
            // 
            this.chMPQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chMPQ.Location = new System.Drawing.Point(483, 40);
            this.chMPQ.Name = "chMPQ";
            this.chMPQ.Size = new System.Drawing.Size(75, 23);
            this.chMPQ.TabIndex = 9;
            this.chMPQ.TabStop = false;
            this.chMPQ.Text = "Выбрать";
            this.chMPQ.UseVisualStyleBackColor = true;
            this.chMPQ.Click += new System.EventHandler(this.button2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(483, 94);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(75, 20);
            this.progressBar1.TabIndex = 19;
            this.progressBar1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::Distribute_Updates.Properties.Resources._104;
            this.pictureBox1.Location = new System.Drawing.Point(580, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 52);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Location = new System.Drawing.Point(500, 440);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(133, 13);
            this.labelCopyright.TabIndex = 17;
            this.labelCopyright.Text = "Copyright 2016.  © Jumper";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(644, 461);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.btn_save_news);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.dataGridViewNews);
            this.tabPage2.Controls.Add(this.textBoxNewsUrl);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.textBoxNewsImage);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.textBoxNewsBody);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.textBoxNewsHead);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.textBoxNews);
            this.tabPage2.Controls.Add(this.btn_choose_news);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(636, 435);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Генератор файла news.txt";
            // 
            // btn_save_news
            // 
            this.btn_save_news.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_save_news.Location = new System.Drawing.Point(171, 405);
            this.btn_save_news.Name = "btn_save_news";
            this.btn_save_news.Size = new System.Drawing.Size(306, 23);
            this.btn_save_news.TabIndex = 19;
            this.btn_save_news.TabStop = false;
            this.btn_save_news.Text = "Сохранить в текстовый документ";
            this.btn_save_news.UseVisualStyleBackColor = true;
            this.btn_save_news.Click += new System.EventHandler(this.btn_save_news_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button2.Location = new System.Drawing.Point(483, 146);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(145, 23);
            this.button2.TabIndex = 18;
            this.button2.TabStop = false;
            this.button2.Text = "Очистить список ... новостей";
            this.toolTip.SetToolTip(this.button2, "Очистить список новостей");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button3.Location = new System.Drawing.Point(324, 146);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(153, 23);
            this.button3.TabIndex = 17;
            this.button3.TabStop = false;
            this.button3.Text = "Сброс текущего прогресса";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button4.Location = new System.Drawing.Point(171, 146);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(147, 23);
            this.button4.TabIndex = 16;
            this.button4.TabStop = false;
            this.button4.Text = "Удалить новость из ... списка";
            this.toolTip.SetToolTip(this.button4, "Удалить новость из списка");
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // button5
            // 
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button5.Location = new System.Drawing.Point(8, 146);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(157, 23);
            this.button5.TabIndex = 15;
            this.button5.TabStop = false;
            this.button5.Text = "Добавить к списку ... новостей";
            this.toolTip.SetToolTip(this.button5, "Добавить к списку новостей");
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // dataGridViewNews
            // 
            this.dataGridViewNews.AllowUserToAddRows = false;
            this.dataGridViewNews.AllowUserToDeleteRows = false;
            this.dataGridViewNews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewNews.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewNews.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewNews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNews.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.news_head,
            this.news_text,
            this.news_image,
            this.news_link});
            this.dataGridViewNews.Location = new System.Drawing.Point(8, 175);
            this.dataGridViewNews.Name = "dataGridViewNews";
            this.dataGridViewNews.ReadOnly = true;
            this.dataGridViewNews.RowHeadersVisible = false;
            this.dataGridViewNews.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewNews.Size = new System.Drawing.Size(620, 219);
            this.dataGridViewNews.TabIndex = 13;
            // 
            // news_head
            // 
            this.news_head.HeaderText = "Заголовок";
            this.news_head.Name = "news_head";
            this.news_head.ReadOnly = true;
            // 
            // news_text
            // 
            this.news_text.HeaderText = "Текст новости";
            this.news_text.Name = "news_text";
            this.news_text.ReadOnly = true;
            // 
            // news_image
            // 
            this.news_image.HeaderText = "Фоновое изображение";
            this.news_image.Name = "news_image";
            this.news_image.ReadOnly = true;
            // 
            // news_link
            // 
            this.news_link.HeaderText = "Ссылка для навигации";
            this.news_link.Name = "news_link";
            this.news_link.ReadOnly = true;
            // 
            // textBoxNewsUrl
            // 
            this.textBoxNewsUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNewsUrl.Location = new System.Drawing.Point(254, 120);
            this.textBoxNewsUrl.Name = "textBoxNewsUrl";
            this.textBoxNewsUrl.Size = new System.Drawing.Size(223, 20);
            this.textBoxNewsUrl.TabIndex = 12;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(50, 120);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(198, 20);
            this.label15.TabIndex = 9;
            this.label15.Text = "URL новости на сайте";
            // 
            // textBoxNewsImage
            // 
            this.textBoxNewsImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNewsImage.Location = new System.Drawing.Point(254, 94);
            this.textBoxNewsImage.Name = "textBoxNewsImage";
            this.textBoxNewsImage.Size = new System.Drawing.Size(223, 20);
            this.textBoxNewsImage.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(-2, 94);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(250, 20);
            this.label14.TabIndex = 9;
            this.label14.Text = "URL изображения в новости";
            // 
            // textBoxNewsBody
            // 
            this.textBoxNewsBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNewsBody.Location = new System.Drawing.Point(254, 68);
            this.textBoxNewsBody.Name = "textBoxNewsBody";
            this.textBoxNewsBody.Size = new System.Drawing.Size(223, 20);
            this.textBoxNewsBody.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(42, 68);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(206, 20);
            this.label13.TabIndex = 9;
            this.label13.Text = "Краткий текст новости";
            // 
            // textBoxNewsHead
            // 
            this.textBoxNewsHead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNewsHead.Location = new System.Drawing.Point(254, 42);
            this.textBoxNewsHead.Name = "textBoxNewsHead";
            this.textBoxNewsHead.Size = new System.Drawing.Size(223, 20);
            this.textBoxNewsHead.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(75, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(173, 20);
            this.label12.TabIndex = 9;
            this.label12.Text = "Заголовок новости";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(20, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(228, 20);
            this.label11.TabIndex = 9;
            this.label11.Text = "Файл новостей (news.txt):";
            // 
            // textBoxNews
            // 
            this.textBoxNews.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxNews.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxNews.Location = new System.Drawing.Point(254, 16);
            this.textBoxNews.Name = "textBoxNews";
            this.textBoxNews.ReadOnly = true;
            this.textBoxNews.Size = new System.Drawing.Size(223, 20);
            this.textBoxNews.TabIndex = 10;
            this.textBoxNews.TabStop = false;
            // 
            // btn_choose_news
            // 
            this.btn_choose_news.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_choose_news.Location = new System.Drawing.Point(483, 14);
            this.btn_choose_news.Name = "btn_choose_news";
            this.btn_choose_news.Size = new System.Drawing.Size(75, 23);
            this.btn_choose_news.TabIndex = 11;
            this.btn_choose_news.TabStop = false;
            this.btn_choose_news.Text = "Выбрать";
            this.btn_choose_news.UseVisualStyleBackColor = true;
            this.btn_choose_news.Click += new System.EventHandler(this.btn_choose_news_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(49, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Data";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 88);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(79, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Data\\ruRU";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // AddLk
            // 
            this.AddLk.Location = new System.Drawing.Point(534, 154);
            this.AddLk.Name = "AddLk";
            this.AddLk.Size = new System.Drawing.Size(97, 25);
            this.AddLk.TabIndex = 2;
            this.AddLk.Text = "Добавить";
            this.AddLk.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(47, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(228, 69);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(8, 16);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(58, 17);
            this.checkBox3.TabIndex = 0;
            this.checkBox3.Text = "Буквы";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(8, 46);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(62, 17);
            this.checkBox4.TabIndex = 1;
            this.checkBox4.Text = "Цифры";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(98, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(98, 31);
            this.panel1.TabIndex = 2;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(59, 8);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(34, 20);
            this.textBox6.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 13);
            this.label7.TabIndex = 1;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(3, 8);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(34, 20);
            this.textBox5.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(98, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(98, 31);
            this.panel2.TabIndex = 2;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(59, 8);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(34, 20);
            this.textBox8.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 1;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(3, 8);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(34, 20);
            this.textBox7.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(47, 110);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(228, 69);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(8, 16);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(58, 17);
            this.checkBox6.TabIndex = 0;
            this.checkBox6.Text = "Буквы";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(8, 46);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(62, 17);
            this.checkBox5.TabIndex = 1;
            this.checkBox5.Text = "Цифры";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(98, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(98, 31);
            this.panel4.TabIndex = 2;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(59, 8);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(34, 20);
            this.textBox12.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(43, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 13);
            this.label10.TabIndex = 1;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(3, 8);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(34, 20);
            this.textBox11.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(98, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(98, 31);
            this.panel3.TabIndex = 2;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(59, 8);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(34, 20);
            this.textBox10.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(43, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 13);
            this.label9.TabIndex = 1;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(3, 8);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(34, 20);
            this.textBox9.TabIndex = 0;
            // 
            // open_news
            // 
            this.open_news.FileName = "news";
            this.open_news.Filter = "(*.txt)|*.txt";
            this.open_news.Title = "Выберите текстовый файл хранящий информацию о новостях";
            this.open_news.FileOk += new System.ComponentModel.CancelEventHandler(this.open_news_FileOk);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 461);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(660, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Distributor";
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPatchlist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNews)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog open_patchList;
        private System.Windows.Forms.OpenFileDialog patchDialog;
        private System.ComponentModel.BackgroundWorker MD5Calculator;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.TextBox textBoxPatchlist;
        private System.Windows.Forms.TextBox textBoxPatchFile;
        private System.Windows.Forms.TextBox textBoxPatchFileMd5;
        private System.Windows.Forms.TextBox textBoxPatchFileDownloadUrl;
        private System.Windows.Forms.Button buttonClearPatchlist;
        private System.Windows.Forms.Button buttonResetPatchlistCurrenpProgress;
        private System.Windows.Forms.Button buttonDeleteFromPatchlist;
        private System.Windows.Forms.Button buttonAddToPatchlist;
        private System.Windows.Forms.Button chTXT;
        private System.Windows.Forms.DataGridView dataGridViewPatchlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn patchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn patchURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn hash;
        private System.Windows.Forms.Button chMPQ;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button AddLk;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPatchFileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_size;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxNews;
        private System.Windows.Forms.Button btn_choose_news;
        private System.Windows.Forms.TextBox textBoxNewsUrl;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxNewsImage;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxNewsBody;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxNewsHead;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dataGridViewNews;
        private System.Windows.Forms.DataGridViewTextBoxColumn news_head;
        private System.Windows.Forms.DataGridViewTextBoxColumn news_text;
        private System.Windows.Forms.DataGridViewTextBoxColumn news_image;
        private System.Windows.Forms.DataGridViewTextBoxColumn news_link;
        private System.Windows.Forms.OpenFileDialog open_news;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label calcPerc;
        private System.Windows.Forms.Button btn_save_news;
    }
}

