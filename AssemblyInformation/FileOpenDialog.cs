using Microsoft.Win32;
using System.Collections.Generic;

namespace AssemblyInformation
{
    public class FileOpenDialog : IOpenFileService
    {
        private bool multiSelect;
        private string filter;

        public FileOpenDialog(bool multiSelect, string filter)
        {
            this.multiSelect = multiSelect;
            this.filter = filter;
        }

        public string SelectedFile { get; private set; }
        public IEnumerable<string> SelectedFiles { get; private set; }

        public bool OpenFile(string initialDirectory = null)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = multiSelect;
            ofd.Filter = filter;
            ofd.InitialDirectory = initialDirectory;
            bool? result = ofd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                if (multiSelect)
                {
                    SelectedFile = null;
                    SelectedFiles = ofd.FileNames;
                }
                else
                {
                    SelectedFile = ofd.FileName;
                    SelectedFiles = null;
                }
                return true;
            }
            return false;
        }
    }
}
