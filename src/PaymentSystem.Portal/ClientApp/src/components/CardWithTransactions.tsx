import React, { useState, useEffect, useCallback } from "react";
import { RouteComponentProps } from "react-router";
import Constants from "../Constants";
import { ITransaction } from "../types";

interface MatchParams {
  cardid: string;
}
interface Props extends RouteComponentProps<MatchParams> {}

export const CardWithTransactions: React.FC<Props> = ({
  match: {
    params: { cardid }
  }
}) => {
  const [transactions, setTransactions] = useState<ITransaction[]>([]);
  const fetchData = async () => {
    const result = await fetch(`${Constants.base}/${cardid}/transactions`);
    const json = await result.json();
    setTransactions(json);
  };
  useEffect(() => {
    fetchData();
  }, []);
  return (
    <div>
      {cardid}
      <h2>Transactions</h2>
      <TransactionList Transactions={transactions} />
      <AddPayment id={cardid} onAdded={fetchData} />
    </div>
  );
};
const AddPayment: React.FC<{ id: string; onAdded: () => {} }> = ({
  id,
  onAdded
}) => {
  const [amount, setAmount] = useState(0);
  const [date, setDate] = useState(Date.now().toString());
  const [error, setError] = useState();
  const addPayment = useCallback(async () => {
    const result = await fetch(`${Constants.base}/${id}/add-payment`, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify({ amount, date })
    });
    if (result.status !== 200) {
      setError("Error, check your data");
    } else {
      setTimeout(onAdded, 200);
      setError(null);
    }
  }, [amount, date]);
  return (
    <>
      {error ? error : null}
      <br />
      <input
        type="number"
        name="amount"
        value={amount}
        onChange={e => setAmount(parseInt(e.currentTarget.value))}
      />
      <br />

      <input
        type="datetime-local"
        name="amount"
        value={date}
        onChange={e => setDate(e.currentTarget.value)}
      />
      <br />

      <button onClick={addPayment}>Add payment</button>
    </>
  );
};

const TransactionList: React.FC<{ Transactions: ITransaction[] }> = ({
  Transactions
}) => {
  return (
    <ul>
      {Transactions.map(t => (
        <li key={t.id}>
          {`${t.amount} ${t.currencyCode} - Total: ${t.total}  ${
            t.currencyCode
          } at: ${t.created}`}
          <FeeTransactionList Transactions={t.feeTransactions} />
        </li>
      ))}
    </ul>
  );
};

const FeeTransactionList: React.FC<{ Transactions: ITransaction[] }> = ({
  Transactions
}) => {
  if (Transactions.length == 0) {
    return null;
  }
  return (
    <ul>
      {Transactions.map(t => (
        <li key={t.id}>{`Fee: ${t.amount} ${t.currencyCode}`}</li>
      ))}
    </ul>
  );
};
