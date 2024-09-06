export class TransactionResponse {
  Id!: string;
  SavingsAccountId!: string;
  SavingsAccountNewBalance!: number;
  TransactionType!: string;
  Amount!: number;
  Description!: string;
  Date!: Date;
}
