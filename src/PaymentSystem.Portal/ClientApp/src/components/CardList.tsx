import React, { useState, useEffect, useCallback } from "react";
import useFetch from "fetch-suspense";
import Contants from "../Constants";
import { ICard, ISubscription } from "../types";
import { timeout } from "q";
import { Link } from "react-router-dom";
export const CardList: React.FC = () => {
  const [cards, setCards] = useState<ICard[]>([]);
  const result = useFetch(Contants.allCards) as ICard[];
  const subscriptions = useFetch(Contants.subscriptions) as ISubscription[];
  const fetchData = async () => {
    const result = await fetch(Contants.allCards);
    const json = await result.json();
    setCards(json);
  };
  useEffect(() => {
    fetchData();
  }, []);
  return (
    <>
      <ol>
        {cards.map(x => (
          <li key={x.id}>
            <Link to={`/cards/${x.id}`}>{`${x.id} - ${
              subscriptions.find(y => y.id == x.subscriptionId)!.name
            }`}</Link>
          </li>
        ))}
      </ol>
      <AddCard Subscriptions={subscriptions} cardAdded={fetchData} />
    </>
  );
};

const AddCard: React.FC<{
  Subscriptions: ISubscription[];
  cardAdded: () => {};
}> = ({ Subscriptions, cardAdded }) => {
  const [cardType, setType] = useState(Subscriptions[0].id);

  const addCard = useCallback(async () => {
    await fetch(Contants.addCard, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json"
      },
      body: JSON.stringify({ creditCardSubscriptionId: cardType })
    });
    setTimeout(cardAdded, 200);
  }, [cardType]);

  return (
    <>
      <select value={cardType} onChange={e => setType(e.currentTarget.value)}>
        {Subscriptions.map(x => (
          <option value={x.id}>{x.name}</option>
        ))}
      </select>
      <button onClick={addCard}>Add card</button>
    </>
  );
};
