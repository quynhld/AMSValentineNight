namespace KN.Config.Ent
{
    /// <summary>
    /// 환율정보용 객체
    /// </summary>
    public class ExchangeMngDs
    {
        public class ExchangeRateInfo
        {
            public int ExchangeSeq
            {
                get;
                set;
            }

            public string AppliedDt
            {
                get;
                set;
            }

            public double DongToDollar
            {
                get;
                set;
            }
        }
    }
}
