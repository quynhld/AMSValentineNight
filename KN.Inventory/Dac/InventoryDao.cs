using System.Data;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;

namespace KN.Inventory.Dac
{
    public class InventoryDao
    {

        //quynhld----------------------------------------------
        public static object[] insertCategory(string groupID, string groupName, string grTypeID, string grTypeName, string grSubTypeID, string grSubTypeName)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = groupID;
            objParams[1] = groupName;
            objParams[2] = grTypeID;
            objParams[3] = grTypeName;
            objParams[4] = grSubTypeID;
            objParams[5] = grSubTypeName;

            objReturn = SPExecute.ExecReturnNo("KN_USP_IVN_INSERT_CATEGORY", objParams);
            return objReturn;
        }

        public static DataSet selectCategory(int pageSize, int pageNow)
        {
            DataSet ds = new DataSet();
            object[] objParams = new object[2];
            objParams[0] = pageSize;
            objParams[1] = pageNow;
            ds = SPExecute.ExecReturnMulti("KN_USP_IVN_SELECT_CATEGORY", objParams);
            return ds;
        }
        
        public static object[] insertItem(object[] parameters)
        {
            object[] objReturn = new object[2];
            objReturn = SPExecute.ExecReturnNo("KN_USP_IVN_ITEM_INSERT",parameters);
            return objReturn;
        }

        public static void updateItem(object[] parameters)
        {
            //object[] objReturn = new object[1];
            //objReturn = SPExecute.ExecReturnNo("KN_USP_IVN_ITEM_UPDATE", parameters);
            //return objReturn; 
            object[] objParams = new object[22];
            objParams[0] = parameters[0];
            objParams[1] = parameters[1];
            objParams[2] = parameters[2];
            objParams[3] = parameters[3];
            objParams[4] = parameters[4];
            objParams[5] = parameters[5];
            objParams[6] = parameters[6];
            objParams[7] = parameters[7];
            objParams[8] = parameters[8];
            objParams[9] = parameters[9];
            objParams[10] = parameters[10];
            objParams[11] = parameters[11];
            objParams[12] = parameters[12];
            objParams[13] = parameters[13];
            objParams[14] = parameters[14];
            objParams[15] = parameters[15];
            objParams[16] = parameters[16];
            objParams[17] = parameters[17];
            objParams[18] = parameters[18];
            objParams[19] = parameters[19];
            objParams[20] = parameters[20];
            objParams[21] = parameters[21];
            SPExecute.ExecReturnNo("KN_USP_IVN_ITEM_UPDATE", parameters);
        }

        public static DataTable selectOneItem(int ivn_ID)
        {
            DataTable dtb = new DataTable();
            object[] objParams = new object[1];
            objParams[0] = ivn_ID;
            dtb = SPExecute.ExecReturnSingle("KN_USP_IVN_ITEM_SELECT_ONE", objParams);
            return dtb;
        }

        public static DataSet selectAllItem(int pageSize, int pageNow, string startDate, string endDate)
        {
            DataSet ds = new DataSet();
            object[] objParams = new object[4];
            objParams[0] = pageSize;
            objParams[1] = pageNow;
            objParams[2] = startDate;
            objParams[3] = endDate;
            ds = SPExecute.ExecReturnMulti("KN_USP_IVN_ITEM_SELECT_ALL", objParams);
            return ds;
        }

        public static DataSet INV_OUT_SELECT_PAGING_BY_ITEMID(int pageSize, int nowPage, string ivnID)
        {
            DataSet dtb = new DataSet();
            object[] objParams = new object[3];
            objParams[0] = pageSize;
            objParams[1] = nowPage;
            objParams[2] = ivnID;

            dtb = SPExecute.ExecReturnMulti("KN_USP_IVN_OUT_SELECT_PAGING_BY_IVNID",objParams);
            return dtb;
        }

        public static DataSet INV_IN_SELECT_PAGING_BY_ITEMID(int pageSize, int nowPage, string ivnID)
        {
            DataSet dtb = new DataSet();
            object[] objParams = new object[3];
            objParams[0] = pageSize;
            objParams[1] = nowPage;
            objParams[2] = ivnID;

            dtb = SPExecute.ExecReturnMulti("KN_USP_IVN_IN_SELECT_PAGING_BY_IVNID", objParams);
            return dtb;
        }
    }
}