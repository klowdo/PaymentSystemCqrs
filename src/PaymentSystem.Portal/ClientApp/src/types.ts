export interface ISubscription {
  id: string;
  name: string;
  fee: string;
}
export interface ICard {
  id: string;
  subscriptionId: string;
}

export interface ITransaction {
  id: string;
  created: string;
  amount: number;
  currencyCode: string;
  feeTransactions: ITransaction[];
  type: string;
  total: number;
}
