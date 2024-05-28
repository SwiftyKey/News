using System.ComponentModel;
using System.Runtime.CompilerServices;

/**
	\brief Пространство имен, в котором содержатся абстрактные классы и интерфейсы
	\param Содержит классы:
		@ref BaseChanged
		@ref BaseEntity
		@ref IBaseRepository
		@ref IReadRepository
		@ref IWriteRepository
*/
namespace News.Models.Common;

/**
	\brief Абстрактный класс, предназначенный для оповещения системы об изменениях свойств объектов
	
	Реализует интерфейс INotifyPropertyChanged, что позволяет реализовать привязку данных через паттерн MVVM
*/
public abstract class BaseChanged : INotifyPropertyChanged
{
	/// Событие, через которое происходит оповещение об изменении свойства объекта
	public event PropertyChangedEventHandler? PropertyChanged;

	/**
		\brief  Метод, который вызывается, чтобы указать, какое свойство изменилось
		\param[in] prop Имя измененного свойства
	*/
	public void OnPropertyChanged([CallerMemberName] string prop = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
	}
}
