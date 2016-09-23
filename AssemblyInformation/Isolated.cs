using System;

namespace AssemblyInformation
{
    public class Isolated<T> : IDisposable where T : MarshalByRefObject
    {
        private bool disposed;
        private AppDomain domain;
        private T handle;

        public Isolated()
        {
            domain = AppDomain.CreateDomain("Isolated:" + Guid.NewGuid(), null, AppDomain.CurrentDomain.SetupInformation);

            Type type = typeof(T);

            handle = (T)domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
        }

        public T Handle
        {
            get
            {
                return handle;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (domain != null)
                {
                    AppDomain.Unload(domain);
                    domain = null;
                }

                if (handle != null && handle is IDisposable)
                {
                    ((IDisposable)handle).Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Isolated()
        {
            Dispose(false);
        }
    }
}
