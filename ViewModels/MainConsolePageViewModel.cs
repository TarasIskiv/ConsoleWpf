using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ConsoleWpf.ViewModels
{
    internal class MainConsolePageViewModel : Base_ViewModel.ViewModel
    {
        
        #region Entry
        private string entry = "console line > ";

        public string Entry
        {
            get => entry;
            set => Set(ref entry, value);
        }
        #endregion

        #region Output

        private string outputValue;
        public string OutputValue
        { 
            get => outputValue;
            set => Set(ref outputValue, value);
        }


        #endregion

        #region Buttons

        public ICommand ResultButton { get; }

        public bool CanResultExecute(object parameter) => true;
        public void ResultExecute(object parameter)
        {
            string searched = Entry;
            selector(searched);
        }

        public ICommand ResetButton { get; }

        public bool CanResetExecute(object parameter) => true;
        public void ResetExecute(object parameter)
        {
            Entry = null;
            OutputValue = null;
            Entry = "console line > ";
        }

        #endregion


        #region Functions

        private void selector(string line)
        {
            line = line.Substring(14).Trim();
            MessageBox.Show(line);
            if (line.Equals("help"))
            {
                OutputValue = helpSelected();
            }
            else if (line.Equals("exit")) 
            {
                exitSelected();
            }
            else
            {
                if (line.StartsWith("-f"))
                {
                    OutputValue = fileSelected(line);
                }
                else if (line.StartsWith("-d"))
                {
                    // work
                }
                else if (line.StartsWith("-u"))
                {
                    OutputValue = uriSelected(line);
                }
                else
                {
                    //bad input
                }
            }
        }
        #region File Selected
        private string fileSelected(string line)
        {
            string result = string.Empty;
            line = line.Substring(3).Trim();
            var elements = line.Split(new string[] { " - " }, StringSplitOptions.None).ToList();
            if (!elements.ToString().EndsWith(@"\"))
            {
                string FILENAME = getFileName(line, elements);
                string path = Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string filepath;
                do
                {
                    path = Path.GetDirectoryName(path);
                    filepath = String.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, FILENAME);
                    if (filepath.StartsWith(@"\\"))
                    {
                        return "File not found";
                    }
                } while (!File.Exists(filepath));
                Console.WriteLine(filepath);

                using (var reader = new StreamReader(filepath))
                {
                    var content = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        content.Add(reader.ReadLine());
                    }

                    Models.FileData.filePath = filepath;
                    Models.FileData.data = content;
                    result = searcher(elements[1]);
                }
            }else
            {
                result = "Bad Input";
            }
            return result;
        }


        private string getFileName(string line, List<string> elements)
        {
            

            string FileName = @elements[0];
            var temp_list = FileName.Split((char)92);

            string FILENAME = temp_list[temp_list.Length - 2] + ((char)92) + temp_list[temp_list.Length - 1];
            MessageBox.Show(FILENAME);
            return FILENAME;
        } 


        private string searcher(string searchedValue)
        {
            MessageBox.Show(searchedValue);
            if(searchedValue.StartsWith(((char)34).ToString()) && searchedValue.EndsWith(((char)34).ToString()))
            {
                searchedValue = searchedValue.Substring(1);
                searchedValue = searchedValue.Substring(0, searchedValue.Length - 1);
                MessageBox.Show(searchedValue);
            }
            var content = Models.FileData.data.ToList();
            int lineNumber = 1;
            var listResults = new List<string>();
            listResults.Add("Searched data : " + searchedValue);
            foreach(var line in content)
            {
                if (line.Contains(searchedValue))
                {
                    listResults.Add("Line Number : " + lineNumber.ToString() + "\nLine: " + line.ToString() + "\n");
                }
                lineNumber++;
            }
            string res = "Bad Input Data";
            if (listResults.Count == 1)
            {
                res = "No matches found";
            }
            else
            {
                res = string.Join("\n", listResults.ToArray());
            }
            return res;
        }

        #endregion
        private string directorySelected(string line)
        {
            return null;
        }

        #region URI Selected
        private string uriSelected(string line)
        {
            line = line.Substring(3).Trim();
            var elements = line.Split(new string[] { " - " }, StringSplitOptions.None).ToList();
            MessageBox.Show(elements[0]);
            MessageBox.Show(elements[1]);
            if(writeContent(elements[0]).ToString().Equals("Bad URI"))
            {
                return "Bad URI";
            }

            string result = URISearhcer(elements[1]);
            return result;
        }

        private string writeContent(string uri)
        {
            var html = new List<string>();
            try
            {
                WebRequest request = WebRequest.Create("https://stackoverflow.com/questions/36756935/how-do-i-add-a-scrollbar-to-a-textbox-in-wpf-c-sharp/36756982");
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                using (StreamReader sr = new StreamReader(data))
                {
                    while (!sr.EndOfStream)
                        html.Add(sr.ReadLine() + "\n");
                }
                Models.URIData.data = html;
                Models.URIData.uri = uri;
            }
            catch (Exception ex)
            {
                return "Bad URI";
            }
            return "Good URI";
        }


        private string URISearhcer(string searchedValue)
        {
            var content = Models.URIData.data.ToList();
            int lineNumber = 1;
            var listResults = new List<string>();
            listResults.Add("Searched data : " + searchedValue);
            foreach(var line in content)
            {
                if (line.Contains(searchedValue))
                {
                    listResults.Add("Line Number : " + lineNumber + "\n" + "Line : " + line.ToString() + "\n");
                }
                lineNumber++;
            }

            if(listResults.Count == 1)
            {
                return "No matches found";
            }
            string result = string.Join("\n", listResults.ToArray());
            return result;
        }
        #endregion
        private string helpSelected()
        {
            string resultLine = "Commands : \nhelp - show possible commands\n" +
                " -f <filepath> - <word> or <\"sentence\">\n" +
                " -d <directorypath> - <word> or <\"sentence\">\n" +
                " -u <url> - <word> or <\"sentence\">\n" +
                " exit - close application\n" +
                " Do not use brackets \"<,>\"";
            return resultLine;
        }

        private void exitSelected()
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Result
        private string result;
        public string Result 
        { 
            get => result;
            set => Set(ref result, value);
        }
        #endregion
        internal MainConsolePageViewModel()
        {
            ResultButton = new Infastructure.LambdaCommand(ResultExecute, CanResultExecute);
            ResetButton = new Infastructure.LambdaCommand(ResetExecute, CanResetExecute);
        }
    }
}
