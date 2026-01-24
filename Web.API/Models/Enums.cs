namespace Web.API.Models
{
    public enum MemberStatus
    {
        Active,
        Inactive
    }

    public enum TransactionType
    {
        Thu,  // Income
        Chi   // Expense
    }

    public enum ChallengeMode
    {
        Singles = 0,
        Doubles = 1,
        MiniGame = 2
    }

    public enum ChallengeStatus
    {
        Open,       // Đang nhận
        Closed,     // Đã chốt
        Completed   // Đã trao giải
    }

    public enum MatchFormat
    {
        Singles,    // Đơn
        Doubles     // Đôi
    }
}
