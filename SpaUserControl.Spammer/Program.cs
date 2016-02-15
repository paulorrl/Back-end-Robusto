using System;
using System.Globalization;
using System.Threading;
using Microsoft.Practices.Unity;
using SpaUserControl.Domain.Contracts.Services;
using SpaUserControl.Startup;

namespace SpaUserControl.Spammer
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo cultureInfo = new CultureInfo("pt-BR"); // en-US
            Thread.CurrentThread.CurrentCulture   = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            var container = new UnityContainer();
            DependencyResolver.Resolve(container);

            var service = container.Resolve<IUserService>();

            try
            {
                // Attribute "Email" is UNIQUE in Database
                service.Register(name: "Paulo Rodrigues", email: "pauloroberto.prrl@gmail.com", password: "pass1234", confirmPassword: "pass1234");
                Console.WriteLine("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                service.Dispose();
                Console.ReadLine();
            }
        }
    }
}