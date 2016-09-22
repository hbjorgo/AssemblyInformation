using System;
using System.IO;
using System.Reflection;

namespace AssemblyInformation
{
    public class AssemblyInfoRetriever : MarshalByRefObject
    {
        public AssemblyInfo GetInfo(string fileName)
        {
            Assembly assembly = Assembly.ReflectionOnlyLoadFrom(fileName);

            PortableExecutableKinds peKind;
            ImageFileMachine imageFileMachine;
            assembly.ManifestModule.GetPEKind(out peKind, out imageFileMachine);

            AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();

            AssemblyInfo ai = new AssemblyInfo(Path.GetFileName(fileName), fileName, Path.GetExtension(fileName), peKind, imageFileMachine, referencedAssemblies);
            return ai;
        }
    }
}
