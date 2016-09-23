using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AssemblyInformation
{
    public class MainWindowViewModel : ViewModelBase
    {
        IOpenFileService openFileService;
        public MainWindowViewModel(IOpenFileService openFileService)
        {
            if (openFileService == null)
                throw new ArgumentNullException("openFileService");
            this.openFileService = openFileService;
        }

        private ObservableCollection<AssemblyInfo> assemblyInfos = new ObservableCollection<AssemblyInfo>();
        public ObservableCollection<AssemblyInfo> AssemblyInfos
        {
            get { return assemblyInfos; }
            set
            {
                if (assemblyInfos != value)
                {
                    assemblyInfos = value;
                    RaisePropertyChanged();
                }
            }
        }

        private AssemblyInfo GetAssemblyInfo(string filePath)
        {
            AssemblyInfo ai = null;

            if (File.Exists(filePath))
            {
                using (Isolated<AssemblyInfoRetriever> isolated = new Isolated<AssemblyInfoRetriever>())
                {
                    try
                    {
                        ai = isolated.Handle.GetInfo(filePath);
                    }
                    catch (BadImageFormatException)
                    {
                        ai = new AssemblyInfo(Path.GetFileName(filePath), filePath, Path.GetExtension(filePath));
                    }
                    catch (Exception ex)
                    {
                        //Something went wrong.
                    }
                }
            }

            return ai;
        }

        private ICommand openCommand;
        public ICommand OpenCommand
        {
            get
            {
                return openCommand ??
                    (openCommand = new RelayCommand(() =>
                    {
                        if (openFileService.OpenFile())
                        {
                            IEnumerable<string> files = openFileService.SelectedFiles;
                            OpenFiles(files);
                        }
                    }));
            }
        }

        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                return exitCommand ??
                    (exitCommand = new RelayCommand(() =>
                    {
                        Application.Current.Shutdown();
                    }));
            }
        }

        private ICommand clearCommand;
        public ICommand ClearCommand
        {
            get
            {
                return clearCommand ??
                    (clearCommand = new RelayCommand(() =>
                    {
                        AssemblyInfos.Clear();
                    }));
            }
        }

        public void OpenFiles(IEnumerable<string> files)
        {
            OpenFiles(files, new Progress<int>(percent =>
            {
                Progress = percent;
            }));
        }

        private void OpenFiles(IEnumerable<string> files, IProgress<int> progress)
        {
            if (files != null)
            {
                Task.Factory.StartNew<IEnumerable<AssemblyInfo>>(() =>
                {
                    List<AssemblyInfo> ais = new List<AssemblyInfo>();
                    int totalNumberOfFiles = files.Count();
                    try
                    {
                        for (int i = 0; i < totalNumberOfFiles; i++)
                        {
                            string file = files.ElementAt(i);
                            if (HasCorrectFileExtension(file))
                            {
                                AssemblyInfo ai = GetAssemblyInfo(file);
                                if (ai != null)
                                    ais.Add(ai);
                            }

                            if (progress != null)
                                progress.Report(100 * (i + 1) / totalNumberOfFiles);
                        }
                    }
                    finally
                    {
                        //Ensure that the progress is set to 100%
                        if (progress != null)
                            progress.Report(100);
                    }
                    return ais;
                }).ContinueWith((antecedent) =>
                {
                    foreach (AssemblyInfo ai in antecedent.Result)
                    {
                        if (AssemblyInfos.FirstOrDefault(x => x.FullPath == ai.FullPath) == null)
                            AssemblyInfos.Add(ai);
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private bool HasCorrectFileExtension(string file)
        {
            string extension = Path.GetExtension(file);
            return (extension == ".dll" || extension == ".exe");
        }

        private int progress = 100;
        public int Progress
        {
            get { return progress; }
            set
            {
                if (progress != value)
                {
                    progress = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
