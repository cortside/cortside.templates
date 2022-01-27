using System.Collections.Generic;
using Acme.WebApiStarter.BootStrap.Installer;
using Cortside.Common.BootStrap;

namespace Acme.WebApiStarter.BootStrap {
    public class DefaultApplicationBootStrapper : BootStrapper {
        public DefaultApplicationBootStrapper() {
            installers = new List<IInstaller> {
                new DomainEventInstaller(),
                new ExampleHostedServiceInstaller(),
                new HealthInstaller(),
                new DbContextInstaller(),
                new DomainInstaller(),
                new MiniProfilerInstaller(),
                new DistributedLockInstaller(),
                new EncryptionInstaller()
            };
        }
    }
}
