using QuizSystemApp_LMS.NewFolder.Core;
using QuizSystemApp_LMS.NewFolder.Core.Menus;

var system = new QuizSystem(); //Luan McShane B00983827
system.LoadSampleData();

var menu = new MainMenu(system);
menu.Show();
