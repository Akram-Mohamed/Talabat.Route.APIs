using Talabat.Core.Entities;

namespace Talabat.Route.APIs.Helpers
{


	public class Pagination<T>
	{
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int Count { get; set; }
		public IReadOnlyList<T> Data { get; set; }
		public Pagination(int pageIndex, int count, int pageSize, IReadOnlyList<T> data)
		{
			PageIndex = pageIndex;
			PageSize = pageSize;
			Data = data;
			Count=count;
		}

	
	}




}
