using System.Collections.Generic;

namespace AssemblyInformation
{
    public interface IOpenFileService
    {
        string SelectedFile { get; }
        IEnumerable<string> SelectedFiles { get; }

        bool OpenFile(string initialDirectory = null);
    }
}