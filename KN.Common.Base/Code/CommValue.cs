namespace KN.Common.Base.Code
{
    public class CommValue
    {
        public CommValue()
        {
        }

        public const string DATE_MOVE_INTO = "20110401";
        public const int SETTLEMENT_STARTTIME = 10;
        public const string CLOSE_WINDOWS = "CLOSEWIN";
        public const string MAIN_COMP_CD = "11111111";
        public const string SUB_COMP_CD = "11111112";
        public const int MAX_INVOICE_CHESTNUT = 7;
        public const int MAX_INVOICE_KEANGNAM = 3;

        #region 공식 도메인

        // 내부IP
        public const string PRIVATE_VALUE_DOMAIN = "http://192.168.101.2";

        // 공식IP
        public const string PUBLIC_VALUE_DOMAIN = "http://192.168.101.2:8008";
        //public const string PUBLIC_VALUE_DOMAIN = "http://localhost:8088";

        #endregion

        #region 테스트 도메인

        public const string PRIVATE_VALUE_TESTDOMAIN = "http://::1:8008";
        public const string PUBLIC_VALUE_TESTHOST = "http://192.168.101.2:8008";
        public const string PUBLIC_VALUE_TESTDOMAIN = "http://::1:8008";

        #endregion

        #region 공식 Port

        public const string PUBLIC_VALUE_PORT = "80";

        #endregion

        #region | 게시판인자(BOARD) |

        public const int BOARD_VALUE_PAGESIZE = 10;
        public const int BOARD_VALUE_ROOMSIZE = 11;
        public const int BOARD_VALUE_ACCSIZE = 20;
        public const int BOARD_VALUE_DEFAULTPAGE = 1;
        public const int BOARD_VALUE_MINIPAGESIZE = 5;
        public const int BOARD_VALUE_EXCHAGEPAGESIZE = 6;

        #endregion

        #region | 파라미터(PARAM) |

        public const string PARAM_VALUE_GROUP = "○";
        public const string PARAM_VALUE_ELEMENT = "●";

        public const char PARAM_VALUE_CHAR_GROUP = '○';
        public const char PARAM_VALUE_CHAR_ELEMENT = '●';

        #endregion

        #region | 데이터타입(TYPE) |

        public const string TYPE_VALUE_IMAGEBUTTON = "imgbtn";
        public const string TYPE_VALUE_CHECKBOX = "CHKBOX";
        public const string TYPE_VALUE_RADIOBTTON = "RDOBTN";
        public const string TYPE_VALUE_DROPDOWNLISt = "DDL";
        public const string TYPE_VALUE_LABEL = "LBL";
        public const string TYPE_VALUE_TEXTBOX = "TXT";
        public const string TYPE_VALUE_IMAGE = "IMAGE";
        public const string TYPE_VALUE_HIDDENFIELD = "HF";
        public const string TYPE_VALUE_HIDDENTEXTBOX = "HFTXT";
        public const string TYPE_VALUE_LINKEDTEXT = "LNKTXT";

        #endregion

        #region | 첨부파일(FILEADDON) |

        public const int FILEADDON_VALUE_FIRST = 1;
        public const int FILEADDON_VALUE_SECOND = 2;
        public const int FILEADDON_VALUE_THIRD = 3;

        #endregion

        #region | 엑셀타입(EXCEL) |

        public const string EXCEL_CONTTYPE_VALUE_CSV = "application/octet-stream";
        public const string EXCEL_CONTTYPE_VALUE_XLS = "application/vnd.ms-excel";
        public const string EXCEL_CONTTYPE_VALUE_XLSX = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public const string EXCEL_TYPE_TEXT_CSV = "csv";
        public const string EXCEL_TYPE_TEXT_XLS = "xls";
        public const string EXCEL_TYPE_TEXT_XLSX = "xlsx";

        #endregion

        #region | 아이콘(ICON) |

        public const string ICON_EXCEL = "<img alt='excel' title='excel' src='/Common/Images/Icon/exell.gif' align='absmiddle'/>";
        public const string ICON_PPT = "<img alt='ppt' title='excel' src='/Common/Images/Icon/ppt.gif' align='absmiddle'/>";
        public const string ICON_PDF = "<img alt='pdf' title='pdf' src='/Common/Images/Icon/pdf.gif' align='absmiddle'/>";
        public const string ICON_HWP = "<img alt='hwp' title='hwp' src='/Common/Images/Icon/han.gif' align='absmiddle'/>";
        public const string ICON_DOC = "<img alt='doc' title='doc' src='/Common/Images/Icon/word.gif' align='absmiddle'/>";
        public const string ICON_OFFICE = "<img alt='office' title='office' src='/Common/Images/Icon/office.gif' align='absmiddle'/>";

        #endregion

        #region | 권한(AUTH) |

        public const string AUTH_VALUE_EMPTY = "00000000";
        public const string AUTH_VALUE_ENTIRE = "99999999";
        public const string AUTH_VALUE_FULL = "67108864";
        public const string AUTH_VALUE_SUPER = "00000001";
        public const string AUTH_VALUE_ADMIN = "00000002";

        public const bool AUTH_VALUE_TRUE = true;
        public const bool AUTH_VALUE_FALSE = false;

        #endregion

        #region | 페이지(PAGE) |

        public const string PAGE_VALUE_DEFAULT = "/Main.aspx";
        //public const string PAGE_VALUE_INDEX = "/Common/Signup/Login.aspx";
        public const string PAGE_VALUE_INDEX = "/Index.aspx";
        public const string PAGE_VALUE_NOMENU = "/Config/Menu/MenuList.aspx";
        public const string PAGE_VALUE_FINDID = "/Common/Signup/IdFind.aspx";
        public const string PAGE_VALUE_NOTFOUND = "/Common/Signup/IdNotFind.aspx";
        public const string PAGE_VALUE_FINDIDEMAIL = "/Common/Signup/IdFindEmail.aspx";
        public const string PAGE_VALUE_FINDIDMOBILE = "/Common/Signup/IdFindMobile.aspx";
        public const string PAGE_VALUE_FINDPASSWORD = "/Common/Signup/PwdFind.aspx";
        public const string PAGE_VALUE_FINDPASSWORDEMAIL = "/Common/Signup/PwdFindEmail.aspx";
        public const string PAGE_VALUE_FINDPASSWORDMOBILE = "/Common/Signup/PwdFindMobile.aspx";
        public const string PAGE_VALUE_FINDNOTPASSWORD = "/Common/Signup/PwdNotFind.aspx";
        public const string PAGE_VALUE_MEMO = "/Board/Memo/MemoReturn.aspx";
        public const string PAGE_VALUE_LOGOUT = "/Common/Signup/LogOut.aspx";
        public const string PAGE_VALUE_SITEMAP = "/Config/SiteMap/SiteMap.aspx";


        public const string PAGE_VALUE_RELEASEVIEW = "/Common/Template/Memo/MemoReleaseCharge.aspx";
        public const string PAGE_VALUE_RELEASE = "/Stock/Release/ReleaseRequestReturn.aspx";
        public const string PAGE_VALUE_ORDERVIEW = "/Common/Template/Memo/MemoOrderCharge.aspx";
        public const string PAGE_VALUE_ORDER = "/Stock/Order/GoodsOrderReturn.aspx";
        public const string PAGE_VALUE_MNGFEELIST = "MngPaymentList.aspx";
        public const string PAGE_VALUE_MNGFEEVIEW = "MngPaymentView.aspx";
        public const string PAGE_VALUE_MNGFEEITEMWRITE = "MonthMngInfoWrite.aspx";
        public const string PAGE_VALUE_MNGFEEITEMVIEW = "MonthMngInfoView.aspx";
        public const string PAGE_VALUE_LATEFEELIST = "LateFeeList.aspx";
        public const string PAGE_VALUE_LATEFEEVIEW = "LateFeeInfoView.aspx";
        public const string PAGE_VALUE_MASTERKEYLIST = "/Board/MasterKeyList.aspx";
        public const string PAGE_VALUE_ACCOUNTMNG = "/Config/Authority/PersonalMngView.aspx";

        #endregion

        public const string PAGEPARAM_VALUE_RELEASEVIEW = "ReleaseData";
        public const string PAGEPARAM_VALUE_PURCHASEVIEW = "PurchaseData";
        
        public const double RATE_VALUE_BUYING = 1.0048d;
        public const double RATE_VALUE_SELLING = 0.9952d;

        public const int NEMUSEQ_VALUE_LOGIN = 0;

        public const double NUMBER_VALUE_0_0 = 0.0;
        public const double NUMBER_VALUE_0_00 = 0.00;
        public const double NUMBER_VALUE_0_000 = 0.000;

        public const string DOUBLE_VALUE_0_0 = "0.0";
        public const string DOUBLE_VALUE_0_00 = "0.00";
        public const string DOUBLE_VALUE_0_000 = "0.000";

        public const int NUMBER_VALUE_0 = 0;
        public const int NUMBER_VALUE_1 = 1;
        public const int NUMBER_VALUE_2 = 2;
        public const int NUMBER_VALUE_3 = 3;
        public const int NUMBER_VALUE_4 = 4;
        public const int NUMBER_VALUE_5 = 5;
        public const int NUMBER_VALUE_6 = 6;

        public const string NUMBER_VALUE_ZERO = "0";
        public const string NUMBER_VALUE_ONE = "1";
        public const string NUMBER_VALUE_TWO = "2";
        public const string NUMBER_VALUE_THREE = "3";

        public const string CODE_VALUE_EMPTY = "0000";
        public const string CODE_VALUE_INIT = "0001";
        public const string CODE_VALUE_ETC = "9999";

        public const int START_YEAR = 2011;

        public const string DEPT_VALUE_FMS = "FMS";

        public const string CHOICE_VALUE_YES = "Y";
        public const string CHOICE_VALUE_NO = "N";
        public const string CHOICE_VALUE_NOTCONFIRM = "X";

        public const string PAGESEQ_VALUE_NOTICE = "3";
        public const string PAGESEQ_VALUE_ARCHIVES = "4";
        public const string PAGESEQ_VALUE_MAIN = "5";

        #region | 공통코드(COMMCD) |

        public const string COMMCD_VALUE_SEARCH_CONDITION = "0002";

        #region | 검색조건(SEARCH_COND)  - 0002 |

        public const string SEARCH_COND_VALUE_BOARD = "0001";
        public const string SEARCH_COND_VALUE_LEASE = "0002";
        public const string SEARCH_COND_VALUE_ALERT = "0003";
        public const string SEARCH_COND_VALUE_CONSULTING = "0004";
        public const string SEARCH_COND_VALUE_ACCMNG = "0005";
        public const string SEARCH_COND_VALUE_EVENT = "0006";
        public const string SEARCH_COND_VALUE_FINDID = "0007";
        public const string SEARCH_COND_VALUE_FINDPASSWORD = "0008";
        public const string SEARCH_COND_VALUE_SUPPLYER = "0009";
        public const string SEARCH_COND_VALUE_FINDMEMBER = "0010";
        public const string SEARCH_COND_VALUE_SCORPE = "0011";
        public const string SEARCH_COND_VALUE_TYPE = "0012";

        #region | 게시판검색조건(BOARD_SEARCH)  - 0002/0001 |

        public const string BOARD_SEARCH_VALUE_TITLE = "0001";
        public const string BOARD_SEARCH_VALUE_CONTENTS = "0002";
        public const string BOARD_SEARCH_VALUE_ENTIRE = "0003";

        #endregion

        #region | 임대계약검색조건(LEASE_SEARCH)  - 0002/0002 |

        public const string LEASE_SEARCH_VALUE_FLOOR = "0001";
        public const string LEASE_SEARCH_VALUE_ROOMNO = "0002";
        public const string LEASE_SEARCH_VALUE_TENANT = "0003";
        public const string LEASE_SEARCH_VALUE_CONTRACTNO = "0004";

        #endregion

        #region | 알림문구검색조건(ALERT_SEARCH)  - 0002/0003 |

        public const string ALERT_SEARCH_VALUE_EXPRESSCD = "0001";
        public const string ALERT_SEARCH_VALUE_EN = "0002";
        public const string ALERT_SEARCH_VALUE_VI = "0003";
        public const string ALERT_SEARCH_VALUE_KR = "0004";

        #endregion

        #region | 임대상담검색조건(CONSULTING_SEARCH)  - 0002/0004 |

        public const string CONSULTING_SEARCH_VALUE_COMPNM = "0001";
        public const string CONSULTING_SEARCH_VALUE_CHARGENM = "0002";
        public const string CONSULTING_SEARCH_VALUE_REMARK = "0003";

        #endregion

        #region | 계정관리검색조건(ACCOUNT_MANAGEMENT_SEARCH)  - 0002/0005 |

        public const string ACCMNG_SEARCH_VALUE_ID = "0001";
        public const string ACCMNG_SEARCH_VALUE_NM = "0002";

        #endregion

        #region | 로그검색조건(EVENT_SEARCH)  - 0002/0006 |

        public const string EVENT_SEARCH_VALUE_CONTENTS = "0001";

        #endregion

        #region | 아이디찾기검색조건(ID_SEARCH)  - 0002/0007 |

        public const string ID_SEARCH_VALUE_EMAIL = "0001";
        public const string ID_SEARCH_VALUE_MOBILE = "0002";

        #endregion

        #region | 비밀번호찾기검색조건(PWD_SEARCH)  - 0002/0008 |

        public const string PASSWORD_SEARCH_VALUE_EMAIL = "0001";
        public const string PASSWORD_SEARCH_VALUE_MOBILE = "0002";

        #endregion

        #region | 공급업체검색조건(SUPPLYER_SEARCH)  - 0002/0009 |

        public const string SUPPLYER_SEARCH_VALUE_COMPNM = "0001";
        public const string SUPPLYER_SEARCH_VALUE_INTRO = "0002";

        #endregion

        #region | 사원검색조건(MEMBER_SEARCH)  - 0002/0010 |

        public const string MEMBER_SEARCH_VALUE_NO = "0001";
        public const string MEMBER_SEARCH_VALUE_NM = "0002";
        public const string MEMBER_SEARCH_VALUE_ADDR = "0003";
        public const string MEMBER_SEARCH_VALUE_TEL = "0004";
        public const string MEMBER_SEARCH_VALUE_CELL = "0005";

        #endregion

        #region | 범위검색조건(SCORPE_SEARCH)  - 0002/0011 |

        public const string SCORPE_SEARCH_VALUE_ENTIRE = "0001";
        public const string SCORPE_SEARCH_VALUE_TAXCD = "0002";
        public const string SCORPE_SEARCH_VALUE_ROOMNO = "0003";
        public const string SCORPE_SEARCH_VALUE_ITEM = "0004";

        #endregion

        #region | 방식검색조건(TYPE_SEARCH)  - 0002/0012 |

        public const string TYPE_SEARCH_VALUE_ENTIRE = "0001";
        public const string TYPE_SEARCH_VALUE_MAX = "0002";

        #endregion

        #endregion

        public const string COMMCD_VALUE_BBS_TYPE = "0003";

        #region | BBS종류(BBS_TYPE) - 0003 |

        public const string BBS_TYPE_VALUE_BOARD = "0001";
        public const string BBS_TYPE_VALUE_DOWNLOAD = "0002";

        #region | 게시판종류(BOARD_TYPE)  - 0003/0001 |

        public const string BOARD_TYPE_VALUE_NOTICE = "0001";
        public const string BOARD_TYPE_VALUE_FAQ = "0002";
        public const string BOARD_TYPE_VALUE_QNA = "0003";
        public const string BOARD_TYPE_VALUE_BOARD = "0004";
        public const string BOARD_TYPE_VALUE_ESTIMATE = "0005";

        #endregion

        #region | 자료실종류(DOWNLOAD_TYPE)  - 0003/0002 |

        public const string DOWNLOAD_TYPE_VALUE_DOWNLOAD = "0001";
        public const string DOWNLOAD_TYPE_VALUE_IMG_DOWNLOAD = "0002";
        public const string DOWNLOAD_TYPE_VALUE_DOC_DOWNLOAD = "0003";

        #endregion

        #endregion

        public const string COMMCD_VALUE_MONEY_TYPE = "0004";

        #region | 금전관련(MONEY_TYPE) - 0004 |

        public const string MONEY_TYPE_VALUE_UNIT = "0001";
        public const string MONEY_TYPE_VALUE_CURRENCY = "0002";

        #region | 단위(UNIT)  - 0004/0001 |

        public const string UNIT_VALUE_ONE = "0001";
        public const string UNIT_VALUE_TEN = "0002";
        public const string UNIT_VALUE_HUNDRED = "0003";
        public const string UNIT_VALUE_THOUSAND = "0004";

        #endregion

        #region | 통화종류(CURRENCY)  - 0004/0002 |

        public const string CURRENCY_VALUE_DOLLAR = "0001";
        public const string CURRENCY_VALUE_DONG = "0002";
        public const string CURRENCY_VALUE_WON = "0003";

        #endregion

        #endregion

        public const string COMMCD_VALUE_SELECT = "0005";

        #region | 선택종류(SELECT_TYPE) - 0005 |

        public const string SELECT_TYPE_VALUE_CONCLUSION = "0001";
        public const string SELECT_TYPE_VALUE_GENDER = "0002";
        public const string SELECT_TYPE_VALUE_PAYMENT = "0003";
        public const string SELECT_TYPE_VALUE_LATE = "0004";
        public const string SELECT_TYPE_VALUE_STOCK = "0005";
        public const string SELECT_TYPE_VALUE_INCLUDED = "0006";

        #region | 체결여부(CONCLUSION_TYPE) - 0005/0001 |

        public const string CONCLUSION_TYPE_VALUE_YES = "0001";
        public const string CONCLUSION_TYPE_VALUE_NO = "0002";

        public const string CONCLUSION_TYPE_TEXT_YES = "Y";
        public const string CONCLUSION_TYPE_TEXT_NO = "N";

        #endregion

        #region | 성별(GENDER_TYPE) - 0005/0002 |

        public const string GENDER_TYPE_VALUE_MALE = "0001";
        public const string GENDER_TYPE_VALUE_FEMALE = "0002";

        public const string GENDER_TYPE_TEXT_MALE = "M";
        public const string GENDER_TYPE_TEXT_FEMALE = "F";

        #endregion

        #region | 수납여부(PAYMENT_TYPE) - 0005/0003 |

        public const string PAYMENT_TYPE_VALUE_PAID = "0001";
        public const string PAYMENT_TYPE_VALUE_NOTPAID = "0002";

        #endregion

        #region | 연체여부(OVERDUE_TYPE) - 0005/0004 |

        public const string OVERDUE_TYPE_VALUE_TRUE = "0001";
        public const string OVERDUE_TYPE_VALUE_FALSE = "0002";

        #endregion

        #region | 재고상태(STOCK_TYPE) - 0005/0005 |

        public const string STOCK_TYPE_VALUE_USABLE = "0001";
        public const string STOCK_TYPE_VALUE_UNUSEABLE = "0002";

        #endregion

        #region | 포함여부(INCLUDED_TYPE) - 0005/0006 |

        public const string INCLUDED_TYPE_VALUE_INCLUDED = "0001";
        public const string INCLUDED_TYPE_VALUE_EXCEPTIONG = "0002";

        #endregion

        #endregion

        public const string COMMCD_VALUE_TEXT = "0006";

        #region | 텍스트종류(TEXT_TYPE) - 0006 |

        public const string TEXT_TYPE_VALUE_MENU = "0001";
        public const string TEXT_TYPE_VALUE_ITEM = "0002";
        public const string TEXT_TYPE_VALUE_ALERT = "0003";

        #region | 알림문종류(ALERT_TYPE) - 0006/0003 |

        public const string ALERT_TYPE_VALUE_ALERT = "0001";
        public const string ALERT_TYPE_VALUE_CONFIRM = "0002";
        public const string ALERT_TYPE_VALUE_INFO = "0003";

        #endregion

        #endregion

        public const string COMMCD_VALUE_RENTAL = "0007";

        #region | 임대관련(RENTAL_TYPE) - 0007 |

        public const string RENTAL_VALUE_COUNTRY = "0001";

        public const string RENTAL_VALUE_COMP_TY = "0002";

        public const string RENTAL_VALUE_LEASE_AREA = "0003";

        public const string RENTAL_VALUE_RENTALFEE = "0004";

        public const string RENTAL_VALUE_SERVICEFARE = "0005";

        public const string RENTAL_VALUE_STAFFNO = "0006";

        public const string RENTAL_VALUE_CONT_PERIOD = "0007";

        public const string RENTAL_VALUE_NO_PARKING_CAR = "0008";

        public const string RENTAL_VALUE_NO_PARKING_BIKE = "0009";

        public const string RENTAL_VALUE_LEASE_PERIOD = "0010";

        public const string RENTAL_VALUE_PERIOD = "0011";

        public const string RENTAL_VALUE_CONSTRUCT_PERIOD = "0012";

        public const string RENTAL_VALUE_REGION = "0013";

        #endregion

        public const string COMMCD_VALUE_ACCOUNT = "0008";

        #region | 회계관련(ACCOUNT_TYPE) - 0008 |

        public const string ACCOUNT_VALUE_DEBITNCREDIT = "0001";

        #region | 대차(DEBITNCREDIT_TYPE) - 0008/0001 |

        public const string DEBITNCREDIT_TYPE_VALUE_CREDIT = "0001";
        public const string DEBITNCREDIT_TYPE_VALUE_DEBIT = "0002";

        #endregion

        public const string ACCOUNT_VALUE_DIRECT_TYPE = "0002";

        #region | 직영여부(DIRECT_TYPE) - 0008/0002 |

        public const string DIRECT_TYPE_VALUE_DIRECT = "0001";
        public const string DIRECT_TYPE_VALUE_AGENT = "0002";

        #endregion

        public const string ACCOUNT_VALUE_ITEM = "0003";

        #region | 항목(ITEM_TYPE) - 0008/0003 |

        public const string ITEM_TYPE_VALUE_MNGFEE = "0001";
        public const string ITEM_TYPE_VALUE_RENTALFEE = "0002";
        public const string ITEM_TYPE_VALUE_MATERIALFEE = "0003";
        public const string ITEM_TYPE_VALUE_PARKINGFEE = "0004";
        public const string ITEM_TYPE_VALUE_LATEMNGFEE = "0005";
        public const string ITEM_TYPE_VALUE_LATERENTALFEE = "0006";
        public const string ITEM_TYPE_VALUE_PARKINGCARDFEE = "0007";
        public const string ITEM_TYPE_VALUE_ELECCHARGE = "0008";
        public const string ITEM_TYPE_VALUE_WATERCHARGE = "0009";
        public const string ITEM_TYPE_VALUE_GASCHARGE = "0010";
        public const string ITEM_TYPE_VALUE_WATER_N_ELECCHARGE = "0011";

        #endregion

        public const string ACCOUNT_VALUE_SCALE = "0004";

        #region | 단위수량(SCALE_TYPE) - 0008/0004 |

        public const string SCALE_TYPE_VALUE_SINGLEITEM = "0001";
        public const string SCALE_TYPE_VALUE_BOX = "0002";
        public const string SCALE_TYPE_VALUE_DOZEN = "0003";
        public const string SCALE_TYPE_VALUE_MONTH = "0004";

        #endregion

        public const string ACCOUNT_VALUE_PAYMENT_METHOD = "0005";

        #region | 지불방법(PAYMENT_TYPE) - 0008/0005 |

        public const string PAYMENT_TYPE_VALUE_CARD = "0001";
        public const string PAYMENT_TYPE_VALUE_CASH = "0002";
        public const string PAYMENT_TYPE_VALUE_TRANSFER = "0003";
        public const string PAYMENT_TYPE_VALUE_NOTE = "0004";
        public const string PAYMENT_TYPE_VALUE_COMPLEX = "0005";


        #endregion

        public const string ACCOUNT_VALUE_APPROVAL = "0006";

        #region | 승인상태(APPROVAL_TYPE) - 0008/0006 |

        public const string APPROVAL_TYPE_VALUE_APPROVAL = "0001";
        public const string APPROVAL_TYPE_VALUE_PENDING = "0002";
        public const string APPROVAL_TYPE_VALUE_REJECTED = "0003";
        public const string APPROVAL_TYPE_VALUE_PARTIALAPPROVAL = "0004";

        #endregion

        public const string ACCOUNT_VALUE_RELEASE = "0007";

        #region | 출고처리(RELEASE_TYPE) - 0008/0007 |

        public const string RELEASE_TYPE_VALUE_WAITING = "0001";
        public const string RELEASE_TYPE_VALUE_PURCHASEREQ = "0002";
        public const string RELEASE_TYPE_VALUE_RELEASED = "0003";
        public const string RELEASE_TYPE_VALUE_CANCELPURCHASE = "0004";
        public const string RELEASE_TYPE_VALUE_CANCELRELEASE = "0005";

        #endregion

        public const string ACCOUNT_VALUE_PURCHASE = "0008";

        #region | 구매처리(PURCHASE_TYPE) - 0008/0008 |

        public const string PURCHASE_TYPE_VALUE_PURCHASEREQ = "0001";
        public const string PURCHASE_TYPE_VALUE_CANCELPURCHASE = "0002";
        public const string PURCHASE_TYPE_VALUE_PURCHASED = "0003";
        public const string PURCHASE_TYPE_VALUE_WAITING = "0004";

        #endregion

        public const string ACCOUNT_VALUE_CALCULATE_STATUS = "0009";

        #region | 정산상태(CALCULATE_STATUS_TYPE) - 0008/0009 |

        public const string CALCULATE_STATUS_TYPE_VALUE_PAID = "0001";
        public const string CALCULATE_STATUS_TYPE_VALUE_APPROVAL = "0002";
        public const string CALCULATE_STATUS_TYPE_VALUE_CANCEL = "0003";
        public const string CALCULATE_STATUS_TYPE_VALUE_PARTIALCANCEL = "0004";
        public const string CALCULATE_STATUS_TYPE_VALUE_HOLD = "0005";

        #endregion

        public const string ACCOUNT_VALUE_TAXPAYER = "0011";

        #region | 납세자(TAXPAYER_TYPE) - 0008/0011 |

        public const string TAXPAYER_TYPE_VALUE_CONTRACTOR = "0001";
        public const string TAXPAYER_TYPE_VALUE_TENANT = "0002";

        #endregion

        public const string ACCOUNT_VALUE_ACCOUNTCODE = "0012";

        #region | 회계코드(ACCOUNTCODE_TYPE) - 0008/0012 |

        public const string ACCOUNTCODE_TYPE_VALUE_UNEARNED = "0001";
        public const string ACCOUNTCODE_TYPE_VALUE_FEETYPE = "0002";
        public const string ACCOUNTCODE_TYPE_VALUE_TAXBILL = "0003";

        #endregion

        #endregion

        public const string COMMCD_VALUE_ETC = "9999";

        #region | 기타코드(ETCCD) - 9999 |

        public const string ETCCD_VALUE_LANG = "0001";
        public const string ETCCD_VALUE_RELATION = "0002";
        public const string ETCCD_VALUE_RENTAL = "0003";
        public const string ETCCD_VALUE_BUSINESS = "0004";
        public const string ETCCD_VALUE_CONTDEL = "0005";
        public const string ETCCD_VALUE_LINK = "0006";
        public const string ETCCD_VALUE_PARAM = "0007";
        public const string ETCCD_VALUE_CARTY = "0008";
        public const string ETCCD_VALUE_FEETY = "0009";
        public const string ETCCD_VALUE_COMTY = "0010";
        public const string ETCCD_VALUE_CHARGETY = "0011";
        public const string ETCCD_VALUE_TIMETY = "0012";
        public const string ETCCD_VALUE_UNITTY = "0013";
        public const string ETCCD_VALUE_RECEIT = "0014";
        public const string ETCCD_VALUE_DOCUMENT = "0015";
        public const string ETCCD_VALUE_APARTMENTTY = "0016";
        public const string ETCCD_VALUE_TERM = "0017";
        public const string ETCCD_VALUE_TENANTTY = "0018";
        public const string ETCCD_VALUE_USERTYCD = "0019";
        public const string ETCCD_VALUE_PARKINGDAYS = "0020";
        public const string ETCCD_VALUE_CONTSTEP = "0021";
        public const string ETCCD_VALUE_GATE = "0022";
        public const string ETCCD_VALUE_INDUSTRY = "0023";
        public const string ETCCD_VALUE_NAT = "0024";
        
        #region | 언어코드(LangCd) - 9999/0001 |

        public const string LANG_VALUE_VIETNAMESE = "0001";
        public const string LANG_VALUE_ENGLISH = "0002";
        public const string LANG_VALUE_KOREAN = "0003";

        #endregion

        #region | 관계(RelationCd) - 9999/0002 |

        public const string RELATION_VALUE_CONTRACTOR = "0001";
        public const string RELATION_VALUE_ROOMMATE = "0002";
        public const string RELATION_VALUE_FAMILY = "0003";
        public const string RELATION_VALUE_FRIEND = "0004";
        public const string RELATION_VALUE_EMPLOYEE = "0005";
        public const string RELATION_VALUE_OTHERS = "9999";

        #endregion

        #region | 임대종류(RentalTy) - 9999/0003 |

        public const string RENTAL_VALUE_OFFICE = "0001";
        public const string RENTAL_VALUE_SHOP = "0002";
        public const string RENTAL_VALUE_SR = "0003";
        public const string RENTAL_VALUE_APTA = "0004";
        public const string RENTAL_VALUE_APTB = "0005";
        public const string RENTAL_VALUE_PARKING = "0006";
        public const string RENTAL_VALUE_APTASHOP = "0007";
        public const string RENTAL_VALUE_APTBSHOP = "0008";
        public const string RENTAL_VALUE_APT = "9000";
        public const string RENTAL_VALUE_APTSHOP = "9900";

        public const string RENTAL_TEXT_OFFICE = "Office";
        public const string RENTAL_TEXT_SHOP = "Retail";
        public const string RENTAL_TEXT_SR = "S/R";
        public const string RENTAL_TEXT_APTA = "ApartA";
        public const string RENTAL_TEXT_APTB = "ApartB";
        public const string RENTAL_TEXT_PARKING = "Parking";
        public const string RENTAL_TEXT_APTASHOP = "Apart A Retail";
        public const string RENTAL_TEXT_APTBSHOP = "Apart B Retail";

        #endregion

        #region | 업종종류(BusinessTy) - 9999/0004 |

        public const string BUSINESS_VALUE_STOCK = "0001";
        public const string BUSINESS_VALUE_BANKING = "0002";
        public const string BUSINESS_VALUE_REALESTATE = "0003";
        public const string BUSINESS_VALUE_CONSTRUCT = "0004";
        public const string BUSINESS_VALUE_LAWYER = "0005";
        public const string BUSINESS_VALUE_ACCOUNT = "0006";
        public const string BUSINESS_VALUE_SERVICE = "0007";
        public const string BUSINESS_VALUE_HEALTH = "0008";
        public const string BUSINESS_VALUE_MANUFACTURE = "0009";
        public const string BUSINESS_VALUE_TRAVEL = "0010";
        public const string BUSINESS_VALUE_AIR = "0011";
        public const string BUSINESS_VALUE_OTHERS = "0012";

        #endregion

        #region | 계약삭제사유(ContDelTy) - 9999/0005 |

        public const string CONTDEL_VALUE_CANCEL = "0001";
        public const string CONTDEL_VALUE_TERMINATE = "0002";
        public const string CONTDEL_VALUE_REGERROR = "0003";
        public const string CONTDEL_VALUE_OTHERREASON = "0004";

        #endregion

        #region | 링크종류(LinkTy) - 9999/0006 |

        public const string LINK_VALUE_REDIRECT = "0001";
        public const string LINK_VALUE_NOAUTH = "0002";
        public const string LINK_VALUE_LIST = "0003";
        public const string LINK_VALUE_WRITE = "0004";
        public const string LINK_VALUE_VIEW = "0005";
        public const string LINK_VALUE_MODIFY = "0006";
        public const string LINK_VALUE_REPLY = "0007";
        public const string LINK_VALUE_POPUP1 = "0008";
        public const string LINK_VALUE_POPUP2 = "0009";

        #endregion

        #region | 파라미터종류(ParamTy) - 9999/0007 |

        public const string PARAM_VALUE_PARAM1 = "0001";
        public const string PARAM_VALUE_PARAM2 = "0002";
        public const string PARAM_VALUE_PARAM3 = "0003";
        public const string PARAM_VALUE_PARAM4 = "0004";
        public const string PARAM_VALUE_PARAM5 = "0005";

        #endregion

        #region | 차종(CarTy) - 9999/0008 |

        //public const string CARTY_VALUE_FULLSIZE = "0001";
        //public const string CARTY_VALUE_MIDSIZE = "0002";
        //public const string CARTY_VALUE_COMPACT = "0003";
        //public const string CARTY_VALUE_MOTORCYCLE = "0004";
        //public const string CARTY_VALUE_FREE_EXCEPTION = "0005";
        //public const string CARTY_VALUE_FREE_OTHERS = "9999";

        public const string CARTY_VALUE_VEHICLE = "0001";
        public const string CARTY_VALUE_MOTORCYCLE = "0002";
        public const string CARTY_VALUE_FREE_EXCEPTION = "0003";

        #endregion

        #region | 비용종류(FeeTy) - 9999/0009 |

        public const string FEETY_VALUE_MNGFEE = "0001";
        public const string FEETY_VALUE_RENTALFEE = "0002";

        #endregion

        #region | 사업장형태(ComTy) - 9999/0010 |

        public const string COMTY_VALUE_HEADOFFICE = "0001";
        public const string COMTY_VALUE_BRANCH = "0002";
        public const string COMTY_VALUE_AGENCY = "0003";

        #endregion

        #region | 요금종류(CHARGETY) - 9999/0011 |

        public const string CHARGETY_VALUE_ELECTRICITY = "0001";
        public const string CHARGETY_VALUE_WATER = "0002";
        public const string CHARGETY_VALUE_GAS = "0003";

        #endregion

        #region | 시간별(TIMETY) - 9999/0012 |

        public const string TIMETY_VALUE_BASIC = "0001";
        public const string TIMETY_VALUE_UNIT = "0002";

        #endregion

        #region | 단위별(UNITTY) - 9999/0013 |

        public const string UNITTY_VALUE_HOUR = "0001";
        public const string UNITTY_VALUE_MINUTE = "0002";

        #endregion

        #region | 납부종류별(PAYMENT) - 9999/0014 |

        public const string RECEIT_VALUE_MNGFEE = "0001";
        public const string RECEIT_VALUE_RENTALFEE = "0002";
        public const string RECEIT_VALUE_PARKINGFEE = "0004";
        public const string RECEIT_VALUE_PARKINGCARDFEE = "0007";
        public const string RECEIT_VALUE_ELECTRICITYFEE = "0008";
        public const string RECEIT_VALUE_WATERATE = "0009";
        public const string RECEIT_VALUE_GASRATE = "0010";
        public const string RECEIT_VALUE_UTILFEE = "0011";
        public const string SPC_VALUE_SPCFEE = "0012";
        public const string SPC_VALUE_DPSCONT = "0013";
        public const string SPC_VALUE_DPSSEC = "0014";

        #endregion

        #region | 문서종류별(DOCUMENT) - 9999/0015 |

        public const string DOCUMENT_VALUE_BILL = "0001";
        public const string DOCUMENT_VALUE_RECEIT = "0002";
        public const string DOCUMENT_VALUE_TAX = "0003";

        #endregion

        #region | 아파트종류별(APARTMENTTY) - 9999/0016 |

        public const string APARTMENTTY_VALUE_APART_A = "0004";
        public const string APARTMENTTY_VALUE_APART_B = "0005";
        public const string APARTMENTTY_VALUE_APARTSTORE_A = "0007";
        public const string APARTMENTTY_VALUE_APARTSTORE_B = "0008";

        #endregion

        #region | 임대기간(TERM) - 9999/0017 |

        public const string TERM_VALUE_LONGTERM = "0001";
        public const string TERM_VALUE_SHORTTERM = "0002";

        #endregion

        #region | 개인/기업(TENANTTY) - 9999/0018 |

        public const string TENANTTY_VALUE_PERSONAL = "0001";
        public const string TENANTTY_VALUE_CORPORATION = "0002";

        #endregion

        #region | 사용자구분(USERTYCD) - 9999/0019 |

        public const string USERTYCD_VALUE_PERSON_CLIENT = "0001";
        public const string USERTYCD_VALUE_STAFF = "0002";
        public const string USERTYCD_VALUE_CORPOL_CLIENT = "0003";

        #endregion

        #region | 주차일(PARKINGDAYS) - 9999/0020 |

        public const string PARKINGDAYS_VALUE_00 = "0000";
        public const string PARKINGDAYS_VALUE_10 = "0010";
        public const string PARKINGDAYS_VALUE_15 = "0015";
        public const string PARKINGDAYS_VALUE_20 = "0020";

        #endregion

        #region | 계약단계(CONTSTEP) - 9999/0021 |

        public const string CONTSTEP_VALUE_OTL = "0001";
        public const string CONTSTEP_VALUE_CONT = "0002";

        #endregion

        #region | 게이트(GATE) - 9999/0022 |

        public const string GATE_VALUE_APARTMENT = "0001";
        public const string GATE_VALUE_OFFICERETAIL = "0002";
        public const string GATE_VALUE_MOTORBIKE = "0004";

        #endregion

        #endregion

        #endregion
    }
}
