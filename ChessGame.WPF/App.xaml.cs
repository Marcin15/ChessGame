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

            var gameConnectionWindow = serviceProvider.GetRequiredService<GameConnectionWindow>();
            gameConnectionWindow.ShowDialog();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvier()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(s => new MainWindow(s.GetRequiredService<MainWindowViewModel>()));
            services.AddSingleton<GameConnectionWindow>(s => new GameConnectionWindow(s.GetRequiredService<GameConnectionWindowViewModel>()));

            services.AddScoped<MainWindowViewModel>();
            services.AddScoped<GameConnectionWindowViewModel>();

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

            services.AddSingleton<ICheckMateChecker, CheckMateChecker>();

            services.AddSingleton<IServerConnection, ServerConnection>();
            services.AddTransient<IDataSender, DataSender>();

            services.AddSingleton<IMessageBoxService, MessageBoxService>();
            services.AddSingleton<IShowMainWindowService, ShowMainWindowService>();
            
            return services.BuildServiceProvider();
        }
    }
}
