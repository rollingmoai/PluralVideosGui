using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using NLog;
using PluralVideosGui.Encryption;
using PluralVideosGui.Model;
using static System.Windows.Forms.ListView;
using Module = PluralVideosGui.Model.Module;

namespace PluralVideosGui
{
    public partial class MainForm : Form
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private AppSetting _appSetting;
        private SQLiteConnection _databaseConnection;
        private List<CourseItem> _listCourse;

        public MainForm()
        {
            InitializeComponent();

            #region Apply setting

            if (File.Exists("settings.json"))
            {
                _appSetting = JsonConvert.DeserializeObject<AppSetting>(File.ReadAllText("settings.json"));

                if (_appSetting.CoursePath == string.Empty)
                {
                    _appSetting.CoursePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Pluralsight", "courses");
                }

                if (_appSetting.DatabasePath == string.Empty)
                {
                    _appSetting.DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Pluralsight", "pluralsight.db");
                }

                coursePathTextBox.Text = Directory.Exists(_appSetting.CoursePath) ? _appSetting.CoursePath : string.Empty;
                dbPathTextBox.Text = File.Exists(_appSetting.DatabasePath) ? _appSetting.DatabasePath : string.Empty;
                outputPathTextBox.Text = Directory.Exists(_appSetting.OutputPath) ? _appSetting.OutputPath : string.Empty;

                createSubCheckBox.Checked = _appSetting.CreateSub;
                deleteCheckBox.Checked = _appSetting.DeleteAfterDecrypt;
                copyImageCheckbox.Checked = _appSetting.CopyImage;
            }
            else
            {
                _appSetting = new AppSetting();
                string folderPath =
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "Pluralsight");
                if (Directory.Exists(folderPath))
                {
                    string coursePath = Path.Combine(folderPath, "courses");
                    string dbPath = Path.Combine(folderPath, "pluralsight.db");

                    coursePathTextBox.Text = Directory.Exists(coursePath) ? coursePath : string.Empty;
                    dbPathTextBox.Text = File.Exists(dbPath) ? dbPath : string.Empty;

                    _appSetting.CoursePath = coursePathTextBox.Text;
                    _appSetting.DatabasePath = dbPathTextBox.Text;
                }
            }

            #endregion

            imgList.ImageSize = new Size(170, 100);
            imgList.ColorDepth = ColorDepth.Depth32Bit;
            courseListView.LargeImageList = imgList;

            bgwDecrypt.DoWork += BgwDecrypt_DoWork;
            bgwDecrypt.ProgressChanged += BgwDecrypt_ProgressChanged;
            bgwDecrypt.RunWorkerCompleted += BgwDecrypt_RunWorkerCompleted;

            bgwGetCourse.DoWork += BgwGetCourse_DoWork;
            bgwGetCourse.ProgressChanged += BgwGetCourse_ProgressChanged;
            bgwGetCourse.RunWorkerCompleted += BgwGetCourse_RunWorkerCompleted;

            tslToolVersion.Text = $"Tool version: {Assembly.GetExecutingAssembly().GetName().Version?.ToString(3)}";
            if (File.Exists(dbPathTextBox.Text.Replace("pluralsight.db", "pluralsight.exe")))
            {
                tslToolVersion.BorderSides =
                    ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
                FileVersionInfo fvInfo =
                    FileVersionInfo.GetVersionInfo(dbPathTextBox.Text.Replace("pluralsight.db", "pluralsight.exe"));
                tslPOPVersion.Text = $"Pluralsight Offline Player Version: {fvInfo.FileVersion}";
            }
        }

        #region BackgroundWorker

        private void BgwGetCourse_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100)
            {
                bgwDecrypt.CancelAsync();
                return;
            }

            if (e.UserState.GetType() == typeof(Log))
            {
                Log log = (Log)e.UserState;
                AddText(log.Text, log.TextColor);
            }
            else
            {
                dynamic obj = e.UserState;
                AddListView(obj.Item as ListViewItem, obj.Image as Image);
            }
        }

        private void BgwGetCourse_DoWork(object sender, DoWorkEventArgs e)
        {
            ReadCourse(coursePathTextBox.Text, dbPathTextBox.Text);

            if (bgwGetCourse.CancellationPending)
            {
                // Set the e.Cancel flag so that the WorkerCompleted event
                // knows that the process was cancelled.
                e.Cancel = true;
                bgwGetCourse.ReportProgress(0);
            }
        }

        private void BgwGetCourse_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlMain.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void BgwDecrypt_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100)
            {
                bgwDecrypt.CancelAsync();
                return;
            }

            Log obj = (Log)e.UserState;
            AddText(obj.Text, obj.TextColor);
        }

        private void BgwDecrypt_DoWork(object sender, DoWorkEventArgs e)
        {
            DecryptCourse((List<ListViewItem>)e.Argument);

            if (bgwDecrypt.CancellationPending)
            {
                // Set the e.Cancel flag so that the WorkerCompleted event
                // knows that the process was cancelled.
                e.Cancel = true;
                bgwDecrypt.ReportProgress(0);
            }
        }

        private void BgwDecrypt_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pnlMain.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        #endregion

        #region Event

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _appSetting = new AppSetting
            {
                CoursePath = coursePathTextBox.Text,
                DatabasePath = dbPathTextBox.Text,
                OutputPath = outputPathTextBox.Text,
                CreateSub = createSubCheckBox.Checked,
                DeleteAfterDecrypt = deleteCheckBox.Checked,
                CopyImage = copyImageCheckbox.Checked
            };

            File.WriteAllText("settings.json", JsonConvert.SerializeObject(_appSetting));
        }

        private void coursePathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog { SelectedPath = coursePathTextBox.Text };
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;

            string coursePath = folderBrowserDialog.SelectedPath;
            coursePathTextBox.Text = coursePath;
            _appSetting.CoursePath = coursePath;
        }

        private void outputPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog { SelectedPath = outputPathTextBox.Text };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                outputPathTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void dbPathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Database file (*.db) | *.db",
                FileName = dbPathTextBox.Text
            };
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            dbPathTextBox.Text = openFileDialog.FileName;
            _appSetting.DatabasePath = openFileDialog.FileName;
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMain.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                courseListView.Items.Clear();
                imgList.Images.Clear();
                logRichTextBox.Clear();

                bgwGetCourse.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Error(ex);
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMain.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                List<ListViewItem> lstCourse = courseListView.CheckedItems.Cast<ListViewItem>().ToList();
                bgwDecrypt.RunWorkerAsync(lstCourse);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Error(ex);
            }
        }

        private void courseListView_ItemActivate(object sender, EventArgs e)
        {
            SelectedListViewItemCollection list = ((ListView)sender).SelectedItems;
            if (list.Count == 0) return;
            list[0].Checked = !list[0].Checked;
        }

        private void openDbButton_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", Path.GetDirectoryName(dbPathTextBox.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Error(ex);
            }
        }

        private void openOutputButton_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", outputPathTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Error(ex);
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            courseListView.Items.CheckUncheck(true);
        }

        private void deselectAllButton_Click(object sender, EventArgs e)
        {
            courseListView.Items.CheckUncheck(false);
        }

        private void logRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.LinkText) { UseShellExecute = true });
        }

        #endregion

        #region Method

        public void ReadCourse(string coursePath, string dbPath)
        {
            try
            {
                if (!Directory.Exists(coursePath) || !InitDb(dbPath)) return;

                bgwGetCourse.ReportProgress(1,
                    new Log { Text = "Getting course data . . .\n", TextColor = Color.Green });

                List<string> folderList = Directory.GetDirectories(coursePath, "*", SearchOption.TopDirectoryOnly)
                    .ToList();
                Logger.Debug($"FolderList1: {folderList.Count}");
                folderList = folderList.Where(r =>
                    Directory.GetDirectories(r, "*", SearchOption.TopDirectoryOnly).Length > 0).ToList();
                Logger.Debug($"FolderList2: {folderList.Count}");
                _listCourse = folderList.Select(r => new CourseItem { CoursePath = r, Course = GetCourseFromDb(r) })
                    .Where(r => r.Course != null).OrderBy(r => r.Course.Title).ToList();
                Logger.Debug($"listCourse1: {_listCourse.Count}");
                _listCourse = _listCourse.Where(c => c.Course.IsDownloaded).ToList();
                Logger.Debug($"listCourse2: {_listCourse.Count}");

                foreach (CourseItem item in _listCourse)
                {
                    Image thumb;
                    try
                    {
                        Image img = File.Exists(Path.Combine(item.CoursePath, "image.jpg"))
                            ? Image.FromFile(Path.Combine(item.CoursePath, "image.jpg"), true)
                            : new Bitmap(100, 100);
                        thumb = img.GetThumbnailImage(160, 90, () => false, IntPtr.Zero);
                        img.Dispose();
                    }
                    catch
                    {
                        thumb = new Bitmap(160, 90);
                        Graphics g = Graphics.FromImage(thumb);
                        g.Clear(Color.Black);
                    }

                    ListViewItem listItem = new ListViewItem
                    { ImageKey = item.Course.Name, Name = item.Course.Name, Text = item.Course.Title };

                    bgwGetCourse.ReportProgress(1, new { Item = listItem, Image = thumb });
                }

                bgwGetCourse.ReportProgress(1,
                    new Log { Text = $"Complete! {_listCourse.Count} courses found.\n", TextColor = Color.Green });

                bgwGetCourse.ReportProgress(100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.Error(ex);
            }
        }

        public void DecryptCourse(List<ListViewItem> list)
        {
            if (string.IsNullOrWhiteSpace(coursePathTextBox.Text))
            {
                MessageBox.Show("Please select course path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(dbPathTextBox.Text))
            {
                MessageBox.Show("Please select database path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(outputPathTextBox.Text))
            {
                MessageBox.Show("Please select output path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            foreach (ListViewItem item in list)
            {
                CourseItem courseItem =
                    _listCourse.Where(r => r.Course.Name == item.Name).Select(r => r).FirstOrDefault();

                bgwDecrypt.ReportProgress(1,
                    new Log
                    {
                        Text = $"Decrypting course \"{courseItem.Course.Title}\"\n",
                        TextColor = Color.Magenta,
                    });

                //Create new course path with the output path
                var newCoursePath = Path.Combine(outputPathTextBox.Text,
                    ReplaceInvalidFileNameChars(courseItem.Course.Title));

                DirectoryInfo courseInfo = Directory.Exists(newCoursePath)
                    ? new DirectoryInfo(newCoursePath)
                    : Directory.CreateDirectory(newCoursePath);

                if (copyImageCheckbox.Checked && File.Exists(Path.Combine(courseItem.CoursePath, "image.jpg")))
                    File.Copy(Path.Combine(courseItem.CoursePath, "image.jpg"), Path.Combine(newCoursePath, "image.jpg"), true);


                //Get list all modules in current course
                List<Module> listModules = courseItem.Course.Modules;

                if (listModules.Count > 0)
                    //Get each module
                    foreach (Module module in listModules)
                    {
                        //Generate module hash name
                        string moduleHash = ModuleHash(module.Name, module.AuthorHandle);
                        //Generate module path
                        string moduleHashPath = Path.Combine(courseItem.CoursePath, moduleHash);
                        //Create new module path with decryption name
                        string newModulePath = Path.Combine(courseInfo.FullName,
                            $"{module.Index + 1:00}. {module.Title}");

                        if (Directory.Exists(moduleHashPath))
                        {
                            DirectoryInfo moduleInfo = Directory.Exists(newModulePath)
                                ? new DirectoryInfo(newModulePath)
                                : Directory.CreateDirectory(newModulePath);
                            //Decrypt all videos in current module folder
                            DecryptAllVideos(moduleHashPath, module, moduleInfo.FullName,
                                courseItem.Course.HasTranscript);
                        }
                        else
                        {
                            bgwDecrypt.ReportProgress(1,
                                new Log
                                {
                                    Text = $"Folder {moduleHash} not found in the current course path\n",
                                    TextColor = Color.Red,
                                });
                        }
                    }

                bgwDecrypt.ReportProgress(1,
                    new Log
                    {
                        Text = $"\"{courseItem.Course.Title}\" done!\n",
                        TextColor = Color.Magenta,
                    });

                if (deleteCheckBox.Checked)
                {
                    try
                    {
                        Directory.Delete(courseItem.CoursePath, true);
                    }
                    catch (Exception ex)
                    {
                        bgwDecrypt.ReportProgress(1,
                            new Log
                            {
                                Text = $"Cannot delete course \"{courseItem.Course.Title}\".\n{ex.Message}\n",
                                TextColor = Color.Gray,
                            });
                    }

                    if (!RemoveCourseInDb(courseItem.CoursePath))
                        bgwDecrypt.ReportProgress(1,
                            new Log
                            {
                                Text = $"Cannot delete course \"{courseItem.Course.Title}\" from db.\n",
                                TextColor = Color.Gray,
                            });

                    bgwDecrypt.ReportProgress(1,
                        new Log
                        {
                            Text = $"Course \"{courseItem.Course.Title}\" deleted successfully.\n",
                            TextColor = Color.Magenta
                        });
                }
            }

            bgwDecrypt.ReportProgress(100);
        }

        public void DecryptAllVideos(string folderPath, Module module, string outputPath, bool hasTranscript)
        {
            try
            {
                // Get all clips of this module from database
                List<Clip> listClips = module.Clips;

                if (listClips.Count > 0)
                    foreach (Clip clip in listClips)
                    {
                        // Get current path of the encrypted video
                        string currentPath = Path.Combine(folderPath, $"{clip.Name}.psv");
                        if (File.Exists(currentPath))
                        {
                            // Create new path with output folder
                            string newPath = Path.Combine(outputPath, $"{clip.Index + 1:00}. {clip.Title}.mp4");

                            // Init video and get it from iStream
                            var playingFileStream = new VirtualFileStream(currentPath);
                            playingFileStream.Clone(out IStream iStream);

                            string fileName = Path.GetFileName(currentPath);

                            bgwDecrypt.ReportProgress(1,
                                new Log { Text = $"Decrypting \"{fileName}\"...", TextColor = Color.Yellow });

                            DecryptVideo(iStream, newPath);
                            if (createSubCheckBox.Checked && hasTranscript)
                                // Generate transcript file if user ask
                                WriteTranscriptFile(clip, newPath);

                            bgwDecrypt.ReportProgress(1,
                                new Log
                                {
                                    Text = $"\"{Path.GetFileName(newPath)}\" decrypt success.\n",
                                    TextColor = Color.Green
                                });
                            playingFileStream.Dispose();
                        }
                        else
                        {
                            bgwDecrypt.ReportProgress(1,
                                new Log
                                {
                                    Text = $"File \"{Path.GetFileName(currentPath)}\" cannot be found\n",
                                    TextColor = Color.Gray,
                                });
                        }
                    }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public void DecryptVideo(IStream currentStream, string newPath)
        {
            currentStream.Stat(out STATSTG stat, 0);
            IntPtr myPtr = (IntPtr)0;
            int streamSize = (int)stat.cbSize;
            byte[] streamInfo = new byte[streamSize];
            currentStream.Read(streamInfo, streamSize, myPtr);
            File.WriteAllBytes(newPath, streamInfo);
        }

        /// <summary>
        ///     Write transcript for the clip if available.
        /// </summary>
        public void WriteTranscriptFile(Clip clipId, string clipPath)
        {
            // Get all transcript to list
            List<ClipTranscript> clipTranscripts = clipId.Subtitle;

            if (clipTranscripts.Count <= 0) return;

            // Create transcript path with the same name of the clip
            string transcriptPath = Path.Combine(Path.GetDirectoryName(clipPath),
                Path.GetFileNameWithoutExtension(clipPath) + ".srt");
            if (File.Exists(transcriptPath)) File.Delete(transcriptPath);

            using (FileStream transcriptStream = File.OpenWrite(transcriptPath))
            {
                using StreamWriter writer = new StreamWriter(transcriptStream);
                // Write it to file with stream writer
                int i = 1;
                foreach (var clipTranscript in clipTranscripts)
                {
                    var start = TimeSpan.FromMilliseconds(clipTranscript.StartTime)
                        .ToString(@"hh\:mm\:ss\,fff");
                    var end = TimeSpan.FromMilliseconds(clipTranscript.EndTime).ToString(@"hh\:mm\:ss\,fff");
                    writer.WriteLine(i++);
                    writer.WriteLine(start + " --> " + end);
                    writer.WriteLine(clipTranscript.Text);
                    writer.WriteLine();
                }
            }

            bgwDecrypt.ReportProgress(1,
                new Log
                {
                    Text = $"Transcript of {Path.GetFileName(clipPath)} has been created.",
                    TextColor = Color.Purple
                });
        }

        #endregion

        #region DB

        /// <summary>
        ///     Get transcript text of specified clip from database.
        /// </summary>
        /// <returns>List of transcript text of the current clip.</returns>
        public List<ClipTranscript> GetTranscriptFromDb(int clipId)
        {
            try
            {
                List<ClipTranscript> list = new List<ClipTranscript>();

                var cmd = _databaseConnection.CreateCommand();
                cmd.CommandText = @"SELECT StartTime, EndTime, Text
                                FROM ClipTranscript
                                WHERE ClipId = @clipId
                                ORDER BY Id ASC";
                cmd.Parameters.Add(new SQLiteParameter("@clipId", clipId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClipTranscript clipTranscript = new ClipTranscript
                    {
                        StartTime = reader.GetInt32(reader.GetOrdinal("StartTime")),
                        EndTime = reader.GetInt32(reader.GetOrdinal("EndTime")),
                        Text = reader.GetString(reader.GetOrdinal("Text"))
                    };
                    list.Add(clipTranscript);
                }

                return list;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return new List<ClipTranscript>();
            }
        }

        /// <summary>
        ///     Get all clips information of specified module from database.
        /// </summary>
        /// <returns>List of information about clips belong to specified module.</returns>
        public List<Clip> GetClipsFromDb(int moduleId, string moduleName, string courseName)
        {
            List<Clip> list = new List<Clip>();

            var cmd = _databaseConnection.CreateCommand();
            cmd.CommandText = @"SELECT Id, Name, Title, ClipIndex
                                FROM Clip 
                                WHERE ModuleId = @moduleId";
            cmd.Parameters.Add(new SQLiteParameter("@moduleId", moduleId));

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Clip clip = new Clip
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Title = ReplaceInvalidFileNameChars(reader.GetString(reader.GetOrdinal("Title"))),
                    Index = reader.GetInt32(reader.GetOrdinal("ClipIndex")),
                    Subtitle = GetTranscriptFromDb(reader.GetInt32(reader.GetOrdinal("Id")))
                };

                clip.IsDownloaded = File.Exists(Path.Combine(_appSetting.CoursePath, courseName, moduleName, $"{clip.Name}.psv"));
                list.Add(clip);
            }

            reader.Close();
            return list;
        }

        /// <summary>
        ///     Get all modules information of specified course from database.
        /// </summary>
        /// <returns>List of modules information of specified course.</returns>
        public List<Module> GetModulesFromDb(string courseName)
        {
            List<Module> list = new List<Module>();

            var cmd = _databaseConnection.CreateCommand();
            cmd.CommandText = @"SELECT Id, Name, Title, AuthorHandle, ModuleIndex
                                FROM Module 
                                WHERE CourseName = @courseName";
            cmd.Parameters.Add(new SQLiteParameter("@courseName", courseName));

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Module module = new Module
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    AuthorHandle = reader.GetString(reader.GetOrdinal("AuthorHandle")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Title = ReplaceInvalidFileNameChars(reader.GetString(reader.GetOrdinal("Title"))),
                    Index = reader.GetInt32(reader.GetOrdinal("ModuleIndex"))
                };

                module.Clips = GetClipsFromDb(module.Id, ModuleHash(module.Name, module.AuthorHandle), courseName);
                list.Add(module);
            }

            reader.Close();
            return list;
        }

        /// <summary>
        ///     Get course information from database.
        /// </summary>
        /// <returns>Course information</returns>
        public Course GetCourseFromDb(string folderCoursePath)
        {
            Course course = null;

            string courseName = new DirectoryInfo(folderCoursePath).Name.ToLower();

            var cmd = _databaseConnection.CreateCommand();
            cmd.CommandText = @"SELECT Name, Title, HasTranscript 
                                FROM Course 
                                WHERE Name = @courseName";
            cmd.Parameters.Add(new SQLiteParameter("@courseName", courseName));

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                course = new Course
                {
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Title = ReplaceInvalidFileNameChars(reader.GetString(reader.GetOrdinal("Title"))),
                    HasTranscript = Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("HasTranscript")))
                };

                course.Modules = GetModulesFromDb(course.Name);
            }

            reader.Close();

            return course;
        }

        /// <summary>
        ///     Initialize database connection.
        /// </summary>
        /// <param name="dbPath">Database file path</param>
        /// <returns>True if database connection is opened successfully</returns>
        public bool InitDb(string dbPath)
        {
            if (File.Exists(dbPath))
            {
                if (Path.GetExtension(dbPath).Equals(".db"))
                {
                    _databaseConnection = new SQLiteConnection($"Data Source={dbPath}; Version=3;FailIfMissing=True");
                    _databaseConnection.Open();
                    bgwGetCourse.ReportProgress(1,
                        new Log { Text = "Database connection opened.\n", TextColor = Color.Green });
                    return true;
                }

                bgwGetCourse.ReportProgress(1, new Log { Text = "File is not a database file.", TextColor = Color.Red });
                return false;
            }

            bgwGetCourse.ReportProgress(1, new Log { Text = "Invalid file path.", TextColor = Color.Red });
            return false;
        }

        public bool RemoveCourseInDb(string coursePath)
        {
            try
            {
                string courseName = new DirectoryInfo(coursePath).Name;

                var cmd = _databaseConnection.CreateCommand();
                cmd.CommandText =
                    @"pragma foreign_keys=on;DELETE FROM Course WHERE Name = @courseName;pragma foreign_keys=off;";
                cmd.Parameters.Add(new SQLiteParameter("@courseName", courseName));

                var reader = cmd.ExecuteNonQuery();

                return reader > 0;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }

        #endregion

        #region Util

        public string ReplaceInvalidFileNameChars(string path)
        {
            path = Path.GetInvalidFileNameChars()
                .Aggregate(path, (current, invalidChar) => current.Replace(invalidChar, '-'));
            return path.Trim();
        }

        /// <summary>
        ///     Encrypt module & author name to get module folder name.
        /// </summary>
        /// <returns>String has been encrypted</returns>
        public string ModuleHash(string moduleName, string moduleAuthorName)
        {
            string s = moduleName + "|" + moduleAuthorName;
            using MD5 md5 = MD5.Create();
            return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(s))).Replace('/', '_');
        }

        public void AddText(string text, Color color)
        {
            logRichTextBox.SelectionColor = color;
            logRichTextBox.AppendText($"{text}\n");

            logRichTextBox.SelectionStart = logRichTextBox.Text.Length;
            logRichTextBox.ScrollToCaret();
        }

        public void AddListView(ListViewItem item, Image img)
        {
            imgList.Images.Add(item.ImageKey, img);
            courseListView.Items.Add(item);

            courseListView.SuspendLayout();
            courseListView.Refresh();
            courseListView.ResumeLayout();
        }

        #endregion
    }

    #region class

    public static class ExtensionMethod
    {
        public static void CheckUncheck(this ListViewItemCollection lstItem, bool selected)
        {
            foreach (ListViewItem item in lstItem)
            {
                item.Selected = selected;
                item.Checked = selected;
            }
        }
    }

    internal class CourseItem
    {
        public Course Course { get; set; }
        public string CoursePath { get; set; }
    }

    internal class Log
    {
        public string Text { get; set; }
        public Color TextColor { get; set; }
    }

    internal class AppSetting
    {
        public AppSetting()
        {
            CoursePath = string.Empty;
            DatabasePath = string.Empty;
            OutputPath = string.Empty;
            CreateSub = true;
            DeleteAfterDecrypt = true;
            CopyImage = false;
        }

        public string CoursePath { get; set; }
        public string DatabasePath { get; set; }
        public string OutputPath { get; set; }
        public bool CreateSub { get; set; }
        public bool DeleteAfterDecrypt { get; set; }
        public bool CopyImage { get; set; }
    }

    #endregion
}