using VContainer;
using VContainer.Unity;

namespace Company.Runtime.Bootstrap {
  public class BootstrapScope : LifetimeScope {

    protected override void Configure(IContainerBuilder builder) {
      builder.RegisterEntryPoint<BootstrapFlow>();
    }
  }
}