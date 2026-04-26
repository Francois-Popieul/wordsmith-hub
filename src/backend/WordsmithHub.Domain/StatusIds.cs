namespace WordsmithHub.Domain;

public static class StatusIds
{
    public static class General
    {
        public const int Active = 1;
        public const int Inactive = 2;
        public const int Draft = 3;
    }

    public static class Invoice
    {
        public const int Draft = 10;
        public const int Sent = 11;
        public const int Paid = 12;
        public const int Cancelled = 13;
    }

    public static class WorkOrder
    {
        public const int Pending = 20;
        public const int InProgress = 21;
        public const int Completed = 22;
        public const int Delivered = 23;
    }

    public static class Project
    {
        public const int InProgress = 30;
        public const int Completed = 31;
    }
}
