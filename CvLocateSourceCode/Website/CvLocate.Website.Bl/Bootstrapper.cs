using AutoMapper;
using CvLocate.Common.DbFacadeInterface;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.Common.Logging;
using CvLocate.DBComponent.EndUserDBFacade;
using CvLocate.Website.Bl.DataWrapper;
using CvLocate.Website.Bl.Interfaces;
using CvLocate.Website.Bl.Response;
using SimpleInjector;
using SimpleInjector.Advanced;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.Website.Bl
{
    public class Bootstrapper
    {

        #region Singleton

        // lock for thread-safety laziness
        private static readonly object _mutex = new object();

        // static field to hold single instance
        private static volatile Bootstrapper _instance = null;

        // property that does some locking and then creates on first call
        public static Bootstrapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_mutex)
                    {
                        if (_instance == null)
                        {
                            _instance = new Bootstrapper();
                        }
                    }
                }

                return _instance;
            }
        }

        private Bootstrapper()
        {
        }

        #endregion

        public Container Container { get; set; }

        public void Initialize()
        {
            this.Container = new Container();
            this.Container.Options.PropertySelectionBehavior = new ImportPropertySelectionBehavior();
            //check in .net5 container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            this.Container.RegisterSingleton<ICvLocateLogger>(new CvLocateLogger("websiteLogger"));
            this.Container.Register<IUserDBFacade, UserDBFacade>(Lifestyle.Transient);
            this.Container.Register<IUserDataWrapper, UserDataWrapper>(Lifestyle.Transient);
            this.Container.Register<IRecruiterDBFacade, RecruiterDBFacade>(Lifestyle.Transient);
            this.Container.Register<IRecruiterDataWrapper, RecruiterDataWrapper>(Lifestyle.Transient);

            this.Container.Register<IUserBl, UserBl>(Lifestyle.Transient);

            RegisterAutoMapper();
        }

        private void RegisterAutoMapper()
        {
            Mapper.CreateMap<SignResponse, SignupResponse>();
        }
    }

    public class ImportPropertySelectionBehavior : IPropertySelectionBehavior
    {
        public bool SelectProperty(Type type, PropertyInfo prop)
        {
            return prop.GetCustomAttributes(typeof(ImportAttribute)).Any();
        }
    }
}
