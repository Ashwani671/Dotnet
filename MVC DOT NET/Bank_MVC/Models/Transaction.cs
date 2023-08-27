using System;
using System.Collections.Generic;

namespace Bank_MVC.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? TransactionDate { get; set; }

    public int? SenderAccount { get; set; }

    public int? ReciverAccount { get; set; }

    public virtual Customer? ReciverAccountNavigation { get; set; }

    public virtual Customer? SenderAccountNavigation { get; set; }
}
