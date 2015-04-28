using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayer.Application.Resources;

namespace NLayer.Application.Modules
{
    public class NLayerModulesManager
    {
        private static List<NLayerModules> _modules;
        private static readonly object LockObject = new object();

        private static void Init()
        {
            lock (LockObject)
            {
                _modules = new List<NLayerModules>
                {
                    new NLayerModules(NLayerModulesType.BootstrapSamples,
                        ModulesResource.BootstrapSamples),

                    new NLayerModules(NLayerModulesType.UserSystem,
                        ModulesResource.UserSystem)
                };
            }
        }

        private NLayerModulesManager()
        {
            Init();
        }

        public static NLayerModulesManager Instance
        {
            get
            {
                return new NLayerModulesManager();
            }
        }

        public static List<NLayerModules> ListAll()
        {
            return _modules;
        }

        public string GetModulesName(NLayerModulesType type)
        {
            var modules = _modules.FirstOrDefault(x => x.Type == type);

            if (modules == null)
            {
                throw new ArgumentException(type.ToString(), "type");
            }

            return modules.Name;
        }
    }
}
