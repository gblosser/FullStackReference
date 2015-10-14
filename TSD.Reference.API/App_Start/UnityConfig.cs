using System;
using Microsoft.Practices.Unity;
using TSD.Reference.Core.Data;
using TSD.Reference.Core.Services;
using TSD.Reference.Core.Services.Interfaces;
using TSD.Reference.Data.PostgreSQL.Repositories;

namespace TSD.Reference.API.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
	        container.RegisterType<ICustomerRepository, CustomerRepository>(new ContainerControlledLifetimeManager());
	        container.RegisterType<ICustomerService, CustomerService>(new ContainerControlledLifetimeManager());

	        container.RegisterType<IAutomobileRepository, AutomobileRepository>(new ContainerControlledLifetimeManager());
	        container.RegisterType<ILocationRepository, LocationRepository>(new ContainerControlledLifetimeManager());
	        container.RegisterType<IAutomobileService, AutomobileService>(new ContainerControlledLifetimeManager());

			container.RegisterType<ILocationService, LocationService>(new ContainerControlledLifetimeManager());

			container.RegisterType<IRentalAgreementRepository, RentalAgreementRepository>(new ContainerControlledLifetimeManager());
			container.RegisterType<IRentalAgreementService, RentalAgreementService>(new ContainerControlledLifetimeManager());

			container.RegisterType<IDriverRepository, DriverRepository>(new ContainerControlledLifetimeManager());
			container.RegisterType<IDriverService, DriverService>(new ContainerControlledLifetimeManager());

			container.RegisterType<IRenterRepository, RenterRepository>(new ContainerControlledLifetimeManager());
			container.RegisterType<IRenterService, RenterService>(new ContainerControlledLifetimeManager());

			container.RegisterType<IUserRepository, UserRepository>(new ContainerControlledLifetimeManager());
			container.RegisterType<IUserService, UserService>(new ContainerControlledLifetimeManager());

		}
    }
}
