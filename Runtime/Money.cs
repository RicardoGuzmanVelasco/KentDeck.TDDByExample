using UnityEditor.UIElements;

namespace Domain
{
    public class Money : MoneyExpression
    {
        public int Amount { get; }
        public string Currency { get; }

        public Money(int amount, string currency)
        {
            this.Amount = amount;
            Currency = currency;
        }

        public Money Times(int multiplier) => new Money(Amount * multiplier, Currency);
        
        public SumExpression Plus(Money addend) => new SumExpression(this, addend);
        
        public static Money Dollar(int amount) => new Money(amount, "USD");
        public static Money Franc(int amount) => new Money(amount, "CHF");

        public Money Reduce(string to)
        {
            var rate = 1;
            if(Currency is "CHF" && to is "USD")
                rate = 2;
            
            return new Money(Amount / rate, to);
        }

        public override bool Equals(object o)
        {
            return o is Money other &&
                   Currency == other.Currency &&
                   Amount == other.Amount;
        }

        public override string ToString() => $"{nameof(Amount)}: {Amount}, {nameof(Currency)}: {Currency}";
    }
}