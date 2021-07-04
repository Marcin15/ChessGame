using ChessGame.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChessGame.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvier();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvier()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));

            services.AddScoped<MainWindowViewModel>();

            services.AddSingleton<ICollectionMerger, CollectionMerger>();
            services.AddSingleton<IPieceCreatorFactory, PieceCreatorFactory>();
            services.AddSingleton<IFieldHightlightManager, FieldHightlightManager>();
            services.AddSingleton<IPieceInteractionManager, PieceInteractionManager>();

            services.AddSingleton<IDarkFieldsModel, DarkFieldsModel>();
            services.AddSingleton<ILightFieldsModel, LightFieldsModel>();

            services.AddSingleton<IAttackMechanicContainer, AttackMechanicContainer>();
            services.AddSingleton<IFlagCleaner, FlagCleaner>();

            services.AddSingleton<IFieldUnderPinChecker, FieldUnderPinChecker>();
            services.AddSingleton<ICheckFinder, CheckFinder>();

            services.AddSingleton<IFlagCleaner, FlagCleaner>();
            services.AddSingleton<IPawnsAttackMechanics, PawnsAttackMechanics>();
            services.AddSingleton<IRookAttackMechanics, RookAttackMechanics>();
            services.AddSingleton<IKnightAttackMechanics, KnightAttackMechanics>();
            services.AddSingleton<IBishopAttackMechanics, BishopAttackMechanics>();
            services.AddSingleton<IQueensAttackMechanics, QueensAttackMechanics>();
            services.AddSingleton<IKingsAttacksMechanics, KingsAttacksMechanics>();

            services.AddSingleton<IInitialLocalizationOfFigures, InitialLocalizationOfFigures>();

            return services.BuildServiceProvider();
        }
    }
}
