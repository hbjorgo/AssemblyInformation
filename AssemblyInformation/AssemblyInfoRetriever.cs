using System;
using System.Reflection;

namespace AssemblyInformation
{
    public class AssemblyInfoRetriever : MarshalByRefObject
    {
        public void GetInfo(string fileName, out PortableExecutableKinds peKind, out ImageFileMachine imageFileMachine)
        {
            Assembly assembly = Assembly.ReflectionOnlyLoadFrom(fileName);
            assembly.ManifestModule.GetPEKind(out peKind, out imageFileMachine);
        }
    }
}
