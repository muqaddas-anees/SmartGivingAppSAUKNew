using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Data;

namespace Deffinity.BLL
{
	/// <summary>
	/// Table RFI_TradingHistory
	/// 
	/// Generated by matricrix's C# Layer Builder
	/// 23/06/2009 17:12:32
	/// </summary>
	public class RFI_TradingHistory_Base_SVC
	{

		/// <summary>
		/// Insert entity
		/// </summary>
		[DataObjectMethod(DataObjectMethodType.Insert, true)]
		public static int Insert(BE.RFI_TradingHistory oRFI_TradingHistory)
		{
			return DAL.RFI_TradingHistory_DAL.Insert(oRFI_TradingHistory);
		}

		/// <summary>
		/// Search entity
		/// </summary>
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static BE.RFI_TradingHistory Select(int tradinghistoryid)
		{
			return DAL.RFI_TradingHistory_DAL.Select(tradinghistoryid);
		}

		/// <summary>
		/// Check if entity exists
		/// </summary>
		public static bool Exists(int tradinghistoryid)
		{
			return DAL.RFI_TradingHistory_DAL.Exists(tradinghistoryid);
		}

		/// <summary>
		/// Fill entity list
		/// </summary>
		[DataObjectMethod(DataObjectMethodType.Fill, true)]
		public static DataTable Fill()
		{
			return DAL.RFI_TradingHistory_DAL.Fill();
		}

		/// <summary>
		/// Update entity
		/// </summary>
		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Update(BE.RFI_TradingHistory oRFI_TradingHistory)
		{
			return DAL.RFI_TradingHistory_DAL.Update(oRFI_TradingHistory);
		}

		/// <summary>
		/// Delete entity record
		/// </summary>
		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(int tradinghistoryid)
		{
			return DAL.RFI_TradingHistory_DAL.Delete(tradinghistoryid);
		}

		/// <summary>
		/// Delete entity ALL records
		/// </summary>
		[DataObjectMethod(DataObjectMethodType.Delete, false)]
		public static int DeleteAll()
		{
			return DAL.RFI_TradingHistory_DAL.DeleteAll();
		}

	}
}
