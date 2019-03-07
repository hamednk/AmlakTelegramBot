using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BijanComponents;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Helpers;
using Telegram.Bot.Exceptions;
using System.Data;
using System.Text.RegularExpressions; 
using AmlakBot.Models;
 //Rent

namespace AmlakBot 
{ 
    class Program
    { 
        private static string Token = "355857160:AAENG9htiidaSP13Y855MZX_Qzb4VJZv0uE";
        private static readonly TelegramBotClient Bot = new TelegramBotClient(Token);
        private static string SendTextMessage, _sale;
        public static ChatState chatState = new ChatState();
        public static Dal dal = new Dal();
        public static DataTable dtLogin, dt = new DataTable();
        public static ActionDb actionDb = new ActionDb();
        public static Message message;
        private static ReplyKeyboardMarkup MainKey, TypeAdsSaleKey, TypeAdsMARKey, TypeAdsKey,
            PreviewKey, ReturnKey, ReturnMainKey, StateDocKey,
            BackBoneKey;
        static void Main(string[] args)
        {
            //try
            //{
            StartConfig();
            //}
            //catch { Console.WriteLine("The network has a problem..."); Console.ReadLine(); }
            //finally
            //{
            //    StartConfig();
            //}
        }
        private static void StartConfig()
        {
            var me = Bot.GetMeAsync().Result;

            Console.WriteLine(me.Username + " Started...");
            Console.Title = me.Username;

            Keyboards();
            Bot.OnMessage += Bot_OnMessage;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.OnReceiveError += BotOnReceiveError;

            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }
        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            //Debugger.Break();
            try { /*Bot.SendTextMessageAsync(message.Chat.Id, "ربات منتظر دستور شما است");*/ } catch { }
        }
        private static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            message = e.Message;

            if (message == null) return;

            actionDb._insertState(message.Chat.Id);
            Section();
            Estate(actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString());
            SysLog();
            Login();


        }
        private static void Section()
        {
            if (message.Text != null && message.Text.Contains("start"))
            {
                SendTextMessage = @"👤 به ربات دستیار املاک خوش آمدید
🔑لطفا رمز عبور خود را وارد کنید";
                Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                actionDb._updateState(message.Chat.Id, "", "", 1, "Login");
            }
            else if (message.Text != null && message.Text == "🔙 بازگشت به منوی اصلی")
            {
                SendTextMessage = @"لطفا گزینه مورد نظر خود را انتخاب کنید";
                Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: MainKey);
                actionDb._updateState(message.Chat.Id, "", "", 1, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString());
            }
            else if (message.Text != null && (message.Text == "ثبت آگهی" || message.Text == "🔙 بازگشت به قبل"))
            {
                SendTextMessage = @"نوع آگهی ارسالی خود را انتخاب کنید";
                Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: TypeAdsKey);
            }
            else if (message.Text != null && message.Text == "رهن/اجاره")
            {
                actionDb._updateState(message.Chat.Id, "رهن/اجاره", "", 1, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString());
                SendTextMessage = @"نوع ملک رهن/اجاره خود را انتخاب کنید";
                Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: TypeAdsMARKey);
            }
            else if (message.Text != null && message.Text == "فروش")
            {
                actionDb._updateState(message.Chat.Id, "فروش", "", 1, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString());
                _sale = message.Text;

                SendTextMessage = @"نوع ملک فروش خود را انتخاب کنید";
                Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: TypeAdsSaleKey);
            }
            else if (message.Text != null && message.Text == "نمایش آگهی")
            {
                _sendAds(long.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["TelegramId"].ToString()));
            }
            else if (message.Text != null && message.Text == "خروج ❌")
            {
                SendTextMessage = @"با تشکر از شما🌺
برای وارد شدن دوباره، روی گزینه /start بزنید";
                Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide(), parseMode: ParseMode.Html);
            }

            else if (message.Text != null && message.Text == GetCatname(message.Text))
            {
                #region Dynamic GetCatname
                if (actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString() == "رهن/اجاره")
                {
                    for (int i = 0; i < ListTypeAds(false).Length; i++)
                    {
                        if (message.Text == ListTypeAds(false)[i])
                        {
                            if (actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString() == "")
                            {
                                actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), GetCatname(ListTypeAds(false)[i]), 1, "");
                                //  Bot.SendTextMessageAsync(message.Chat.Id, "لطفا اطلاعات ملک مورد نظر را وارد کنید", replyMarkup: ReturnKey);
                                Estate(actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString());
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < ListTypeAds(true).Length; j++)
                    {
                        if (message.Text == ListTypeAds(true)[j])
                        {
                            if (actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString() == "")
                            {
                                actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), GetCatname(ListTypeAds(true)[j]), 1, "");
                                // Bot.SendTextMessageAsync(message.Chat.Id, "لطفا اطلاعات ملک مورد نظر را وارد کنید", replyMarkup: ReturnKey);
                                Estate(actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString());
                            }
                        }
                    }
                }
                #endregion
            }
            //            else if (message.Text != null && message.Text == "ذخیره آگهی")
            //            {
            //                chatState.State = 0;
            //                chatState.SubCommand = "";
            //                chatState.PrimaryCommand = "";
            //                SendTextMessage = @"باتشکر
            //اطلاعات شما با موفقیت ذخیره شد
            //کارشناسان بعد بررسی، آگهی شما را روی کانال قرار میدهند.";
            //                Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
            //            }
        }

        private static async void _sendAds(long TlgId)
        {
            string hashTagPrice1, hashTagPrice2, hashTagArea;
            try
            {
                DataTable dtGetInfoEstate, dtSetDataValues = new DataTable();
                Dictionary<string, object> Select_Estates_GetUserId = new Dictionary<string, object>();
                Select_Estates_GetUserId["@TelegramUserId"] = TlgId;
                dtGetInfoEstate = dal.ExecuteReader("Select_Estates_GetUserId", Select_Estates_GetUserId);

                foreach (DataRow dr in dtGetInfoEstate.Rows)
                {
                    string[] Value = _getValueGenerate(int.Parse(dr["Price1"].ToString()), int.Parse(dr["Price2"].ToString()), int.Parse(dr["area"].ToString())).Split('-');

                    hashTagArea = Value[2];
                    hashTagPrice1 = Value[0];
                    hashTagPrice2 = Value[1];
                    string stdoc = dr["StatusDocument"].ToString().Contains(' ') == true ? dr["StatusDocument"].ToString().Replace(" ", "_") : dr["StatusDocument"].ToString();
                    string region = dr["Region"].ToString().Contains(' ') == true ? dr["Region"].ToString().Replace(" ", "_") : dr["Region"].ToString();
                    SendTextMessage = @"کد : " + dr["EstateId"] + "\n#" + dr["TOT"] + "_" + dr["CatName"] + " #" + region + hashTagArea +" #" +
                      dr["Spec4"] + "خواب #" + stdoc + hashTagPrice1 + hashTagPrice2 + "\n 🌿" + dr["area"] + "متر\n"  +
                    "🎲طبقه " + dr["Spec2"] + " از " + dr["Spec1"] + "\n💰" + dr["Price1"] + "نقد  + " + dr["Price2"] + " وام\n📞کارشناس:" + dr["fullname"] + " " + dr["MobileBongah"] +
                    "\n@ChannelName";
                    await Bot.SendPhotoAsync(message.Chat.Id, dr["EstateImg"].ToString(), SendTextMessage);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private static string _getValueGenerate(int price1, int price2, int area)
        {
            string P1 = "", P2 = "", Ar = "";
            if (price1 <= 50)
            {
                P1 = " #نقدی0تا50میلیون ";
            }
            else if (price1 <= 100)
            {
                P1 = " #نقدی50تا100میلیون ";
            }
            else if (price1 <= 150)
            {
                P1 = " #نقدی100تا150میلیون ";
            }
            else if (price1 > 150)
            {
                P1 = " #نقدی150میلیون_به_بالا ";
            }
            //----------------------End Price1--------------
            if (price2 <= 10)
            {
                P2 = " #وام0تا10میلیون ";
            }
            else if (price2 <= 20)
            {
                P2 = " #وام10تا20میلیون ";
            }
            else if (price2 <= 30)
            {
                P2 = " #وام20تا30میلیون ";
            }
            else if (price2 > 30)
            {
                P2 = " #وام30میلیون_به_بالا ";
            }
            //----------------------End Price2--------------
            if (area <= 50)
            {
                Ar = " #0تا50متر ";
            }
            else if (area <= 100)
            {
                Ar = " #50تا100متر ";
            }
            else if (area <= 150)
            {
                Ar = " #100تا150متر ";
            }
            else if (area > 150)
            {
                Ar = " #150متر_به_بالا ";
            }

            return P1 + "-" + P2 + "-" + Ar;
        }

        private static void Login()
        {
            try
            {
                if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 1 && long.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["TelegramId"].ToString()) == message.Chat.Id && actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() == "Login")
                {
                    actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 2, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString());
                }
                else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 2 && long.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["TelegramId"].ToString()) == message.Chat.Id && actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() == "Login")
                {
                    actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), message.Text, 2, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString());
                    Dictionary<string, object> Select_Users_Login = new Dictionary<string, object>();
                    Select_Users_Login["@TelegramUserId"] = actionDb._selectState(message.Chat.Id).Rows[0]["TelegramId"];
                    Select_Users_Login["@Password"] = ConvertPnNumberToEn(actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString());
                    dtLogin = dal.ExecuteReader("Select_Users_Login", Select_Users_Login);

                    if (dtLogin.Rows.Count > 0)
                    {
                        Bot.SendTextMessageAsync(message.Chat.Id, @"🌺 آقا/خانم " + string.Format("*{0}*", dtLogin.Rows[0]["Fullname"]) + @" خوش آمدید 
لطفا یکی از گزینه های زیر را انتخاب کنید", replyMarkup: MainKey, parseMode: ParseMode.Markdown);
                        actionDb._deleteState(message.Chat.Id);
                    }
                    else
                    {
                        Bot.SendTextMessageAsync(message.Chat.Id, @"⛔️ رمز عبور اشتباه است 
لطفا مجددا رمز عبور خود را وارد نمایید", replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, "", "", 1, "Login");
                        Login();
                    }
                }
            }
            catch { }
        }
        public static async void Estate(string catName)
        {

            switch (catName)
            {
                case "آپارتمان":
                    #region Get Info Apartment


                    if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 1)// && actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString() == "فروش")
                    {
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 2, "");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 2)
                    {
                        SendTextMessage = @"لطفا منطقه را بنویسید";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 3, "");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 3)
                    {
                        SendTextMessage = @"لطفا متراژ آپارتمان را بنویسید";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 4, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + message.Text + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 4)
                    {
                        SendTextMessage = @"لطفا وضعیت سند آپارتمان خود را انتخاب کنید";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: StateDocKey);

                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 5, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + _onlyNumbers(message.Text)[0] + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 5)
                    {
                        SendTextMessage = @"لطفا تعداد خواب آپارتمان را بنویسید";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 6, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + message.Text + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 6)
                    {
                        SendTextMessage = @"قیمت نقدی آپارتمان چند میلیون است؟";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 7, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + _onlyNumbers(message.Text)[0] + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 7)
                    {
                        SendTextMessage = @"مقدار وام آپارتمان چند میلیون است؟";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 8, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + _onlyNumbers(message.Text)[0] + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 8)
                    {
                        SendTextMessage = @"لطفا نام مالک آپارتمان را بنویسید";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 9, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + _onlyNumbers(message.Text)[0] + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 9)
                    {
                        SendTextMessage = @"لطفا تلفن همراه مالک آپارتمان را بنویسید";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 10, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + message.Text + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 10)
                    {
                        SendTextMessage = @"آپارتمان شما چند طبقه است؟";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 11, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + message.Text + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 11)
                    {
                        SendTextMessage = @"واحد شما طبقه چندم است؟";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 12, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + message.Text + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 12)
                    {
                        SendTextMessage = @"اسکلت آپارتمان از چه نوعی است؟";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: BackBoneKey);
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 13, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + message.Text + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 13)
                    {
                        SendTextMessage = @"عمر آپارتمان چند سال است؟";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 14, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + message.Text + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 14)
                    {
                        SendTextMessage = @"توضیحات تکمیلی خود را بنویسید";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 15, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + message.Text + "~");
                    }
                    else if (int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 15)
                    {
                        SendTextMessage = @"لطفا یک تصویر برای آپارتمان ارسال کنید";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: new ReplyKeyboardHide());
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 16, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + message.Text + "~");
                    }
                    else if (message.Type == MessageType.PhotoMessage && int.Parse(actionDb._selectState(message.Chat.Id).Rows[0]["state"].ToString()) == 16)
                    {
                        SendTextMessage = @"آگهی شما با موفقیت ثبت شد
برای دیدن آگهی خود، نمایش آگهی را بزنید";
                        await Bot.SendTextMessageAsync(message.Chat.Id, SendTextMessage, replyMarkup: MainKey);
                        actionDb._updateState(message.Chat.Id, actionDb._selectState(message.Chat.Id).Rows[0]["PrimaryCommand"].ToString(), actionDb._selectState(message.Chat.Id).Rows[0]["SubCommand"].ToString(), 1, actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString() + message.Photo[0].FileId);

                        string[] Values = actionDb._selectState(message.Chat.Id).Rows[0]["PM"].ToString().Split('~');

                        actionDb._insertEstate(@"آپارتمان", message.Chat.Id, Values[6], Values[7], Values[0], int.Parse(Values[1]),
                            int.Parse(Values[4]), int.Parse(Values[5]), Values[2], Values[12], message.Photo[0].FileId, Values[8], Values[9], Values[10], Values[3],
                            Values[11], "-", 1, _sale);
                    }
                    #endregion
                    break;
            }
        }
        public static string[] _onlyNumbers(string text)
        {
            string[] onlyNumber = Regex.Split(text, @"\D+");
            return onlyNumber;
        }
        public static void SysLog()
        {
            //Dictionary<string, object> InsertN_Log = new Dictionary<string, object>();
            //InsertN_Log["@TelegramUserId"] = message.Chat.Username;
            //InsertN_Log["@DateTimeRun"] = DateTime.Now;
            //InsertN_Log["@Command"] = message.Text;
            //InsertN_Log["@LogicalDeleted"] = false;
            //dt = dal.ExecuteReader("InsertN_Log", InsertN_Log);
        }
        private static void Keyboards()
        {
            MainKey = new ReplyKeyboardMarkup(new[]
            {
                new []
                {
                    new KeyboardButton("اطلاعات")  ,
                    new KeyboardButton("نمایش آگهی")  ,
                    new KeyboardButton("ثبت آگهی")  ,

                },
                //new []
                //{
                //    new KeyboardButton("ℹ️ درباره ما"),
                //    new KeyboardButton("❓ راهنما") ,
                //},
                 new []
                {
                    new KeyboardButton("📨 انتقادات و پیشنهادات") ,
                },
                  new []
                {
                    new KeyboardButton("خروج ❌") ,
                }
            });
            MainKey.ResizeKeyboard = true;
            //---------------------------------------------------------------------
            TypeAdsKey = new ReplyKeyboardMarkup(new[]
            {
                new []
                {
                    new KeyboardButton("فروش"),
                    new KeyboardButton("رهن/اجاره") ,
                },
                new []
                {
                    new KeyboardButton("🔙 بازگشت به منوی اصلی") ,
                }
            });
            TypeAdsKey.ResizeKeyboard = true;
            //---------------------------------------------------------------------
            TypeAdsMARKey = new ReplyKeyboardMarkup(DynamicKeyboard.GetKeyboard(ListTypeAds(false), 2));
            TypeAdsMARKey.ResizeKeyboard = true;
            //---------------------------------------------------------------------
            TypeAdsSaleKey = new ReplyKeyboardMarkup(DynamicKeyboard.GetKeyboard(ListTypeAds(true), 2));
            TypeAdsSaleKey.ResizeKeyboard = true;
            //---------------------------------------------------------------------
            StateDocKey = new ReplyKeyboardMarkup(DynamicKeyboard.GetKeyboard(_stateDocument(), 4));
            StateDocKey.ResizeKeyboard = true;
            //---------------------------------------------------------------------
            BackBoneKey = new ReplyKeyboardMarkup(new[]
            {
                new []
                {
                    new KeyboardButton("فلزی"),
                    new KeyboardButton("بتون") ,
                },
            });
            BackBoneKey.ResizeKeyboard = true;
            //---------------------------------------------------------------------
            PreviewKey = new ReplyKeyboardMarkup(new[]
            {
                new []
                {
                    new KeyboardButton("پیش نمایش آگهی") ,
                    new KeyboardButton("ذخیره آگهی") ,
                },
                new []
                {
                    new KeyboardButton("🔙 بازگشت به قبل") ,
                }
            });
            PreviewKey.ResizeKeyboard = true;

            ReturnMainKey = new ReplyKeyboardMarkup(new[]
            {
               new []
                {
                    new KeyboardButton("🔙 بازگشت به منوی اصلی") ,
                }
            });
            ReturnMainKey.ResizeKeyboard = true;

            ReturnKey = new ReplyKeyboardMarkup(new[]
            {
               new []
                {
                    new KeyboardButton("🔙 بازگشت به قبل") ,
                }
            });
            ReturnKey.ResizeKeyboard = true;
        }
        private static void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            try
            {
                var callbackQuery = callbackQueryEventArgs.CallbackQuery;
            }
            catch { }

        }

        public static string[] ListTypeAds(bool value)
        {
            Dictionary<string, object> Select_EstateCats = new Dictionary<string, object>();
            Select_EstateCats["@value"] = value;
            dt = dal.ExecuteReader("Select_EstateCats", Select_EstateCats);
            string[] ListTypeAdsName = dt.Rows.OfType<DataRow>().Select(k => k[1].ToString()).ToArray();
            return ListTypeAdsName;
        }
        public static string[] _keyNumbers(int value)
        {
            dt = dal.ExecuteReader("Select_KeyNumbers");
            string[] Keys = ToPersianNumber(dt.Rows[0][value].ToString()).Split('-');
            return Keys;
        }
        public static string[] _stateDocument()
        {
            dt = dal.ExecuteReader("Select_StatusDocument");
            string[] StateDoc = dt.Rows.OfType<DataRow>().Select(k => k[1].ToString()).ToArray();
            return StateDoc;
        }

        public static string GetCatname(string catName)
        {
            Dictionary<string, object> Select_EstateCats_GetCat = new Dictionary<string, object>();
            Select_EstateCats_GetCat["@CatName"] = catName;
            dt = dal.ExecuteReader("Select_EstateCats_GetCat", Select_EstateCats_GetCat);
            return dt.Rows.Count == 0 ? "" : dt.Rows[0]["Catname"].ToString();
        }

        #region ConvertNumber
        private static readonly string[] pn = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        private static readonly string[] en = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };


        public static string ToPersianNumber(string strNum)
        {
            string chash = strNum;
            for (int i = 0; i < 10; i++)
                chash = chash.Replace(en[i], pn[i]);
            return chash;
        }
        public static string ConvertPnNumberToEn(string strNum)
        {
            string chash = strNum;
            for (int i = 0; i < 10; i++)
                chash = chash.Replace(pn[i], en[i]);
            return chash;
        }
        #endregion
    }
}
