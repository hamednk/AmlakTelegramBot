using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace AmlakBot.Models
{
    class ActionDb
    {
        Dal dal = new Dal();
        DataTable dtCmdState, dtCmdStateChk = new DataTable();
        public void _insertState(long TlgId)
        {
            try
            {
                if (_selectState(TlgId).Rows.Count == 0)
                {
                    Dictionary<string, object> Insert_CommandState = new Dictionary<string, object>();
                    Insert_CommandState["@TelegramId"] = TlgId;
                    dtCmdState = dal.ExecuteReader("Insert_CommandState", Insert_CommandState);
                }
            }
            catch { }
        }
        public void _deleteState(long TlgId)
        {
            try
            {
                Dictionary<string, object> Delete_CommandState = new Dictionary<string, object>();
                Delete_CommandState["@TelegramId"] = TlgId;
                dtCmdStateChk = dal.ExecuteReader("Delete_CommandState", Delete_CommandState);
            }
            catch { }
        }
        public DataTable _selectState(long TlgId)
        {
            //  dtCmdStateChk.Clear();
            Dictionary<string, object> Select_CommandState_GetTlgId = new Dictionary<string, object>();
            Select_CommandState_GetTlgId["@TelegramId"] = TlgId;
            dtCmdStateChk = dal.ExecuteReader("Select_CommandState_GetTlgId", Select_CommandState_GetTlgId);
            return dtCmdStateChk;
        }
        public void _updateState(long TlgId, string PrimaryCmd, string SubCmd, int State, string PM)
        {

            //  dtCmdState.Clear();
            Dictionary<string, object> Update_CommandState = new Dictionary<string, object>();
            Update_CommandState["@TelegramId"] = TlgId;
            Update_CommandState["@PrimaryCommand"] = PrimaryCmd;
            Update_CommandState["@SubCommand"] = SubCmd;
            Update_CommandState["@State"] = State;
            Update_CommandState["@PM"] = PM;
            dtCmdState = dal.ExecuteReader("Update_CommandState", Update_CommandState);

        }
        public void _insertEstate(string CatName, long tlgId, string FullnameOwner, string MobileNo, string Region, int Area, int Price1, int Price2, string StatusDocument, string Descr, string EsImage, string Spec1, string Spec2, string Spec3, string Spec4, string Spec5, string Spec6, int State,string TOT)
        {
            //  dtCmdState.Clear();
            Dictionary<string, object> Insert_Estates = new Dictionary<string, object>();
            Insert_Estates["@EstateCatId"] = int.Parse(_getValue(tlgId, StatusDocument, CatName).Rows[0]["EstateCatsId"].ToString());
            Insert_Estates["@DateTimeCreated"] = DateTime.Now;
            Insert_Estates["@UserId"] = int.Parse(_getValue(tlgId, StatusDocument, CatName).Rows[0]["UserId"].ToString());
            Insert_Estates["@FullnameOwner"] = FullnameOwner;
            Insert_Estates["@MobileNo"] = MobileNo;
            Insert_Estates["@Region"] = Region;
            Insert_Estates["@Area"] = Area;
            Insert_Estates["@Price1"] = Price1;
            Insert_Estates["@Price2"] = Price2;
            Insert_Estates["@StatusDocumentId"] = int.Parse(_getValue(tlgId, StatusDocument, CatName).Rows[0]["StatusDocumentsId"].ToString());
            Insert_Estates["@Descr"] = Descr;
            Insert_Estates["@EstateImg"] = EsImage;
            Insert_Estates["@Spec1"] = Spec1;
            Insert_Estates["@Spec2"] = Spec2;
            Insert_Estates["@Spec3"] = Spec3;
            Insert_Estates["@Spec4"] = Spec4;
            Insert_Estates["@Spec5"] = Spec5;
            Insert_Estates["@Spec6"] = Spec6;
            Insert_Estates["@State"] = State;
            Insert_Estates["@TOT"] = TOT;
            dtCmdState = dal.ExecuteReader("Insert_Estates", Insert_Estates);
        }
        public DataTable _getValue(long tlgId, string statusDoc, string catName)
        {
            DataTable dtValue = new DataTable();
            Dictionary<string, object> Select_AnyFieldGetValue = new Dictionary<string, object>();
            Select_AnyFieldGetValue["@TlgId"] = tlgId;
            Select_AnyFieldGetValue["@StateDoc"] = statusDoc;
            Select_AnyFieldGetValue["@CatName"] = catName;
            dtValue = dal.ExecuteReader("Select_AnyFieldGetValue", Select_AnyFieldGetValue);
            return dtValue;
        }
    }
}
