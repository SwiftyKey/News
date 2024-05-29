namespace News.Views.MarkupExtensions;

/**
	\brief Интерфейс, определяющий стркутура фильтров
*/
public interface IFilter
{
	/**
		\brief Метод для фильтрации
		\param[in] item Значение, которое сравнивается
		\return Булево значение, обозначающее, подходит ли переданное значение
	*/
	bool Filter(object item);
}
