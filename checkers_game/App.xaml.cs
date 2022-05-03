using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using checkers_game.Model;
using checkers_game.ViewModel;
using checkers_game.Persistence;
using System.ComponentModel;
using Microsoft.Win32;

namespace checkers_game
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Private variables
        private GameModel _model;
        private GameViewModel _viewModel;
        private MainWindow _view;
        #endregion
        #region Constructor
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }
        #endregion
        #region Application event handlers
        private void App_Startup(object sender, StartupEventArgs e)
        {
            _model = new GameModel(new ModelDataAccess());
            _model.GameOver += new Action<Player>(ViewModel_GameOver);
            _model.NewGame();

            _viewModel = new GameViewModel(_model);
            _viewModel.NewGame += new EventHandler(ViewModel_NewGame);
            _viewModel.LoadGame += new EventHandler(ViewModel_LoadGame);
            _viewModel.SaveGame += new EventHandler(ViewModel_SaveGame);
            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing);
            _view.Show();

        }
        #endregion
        #region View event handlers
        private void View_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Checker_Game", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
        #endregion
        #region ViewModel event handlers
        private void ViewModel_NewGame(object sender, EventArgs e)
        {
            _model.NewGame();
            _viewModel.RefreshTable();
        }
        private void ViewModel_GameOver(Player e)
        {

            if (e==Player.PlayerWhite)
            {
                MessageBox.Show("Black Player Won!", "OK");
                _model.NewGame();
                _viewModel.RefreshTable();

            }
            if (e == Player.PlayerBlack)
            {
                MessageBox.Show("White Player Won!", "OK");
                _model.NewGame();
                _viewModel.RefreshTable();
            }
        }
        private async void ViewModel_LoadGame(object sender, System.EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Checker_Game loading";
                openFileDialog.Filter = "Checker_Game table|*.stl";
                if (openFileDialog.ShowDialog() == true)
                {
                    await _model.LoadGame(openFileDialog.FileName);
                }
            }
            catch (ModelDataException)
            {
                MessageBox.Show("Error during file loading!", "Checker_Game", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _viewModel.RefreshTable();
        }
        private async void ViewModel_SaveGame(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog(); // dialógablak
                saveFileDialog.Title = "Checker_Game saving";
                saveFileDialog.Filter = "Checker_Game table|*.stl";
                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        await _model.SaveGame(saveFileDialog.FileName);
                    }
                    catch (ModelDataException)
                    {
                        MessageBox.Show("Error during saving!" + Environment.NewLine + "Wrong path or file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error during saving!", "Checker_Game", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _viewModel.RefreshTable();
        }
        #endregion
    }
}
