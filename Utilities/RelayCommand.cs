using System.Windows.Input;

/**
	\brief Пространство имен, в котором содержится класс RelayCommand
	\param Содержит класс:
		@ref RelayCommand
*/
namespace News.Utilities;

/**
	\brief Класс, предназначенный для создания команд, использующиеся для взаимодействия пользователя и приложения
	
	Наследуется от ICommand
*/
public class RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null) : ICommand
{
	/// Делегат действия команды
	private readonly Action<object?> execute = execute;

	/// Булево значение, показывающее, возможно ли выполнить команду
	private readonly Func<object?, bool>? canExecute = canExecute;


	/// Событие изменения источника команды
	public event EventHandler? CanExecuteChanged
	{
		add { CommandManager.RequerySuggested += value; }
		remove { CommandManager.RequerySuggested -= value; }
	}

	/**
		\brief Метод, проверяющий возможность выполнения команды
		\param[in] parameter Параметр делегата
		\return Булово значение выполнимости команды
	*/
	public bool CanExecute(object? parameter) => canExecute == null || canExecute(parameter);


	/**
		\brief Метод, выполняющий действие команды
		\param[in] parameter Параметр делегата
	*/
	public void Execute(object? parameter) => execute(parameter);
}