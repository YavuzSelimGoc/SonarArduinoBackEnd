using System;
using Core.Entities;

namespace Entities.Concrete
{
	public class Data:IEntity
	{
		public int DataId { get; set; }
		public string Distance { get; set; }
		public string Date { get; set; }
    }
}

