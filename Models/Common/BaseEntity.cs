﻿using System.ComponentModel.DataAnnotations;

namespace News.Models.Common;

/**
	\brief Абстрактный класс, являющийся общим для всех объектов сущности базы данных
	
	Наследуется от абстрактного класса BaseChanged
*/
public abstract class BaseEntity: BaseChanged
{
	/// Идентификатор (первичный ключ) сущности базы данных
	[Key]
	public int Id { get; set; }
}
