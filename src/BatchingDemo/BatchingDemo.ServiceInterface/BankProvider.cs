using System;

namespace BatchingDemo.ServiceInterface
{
    public interface ITenantBankingProvider
    {
        bool IsBankingSetup(long tenantId);
        void SetupBanking(long tenantId);
    }

    public class TenantBankingProvider : ITenantBankingProvider
    {
        private readonly IWinOrLose isBankingSetupWinOrLose;
        private readonly IWinOrLose setupWinOrLose;

        public TenantBankingProvider(Func<int, IWinOrLose> winOrLoseFunc)
        {
            isBankingSetupWinOrLose = winOrLoseFunc(Probability.Even);
            setupWinOrLose = winOrLoseFunc(Probability.High);
        }

        public bool IsBankingSetup(long tenantId)
        {
            return isBankingSetupWinOrLose.Next() == Outcome.Win;
        }

        public void SetupBanking(long tenantId)
        {
            if (setupWinOrLose.Next() == Outcome.Win) return;
            
            throw new Exception("Banking setup error");
        }
    }
}