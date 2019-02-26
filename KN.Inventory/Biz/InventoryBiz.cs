using System.Data;

using KN.Inventory.Dac;
using KN.Common.Base.Code;


namespace KN.Inventory.Biz
{
    public class InventoryBiz
    {
 
        
        public static object[] insertItem(object[] parameters)
        {
            return InventoryDao.insertItem(parameters);
        }

        public static void updateItem(object[] parameters)
        {
            InventoryDao.updateItem(parameters);
        }

        public static object[] insertCategory(string groupID,string groupName,string grTypeID, string grTypeName, string grSubTypeID, string grSubTypeName)
        {
            return InventoryDao.insertCategory(groupID,groupName,grTypeID,grTypeName,grSubTypeID,grSubTypeName);
        }

        public static DataSet selectCategory(int pageSize, int pageNow)
        {
            return InventoryDao.selectCategory(pageSize,pageNow);
        }


        public static DataTable selectOneItem(int ivn_ID)
        {
            return InventoryDao.selectOneItem(ivn_ID);
        }

        public static DataSet selectAllItem(int pageSize, int pageNow, string startDate, string endDate)
        {
            return InventoryDao.selectAllItem(pageSize,pageNow,startDate,endDate);
        }

        public static DataSet INV_OUT_SELECT_PAGING_BY_ITEMID(int pageSize, int nowPage, string ivnID)
        {
            return InventoryDao.INV_OUT_SELECT_PAGING_BY_ITEMID(pageSize,nowPage ,ivnID);
        }

        public static DataSet INV_OUT_SELECT_PAGING_ALL(int pageSize, int nowPage, string startDate, string endDate)
        {
            return InventoryDao.INV_OUT_SELECT_PAGING_ALL(pageSize, nowPage, startDate, endDate);
        }
        public static DataTable IVN_OUT_DETAIL_SELECT_BY_ID(int outID)
        {
            return InventoryDao.IVN_OUT_DETAIL_SELECT_BY_ID(outID);
        }

        public static DataTable IVN_OUT_SELECT_BY_ID(int outID)
        {
            return InventoryDao.IVN_OUT_SELECT_BY_ID(outID);
        }
        

        public static DataSet INV_IN_SELECT_PAGING_BY_ITEMID(int pageSize, int nowPage,  string ivnID)
        {
            return InventoryDao.INV_IN_SELECT_PAGING_BY_ITEMID(pageSize, nowPage, ivnID);
        }
        public static DataSet INV_IN_SELECT_PAGING_ALL(int pageSize, int nowPage, string startDate, string endDate)
        {
            return InventoryDao.INV_IN_SELECT_PAGING_ALL(pageSize, nowPage, startDate, endDate);
        }

        public static DataTable IVN_IN_DETAIL_SELECT_BY_ID(int inID)
        {
            return InventoryDao.IVN_IN_DETAIL_SELECT_BY_ID(inID);
        }

        public static DataSet selectInDetailsCategory(int pageSize, int nowPage)
        {
            return InventoryDao.selectInDetailsCategory(pageSize, nowPage);
        }

        public static DataSet selectInCategory(int pageSize, int nowPage)
        {
            return InventoryDao.selectInCategory(pageSize, nowPage);
        }

        
    }
}